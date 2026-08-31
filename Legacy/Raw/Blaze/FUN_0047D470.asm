
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d470  section .text  offset fichier 0x7c870  395 octets

  0047d470  83ec0c                sub       esp,0Ch
  0047d473  53                    push      ebx
  0047d474  55                    push      ebp
  0047d475  56                    push      esi
  0047d476  8b74241c              mov       esi,[esp+1Ch]
  0047d47a  57                    push      edi
  0047d47b  66c744241000fc        mov       word [esp+10h],0FC00h
  0047d482  66837e1000            cmp       word [esi+10h],0
  0047d487  66c7442412c0ff        mov       word [esp+12h],0FFC0h
  0047d48e  66c74424144000        mov       word [esp+14h],40h
  0047d495  66c74424168000        mov       word [esp+16h],80h
  0047d49c  66c74424180000        mov       word [esp+18h],0
  0047d4a3  66c744241a8001        mov       word [esp+1Ah],180h
  0047d4aa  753f                  jne       short 0047D4EBh
  0047d4ac  8b4674                mov       eax,[esi+74h]
  0047d4af  8b38                  mov       edi,[eax]
  0047d4b1  81cf80000000          or        edi,80h
  0047d4b7  8938                  mov       [eax],edi
  0047d4b9  668b4636              mov       ax,[esi+36h]
  0047d4bd  8b16                  mov       edx,[esi]
  0047d4bf  24fe                  and       al,0FEh
  0047d4c1  0c04                  or        al,4
  0047d4c3  80ca80                or        dl,80h
  0047d4c6  66ff4610              inc       word [esi+10h]
  0047d4ca  66894636              mov       [esi+36h],ax
  0047d4ce  668b4628              mov       ax,[esi+28h]
  0047d4d2  8916                  mov       [esi],edx
  0047d4d4  668bc8                mov       cx,ax
  0047d4d7  66c1f805              sar       ax,5
  0047d4db  66c1f904              sar       cx,4
  0047d4df  f7d8                  neg       eax
  0047d4e1  66894c241a            mov       [esp+1Ah],cx
  0047d4e6  6689442412            mov       [esp+12h],ax

LAB_0047d4eb:
  0047d4eb  e8509c0c00            call      00547140h
  0047d4f0  99                    cdq
  0047d4f1  b903000000            mov       ecx,3
  0047d4f6  f7f9                  idiv      ecx
  0047d4f8  80c208                add       dl,8
  0047d4fb  885604                mov       [esi+4],dl
  0047d4fe  e83d9c0c00            call      00547140h
  0047d503  25ff0f0000            and       eax,0FFFh
  0047d508  0500200000            add       eax,2000h
  0047d50d  6689462a              mov       [esi+2Ah],ax
  0047d511  e82a9c0c00            call      00547140h
  0047d516  25ff0f0000            and       eax,0FFFh
  0047d51b  668b562a              mov       dx,[esi+2Ah]
  0047d51f  0500280000            add       eax,2800h
  0047d524  6689462c              mov       [esi+2Ch],ax
  0047d528  66c1f807              sar       ax,7
  0047d52c  6689442414            mov       [esp+14h],ax
  0047d531  8d442410              lea       eax,[esp+10h]
  0047d535  66c1fa06              sar       dx,6
  0047d539  50                    push      eax
  0047d53a  56                    push      esi
  0047d53b  668954241e            mov       [esp+1Eh],dx
  0047d540  e85b77ffff            call      00474CA0h
  0047d545  8bf8                  mov       edi,eax
  0047d547  83c408                add       esp,8
  0047d54a  85ff                  test      edi,edi
  0047d54c  0f8484000000          je        near 0047D5D6h
  0047d552  8b4674                mov       eax,[esi+74h]
  0047d555  33c9                  xor       ecx,ecx
  0047d557  8a4e06                mov       cl,[esi+6]
  0047d55a  33d2                  xor       edx,edx
  0047d55c  8a5607                mov       dl,[esi+7]
  0047d55f  6a00                  push      0
  0047d561  51                    push      ecx
  0047d562  52                    push      edx
  0047d563  57                    push      edi
  0047d564  50                    push      eax
  0047d565  e846cd0b00            call      0053A2B0h
  0047d56a  83c414                add       esp,14h
  0047d56d  33c9                  xor       ecx,ecx
  0047d56f  8a4e0b                mov       cl,[esi+0Bh]
  0047d572  8d5e1c                lea       ebx,[esi+1Ch]
  0047d575  6aff                  push      0FFFFFFFFh
  0047d577  51                    push      ecx
  0047d578  6a49                  push      49h
  0047d57a  68601a5500            push      551A60h
  0047d57f  53                    push      ebx
  0047d580  56                    push      esi
  0047d581  897e78                mov       [esi+78h],edi
  0047d584  e8b7d6ffff            call      0047AC40h
  0047d589  0fbf6e32              movsx     ebp,word [esi+32h]
  0047d58d  83c418                add       esp,18h
  0047d590  55                    push      ebp
  0047d591  e886840c00            call      00545A1Ch
  0047d596  8b4f50                mov       ecx,[edi+50h]
  0047d599  55                    push      ebp
  0047d59a  c1e004                shl       eax,4
  0047d59d  03c8                  add       ecx,eax
  0047d59f  894f50                mov       [edi+50h],ecx
  0047d5a2  e86f840c00            call      00545A16h
  0047d5a7  8b4f58                mov       ecx,[edi+58h]
  0047d5aa  53                    push      ebx
  0047d5ab  c1e004                shl       eax,4
  0047d5ae  03c8                  add       ecx,eax
  0047d5b0  8b07                  mov       eax,[edi]
  0047d5b2  6800120000            push      1200h
  0047d5b7  0c40                  or        al,40h
  0047d5b9  6800080000            push      800h
  0047d5be  6a14                  push      14h
  0047d5c0  894f58                mov       [edi+58h],ecx
  0047d5c3  66c787960000001000    mov       word [edi+96h],10h
  0047d5cc  8907                  mov       [edi],eax
  0047d5ce  e84d050c00            call      0053DB20h
  0047d5d3  83c410                add       esp,10h

LAB_0047d5d6:
  0047d5d6  8b4638                mov       eax,[esi+38h]
  0047d5d9  05f0efefff            add       eax,0FFEFEFF0h
  0047d5de  894638                mov       [esi+38h],eax
  0047d5e1  7510                  jne       short 0047D5F3h
  0047d5e3  8b4674                mov       eax,[esi+74h]
  0047d5e6  8b08                  mov       ecx,[eax]
  0047d5e8  80e17f                and       cl,7Fh
  0047d5eb  8908                  mov       [eax],ecx
  0047d5ed  c70600000000          mov       dword [esi],0

LAB_0047d5f3:
  0047d5f3  5f                    pop       edi
  0047d5f4  5e                    pop       esi
  0047d5f5  5d                    pop       ebp
  0047d5f6  5b                    pop       ebx
  0047d5f7  83c40c                add       esp,0Ch
  0047d5fa  c3                    ret

3 cible(s) de saut interne : 0x0047d4eb, 0x0047d5d6, 0x0047d5f3
