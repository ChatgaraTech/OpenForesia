
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00484190  section .text  offset fichier 0x83590  784 octets

  00484190  83ec30                sub       esp,30h
  00484193  53                    push      ebx
  00484194  55                    push      ebp
  00484195  56                    push      esi
  00484196  8b742440              mov       esi,[esp+40h]
  0048419a  33c0                  xor       eax,eax
  0048419c  57                    push      edi
  0048419d  66394610              cmp       [esi+10h],ax
  004841a1  66c744242800fc        mov       word [esp+28h],0FC00h
  004841a8  66c744242a80ff        mov       word [esp+2Ah],0FF80h
  004841af  66c744242c3200        mov       word [esp+2Ch],32h
  004841b6  66c744242ec800        mov       word [esp+2Eh],0C8h
  004841bd  6689442430            mov       [esp+30h],ax
  004841c2  66c74424328000        mov       word [esp+32h],80h
  004841c9  7511                  jne       short 004841DCh
  004841cb  8b16                  mov       edx,[esi]
  004841cd  66c746100100          mov       word [esi+10h],1
  004841d3  80ca80                or        dl,80h
  004841d6  804e3606              or        byte [esi+36h],6
  004841da  8916                  mov       [esi],edx

LAB_004841dc:
  004841dc  8b4e1c                mov       ecx,[esi+1Ch]
  004841df  8b6e78                mov       ebp,[esi+78h]
  004841e2  8b5620                mov       edx,[esi+20h]
  004841e5  894c2434              mov       [esp+34h],ecx
  004841e9  8b4e24                mov       ecx,[esi+24h]
  004841ec  3be8                  cmp       ebp,eax
  004841ee  89542438              mov       [esp+38h],edx
  004841f2  894c243c              mov       [esp+3Ch],ecx
  004841f6  896c2444              mov       [esp+44h],ebp
  004841fa  0f8481000000          je        near 00484281h
  00484200  f60610                test      byte [esi],10h
  00484203  757c                  jne       short 00484281h
  00484205  f7450000000050        test      dword [ebp],50000000h
  0048420c  740e                  je        short 0048421Ch
  0048420e  894574                mov       [ebp+74h],eax
  00484211  33ed                  xor       ebp,ebp
  00484213  894678                mov       [esi+78h],eax
  00484216  896c2444              mov       [esp+44h],ebp
  0048421a  eb03                  jmp       short 0048421Fh

LAB_0048421c:
  0048421c  897574                mov       [ebp+74h],esi

LAB_0048421f:
  0048421f  8b7d1c                mov       edi,[ebp+1Ch]
  00484222  8b561c                mov       edx,[esi+1Ch]
  00484225  2bfa                  sub       edi,edx
  00484227  55                    push      ebp
  00484228  c1ff0c                sar       edi,0Ch
  0048422b  e890490b00            call      00538BC0h
  00484230  8bd8                  mov       ebx,eax
  00484232  8b4520                mov       eax,[ebp+20h]
  00484235  8b6d24                mov       ebp,[ebp+24h]
  00484238  83e3fe                and       ebx,0FFFFFFFEh
  0048423b  8b5620                mov       edx,[esi+20h]
  0048423e  83c404                add       esp,4
  00484241  c1e30b                shl       ebx,0Bh
  00484244  03d8                  add       ebx,eax
  00484246  8b4624                mov       eax,[esi+24h]
  00484249  2be8                  sub       ebp,eax
  0048424b  2bda                  sub       ebx,edx
  0048424d  c1fd0c                sar       ebp,0Ch
  00484250  8bd7                  mov       edx,edi
  00484252  8bc5                  mov       eax,ebp
  00484254  0fafd7                imul      edx,edi
  00484257  0fafc5                imul      eax,ebp
  0048425a  03d0                  add       edx,eax
  0048425c  52                    push      edx
  0048425d  c1fb0c                sar       ebx,0Ch
  00484260  e8ab170c00            call      00545A10h
  00484265  55                    push      ebp
  00484266  57                    push      edi
  00484267  89442418              mov       [esp+18h],eax
  0048426b  e8b2170c00            call      00545A22h
  00484270  8b4c2410              mov       ecx,[esp+10h]
  00484274  8bf8                  mov       edi,eax
  00484276  51                    push      ecx
  00484277  53                    push      ebx
  00484278  e8a5170c00            call      00545A22h
  0048427d  8be8                  mov       ebp,eax
  0048427f  eb1f                  jmp       short 004842A0h

LAB_00484281:
  00484281  66ff4610              inc       word [esi+10h]
  00484285  66837e1014            cmp       word [esi+10h],14h
  0048428a  7c06                  jl        short 00484292h
  0048428c  810e00000040          or        dword [esi],40000000h

LAB_00484292:
  00484292  0fbf7e32              movsx     edi,word [esi+32h]
  00484296  0fbf6e34              movsx     ebp,word [esi+34h]
  0048429a  81ef00040000          sub       edi,400h

LAB_004842a0:
  004842a0  6a0c                  push      0Ch
  004842a2  55                    push      ebp
  004842a3  8d542424              lea       edx,[esp+24h]
  004842a7  57                    push      edi
  004842a8  52                    push      edx
  004842a9  e822e80a00            call      00532AD0h
  004842ae  8b4650                mov       eax,[esi+50h]
  004842b1  8b4c242c              mov       ecx,[esp+2Ch]
  004842b5  8b5654                mov       edx,[esi+54h]
  004842b8  8b5c2434              mov       ebx,[esp+34h]
  004842bc  03c1                  add       eax,ecx
  004842be  8b4c2430              mov       ecx,[esp+30h]
  004842c2  03ca                  add       ecx,edx
  004842c4  8b5658                mov       edx,[esi+58h]
  004842c7  03d3                  add       edx,ebx
  004842c9  8bd8                  mov       ebx,eax
  004842cb  c1fb03                sar       ebx,3
  004842ce  2bc3                  sub       eax,ebx
  004842d0  8bd9                  mov       ebx,ecx
  004842d2  c1fb03                sar       ebx,3
  004842d5  2bcb                  sub       ecx,ebx
  004842d7  8bda                  mov       ebx,edx
  004842d9  c1fb03                sar       ebx,3
  004842dc  2bd3                  sub       edx,ebx
  004842de  8b5e1c                mov       ebx,[esi+1Ch]
  004842e1  03d8                  add       ebx,eax
  004842e3  894650                mov       [esi+50h],eax
  004842e6  895e1c                mov       [esi+1Ch],ebx
  004842e9  8bc3                  mov       eax,ebx
  004842eb  8b5e20                mov       ebx,[esi+20h]
  004842ee  894e54                mov       [esi+54h],ecx
  004842f1  03d9                  add       ebx,ecx
  004842f3  895658                mov       [esi+58h],edx
  004842f6  895e20                mov       [esi+20h],ebx
  004842f9  8bcb                  mov       ecx,ebx
  004842fb  8b5e24                mov       ebx,[esi+24h]
  004842fe  83c410                add       esp,10h
  00484301  03da                  add       ebx,edx
  00484303  895e24                mov       [esi+24h],ebx
  00484306  8bd3                  mov       edx,ebx
  00484308  8b5c2434              mov       ebx,[esp+34h]
  0048430c  2bc3                  sub       eax,ebx
  0048430e  8b5c2438              mov       ebx,[esp+38h]
  00484312  2bcb                  sub       ecx,ebx
  00484314  8b5c243c              mov       ebx,[esp+3Ch]
  00484318  2bd3                  sub       edx,ebx
  0048431a  c1f80c                sar       eax,0Ch
  0048431d  c1fa0c                sar       edx,0Ch
  00484320  89442414              mov       [esp+14h],eax
  00484324  89542410              mov       [esp+10h],edx
  00484328  c1f90c                sar       ecx,0Ch
  0048432b  0fafc0                imul      eax,eax
  0048432e  0fafd2                imul      edx,edx
  00484331  8bd9                  mov       ebx,ecx
  00484333  8944241c              mov       [esp+1Ch],eax
  00484337  0fafd9                imul      ebx,ecx
  0048433a  03c2                  add       eax,edx
  0048433c  894c2418              mov       [esp+18h],ecx
  00484340  50                    push      eax
  00484341  895c2424              mov       [esp+24h],ebx
  00484345  89542428              mov       [esp+28h],edx
  00484349  e8c2160c00            call      00545A10h
  0048434e  8b4c2414              mov       ecx,[esp+14h]
  00484352  8bd8                  mov       ebx,eax
  00484354  8b442410              mov       eax,[esp+10h]
  00484358  50                    push      eax
  00484359  51                    push      ecx
  0048435a  e8c3160c00            call      00545A22h
  0048435f  8b542418              mov       edx,[esp+18h]
  00484363  0500040000            add       eax,400h
  00484368  53                    push      ebx
  00484369  52                    push      edx
  0048436a  66894632              mov       [esi+32h],ax
  0048436e  e8af160c00            call      00545A22h
  00484373  668b4e32              mov       cx,[esi+32h]
  00484377  66894634              mov       [esi+34h],ax
  0048437b  0fbfc0                movsx     eax,ax
  0048437e  2bf9                  sub       edi,ecx
  00484380  2be8                  sub       ebp,eax
  00484382  81c780040000          add       edi,480h
  00484388  81e7ff0f0000          and       edi,0FFFh
  0048438e  81ff00010000          cmp       edi,100h
  00484394  7d1a                  jge       short 004843B0h
  00484396  81c580000000          add       ebp,80h
  0048439c  81e5ff0f0000          and       ebp,0FFFh
  004843a2  81fd00010000          cmp       ebp,100h
  004843a8  7d06                  jge       short 004843B0h
  004843aa  8b06                  mov       eax,[esi]
  004843ac  0c10                  or        al,10h
  004843ae  8906                  mov       [esi],eax

LAB_004843b0:
  004843b0  56                    push      esi
  004843b1  e88af50a00            call      00533940h
  004843b6  8b4660                mov       eax,[esi+60h]
  004843b9  8b4e20                mov       ecx,[esi+20h]
  004843bc  83c404                add       esp,4
  004843bf  3bc8                  cmp       ecx,eax
  004843c1  7f0d                  jg        short 004843D0h
  004843c3  894620                mov       [esi+20h],eax
  004843c6  c7869000000000080000  mov       dword [esi+90h],800h

LAB_004843d0:
  004843d0  f70600000040          test      dword [esi],40000000h
  004843d6  7554                  jne       short 0048442Ch
  004843d8  8b8690000000          mov       eax,[esi+90h]
  004843de  f6c4a0                test      ah,0A0h
  004843e1  7507                  jne       short 004843EAh
  004843e3  a902080000            test      eax,802h
  004843e8  7542                  jne       short 0048442Ch

LAB_004843ea:
  004843ea  8d542428              lea       edx,[esp+28h]
  004843ee  52                    push      edx
  004843ef  56                    push      esi
  004843f0  e8ab08ffff            call      00474CA0h
  004843f5  8bf8                  mov       edi,eax
  004843f7  83c408                add       esp,8
  004843fa  85ff                  test      edi,edi
  004843fc  0f848f000000          je        near 00484491h
  00484402  8b4674                mov       eax,[esi+74h]
  00484405  57                    push      edi
  00484406  50                    push      eax
  00484407  e874570b00            call      00539B80h
  0048440c  8b07                  mov       eax,[edi]
  0048440e  83c408                add       esp,8
  00484411  0c40                  or        al,40h
  00484413  66c787960000000800    mov       word [edi+96h],8
  0048441c  8907                  mov       [edi],eax
  0048441e  c70600000000          mov       dword [esi],0
  00484424  5f                    pop       edi
  00484425  5e                    pop       esi
  00484426  5d                    pop       ebp
  00484427  5b                    pop       ebx
  00484428  83c430                add       esp,30h
  0048442b  c3                    ret

LAB_0048442c:
  0048442c  33ff                  xor       edi,edi
  0048442e  6a02                  push      2
  00484430  57                    push      edi
  00484431  bb00180000            mov       ebx,1800h
  00484436  6a6c                  push      6Ch
  00484438  56                    push      esi
  00484439  897e70                mov       [esi+70h],edi
  0048443c  897e44                mov       [esi+44h],edi
  0048443f  897e48                mov       [esi+48h],edi
  00484442  897e4c                mov       [esi+4Ch],edi
  00484445  895e50                mov       [esi+50h],ebx
  00484448  c7465400140000        mov       dword [esi+54h],1400h
  0048444f  895e58                mov       [esi+58h],ebx
  00484452  c7463880808000        mov       dword [esi+38h],808080h
  00484459  897e28                mov       [esi+28h],edi
  0048445c  66897e2c              mov       [esi+2Ch],di
  00484460  66897e34              mov       [esi+34h],di
  00484464  c6460401              mov       byte [esi+4],1
  00484468  e87366ffff            call      0047AAE0h
  0048446d  83c410                add       esp,10h
  00484470  8d461c                lea       eax,[esi+1Ch]
  00484473  50                    push      eax
  00484474  53                    push      ebx
  00484475  6802030000            push      302h
  0048447a  6a21                  push      21h
  0048447c  e89f960b00            call      0053DB20h
  00484481  8b442454              mov       eax,[esp+54h]
  00484485  83c410                add       esp,10h
  00484488  3bc7                  cmp       eax,edi
  0048448a  7403                  je        short 0048448Fh
  0048448c  897874                mov       [eax+74h],edi

LAB_0048448f:
  0048448f  893e                  mov       [esi],edi

LAB_00484491:
  00484491  5f                    pop       edi
  00484492  5e                    pop       esi
  00484493  5d                    pop       ebp
  00484494  5b                    pop       ebx
  00484495  83c430                add       esp,30h
  00484498  c3                    ret

12 cible(s) de saut interne : 0x004841dc, 0x0048421c, 0x0048421f, 0x00484281, 0x00484292, 0x004842a0, 0x004843b0, 0x004843d0, 0x004843ea, 0x0048442c, 0x0048448f, 0x00484491
