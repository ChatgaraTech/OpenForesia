
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047f070  section .text  offset fichier 0x7e470  532 octets

  0047f070  83ec0c                sub       esp,0Ch
  0047f073  55                    push      ebp
  0047f074  56                    push      esi
  0047f075  8b742418              mov       esi,[esp+18h]
  0047f079  33c0                  xor       eax,eax
  0047f07b  66c744240800f8        mov       word [esp+8],0F800h
  0047f082  668944240a            mov       [esp+0Ah],ax
  0047f087  8b0e                  mov       ecx,[esi]
  0047f089  8b6e74                mov       ebp,[esi+74h]
  0047f08c  f7c100000002          test      ecx,2000000h
  0047f092  668944240c            mov       [esp+0Ch],ax
  0047f097  66c744240e9600        mov       word [esp+0Eh],96h
  0047f09e  66c74424105501        mov       word [esp+10h],155h
  0047f0a5  6689442412            mov       [esp+12h],ax
  0047f0aa  7408                  je        short 0047F0B4h
  0047f0ac  8906                  mov       [esi],eax
  0047f0ae  5e                    pop       esi
  0047f0af  5d                    pop       ebp
  0047f0b0  83c40c                add       esp,0Ch
  0047f0b3  c3                    ret

LAB_0047f0b4:
  0047f0b4  66394610              cmp       [esi+10h],ax
  0047f0b8  57                    push      edi
  0047f0b9  53                    push      ebx
  0047f0ba  7527                  jne       short 0047F0E3h
  0047f0bc  668b4636              mov       ax,[esi+36h]
  0047f0c0  8d4e1c                lea       ecx,[esi+1Ch]
  0047f0c3  24fe                  and       al,0FEh
  0047f0c5  51                    push      ecx
  0047f0c6  0c06                  or        al,6
  0047f0c8  6858255500            push      552558h
  0047f0cd  66894636              mov       [esi+36h],ax
  0047f0d1  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  0047f0d7  c646040c              mov       byte [esi+4],0Ch
  0047f0db  e800eb0b00            call      0053DBE0h
  0047f0e0  83c408                add       esp,8

LAB_0047f0e3:
  0047f0e3  0fbf5610              movsx     edx,word [esi+10h]
  0047f0e7  668146328000          add       word [esi+32h],80h
  0047f0ed  c1e209                shl       edx,9
  0047f0f0  52                    push      edx
  0047f0f1  e826690c00            call      00545A1Ch
  0047f0f6  0fbf5610              movsx     edx,word [esi+10h]
  0047f0fa  c1f806                sar       eax,6
  0047f0fd  05bf000000            add       eax,0BFh
  0047f102  8bc8                  mov       ecx,eax
  0047f104  c1e108                shl       ecx,8
  0047f107  0bc8                  or        ecx,eax
  0047f109  c1e108                shl       ecx,8
  0047f10c  c1e207                shl       edx,7
  0047f10f  0bc8                  or        ecx,eax
  0047f111  52                    push      edx
  0047f112  894e38                mov       [esi+38h],ecx
  0047f115  e8fc680c00            call      00545A16h
  0047f11a  8bf8                  mov       edi,eax
  0047f11c  8d5e1c                lea       ebx,[esi+1Ch]
  0047f11f  d1ff                  sar       edi,1
  0047f121  81c700300000          add       edi,3000h
  0047f127  6aff                  push      0FFFFFFFFh
  0047f129  8bc7                  mov       eax,edi
  0047f12b  66897e28              mov       [esi+28h],di
  0047f12f  d1f8                  sar       eax,1
  0047f131  6689462a              mov       [esi+2Ah],ax
  0047f135  33c0                  xor       eax,eax
  0047f137  8a460b                mov       al,[esi+0Bh]
  0047f13a  66897e2c              mov       [esi+2Ch],di
  0047f13e  8b4d1c                mov       ecx,[ebp+1Ch]
  0047f141  50                    push      eax
  0047f142  890b                  mov       [ebx],ecx
  0047f144  8b5524                mov       edx,[ebp+24h]
  0047f147  6884000000            push      84h
  0047f14c  56                    push      esi
  0047f14d  895624                mov       [esi+24h],edx
  0047f150  e88bb9ffff            call      0047AAE0h
  0047f155  8bc7                  mov       eax,edi
  0047f157  83c410                add       esp,10h
  0047f15a  c1e003                shl       eax,3
  0047f15d  2bc7                  sub       eax,edi
  0047f15f  6800080000            push      800h
  0047f164  f7d8                  neg       eax
  0047f166  8d0c80                lea       ecx,[eax+eax*4]
  0047f169  8bc7                  mov       eax,edi
  0047f16b  c1e003                shl       eax,3
  0047f16e  2bc7                  sub       eax,edi
  0047f170  d1e1                  shl       ecx,1
  0047f172  8d1480                lea       edx,[eax+eax*4]
  0047f175  8b06                  mov       eax,[esi]
  0047f177  c1e202                shl       edx,2
  0047f17a  c1f90c                sar       ecx,0Ch
  0047f17d  c1fa0c                sar       edx,0Ch
  0047f180  a900008000            test      eax,800000h
  0047f185  66894c2416            mov       [esp+16h],cx
  0047f18a  668954241e            mov       [esp+1Eh],dx
  0047f18f  740e                  je        short 0047F19Fh
  0047f191  8d442414              lea       eax,[esp+14h]
  0047f195  50                    push      eax
  0047f196  53                    push      ebx
  0047f197  56                    push      esi
  0047f198  e893aa0900            call      00519C30h
  0047f19d  eb0c                  jmp       short 0047F1ABh

LAB_0047f19f:
  0047f19f  8d4c2414              lea       ecx,[esp+14h]
  0047f1a3  51                    push      ecx
  0047f1a4  53                    push      ebx
  0047f1a5  56                    push      esi
  0047f1a6  e845a90900            call      00519AF0h

LAB_0047f1ab:
  0047f1ab  83c410                add       esp,10h
  0047f1ae  85c0                  test      eax,eax
  0047f1b0  7503                  jne       short 0047F1B5h
  0047f1b2  8b4674                mov       eax,[esi+74h]

LAB_0047f1b5:
  0047f1b5  8b5674                mov       edx,[esi+74h]
  0047f1b8  894678                mov       [esi+78h],eax
  0047f1bb  668b4606              mov       ax,[esi+6]
  0047f1bf  6a00                  push      0
  0047f1c1  66894206              mov       [edx+6],ax
  0047f1c5  8b4e78                mov       ecx,[esi+78h]
  0047f1c8  8b5674                mov       edx,[esi+74h]
  0047f1cb  51                    push      ecx
  0047f1cc  52                    push      edx
  0047f1cd  e85ed10b00            call      0053C330h
  0047f1d2  8b06                  mov       eax,[esi]
  0047f1d4  83c40c                add       esp,0Ch
  0047f1d7  a900008000            test      eax,800000h
  0047f1dc  7479                  je        short 0047F257h
  0047f1de  8b4e74                mov       ecx,[esi+74h]
  0047f1e1  8b3db4876000          mov       edi,[6087B4h]
  0047f1e7  33d2                  xor       edx,edx
  0047f1e9  8a5108                mov       dl,[ecx+8]
  0047f1ec  8b1f                  mov       ebx,[edi]
  0047f1ee  8d4a78                lea       ecx,[edx+78h]
  0047f1f1  8d3cd2                lea       edi,[edx+edx*8]
  0047f1f4  c1e10d                shl       ecx,0Dh
  0047f1f7  03cb                  add       ecx,ebx
  0047f1f9  8d147a                lea       edx,[edx+edi*2]
  0047f1fc  33db                  xor       ebx,ebx
  0047f1fe  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047f205  c1e304                shl       ebx,4
  0047f208  668b93204a6000        mov       dx,[ebx+604A20h]
  0047f20f  668591c41f0000        test      [ecx+1FC4h],dx
  0047f216  7507                  jne       short 0047F21Fh
  0047f218  0d00000002            or        eax,2000000h
  0047f21d  8906                  mov       [esi],eax

LAB_0047f21f:
  0047f21f  a1f02c5f00            mov       eax,[5F2CF0h]
  0047f224  33d2                  xor       edx,edx
  0047f226  bf14000000            mov       edi,14h
  0047f22b  f7f7                  div       edi
  0047f22d  85d2                  test      edx,edx
  0047f22f  7534                  jne       short 0047F265h
  0047f231  668b814e010000        mov       ax,[ecx+14Eh]
  0047f238  663d0400              cmp       ax,4
  0047f23c  7c0c                  jl        short 0047F24Ah
  0047f23e  83c0fc                add       eax,0FFFFFFFCh
  0047f241  6689814e010000        mov       [ecx+14Eh],ax
  0047f248  eb1b                  jmp       short 0047F265h

LAB_0047f24a:
  0047f24a  66c7814e0100000000    mov       word [ecx+14Eh],0
  0047f253  8b06                  mov       eax,[esi]
  0047f255  eb07                  jmp       short 0047F25Eh

LAB_0047f257:
  0047f257  66837e1078            cmp       word [esi+10h],78h
  0047f25c  7c07                  jl        short 0047F265h

LAB_0047f25e:
  0047f25e  0d00000002            or        eax,2000000h
  0047f263  8906                  mov       [esi],eax

LAB_0047f265:
  0047f265  56                    push      esi
  0047f266  e8a55effff            call      00475110h
  0047f26b  83c404                add       esp,4
  0047f26e  85c0                  test      eax,eax
  0047f270  7406                  je        short 0047F278h
  0047f272  810e00000002          or        dword [esi],2000000h

LAB_0047f278:
  0047f278  66ff4610              inc       word [esi+10h]
  0047f27c  5b                    pop       ebx
  0047f27d  5f                    pop       edi
  0047f27e  5e                    pop       esi
  0047f27f  5d                    pop       ebp
  0047f280  83c40c                add       esp,0Ch
  0047f283  c3                    ret

11 cible(s) de saut interne : 0x0047f0b4, 0x0047f0e3, 0x0047f19f, 0x0047f1ab, 0x0047f1b5, 0x0047f21f, 0x0047f24a, 0x0047f257, 0x0047f25e, 0x0047f265, 0x0047f278
