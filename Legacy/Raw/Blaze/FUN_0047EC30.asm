
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047ec30  section .text  offset fichier 0x7e030  808 octets

  0047ec30  83ec10                sub       esp,10h
  0047ec33  53                    push      ebx
  0047ec34  55                    push      ebp
  0047ec35  56                    push      esi
  0047ec36  8b742420              mov       esi,[esp+20h]
  0047ec3a  57                    push      edi
  0047ec3b  f70600000002          test      dword [esi],2000000h
  0047ec41  745e                  je        short 0047ECA1h
  0047ec43  668b4628              mov       ax,[esi+28h]
  0047ec47  668bc8                mov       cx,ax
  0047ec4a  66c1f903              sar       cx,3
  0047ec4e  2bc1                  sub       eax,ecx
  0047ec50  66894628              mov       [esi+28h],ax
  0047ec54  668b462c              mov       ax,[esi+2Ch]
  0047ec58  668bd0                mov       dx,ax
  0047ec5b  66c1fa03              sar       dx,3
  0047ec5f  2bc2                  sub       eax,edx
  0047ec61  6689462c              mov       [esi+2Ch],ax
  0047ec65  668b4612              mov       ax,[esi+12h]
  0047ec69  8bc8                  mov       ecx,eax
  0047ec6b  c1e106                shl       ecx,6
  0047ec6e  66014e32              add       [esi+32h],cx
  0047ec72  40                    inc       eax
  0047ec73  663d1400              cmp       ax,14h
  0047ec77  66894612              mov       [esi+12h],ax
  0047ec7b  0f8ccf020000          jl        near 0047EF50h
  0047ec81  c70600000000          mov       dword [esi],0
  0047ec87  8b7678                mov       esi,[esi+78h]
  0047ec8a  85f6                  test      esi,esi
  0047ec8c  0f84be020000          je        near 0047EF50h
  0047ec92  c7467400000000        mov       dword [esi+74h],0
  0047ec99  5f                    pop       edi
  0047ec9a  5e                    pop       esi
  0047ec9b  5d                    pop       ebp
  0047ec9c  5b                    pop       ebx
  0047ec9d  83c410                add       esp,10h
  0047eca0  c3                    ret

LAB_0047eca1:
  0047eca1  8b7e74                mov       edi,[esi+74h]
  0047eca4  8d567c                lea       edx,[esi+7Ch]
  0047eca7  6a06                  push      6
  0047eca9  52                    push      edx
  0047ecaa  8d477c                lea       eax,[edi+7Ch]
  0047ecad  50                    push      eax
  0047ecae  e82d660b00            call      005352E0h
  0047ecb3  8b8690000000          mov       eax,[esi+90h]
  0047ecb9  83c40c                add       esp,0Ch
  0047ecbc  f6c480                test      ah,80h
  0047ecbf  7540                  jne       short 0047ED01h
  0047ecc1  8b461c                mov       eax,[esi+1Ch]
  0047ecc4  8b4f1c                mov       ecx,[edi+1Ch]
  0047ecc7  8b5e20                mov       ebx,[esi+20h]
  0047ecca  2bc8                  sub       ecx,eax
  0047eccc  c1f902                sar       ecx,2
  0047eccf  03c8                  add       ecx,eax
  0047ecd1  57                    push      edi
  0047ecd2  894e1c                mov       [esi+1Ch],ecx
  0047ecd5  e8e69e0b00            call      00538BC0h
  0047ecda  8b6f20                mov       ebp,[edi+20h]
  0047ecdd  24fe                  and       al,0FEh
  0047ecdf  c1e00b                shl       eax,0Bh
  0047ece2  03c5                  add       eax,ebp
  0047ece4  83c404                add       esp,4
  0047ece7  2bc3                  sub       eax,ebx
  0047ece9  c1f803                sar       eax,3
  0047ecec  03c3                  add       eax,ebx
  0047ecee  894620                mov       [esi+20h],eax
  0047ecf1  8b4624                mov       eax,[esi+24h]
  0047ecf4  8b5724                mov       edx,[edi+24h]
  0047ecf7  2bd0                  sub       edx,eax
  0047ecf9  c1fa02                sar       edx,2
  0047ecfc  03d0                  add       edx,eax
  0047ecfe  895624                mov       [esi+24h],edx

LAB_0047ed01:
  0047ed01  8b0e                  mov       ecx,[esi]
  0047ed03  f6c102                test      cl,2
  0047ed06  0f84fb010000          je        near 0047EF07h
  0047ed0c  8b466c                mov       eax,[esi+6Ch]
  0047ed0f  8b00                  mov       eax,[eax]
  0047ed11  2500000002            and       eax,2000000h
  0047ed16  0bc1                  or        eax,ecx
  0047ed18  a900000002            test      eax,2000000h
  0047ed1d  8906                  mov       [esi],eax
  0047ed1f  7418                  je        short 0047ED39h
  0047ed21  8d4e1c                lea       ecx,[esi+1Ch]
  0047ed24  51                    push      ecx
  0047ed25  6800120000            push      1200h
  0047ed2a  686a010000            push      16Ah
  0047ed2f  6a31                  push      31h
  0047ed31  e8eaed0b00            call      0053DB20h
  0047ed36  83c410                add       esp,10h

LAB_0047ed39:
  0047ed39  8b5e78                mov       ebx,[esi+78h]
  0047ed3c  33ff                  xor       edi,edi
  0047ed3e  3bdf                  cmp       ebx,edi
  0047ed40  7426                  je        short 0047ED68h
  0047ed42  66ff4610              inc       word [esi+10h]
  0047ed46  8b0b                  mov       ecx,[ebx]
  0047ed48  668b4610              mov       ax,[esi+10h]
  0047ed4c  f7c100000050          test      ecx,50000000h
  0047ed52  7506                  jne       short 0047ED5Ah
  0047ed54  663d2800              cmp       ax,28h
  0047ed58  7c3c                  jl        short 0047ED96h

LAB_0047ed5a:
  0047ed5a  897b74                mov       [ebx+74h],edi
  0047ed5d  897e78                mov       [esi+78h],edi
  0047ed60  66897e10              mov       [esi+10h],di
  0047ed64  33db                  xor       ebx,ebx
  0047ed66  eb2e                  jmp       short 0047ED96h

LAB_0047ed68:
  0047ed68  8b5674                mov       edx,[esi+74h]
  0047ed6b  6a01                  push      1
  0047ed6d  6aff                  push      0FFFFFFFFh
  0047ed6f  52                    push      edx
  0047ed70  e83b61ffff            call      00474EB0h
  0047ed75  8bd8                  mov       ebx,eax
  0047ed77  83c40c                add       esp,0Ch
  0047ed7a  3bdf                  cmp       ebx,edi
  0047ed7c  7441                  je        short 0047EDBFh
  0047ed7e  8d461c                lea       eax,[esi+1Ch]
  0047ed81  50                    push      eax
  0047ed82  6800120000            push      1200h
  0047ed87  6800100000            push      1000h
  0047ed8c  6a23                  push      23h
  0047ed8e  e88ded0b00            call      0053DB20h
  0047ed93  83c410                add       esp,10h

LAB_0047ed96:
  0047ed96  3bdf                  cmp       ebx,edi
  0047ed98  7425                  je        short 0047EDBFh
  0047ed9a  895e78                mov       [esi+78h],ebx
  0047ed9d  8b4b24                mov       ecx,[ebx+24h]
  0047eda0  897374                mov       [ebx+74h],esi
  0047eda3  8b4624                mov       eax,[esi+24h]
  0047eda6  8b531c                mov       edx,[ebx+1Ch]
  0047eda9  2bc8                  sub       ecx,eax
  0047edab  c1f90c                sar       ecx,0Ch
  0047edae  51                    push      ecx
  0047edaf  8b4e1c                mov       ecx,[esi+1Ch]
  0047edb2  2bd1                  sub       edx,ecx
  0047edb4  c1fa0c                sar       edx,0Ch
  0047edb7  52                    push      edx
  0047edb8  e8656c0c00            call      00545A22h
  0047edbd  eb07                  jmp       short 0047EDC6h

LAB_0047edbf:
  0047edbf  8b5674                mov       edx,[esi+74h]
  0047edc2  668b4232              mov       ax,[edx+32h]

LAB_0047edc6:
  0047edc6  668b4e32              mov       cx,[esi+32h]
  0047edca  2bc1                  sub       eax,ecx
  0047edcc  25ff0f0000            and       eax,0FFFh
  0047edd1  3d00080000            cmp       eax,800h
  0047edd6  7d07                  jge       short 0047EDDFh
  0047edd8  c1f803                sar       eax,3
  0047eddb  8bc8                  mov       ecx,eax
  0047eddd  eb0c                  jmp       short 0047EDEBh

LAB_0047eddf:
  0047eddf  b900100000            mov       ecx,1000h
  0047ede4  2bc8                  sub       ecx,eax
  0047ede6  c1f903                sar       ecx,3
  0047ede9  f7d9                  neg       ecx

LAB_0047edeb:
  0047edeb  66014e32              add       [esi+32h],cx
  0047edef  668b4632              mov       ax,[esi+32h]
  0047edf3  3bdf                  cmp       ebx,edi
  0047edf5  0f8455010000          je        near 0047EF50h
  0047edfb  8b561c                mov       edx,[esi+1Ch]
  0047edfe  8b4e20                mov       ecx,[esi+20h]
  0047ee01  0fbff8                movsx     edi,ax
  0047ee04  89542414              mov       [esp+14h],edx
  0047ee08  8b5624                mov       edx,[esi+24h]
  0047ee0b  57                    push      edi
  0047ee0c  894c241c              mov       [esp+1Ch],ecx
  0047ee10  89542420              mov       [esp+20h],edx
  0047ee14  897c2414              mov       [esp+14h],edi
  0047ee18  e8ff6b0c00            call      00545A1Ch
  0047ee1d  8b4e1c                mov       ecx,[esi+1Ch]
  0047ee20  8d0440                lea       eax,[eax+eax*2]
  0047ee23  57                    push      edi
  0047ee24  8d0480                lea       eax,[eax+eax*4]
  0047ee27  d1e0                  shl       eax,1
  0047ee29  03c8                  add       ecx,eax
  0047ee2b  8b4620                mov       eax,[esi+20h]
  0047ee2e  0500401000            add       eax,104000h
  0047ee33  894e1c                mov       [esi+1Ch],ecx
  0047ee36  894620                mov       [esi+20h],eax
  0047ee39  e8d86b0c00            call      00545A16h
  0047ee3e  8d0440                lea       eax,[eax+eax*2]
  0047ee41  8b6e1c                mov       ebp,[esi+1Ch]
  0047ee44  8d0c80                lea       ecx,[eax+eax*4]
  0047ee47  8b4624                mov       eax,[esi+24h]
  0047ee4a  d1e1                  shl       ecx,1
  0047ee4c  03c1                  add       eax,ecx
  0047ee4e  8b4e20                mov       ecx,[esi+20h]
  0047ee51  894624                mov       [esi+24h],eax
  0047ee54  8b7b20                mov       edi,[ebx+20h]
  0047ee57  8bd0                  mov       edx,eax
  0047ee59  8b431c                mov       eax,[ebx+1Ch]
  0047ee5c  2bf9                  sub       edi,ecx
  0047ee5e  8b4b24                mov       ecx,[ebx+24h]
  0047ee61  2bc5                  sub       eax,ebp
  0047ee63  2bca                  sub       ecx,edx
  0047ee65  c1f80c                sar       eax,0Ch
  0047ee68  c1f90c                sar       ecx,0Ch
  0047ee6b  8bd8                  mov       ebx,eax
  0047ee6d  8be9                  mov       ebp,ecx
  0047ee6f  0fafd8                imul      ebx,eax
  0047ee72  0fafe9                imul      ebp,ecx
  0047ee75  8d142b                lea       edx,[ebx+ebp]
  0047ee78  52                    push      edx
  0047ee79  c1ff0c                sar       edi,0Ch
  0047ee7c  e88f6b0c00            call      00545A10h
  0047ee81  50                    push      eax
  0047ee82  57                    push      edi
  0047ee83  e89a6b0c00            call      00545A22h
  0047ee88  89442424              mov       [esp+24h],eax
  0047ee8c  8bc7                  mov       eax,edi
  0047ee8e  0fafc7                imul      eax,edi
  0047ee91  03c3                  add       eax,ebx
  0047ee93  03c5                  add       eax,ebp
  0047ee95  50                    push      eax
  0047ee96  e8756b0c00            call      00545A10h
  0047ee9b  8b4c2424              mov       ecx,[esp+24h]
  0047ee9f  8b542410              mov       edx,[esp+10h]
  0047eea3  8b7e28                mov       edi,[esi+28h]
  0047eea6  8b5e2c                mov       ebx,[esi+2Ch]
  0047eea9  8b6e04                mov       ebp,[esi+4]
  0047eeac  c7462800040008        mov       dword [esi+28h],8000400h
  0047eeb3  c1f803                sar       eax,3
  0047eeb6  50                    push      eax
  0047eeb7  51                    push      ecx
  0047eeb8  8d4650                lea       eax,[esi+50h]
  0047eebb  52                    push      edx
  0047eebc  50                    push      eax
  0047eebd  66c7462c0004          mov       word [esi+2Ch],400h
  0047eec3  e8083c0b00            call      00532AD0h
  0047eec8  83c410                add       esp,10h
  0047eecb  c646041e              mov       byte [esi+4],1Eh
  0047eecf  6a04                  push      4
  0047eed1  6a00                  push      0
  0047eed3  688a000000            push      8Ah
  0047eed8  56                    push      esi
  0047eed9  e802bcffff            call      0047AAE0h
  0047eede  83c410                add       esp,10h
  0047eee1  897e28                mov       [esi+28h],edi
  0047eee4  895e2c                mov       [esi+2Ch],ebx
  0047eee7  896e04                mov       [esi+4],ebp
  0047eeea  8b4c2414              mov       ecx,[esp+14h]
  0047eeee  8b542418              mov       edx,[esp+18h]
  0047eef2  8b44241c              mov       eax,[esp+1Ch]
  0047eef6  894e1c                mov       [esi+1Ch],ecx
  0047eef9  895620                mov       [esi+20h],edx
  0047eefc  894624                mov       [esi+24h],eax
  0047eeff  5f                    pop       edi
  0047ef00  5e                    pop       esi
  0047ef01  5d                    pop       ebp
  0047ef02  5b                    pop       ebx
  0047ef03  83c410                add       esp,10h
  0047ef06  c3                    ret

LAB_0047ef07:
  0047ef07  8b4638                mov       eax,[esi+38h]
  0047ef0a  3df0f0f000            cmp       eax,0F0F0F0h
  0047ef0f  7308                  jae       short 0047EF19h
  0047ef11  0510101000            add       eax,101010h
  0047ef16  894638                mov       [esi+38h],eax

LAB_0047ef19:
  0047ef19  668b462a              mov       ax,[esi+2Ah]
  0047ef1d  ba00180000            mov       edx,1800h
  0047ef22  0fbfc8                movsx     ecx,ax
  0047ef25  2bd1                  sub       edx,ecx
  0047ef27  c1fa03                sar       edx,3
  0047ef2a  03c2                  add       eax,edx
  0047ef2c  663d4020              cmp       ax,2040h
  0047ef30  6689462a              mov       [esi+2Ah],ax
  0047ef34  7d1a                  jge       short 0047EF50h
  0047ef36  8b16                  mov       edx,[esi]
  0047ef38  8b466c                mov       eax,[esi+6Ch]
  0047ef3b  83ca02                or        edx,2
  0047ef3e  8916                  mov       [esi],edx
  0047ef40  8b08                  mov       ecx,[eax]
  0047ef42  83c902                or        ecx,2
  0047ef45  8908                  mov       [eax],ecx
  0047ef47  8b7674                mov       esi,[esi+74h]
  0047ef4a  8b06                  mov       eax,[esi]
  0047ef4c  247f                  and       al,7Fh
  0047ef4e  8906                  mov       [esi],eax

LAB_0047ef50:
  0047ef50  5f                    pop       edi
  0047ef51  5e                    pop       esi
  0047ef52  5d                    pop       ebp
  0047ef53  5b                    pop       ebx
  0047ef54  83c410                add       esp,10h
  0047ef57  c3                    ret

13 cible(s) de saut interne : 0x0047eca1, 0x0047ed01, 0x0047ed39, 0x0047ed5a, 0x0047ed68, 0x0047ed96, 0x0047edbf, 0x0047edc6, 0x0047eddf, 0x0047edeb, 0x0047ef07, 0x0047ef19, 0x0047ef50
