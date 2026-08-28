Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004765d0  section .text  offset fichier 0x759d0  350 octets

  004765d0  53                    push      ebx
  004765d1  56                    push      esi
  004765d2  8b74240c              mov       esi,[esp+0Ch]
  004765d6  57                    push      edi
  004765d7  f70600000002          test      dword [esi],2000000h
  004765dd  7438                  je        short 00476617h
  004765df  0fbf4628              movsx     eax,word [esi+28h]
  004765e3  8bc8                  mov       ecx,eax
  004765e5  d1f9                  sar       ecx,1
  004765e7  2bc1                  sub       eax,ecx
  004765e9  8bd0                  mov       edx,eax
  004765eb  6689462c              mov       [esi+2Ch],ax
  004765ef  c1e210                shl       edx,10h
  004765f2  0bd0                  or        edx,eax
  004765f4  3d00010000            cmp       eax,100h
  004765f9  895628                mov       [esi+28h],edx
  004765fc  0f8de3000000          jge       near 004766E5h
  00476602  6683463240            add       word [esi+32h],40h
  00476607  668146348000          add       word [esi+34h],80h
  0047660d  c70600000000          mov       dword [esi],0
  00476613  5f                    pop       edi
  00476614  5e                    pop       esi
  00476615  5b                    pop       ebx
  00476616  c3                    ret

LAB_00476617:
  00476617  33db                  xor       ebx,ebx
  00476619  66395e10              cmp       [esi+10h],bx
  0047661d  754d                  jne       short 0047666Ch
  0047661f  8b466c                mov       eax,[esi+6Ch]
  00476622  804e3606              or        byte [esi+36h],6
  00476626  66c746100100          mov       word [esi+10h],1
  0047662c  c74638ffffff00        mov       dword [esi+38h],0FFFFFFh
  00476633  895e28                mov       [esi+28h],ebx
  00476636  66895e2c              mov       [esi+2Ch],bx
  0047663a  66895e34              mov       [esi+34h],bx
  0047663e  8b08                  mov       ecx,[eax]
  00476640  f7c100001000          test      ecx,100000h
  00476646  0f84a8000000          je        near 004766F4h
  0047664c  8b406c                mov       eax,[eax+6Ch]
  0047664f  c646043d              mov       byte [esi+4],3Dh
  00476653  8bc8                  mov       ecx,eax
  00476655  89466c                mov       [esi+6Ch],eax
  00476658  51                    push      ecx
  00476659  885e0b                mov       [esi+0Bh],bl
  0047665c  c6460a0f              mov       byte [esi+0Ah],0Fh
  00476660  e85b250c00            call      00538BC0h
  00476665  83c404                add       esp,4
  00476668  66894612              mov       [esi+12h],ax

LAB_0047666c:
  0047666c  0fbf7e12              movsx     edi,word [esi+12h]
  00476670  8bd7                  mov       edx,edi
  00476672  b8b5814e1b            mov       eax,1B4E81B5h
  00476677  c1e20c                shl       edx,0Ch
  0047667a  f7ea                  imul      edx
  0047667c  0fbf4e28              movsx     ecx,word [esi+28h]
  00476680  c1fa04                sar       edx,4
  00476683  8bc2                  mov       eax,edx
  00476685  8b5e6c                mov       ebx,[esi+6Ch]
  00476688  c1e81f                shr       eax,1Fh
  0047668b  03d0                  add       edx,eax
  0047668d  83e7fe                and       edi,0FFFFFFFEh
  00476690  2bd1                  sub       edx,ecx
  00476692  c1fa02                sar       edx,2
  00476695  03ca                  add       ecx,edx
  00476697  8bd1                  mov       edx,ecx
  00476699  66894e2c              mov       [esi+2Ch],cx
  0047669d  c1e210                shl       edx,10h
  004766a0  0bd1                  or        edx,ecx
  004766a2  895628                mov       [esi+28h],edx
  004766a5  8b431c                mov       eax,[ebx+1Ch]
  004766a8  89461c                mov       [esi+1Ch],eax
  004766ab  8b4b20                mov       ecx,[ebx+20h]
  004766ae  c1e70b                shl       edi,0Bh
  004766b1  03cf                  add       ecx,edi
  004766b3  894e20                mov       [esi+20h],ecx
  004766b6  8b5324                mov       edx,[ebx+24h]
  004766b9  895624                mov       [esi+24h],edx
  004766bc  8b03                  mov       eax,[ebx]
  004766be  a900000050            test      eax,50000000h
  004766c3  751a                  jne       short 004766DFh
  004766c5  a900008000            test      eax,800000h
  004766ca  7407                  je        short 004766D3h
  004766cc  66837b1805            cmp       word [ebx+18h],5
  004766d1  750c                  jne       short 004766DFh

LAB_004766d3:
  004766d3  2504004000            and       eax,400004h
  004766d8  3d00004000            cmp       eax,400000h
  004766dd  7506                  jne       short 004766E5h

LAB_004766df:
  004766df  810e00000002          or        dword [esi],2000000h

LAB_004766e5:
  004766e5  6683463240            add       word [esi+32h],40h
  004766ea  668146348000          add       word [esi+34h],80h
  004766f0  5f                    pop       edi
  004766f1  5e                    pop       esi
  004766f2  5b                    pop       ebx
  004766f3  c3                    ret

LAB_004766f4:
  004766f4  53                    push      ebx
  004766f5  53                    push      ebx
  004766f6  bf00080000            mov       edi,800h
  004766fb  68bd000000            push      0BDh
  00476700  66017e32              add       [esi+32h],di
  00476704  56                    push      esi
  00476705  e8d6430000            call      0047AAE0h
  0047670a  66017e32              add       [esi+32h],di
  0047670e  83c410                add       esp,10h
  00476711  53                    push      ebx
  00476712  53                    push      ebx
  00476713  68bd000000            push      0BDh
  00476718  56                    push      esi
  00476719  e8c2430000            call      0047AAE0h
  0047671e  8b06                  mov       eax,[esi]
  00476720  83c410                add       esp,10h
  00476723  0d00000002            or        eax,2000000h
  00476728  8906                  mov       [esi],eax
  0047672a  5f                    pop       edi
  0047672b  5e                    pop       esi
  0047672c  5b                    pop       ebx
  0047672d  c3                    ret

6 cible(s) de saut interne : 0x00476617, 0x0047666c, 0x004766d3, 0x004766df, 0x004766e5, 0x004766f4
