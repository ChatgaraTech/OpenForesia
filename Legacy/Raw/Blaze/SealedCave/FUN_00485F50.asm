  00485f50  83ec0c                sub       esp,0Ch
  00485f53  53                    push      ebx
  00485f54  55                    push      ebp
  00485f55  56                    push      esi
  00485f56  8b74241c              mov       esi,[esp+1Ch]
  00485f5a  57                    push      edi
  00485f5b  33ed                  xor       ebp,ebp
  00485f5d  8b466c                mov       eax,[esi+6Ch]
  00485f60  8d7e1c                lea       edi,[esi+1Ch]
  00485f63  66396e10              cmp       [esi+10h],bp
  00485f67  8b481c                mov       ecx,[eax+1Ch]
  00485f6a  890f                  mov       [edi],ecx
  00485f6c  8b5020                mov       edx,[eax+20h]
  00485f6f  895620                mov       [esi+20h],edx
  00485f72  8b4824                mov       ecx,[eax+24h]
  00485f75  894e24                mov       [esi+24h],ecx
  00485f78  7549                  jne       short 00485FC3h
  00485f7a  0fbf5e32              movsx     ebx,word [esi+32h]
  00485f7e  81c32c010000          add       ebx,12Ch
  00485f84  894674                mov       [esi+74h],eax
  00485f87  53                    push      ebx
  00485f88  894670                mov       [esi+70h],eax
  00485f8b  e88cfa0b00            call      00545A1Ch
  00485f90  8d0480                lea       eax,[eax+eax*4]
  00485f93  53                    push      ebx
  00485f94  8d1480                lea       edx,[eax+eax*4]
  00485f97  c1e202                shl       edx,2
  00485f9a  895644                mov       [esi+44h],edx
  00485f9d  e874fa0b00            call      00545A16h
  00485fa2  8d0480                lea       eax,[eax+eax*4]
  00485fa5  57                    push      edi
  00485fa6  6890275500            push      552790h
  00485fab  c7464800e01a00        mov       dword [esi+48h],1AE000h
  00485fb2  8d0480                lea       eax,[eax+eax*4]
  00485fb5  c1e002                shl       eax,2
  00485fb8  89464c                mov       [esi+4Ch],eax
  00485fbb  e8207c0b00            call      0053DBE0h
  00485fc0  83c408                add       esp,8
  00485fc3  66837e100e            cmp       word [esi+10h],0Eh
  00485fc8  7d5e                  jge       short 00486028h
  00485fca  e871110c00            call      00547140h
  00485fcf  66894632              mov       [esi+32h],ax
  00485fd3  e868110c00            call      00547140h
  00485fd8  66894634              mov       [esi+34h],ax
  00485fdc  b800010000            mov       eax,100h
  00485fe1  66c746360400          mov       word [esi+36h],4
  00485fe7  894628                mov       [esi+28h],eax
  00485fea  6689462c              mov       [esi+2Ch],ax
  00485fee  896e38                mov       [esi+38h],ebp
  00485ff1  66c746122003          mov       word [esi+12h],320h
  00485ff7  66c746144000          mov       word [esi+14h],40h
  00485ffd  c6460400              mov       byte [esi+4],0
  00486001  8b0dcc975e00          mov       ecx,[5E97CCh]
  00486007  6a07                  push      7
  00486009  6a01                  push      1
  0048600b  8b91d0000000          mov       edx,[ecx+0D0h]
  00486011  33c9                  xor       ecx,ecx
  00486013  8b02                  mov       eax,[edx]
  00486015  8a4805                mov       cl,[eax+5]
  00486018  81c1c0000000          add       ecx,0C0h
  0048601e  51                    push      ecx
  0048601f  56                    push      esi
  00486020  e8bb4affff            call      0047AAE0h
  00486025  83c410                add       esp,10h
  00486028  66837e1014            cmp       word [esi+10h],14h
  0048602d  7551                  jne       short 00486080h
  0048602f  6a07                  push      7
  00486031  6a01                  push      1
  00486033  68be000000            push      0BEh
  00486038  68b01b5500            push      551BB0h
  0048603d  57                    push      edi
  0048603e  56                    push      esi
  0048603f  896e50                mov       [esi+50h],ebp
  00486042  896e54                mov       [esi+54h],ebp
  00486045  896e58                mov       [esi+58h],ebp
  00486048  66c746121f00          mov       word [esi+12h],1Fh
  0048604e  896e28                mov       [esi+28h],ebp
  00486051  66c746140040          mov       word [esi+14h],4000h
  00486057  66c746160200          mov       word [esi+16h],2
  0048605d  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  00486064  66c7461a0100          mov       word [esi+1Ah],1
  0048606a  e8d14bffff            call      0047AC40h
  0048606f  83c418                add       esp,18h
  00486072  57                    push      edi
  00486073  68a0275500            push      5527A0h
  00486078  e8637b0b00            call      0053DBE0h
  0048607d  83c408                add       esp,8
  00486080  66837e1022            cmp       word [esi+10h],22h
  00486085  7569                  jne       short 004860F0h
  00486087  6a07                  push      7
  00486089  6a01                  push      1
  0048608b  68be000000            push      0BEh
  00486090  68b01b5500            push      551BB0h
  00486095  bbf0f0f000            mov       ebx,0F0F0F0h
  0048609a  57                    push      edi
  0048609b  56                    push      esi
  0048609c  c74628fc3f0000        mov       dword [esi+28h],3FFCh
  004860a3  66896e14              mov       [esi+14h],bp
  004860a7  66c746160300          mov       word [esi+16h],3
  004860ad  895e38                mov       [esi+38h],ebx
  004860b0  66c7461a0100          mov       word [esi+1Ah],1
  004860b6  e8854bffff            call      0047AC40h
  004860bb  83c418                add       esp,18h
  004860be  c705a0935e0040004000  mov       dword [5E93A0h],400040h
  004860c8  66c705a4935e00ff00    mov       word [5E93A4h],0FFh
  004860d1  895e38                mov       [esi+38h],ebx
  004860d4  57                    push      edi
  004860d5  68b8275500            push      5527B8h
  004860da  896e28                mov       [esi+28h],ebp
  004860dd  66c7462c4006          mov       word [esi+2Ch],640h
  004860e3  e8f87a0b00            call      0053DBE0h
  004860e8  83c408                add       esp,8
  004860eb  66837e1022            cmp       word [esi+10h],22h
  004860f0  0f8e2b010000          jle       near 00486221h
  004860f6  8b466c                mov       eax,[esi+6Ch]
  004860f9  8b5e44                mov       ebx,[esi+44h]
  004860fc  8b4e20                mov       ecx,[esi+20h]
  004860ff  8b5624                mov       edx,[esi+24h]
  00486102  8b686c                mov       ebp,[eax+6Ch]
  00486105  894674                mov       [esi+74h],eax
  00486108  8b07                  mov       eax,[edi]
  0048610a  894c2414              mov       [esp+14h],ecx
  0048610e  89442410              mov       [esp+10h],eax
  00486112  03c3                  add       eax,ebx
  00486114  8907                  mov       [edi],eax
  00486116  8b7e48                mov       edi,[esi+48h]
  00486119  03f9                  add       edi,ecx
  0048611b  8b4e4c                mov       ecx,[esi+4Ch]
  0048611e  03ca                  add       ecx,edx
  00486120  896e78                mov       [esi+78h],ebp
  00486123  897e20                mov       [esi+20h],edi
  00486126  894e24                mov       [esi+24h],ecx
  00486129  8b5d1c                mov       ebx,[ebp+1Ch]
  0048612c  55                    push      ebp
  0048612d  2bd8                  sub       ebx,eax
  0048612f  8954241c              mov       [esp+1Ch],edx
  00486133  c1fb0c                sar       ebx,0Ch
  00486136  e8852a0b00            call      00538BC0h
  0048613b  8bf8                  mov       edi,eax
  0048613d  8b4520                mov       eax,[ebp+20h]
  00486140  8b6d24                mov       ebp,[ebp+24h]
  00486143  83e7fe                and       edi,0FFFFFFFEh
  00486146  8b5620                mov       edx,[esi+20h]
  00486149  83c404                add       esp,4
  0048614c  c1e70b                shl       edi,0Bh
  0048614f  03f8                  add       edi,eax
  00486151  8b4624                mov       eax,[esi+24h]
  00486154  2be8                  sub       ebp,eax
  00486156  2bfa                  sub       edi,edx
  00486158  c1fd0c                sar       ebp,0Ch
  0048615b  8bd3                  mov       edx,ebx
  0048615d  8bc5                  mov       eax,ebp
  0048615f  0fafd3                imul      edx,ebx
  00486162  0fafc5                imul      eax,ebp
  00486165  03d0                  add       edx,eax
  00486167  52                    push      edx
  00486168  c1ff0c                sar       edi,0Ch
  0048616b  e8a0f80b00            call      00545A10h
  00486170  55                    push      ebp
  00486171  53                    push      ebx
  00486172  89442428              mov       [esp+28h],eax
  00486176  e8a7f80b00            call      00545A22h
  0048617b  8bd8                  mov       ebx,eax
  0048617d  e8be0f0c00            call      00547140h
  00486182  8b542420              mov       edx,[esp+20h]
  00486186  25ff030000            and       eax,3FFh
  0048618b  52                    push      edx
  0048618c  57                    push      edi
  0048618d  8d8c03000a0000        lea       ecx,[ebx+eax+0A00h]
  00486194  66894e32              mov       [esi+32h],cx
  00486198  e885f80b00            call      00545A22h
  0048619d  8bf8                  mov       edi,eax
  0048619f  e89c0f0c00            call      00547140h
  004861a4  0fbf4e32              movsx     ecx,word [esi+32h]
  004861a8  25ff030000            and       eax,3FFh
  004861ad  6a64                  push      64h
  004861af  2d00020000            sub       eax,200h
  004861b4  81e900040000          sub       ecx,400h
  004861ba  2bc7                  sub       eax,edi
  004861bc  8d5650                lea       edx,[esi+50h]
  004861bf  66894634              mov       [esi+34h],ax
  004861c3  0fbfc0                movsx     eax,ax
  004861c6  50                    push      eax
  004861c7  51                    push      ecx
  004861c8  52                    push      edx
  004861c9  e802c90a00            call      00532AD0h
  004861ce  c646043f              mov       byte [esi+4],3Fh
  004861d2  a1cc975e00            mov       eax,[5E97CCh]
  004861d7  83c410                add       esp,10h
  004861da  8b88d0000000          mov       ecx,[eax+0D0h]
  004861e0  33c0                  xor       eax,eax
  004861e2  6a07                  push      7
  004861e4  6a01                  push      1
  004861e6  8b11                  mov       edx,[ecx]
  004861e8  8a4206                mov       al,[edx+6]
  004861eb  05c0000000            add       eax,0C0h
  004861f0  50                    push      eax
  004861f1  56                    push      esi
  004861f2  e8e948ffff            call      0047AAE0h
  004861f7  8b4c2420              mov       ecx,[esp+20h]
  004861fb  8b542424              mov       edx,[esp+24h]
  004861ff  8d461c                lea       eax,[esi+1Ch]
  00486202  83c410                add       esp,10h
  00486205  895620                mov       [esi+20h],edx
  00486208  8908                  mov       [eax],ecx
  0048620a  8b4c2418              mov       ecx,[esp+18h]
  0048620e  50                    push      eax
  0048620f  68f0275500            push      5527F0h
  00486214  894e24                mov       [esi+24h],ecx
  00486217  e8c4790b00            call      0053DBE0h
  0048621c  83c408                add       esp,8
  0048621f  33ed                  xor       ebp,ebp
  00486221  66ff4610              inc       word [esi+10h]
  00486225  66837e1030            cmp       word [esi+10h],30h
  0048622a  7c02                  jl        short 0048622Eh
  0048622c  892e                  mov       [esi],ebp
  0048622e  5f                    pop       edi
  0048622f  5e                    pop       esi
  00486230  5d                    pop       ebp
  00486231  5b                    pop       ebx
  00486232  83c40c                add       esp,0Ch
  00486235  c3                    ret
