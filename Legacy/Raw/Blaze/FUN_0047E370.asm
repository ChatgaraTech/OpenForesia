
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047e370  section .text  offset fichier 0x7d770  531 octets

  0047e370  83ec0c                sub       esp,0Ch
  0047e373  53                    push      ebx
  0047e374  55                    push      ebp
  0047e375  56                    push      esi
  0047e376  8b74241c              mov       esi,[esp+1Ch]
  0047e37a  57                    push      edi
  0047e37b  8b06                  mov       eax,[esi]
  0047e37d  804e3606              or        byte [esi+36h],6
  0047e381  a900000002            test      eax,2000000h
  0047e386  7431                  je        short 0047E3B9h
  0047e388  668b4628              mov       ax,[esi+28h]
  0047e38c  668bc8                mov       cx,ax
  0047e38f  66c1f902              sar       cx,2
  0047e393  2bc1                  sub       eax,ecx
  0047e395  663d8000              cmp       ax,80h
  0047e399  66894628              mov       [esi+28h],ax
  0047e39d  6689462a              mov       [esi+2Ah],ax
  0047e3a1  6689462c              mov       [esi+2Ch],ax
  0047e3a5  0f8dd0010000          jge       near 0047E57Bh
  0047e3ab  c70600000000          mov       dword [esi],0
  0047e3b1  5f                    pop       edi
  0047e3b2  5e                    pop       esi
  0047e3b3  5d                    pop       ebp
  0047e3b4  5b                    pop       ebx
  0047e3b5  83c40c                add       esp,0Ch
  0047e3b8  c3                    ret

LAB_0047e3b9:
  0047e3b9  8b566c                mov       edx,[esi+6Ch]
  0047e3bc  0c80                  or        al,80h
  0047e3be  8906                  mov       [esi],eax
  0047e3c0  8b7e74                mov       edi,[esi+74h]
  0047e3c3  8b0a                  mov       ecx,[edx]
  0047e3c5  8b5e20                mov       ebx,[esi+20h]
  0047e3c8  81e100000002          and       ecx,2000000h
  0047e3ce  57                    push      edi
  0047e3cf  0bc8                  or        ecx,eax
  0047e3d1  668b4628              mov       ax,[esi+28h]
  0047e3d5  0fbfd0                movsx     edx,ax
  0047e3d8  890e                  mov       [esi],ecx
  0047e3da  b900100000            mov       ecx,1000h
  0047e3df  2bca                  sub       ecx,edx
  0047e3e1  ba80000000            mov       edx,80h
  0047e3e6  c1f902                sar       ecx,2
  0047e3e9  03c1                  add       eax,ecx
  0047e3eb  66894628              mov       [esi+28h],ax
  0047e3ef  6689462a              mov       [esi+2Ah],ax
  0047e3f3  6689462c              mov       [esi+2Ch],ax
  0047e3f7  33c0                  xor       eax,eax
  0047e3f9  8a4638                mov       al,[esi+38h]
  0047e3fc  2bd0                  sub       edx,eax
  0047e3fe  c1fa02                sar       edx,2
  0047e401  03c2                  add       eax,edx
  0047e403  8bc8                  mov       ecx,eax
  0047e405  c1e108                shl       ecx,8
  0047e408  0bc8                  or        ecx,eax
  0047e40a  c1e108                shl       ecx,8
  0047e40d  0bc8                  or        ecx,eax
  0047e40f  8b461c                mov       eax,[esi+1Ch]
  0047e412  894e38                mov       [esi+38h],ecx
  0047e415  8b571c                mov       edx,[edi+1Ch]
  0047e418  2bd0                  sub       edx,eax
  0047e41a  c1fa02                sar       edx,2
  0047e41d  03d0                  add       edx,eax
  0047e41f  89561c                mov       [esi+1Ch],edx
  0047e422  e899a70b00            call      00538BC0h
  0047e427  8b4f20                mov       ecx,[edi+20h]
  0047e42a  83c064                add       eax,64h
  0047e42d  c1e00c                shl       eax,0Ch
  0047e430  03c1                  add       eax,ecx
  0047e432  83c404                add       esp,4
  0047e435  2bc3                  sub       eax,ebx
  0047e437  c1f802                sar       eax,2
  0047e43a  03c3                  add       eax,ebx
  0047e43c  6a01                  push      1
  0047e43e  894620                mov       [esi+20h],eax
  0047e441  8b4624                mov       eax,[esi+24h]
  0047e444  8b4f24                mov       ecx,[edi+24h]
  0047e447  6aff                  push      0FFFFFFFFh
  0047e449  2bc8                  sub       ecx,eax
  0047e44b  57                    push      edi
  0047e44c  c1f902                sar       ecx,2
  0047e44f  03c8                  add       ecx,eax
  0047e451  894e24                mov       [esi+24h],ecx
  0047e454  e8576affff            call      00474EB0h
  0047e459  8be8                  mov       ebp,eax
  0047e45b  83c40c                add       esp,0Ch
  0047e45e  85ed                  test      ebp,ebp
  0047e460  0f8405010000          je        near 0047E56Bh
  0047e466  8b5630                mov       edx,[esi+30h]
  0047e469  8b4634                mov       eax,[esi+34h]
  0047e46c  8b4e04                mov       ecx,[esi+4]
  0047e46f  896e78                mov       [esi+78h],ebp
  0047e472  89542410              mov       [esp+10h],edx
  0047e476  8b561c                mov       edx,[esi+1Ch]
  0047e479  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047e480  c7462800080006        mov       dword [esi+28h],6000800h
  0047e487  66c7462c0006          mov       word [esi+2Ch],600h
  0047e48d  8b5d1c                mov       ebx,[ebp+1Ch]
  0047e490  2bda                  sub       ebx,edx
  0047e492  55                    push      ebp
  0047e493  89442418              mov       [esp+18h],eax
  0047e497  894c241c              mov       [esp+1Ch],ecx
  0047e49b  c1fb0c                sar       ebx,0Ch
  0047e49e  e81da70b00            call      00538BC0h
  0047e4a3  8bf8                  mov       edi,eax
  0047e4a5  8b4520                mov       eax,[ebp+20h]
  0047e4a8  8b6d24                mov       ebp,[ebp+24h]
  0047e4ab  83e7fe                and       edi,0FFFFFFFEh
  0047e4ae  8b5620                mov       edx,[esi+20h]
  0047e4b1  83c404                add       esp,4
  0047e4b4  c1e70b                shl       edi,0Bh
  0047e4b7  03f8                  add       edi,eax
  0047e4b9  8b4624                mov       eax,[esi+24h]
  0047e4bc  2be8                  sub       ebp,eax
  0047e4be  2bfa                  sub       edi,edx
  0047e4c0  c1fd0c                sar       ebp,0Ch
  0047e4c3  8bd3                  mov       edx,ebx
  0047e4c5  8bc5                  mov       eax,ebp
  0047e4c7  0fafd3                imul      edx,ebx
  0047e4ca  0fafc5                imul      eax,ebp
  0047e4cd  03d0                  add       edx,eax
  0047e4cf  52                    push      edx
  0047e4d0  c1ff0c                sar       edi,0Ch
  0047e4d3  e838750c00            call      00545A10h
  0047e4d8  55                    push      ebp
  0047e4d9  53                    push      ebx
  0047e4da  89442428              mov       [esp+28h],eax
  0047e4de  e83f750c00            call      00545A22h
  0047e4e3  8bd8                  mov       ebx,eax
  0047e4e5  e8568c0c00            call      00547140h
  0047e4ea  8b542420              mov       edx,[esp+20h]
  0047e4ee  25ff0f0000            and       eax,0FFFh
  0047e4f3  52                    push      edx
  0047e4f4  57                    push      edi
  0047e4f5  8d8c03000a0000        lea       ecx,[ebx+eax+0A00h]
  0047e4fc  66894e32              mov       [esi+32h],cx
  0047e500  e81d750c00            call      00545A22h
  0047e505  0fbf4e32              movsx     ecx,word [esi+32h]
  0047e509  0500010000            add       eax,100h
  0047e50e  6a20                  push      20h
  0047e510  66894634              mov       [esi+34h],ax
  0047e514  81e900040000          sub       ecx,400h
  0047e51a  0fbfc0                movsx     eax,ax
  0047e51d  50                    push      eax
  0047e51e  8d5650                lea       edx,[esi+50h]
  0047e521  51                    push      ecx
  0047e522  52                    push      edx
  0047e523  e8a8450b00            call      00532AD0h
  0047e528  83c410                add       esp,10h
  0047e52b  33c0                  xor       eax,eax
  0047e52d  8a460b                mov       al,[esi+0Bh]
  0047e530  c6460414              mov       byte [esi+4],14h
  0047e534  6aff                  push      0FFFFFFFFh
  0047e536  50                    push      eax
  0047e537  6a6d                  push      6Dh
  0047e539  56                    push      esi
  0047e53a  e8a1c5ffff            call      0047AAE0h
  0047e53f  8b4c2420              mov       ecx,[esp+20h]
  0047e543  8b542424              mov       edx,[esp+24h]
  0047e547  8b442428              mov       eax,[esp+28h]
  0047e54b  83c410                add       esp,10h
  0047e54e  894e30                mov       [esi+30h],ecx
  0047e551  895634                mov       [esi+34h],edx
  0047e554  894604                mov       [esi+4],eax
  0047e557  c74638ffffff00        mov       dword [esi+38h],0FFFFFFh
  0047e55e  c74628000c000c        mov       dword [esi+28h],0C000C00h
  0047e565  66c7462c000c          mov       word [esi+2Ch],0C00h

LAB_0047e56b:
  0047e56b  8b06                  mov       eax,[esi]
  0047e56d  a900000040            test      eax,40000000h
  0047e572  7407                  je        short 0047E57Bh
  0047e574  0d00000002            or        eax,2000000h
  0047e579  8906                  mov       [esi],eax

LAB_0047e57b:
  0047e57b  5f                    pop       edi
  0047e57c  5e                    pop       esi
  0047e57d  5d                    pop       ebp
  0047e57e  5b                    pop       ebx
  0047e57f  83c40c                add       esp,0Ch
  0047e582  c3                    ret

3 cible(s) de saut interne : 0x0047e3b9, 0x0047e56b, 0x0047e57b
