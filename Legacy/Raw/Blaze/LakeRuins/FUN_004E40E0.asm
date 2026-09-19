Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e40e0  section .text  offset fichier 0xe34e0  1968 octets

  004e40e0  83ec1c                sub       esp,1Ch
  004e40e3  8d442414              lea       eax,[esp+14h]
  004e40e7  8d4c2418              lea       ecx,[esp+18h]
  004e40eb  8d542410              lea       edx,[esp+10h]
  004e40ef  53                    push      ebx
  004e40f0  55                    push      ebp
  004e40f1  56                    push      esi
  004e40f2  8b74242c              mov       esi,[esp+2Ch]
  004e40f6  57                    push      edi
  004e40f7  50                    push      eax
  004e40f8  51                    push      ecx
  004e40f9  52                    push      edx
  004e40fa  56                    push      esi
  004e40fb  e880480500            call      00538980h
  004e4100  83c410                add       esp,10h
  004e4103  8d44241c              lea       eax,[esp+1Ch]
  004e4107  8d4c2414              lea       ecx,[esp+14h]
  004e410b  8d542430              lea       edx,[esp+30h]
  004e410f  50                    push      eax
  004e4110  51                    push      ecx
  004e4111  8d442420              lea       eax,[esp+20h]
  004e4115  52                    push      edx
  004e4116  8d4c241c              lea       ecx,[esp+1Ch]
  004e411a  50                    push      eax
  004e411b  51                    push      ecx
  004e411c  56                    push      esi
  004e411d  e8fe1d0500            call      00535F20h
  004e4122  8b44242c              mov       eax,[esp+2Ch]
  004e4126  33db                  xor       ebx,ebx
  004e4128  83c418                add       esp,18h
  004e412b  3bc3                  cmp       eax,ebx
  004e412d  0f84c0050000          je        near 004E46F3h
  004e4133  56                    push      esi
  004e4134  e86774ffff            call      004DB5A0h
  004e4139  8b06                  mov       eax,[esi]
  004e413b  83c404                add       esp,4
  004e413e  a802                  test      al,2
  004e4140  0f84ca030000          je        near 004E4510h
  004e4146  f644241c10            test      byte [esp+1Ch],10h
  004e414b  740c                  je        short 004E4159h
  004e414d  24fd                  and       al,0FDh
  004e414f  885e43                mov       [esi+43h],bl
  004e4152  8906                  mov       [esi],eax
  004e4154  e99a050000            jmp       004E46F3h

LAB_004e4159:
  004e4159  33c9                  xor       ecx,ecx
  004e415b  8a4e42                mov       cl,[esi+42h]
  004e415e  2bcb                  sub       ecx,ebx
  004e4160  0f842a020000          je        near 004E4390h
  004e4166  49                    dec       ecx
  004e4167  0f84b5000000          je        near 004E4222h
  004e416d  49                    dec       ecx
  004e416e  0f857f050000          jne       near 004E46F3h
  004e4174  668b5610              mov       dx,[esi+10h]
  004e4178  0fbfca                movsx     ecx,dx
  004e417b  2bcb                  sub       ecx,ebx
  004e417d  0f8489000000          je        near 004E420Ch
  004e4183  49                    dec       ecx
  004e4184  0f8590000000          jne       near 004E421Ah
  004e418a  a900000002            test      eax,2000000h
  004e418f  7409                  je        short 004E419Ah
  004e4191  4a                    dec       edx
  004e4192  c6464328              mov       byte [esi+43h],28h
  004e4196  66895610              mov       [esi+10h],dx

LAB_004e419a:
  004e419a  66837e1a06            cmp       word [esi+1Ah],6
  004e419f  7579                  jne       short 004E421Ah
  004e41a1  53                    push      ebx
  004e41a2  56                    push      esi
  004e41a3  e8c8150500            call      00535770h
  004e41a8  0fbf10                movsx     edx,word [eax]
  004e41ab  c1e20c                shl       edx,0Ch
  004e41ae  895644                mov       [esi+44h],edx
  004e41b1  83c408                add       esp,8
  004e41b4  0fbf4802              movsx     ecx,word [eax+2]
  004e41b8  c1e10c                shl       ecx,0Ch
  004e41bb  894e48                mov       [esi+48h],ecx
  004e41be  6a01                  push      1
  004e41c0  0fbf5004              movsx     edx,word [eax+4]
  004e41c4  c1e20c                shl       edx,0Ch
  004e41c7  89564c                mov       [esi+4Ch],edx
  004e41ca  a1cc975e00            mov       eax,[5E97CCh]
  004e41cf  53                    push      ebx
  004e41d0  8b88cc000000          mov       ecx,[eax+0CCh]
  004e41d6  33c0                  xor       eax,eax
  004e41d8  8b11                  mov       edx,[ecx]
  004e41da  8a420a                mov       al,[edx+0Ah]
  004e41dd  05c0000000            add       eax,0C0h
  004e41e2  50                    push      eax
  004e41e3  56                    push      esi
  004e41e4  e8e767f9ff            call      0047A9D0h
  004e41e9  83c410                add       esp,10h
  004e41ec  8d4e1c                lea       ecx,[esi+1Ch]
  004e41ef  51                    push      ecx
  004e41f0  6800180000            push      1800h
  004e41f5  6853060000            push      653h
  004e41fa  6a08                  push      8
  004e41fc  e81f990500            call      0053DB20h
  004e4201  83c410                add       esp,10h
  004e4204  895e5c                mov       [esi+5Ch],ebx
  004e4207  e9e7040000            jmp       004E46F3h

LAB_004e420c:
  004e420c  8a4e43                mov       cl,[esi+43h]
  004e420f  fec9                  dec       cl
  004e4211  884e43                mov       [esi+43h],cl
  004e4214  7504                  jne       short 004E421Ah
  004e4216  24fd                  and       al,0FDh
  004e4218  8906                  mov       [esi],eax

LAB_004e421a:
  004e421a  895e5c                mov       [esi+5Ch],ebx
  004e421d  e9d1040000            jmp       004E46F3h

LAB_004e4222:
  004e4222  0fbf4610              movsx     eax,word [esi+10h]
  004e4226  2bc3                  sub       eax,ebx
  004e4228  0f8495000000          je        near 004E42C3h
  004e422e  48                    dec       eax
  004e422f  0f85be040000          jne       near 004E46F3h
  004e4235  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004e423a  895e5c                mov       [esi+5Ch],ebx
  004e423d  0f85ae010000          jne       near 004E43F1h
  004e4243  0fbf5632              movsx     edx,word [esi+32h]
  004e4247  52                    push      edx
  004e4248  c7465c00800200        mov       dword [esi+5Ch],28000h
  004e424f  e8c8170600            call      00545A1Ch
  004e4254  0faf465c              imul      eax,[esi+5Ch]
  004e4258  8b6e50                mov       ebp,[esi+50h]
  004e425b  c1f80c                sar       eax,0Ch
  004e425e  03e8                  add       ebp,eax
  004e4260  0fbf4632              movsx     eax,word [esi+32h]
  004e4264  50                    push      eax
  004e4265  896e50                mov       [esi+50h],ebp
  004e4268  e8a9170600            call      00545A16h
  004e426d  0faf465c              imul      eax,[esi+5Ch]
  004e4271  8b4e58                mov       ecx,[esi+58h]
  004e4274  c7465c00500000        mov       dword [esi+5Ch],5000h
  004e427b  c1f80c                sar       eax,0Ch
  004e427e  03c8                  add       ecx,eax
  004e4280  8b442430              mov       eax,[esp+30h]
  004e4284  66ff4e10              dec       word [esi+10h]
  004e4288  894e58                mov       [esi+58h],ecx
  004e428b  8d8880000000          lea       ecx,[eax+80h]
  004e4291  81e1ff0f0000          and       ecx,0FFFh
  004e4297  81f900010000          cmp       ecx,100h
  004e429d  0f8e4c040000          jle       near 004E46EFh
  004e42a3  25ff0f0000            and       eax,0FFFh
  004e42a8  33d2                  xor       edx,edx
  004e42aa  3d00080000            cmp       eax,800h
  004e42af  0f9dc2                setge     dl
  004e42b2  4a                    dec       edx
  004e42b3  81e200010000          and       edx,100h
  004e42b9  83c280                add       edx,0FFFFFF80h
  004e42bc  8bc2                  mov       eax,edx
  004e42be  e92c040000            jmp       004E46EFh

LAB_004e42c3:
  004e42c3  8b4650                mov       eax,[esi+50h]
  004e42c6  8b4e58                mov       ecx,[esi+58h]
  004e42c9  c1f80c                sar       eax,0Ch
  004e42cc  8bd0                  mov       edx,eax
  004e42ce  c1f90c                sar       ecx,0Ch
  004e42d1  0fafd0                imul      edx,eax
  004e42d4  8bc1                  mov       eax,ecx
  004e42d6  0fafc1                imul      eax,ecx
  004e42d9  03d0                  add       edx,eax
  004e42db  52                    push      edx
  004e42dc  e82f170600            call      00545A10h
  004e42e1  8b0e                  mov       ecx,[esi]
  004e42e3  f7c100000002          test      ecx,2000000h
  004e42e9  740a                  je        short 004E42F5h
  004e42eb  83f805                cmp       eax,5
  004e42ee  7f05                  jg        short 004E42F5h
  004e42f0  83e1dd                and       ecx,0FFFFFFDDh
  004e42f3  890e                  mov       [esi],ecx

LAB_004e42f5:
  004e42f5  83f803                cmp       eax,3
  004e42f8  0f8cf5030000          jl        near 004E46F3h
  004e42fe  8b442430              mov       eax,[esp+30h]
  004e4302  8d4828                lea       ecx,[eax+28h]
  004e4305  81e1ff0f0000          and       ecx,0FFFh
  004e430b  83f950                cmp       ecx,50h
  004e430e  7e18                  jle       short 004E4328h
  004e4310  25ff0f0000            and       eax,0FFFh
  004e4315  33d2                  xor       edx,edx
  004e4317  3d00080000            cmp       eax,800h
  004e431c  0f9dc2                setge     dl
  004e431f  4a                    dec       edx
  004e4320  83e250                and       edx,50h
  004e4323  83c2d8                add       edx,0FFFFFFD8h
  004e4326  8bc2                  mov       eax,edx

LAB_004e4328:
  004e4328  66014632              add       [esi+32h],ax
  004e432c  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004e4331  0f8cbc030000          jl        near 004E46F3h
  004e4337  53                    push      ebx
  004e4338  8d461c                lea       eax,[esi+1Ch]
  004e433b  68fc2d5500            push      552DFCh
  004e4340  50                    push      eax
  004e4341  56                    push      esi
  004e4342  e8e9580300            call      00519C30h
  004e4347  8bf8                  mov       edi,eax
  004e4349  83c410                add       esp,10h
  004e434c  3bfb                  cmp       edi,ebx
  004e434e  7406                  je        short 004E4356h
  004e4350  8b06                  mov       eax,[esi]
  004e4352  0c20                  or        al,20h
  004e4354  8906                  mov       [esi],eax

LAB_004e4356:
  004e4356  391db8e05d00          cmp       [5DE0B8h],ebx
  004e435c  0f8491030000          je        near 004E46F3h
  004e4362  8b2f                  mov       ebp,[edi]
  004e4364  57                    push      edi
  004e4365  83cd40                or        ebp,40h
  004e4368  56                    push      esi
  004e4369  66c787960000000800    mov       word [edi+96h],8
  004e4372  892f                  mov       [edi],ebp
  004e4374  e807580500            call      00539B80h
  004e4379  83c408                add       esp,8
  004e437c  c6460707              mov       byte [esi+7],7
  004e4380  53                    push      ebx
  004e4381  57                    push      edi
  004e4382  56                    push      esi
  004e4383  e888740500            call      0053B810h
  004e4388  83c40c                add       esp,0Ch
  004e438b  e963010000            jmp       004E44F3h

LAB_004e4390:
  004e4390  0fbf4610              movsx     eax,word [esi+10h]
  004e4394  2bc3                  sub       eax,ebx
  004e4396  0f8491000000          je        near 004E442Dh
  004e439c  48                    dec       eax
  004e439d  0f8550030000          jne       near 004E46F3h
  004e43a3  66837e1a04            cmp       word [esi+1Ah],4
  004e43a8  895e5c                mov       [esi+5Ch],ebx
  004e43ab  7544                  jne       short 004E43F1h
  004e43ad  0fbf4e32              movsx     ecx,word [esi+32h]
  004e43b1  51                    push      ecx
  004e43b2  c7465c00800200        mov       dword [esi+5Ch],28000h
  004e43b9  e85e160600            call      00545A1Ch
  004e43be  0faf465c              imul      eax,[esi+5Ch]
  004e43c2  0fbf5632              movsx     edx,word [esi+32h]
  004e43c6  8b6e50                mov       ebp,[esi+50h]
  004e43c9  52                    push      edx
  004e43ca  c1f80c                sar       eax,0Ch
  004e43cd  03e8                  add       ebp,eax
  004e43cf  896e50                mov       [esi+50h],ebp
  004e43d2  e83f160600            call      00545A16h
  004e43d7  0faf465c              imul      eax,[esi+5Ch]
  004e43db  8b4e58                mov       ecx,[esi+58h]
  004e43de  c7465c00500000        mov       dword [esi+5Ch],5000h
  004e43e5  c1f80c                sar       eax,0Ch
  004e43e8  03c8                  add       ecx,eax
  004e43ea  66ff4e10              dec       word [esi+10h]
  004e43ee  894e58                mov       [esi+58h],ecx

LAB_004e43f1:
  004e43f1  8b442430              mov       eax,[esp+30h]
  004e43f5  8d8880000000          lea       ecx,[eax+80h]
  004e43fb  81e1ff0f0000          and       ecx,0FFFh
  004e4401  81f900010000          cmp       ecx,100h
  004e4407  0f8ee2020000          jle       near 004E46EFh
  004e440d  25ff0f0000            and       eax,0FFFh
  004e4412  33d2                  xor       edx,edx
  004e4414  3d00080000            cmp       eax,800h
  004e4419  0f9dc2                setge     dl
  004e441c  4a                    dec       edx
  004e441d  81e200010000          and       edx,100h
  004e4423  83c280                add       edx,0FFFFFF80h
  004e4426  8bc2                  mov       eax,edx
  004e4428  e9c2020000            jmp       004E46EFh

LAB_004e442d:
  004e442d  8b4650                mov       eax,[esi+50h]
  004e4430  8b4e58                mov       ecx,[esi+58h]
  004e4433  c1f80c                sar       eax,0Ch
  004e4436  8bd0                  mov       edx,eax
  004e4438  c1f90c                sar       ecx,0Ch
  004e443b  0fafd0                imul      edx,eax
  004e443e  8bc1                  mov       eax,ecx
  004e4440  0fafc1                imul      eax,ecx
  004e4443  03d0                  add       edx,eax
  004e4445  52                    push      edx
  004e4446  e8c5150600            call      00545A10h
  004e444b  8b0e                  mov       ecx,[esi]
  004e444d  f7c100000002          test      ecx,2000000h
  004e4453  740a                  je        short 004E445Fh
  004e4455  83f805                cmp       eax,5
  004e4458  7f05                  jg        short 004E445Fh
  004e445a  83e1dd                and       ecx,0FFFFFFDDh
  004e445d  890e                  mov       [esi],ecx

LAB_004e445f:
  004e445f  83f803                cmp       eax,3
  004e4462  0f8c8b020000          jl        near 004E46F3h
  004e4468  8b442430              mov       eax,[esp+30h]
  004e446c  8d4828                lea       ecx,[eax+28h]
  004e446f  81e1ff0f0000          and       ecx,0FFFh
  004e4475  83f950                cmp       ecx,50h
  004e4478  7e18                  jle       short 004E4492h
  004e447a  25ff0f0000            and       eax,0FFFh
  004e447f  33d2                  xor       edx,edx
  004e4481  3d00080000            cmp       eax,800h
  004e4486  0f9dc2                setge     dl
  004e4489  4a                    dec       edx
  004e448a  83e250                and       edx,50h
  004e448d  83c2d8                add       edx,0FFFFFFD8h
  004e4490  8bc2                  mov       eax,edx

LAB_004e4492:
  004e4492  66014632              add       [esi+32h],ax
  004e4496  668b461a              mov       ax,[esi+1Ah]
  004e449a  663d0500              cmp       ax,5
  004e449e  0f8c4f020000          jl        near 004E46F3h
  004e44a4  663d0700              cmp       ax,7
  004e44a8  0f8f45020000          jg        near 004E46F3h
  004e44ae  53                    push      ebx
  004e44af  8d461c                lea       eax,[esi+1Ch]
  004e44b2  68f02d5500            push      552DF0h
  004e44b7  50                    push      eax
  004e44b8  56                    push      esi
  004e44b9  e872570300            call      00519C30h
  004e44be  8bf8                  mov       edi,eax
  004e44c0  83c410                add       esp,10h
  004e44c3  3bfb                  cmp       edi,ebx
  004e44c5  7406                  je        short 004E44CDh
  004e44c7  8b06                  mov       eax,[esi]
  004e44c9  0c20                  or        al,20h
  004e44cb  8906                  mov       [esi],eax

LAB_004e44cd:
  004e44cd  391db8e05d00          cmp       [5DE0B8h],ebx
  004e44d3  0f841a020000          je        near 004E46F3h
  004e44d9  8b2f                  mov       ebp,[edi]
  004e44db  57                    push      edi
  004e44dc  83cd40                or        ebp,40h
  004e44df  56                    push      esi
  004e44e0  66c787960000000800    mov       word [edi+96h],8
  004e44e9  892f                  mov       [edi],ebp
  004e44eb  e890560500            call      00539B80h
  004e44f0  83c408                add       esp,8

LAB_004e44f3:
  004e44f3  83c71c                add       edi,1Ch
  004e44f6  57                    push      edi
  004e44f7  6800180000            push      1800h
  004e44fc  6800030000            push      300h
  004e4501  6a03                  push      3
  004e4503  e818960500            call      0053DB20h
  004e4508  83c410                add       esp,10h
  004e450b  e9e3010000            jmp       004E46F3h

LAB_004e4510:
  004e4510  a811                  test      al,11h
  004e4512  0f85db010000          jne       near 004E46F3h
  004e4518  a904080000            test      eax,804h
  004e451d  0f85d0010000          jne       near 004E46F3h
  004e4523  33c9                  xor       ecx,ecx
  004e4525  8a4e42                mov       cl,[esi+42h]
  004e4528  83f903                cmp       ecx,3
  004e452b  0f87c2010000          ja        near 004E46F3h
  004e4531  ff248d78484e00        jmp       dword [ecx*4+4E4878h]
  004e4538  66395e18              cmp       [esi+18h],bx
  004e453c  740a                  je        short 004E4548h
  004e453e  0d00000001            or        eax,1000000h
  004e4543  895e18                mov       [esi+18h],ebx
  004e4546  8906                  mov       [esi],eax

LAB_004e4548:
  004e4548  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e4550  7d08                  jge       short 004E455Ah
  004e4552  885e43                mov       [esi+43h],bl
  004e4555  e999010000            jmp       004E46F3h

LAB_004e455a:
  004e455a  8a4643                mov       al,[esi+43h]
  004e455d  3ac3                  cmp       al,bl
  004e455f  7473                  je        short 004E45D4h
  004e4561  fec8                  dec       al
  004e4563  884643                mov       [esi+43h],al
  004e4566  8b465c                mov       eax,[esi+5Ch]
  004e4569  8bc8                  mov       ecx,eax
  004e456b  c1f904                sar       ecx,4
  004e456e  2bc1                  sub       eax,ecx
  004e4570  89465c                mov       [esi+5Ch],eax
  004e4573  e97b010000            jmp       004E46F3h
  004e4578  66395e18              cmp       [esi+18h],bx
  004e457c  740a                  je        short 004E4588h
  004e457e  0d00000001            or        eax,1000000h
  004e4583  895e18                mov       [esi+18h],ebx
  004e4586  8906                  mov       [esi],eax

LAB_004e4588:
  004e4588  8b542410              mov       edx,[esp+10h]
  004e458c  8b4c2430              mov       ecx,[esp+30h]
  004e4590  8d841280000000        lea       eax,[edx+edx+80h]
  004e4597  8d1408                lea       edx,[eax+ecx]
  004e459a  8d3c00                lea       edi,[eax+eax]
  004e459d  81e2ff0f0000          and       edx,0FFFh
  004e45a3  3bd7                  cmp       edx,edi
  004e45a5  7f19                  jg        short 004E45C0h
  004e45a7  8b465c                mov       eax,[esi+5Ch]
  004e45aa  66014e32              add       [esi+32h],cx
  004e45ae  8bc8                  mov       ecx,eax
  004e45b0  885e43                mov       [esi+43h],bl
  004e45b3  c1f904                sar       ecx,4
  004e45b6  2bc1                  sub       eax,ecx
  004e45b8  89465c                mov       [esi+5Ch],eax
  004e45bb  e933010000            jmp       004E46F3h

LAB_004e45c0:
  004e45c0  81e1ff0f0000          and       ecx,0FFFh
  004e45c6  81f900080000          cmp       ecx,800h
  004e45cc  7c02                  jl        short 004E45D0h
  004e45ce  f7d8                  neg       eax

LAB_004e45d0:
  004e45d0  66014632              add       [esi+32h],ax

LAB_004e45d4:
  004e45d4  8b465c                mov       eax,[esi+5Ch]
  004e45d7  8bc8                  mov       ecx,eax
  004e45d9  c1f904                sar       ecx,4
  004e45dc  2bc1                  sub       eax,ecx
  004e45de  89465c                mov       [esi+5Ch],eax
  004e45e1  e90d010000            jmp       004E46F3h
  004e45e6  66395e18              cmp       [esi+18h],bx
  004e45ea  740a                  je        short 004E45F6h
  004e45ec  0d00000001            or        eax,1000000h
  004e45f1  895e18                mov       [esi+18h],ebx
  004e45f4  8906                  mov       [esi],eax

LAB_004e45f6:
  004e45f6  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e45fe  7d08                  jge       short 004E4608h
  004e4600  885e43                mov       [esi+43h],bl
  004e4603  e9eb000000            jmp       004E46F3h

LAB_004e4608:
  004e4608  8a4643                mov       al,[esi+43h]
  004e460b  3ac3                  cmp       al,bl
  004e460d  7405                  je        short 004E4614h
  004e460f  fec8                  dec       al
  004e4611  884643                mov       [esi+43h],al

LAB_004e4614:
  004e4614  8b442410              mov       eax,[esp+10h]
  004e4618  8b4c2430              mov       ecx,[esp+30h]
  004e461c  c1f802                sar       eax,2
  004e461f  83c038                add       eax,38h
  004e4622  8d1408                lea       edx,[eax+ecx]
  004e4625  8d3c00                lea       edi,[eax+eax]
  004e4628  81e2ff0f0000          and       edx,0FFFh
  004e462e  3bd7                  cmp       edx,edi
  004e4630  7f06                  jg        short 004E4638h
  004e4632  66014e32              add       [esi+32h],cx
  004e4636  eb14                  jmp       short 004E464Ch

LAB_004e4638:
  004e4638  81e1ff0f0000          and       ecx,0FFFh
  004e463e  81f900080000          cmp       ecx,800h
  004e4644  7c02                  jl        short 004E4648h
  004e4646  f7d8                  neg       eax

LAB_004e4648:
  004e4648  66014632              add       [esi+32h],ax

LAB_004e464c:
  004e464c  8b465c                mov       eax,[esi+5Ch]
  004e464f  b900000100            mov       ecx,10000h
  004e4654  2bc8                  sub       ecx,eax
  004e4656  c1f904                sar       ecx,4
  004e4659  03c8                  add       ecx,eax
  004e465b  894e5c                mov       [esi+5Ch],ecx
  004e465e  e990000000            jmp       004E46F3h
  004e4663  66395e18              cmp       [esi+18h],bx
  004e4667  740a                  je        short 004E4673h
  004e4669  0d00000001            or        eax,1000000h
  004e466e  895e18                mov       [esi+18h],ebx
  004e4671  8906                  mov       [esi],eax

LAB_004e4673:
  004e4673  8a4643                mov       al,[esi+43h]
  004e4676  3ac3                  cmp       al,bl
  004e4678  7405                  je        short 004E467Fh
  004e467a  fec8                  dec       al
  004e467c  884643                mov       [esi+43h],al

LAB_004e467f:
  004e467f  0fbf5610              movsx     edx,word [esi+10h]
  004e4683  52                    push      edx
  004e4684  c7465c00300000        mov       dword [esi+5Ch],3000h
  004e468b  e88c130600            call      00545A1Ch
  004e4690  0faf465c              imul      eax,[esi+5Ch]
  004e4694  8b4e50                mov       ecx,[esi+50h]
  004e4697  c1f80c                sar       eax,0Ch
  004e469a  03c8                  add       ecx,eax
  004e469c  0fbf4610              movsx     eax,word [esi+10h]
  004e46a0  50                    push      eax
  004e46a1  894e50                mov       [esi+50h],ecx
  004e46a4  e86d130600            call      00545A16h
  004e46a9  0faf465c              imul      eax,[esi+5Ch]
  004e46ad  8b5658                mov       edx,[esi+58h]
  004e46b0  8b4c2430              mov       ecx,[esp+30h]
  004e46b4  c1f80c                sar       eax,0Ch
  004e46b7  03d0                  add       edx,eax
  004e46b9  8b442410              mov       eax,[esp+10h]
  004e46bd  c1f802                sar       eax,2
  004e46c0  83c040                add       eax,40h
  004e46c3  895658                mov       [esi+58h],edx
  004e46c6  895e5c                mov       [esi+5Ch],ebx
  004e46c9  8d1408                lea       edx,[eax+ecx]
  004e46cc  8d3c00                lea       edi,[eax+eax]
  004e46cf  81e2ff0f0000          and       edx,0FFFh
  004e46d5  3bd7                  cmp       edx,edi
  004e46d7  7f06                  jg        short 004E46DFh
  004e46d9  66014e32              add       [esi+32h],cx
  004e46dd  eb14                  jmp       short 004E46F3h

LAB_004e46df:
  004e46df  81e1ff0f0000          and       ecx,0FFFh
  004e46e5  81f900080000          cmp       ecx,800h
  004e46eb  7c02                  jl        short 004E46EFh
  004e46ed  f7d8                  neg       eax

LAB_004e46ef:
  004e46ef  66014632              add       [esi+32h],ax

LAB_004e46f3:
  004e46f3  8b442414              mov       eax,[esp+14h]
  004e46f7  50                    push      eax
  004e46f8  56                    push      esi
  004e46f9  e8e261fbff            call      0049A8E0h
  004e46fe  83c408                add       esp,8
  004e4701  c746200000e7ff        mov       dword [esi+20h],0FFE70000h
  004e4708  56                    push      esi
  004e4709  e832f20400            call      00533940h
  004e470e  8b8e90000000          mov       ecx,[esi+90h]
  004e4714  8b542424              mov       edx,[esp+24h]
  004e4718  83c404                add       esp,4
  004e471b  81f100200000          xor       ecx,2000h
  004e4721  81e1ffffffdf          and       ecx,0DFFFFFFFh
  004e4727  52                    push      edx
  004e4728  56                    push      esi
  004e4729  898e90000000          mov       [esi+90h],ecx
  004e472f  e8bcd5fbff            call      004A1CF0h
  004e4734  8b44241c              mov       eax,[esp+1Ch]
  004e4738  83c408                add       esp,8
  004e473b  3bc3                  cmp       eax,ebx
  004e473d  0f842c010000          je        near 004E486Fh
  004e4743  f70616080000          test      dword [esi],816h
  004e4749  0f8520010000          jne       near 004E486Fh
  004e474f  385e43                cmp       [esi+43h],bl
  004e4752  0f8517010000          jne       near 004E486Fh
  004e4758  8b466c                mov       eax,[esi+6Ch]
  004e475b  8b561c                mov       edx,[esi+1Ch]
  004e475e  8b781c                mov       edi,[eax+1Ch]
  004e4761  8b6824                mov       ebp,[eax+24h]
  004e4764  8b4624                mov       eax,[esi+24h]
  004e4767  2bfa                  sub       edi,edx
  004e4769  2be8                  sub       ebp,eax
  004e476b  c1ff0c                sar       edi,0Ch
  004e476e  c1fd0c                sar       ebp,0Ch
  004e4771  8bc7                  mov       eax,edi
  004e4773  8bcd                  mov       ecx,ebp
  004e4775  0fafc7                imul      eax,edi
  004e4778  0fafcd                imul      ecx,ebp
  004e477b  03c1                  add       eax,ecx
  004e477d  50                    push      eax
  004e477e  e88d120600            call      00545A10h
  004e4783  55                    push      ebp
  004e4784  57                    push      edi
  004e4785  89442420              mov       [esp+20h],eax
  004e4789  e894120600            call      00545A22h
  004e478e  668b5632              mov       dx,[esi+32h]
  004e4792  2bc2                  sub       eax,edx
  004e4794  25ff0f0000            and       eax,0FFFh
  004e4799  89442430              mov       [esp+30h],eax
  004e479d  e89e290600            call      00547140h
  004e47a2  99                    cdq
  004e47a3  b964000000            mov       ecx,64h
  004e47a8  f7f9                  idiv      ecx
  004e47aa  83fa1e                cmp       edx,1Eh
  004e47ad  7d52                  jge       short 004E4801h
  004e47af  8b542430              mov       edx,[esp+30h]
  004e47b3  81c200010000          add       edx,100h
  004e47b9  81e2ff0f0000          and       edx,0FFFh
  004e47bf  81fa00020000          cmp       edx,200h
  004e47c5  7f2a                  jg        short 004E47F1h
  004e47c7  8b06                  mov       eax,[esi]
  004e47c9  25fffffffe            and       eax,0FEFFFFFFh
  004e47ce  0c02                  or        al,2
  004e47d0  8906                  mov       [esi],eax
  004e47d2  e869290600            call      00547140h
  004e47d7  83e001                and       eax,1
  004e47da  66c746100100          mov       word [esi+10h],1
  004e47e0  884642                mov       [esi+42h],al
  004e47e3  8d4802                lea       ecx,[eax+2]
  004e47e6  894e18                mov       [esi+18h],ecx
  004e47e9  5f                    pop       edi
  004e47ea  5e                    pop       esi
  004e47eb  5d                    pop       ebp
  004e47ec  5b                    pop       ebx
  004e47ed  83c41c                add       esp,1Ch
  004e47f0  c3                    ret

LAB_004e47f1:
  004e47f1  c6464201              mov       byte [esi+42h],1
  004e47f5  c6464301              mov       byte [esi+43h],1
  004e47f9  5f                    pop       edi
  004e47fa  5e                    pop       esi
  004e47fb  5d                    pop       ebp
  004e47fc  5b                    pop       ebx
  004e47fd  83c41c                add       esp,1Ch
  004e4800  c3                    ret

LAB_004e4801:
  004e4801  83fa28                cmp       edx,28h
  004e4804  7d26                  jge       short 004E482Ch
  004e4806  8b16                  mov       edx,[esi]
  004e4808  c7461802000000        mov       dword [esi+18h],2
  004e480f  81e2fffffffe          and       edx,0FEFFFFFFh
  004e4815  c6464202              mov       byte [esi+42h],2
  004e4819  83ca02                or        edx,2
  004e481c  66c746100100          mov       word [esi+10h],1
  004e4822  8916                  mov       [esi],edx
  004e4824  5f                    pop       edi
  004e4825  5e                    pop       esi
  004e4826  5d                    pop       ebp
  004e4827  5b                    pop       ebx
  004e4828  83c41c                add       esp,1Ch
  004e482b  c3                    ret

LAB_004e482c:
  004e482c  83fa50                cmp       edx,50h
  004e482f  7d27                  jge       short 004E4858h
  004e4831  c6464203              mov       byte [esi+42h],3
  004e4835  e806290600            call      00547140h
  004e483a  99                    cdq
  004e483b  b90a000000            mov       ecx,0Ah
  004e4840  f7f9                  idiv      ecx
  004e4842  02d1                  add       dl,cl
  004e4844  885643                mov       [esi+43h],dl
  004e4847  e8f4280600            call      00547140h
  004e484c  66894610              mov       [esi+10h],ax
  004e4850  5f                    pop       edi
  004e4851  5e                    pop       esi
  004e4852  5d                    pop       ebp
  004e4853  5b                    pop       ebx
  004e4854  83c41c                add       esp,1Ch
  004e4857  c3                    ret

LAB_004e4858:
  004e4858  c6464202              mov       byte [esi+42h],2
  004e485c  e8df280600            call      00547140h
  004e4861  99                    cdq
  004e4862  b90a000000            mov       ecx,0Ah
  004e4867  f7f9                  idiv      ecx
  004e4869  80c214                add       dl,14h
  004e486c  885643                mov       [esi+43h],dl

LAB_004e486f:
  004e486f  5f                    pop       edi
  004e4870  5e                    pop       esi
  004e4871  5d                    pop       ebp
  004e4872  5b                    pop       ebx
  004e4873  83c41c                add       esp,1Ch
  004e4876  c3                    ret
  004e4877  90                    nop
  004e4878  38454e                cmp       [ebp+4Eh],al
  004e487b  007845                add       [eax+45h],bh
  004e487e  4e                    dec       esi
  004e487f  00e6                  add       dh,ah
  004e4881  45                    inc       ebp
  004e4882  4e                    dec       esi
  004e4883  006346                add       [ebx+46h],ah
  004e4886  4e                    dec       esi
  004e4887  009090909090          add       [eax-6F6F6F70h],dl
  004e488d  90                    nop
  004e488e  90                    nop
  004e488f  90                    nop

39 cible(s) de saut interne : 0x004e4159, 0x004e419a, 0x004e420c, 0x004e421a, 0x004e4222, 0x004e42c3, 0x004e42f5, 0x004e4328, 0x004e4356, 0x004e4390, 0x004e43f1, 0x004e442d, 0x004e445f, 0x004e4492, 0x004e44cd, 0x004e44f3, 0x004e4510, 0x004e4548, 0x004e455a, 0x004e4588, 0x004e45c0, 0x004e45d0, 0x004e45d4, 0x004e45f6, 0x004e4608, 0x004e4614, 0x004e4638, 0x004e4648, 0x004e464c, 0x004e4673, 0x004e467f, 0x004e46df, 0x004e46ef, 0x004e46f3, 0x004e47f1, 0x004e4801, 0x004e482c, 0x004e4858, 0x004e486f
