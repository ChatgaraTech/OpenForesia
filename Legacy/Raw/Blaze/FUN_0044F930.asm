Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044f930  section .text  offset fichier 0x4ed30  477 octets

  0044f930  83ec14                sub       esp,14h
  0044f933  53                    push      ebx
  0044f934  55                    push      ebp
  0044f935  56                    push      esi
  0044f936  33db                  xor       ebx,ebx
  0044f938  57                    push      edi
  0044f939  8b7c2428              mov       edi,[esp+28h]
  0044f93d  53                    push      ebx
  0044f93e  53                    push      ebx
  0044f93f  57                    push      edi
  0044f940  e8fb86ffff            call      00448040h
  0044f945  83c40c                add       esp,0Ch
  0044f948  66395f10              cmp       [edi+10h],bx
  0044f94c  750b                  jne       short 0044F959h
  0044f94e  53                    push      ebx
  0044f94f  53                    push      ebx
  0044f950  57                    push      edi
  0044f951  e85a83ffff            call      00447CB0h
  0044f956  83c40c                add       esp,0Ch

LAB_0044f959:
  0044f959  66395f10              cmp       [edi+10h],bx
  0044f95d  be01000000            mov       esi,1
  0044f962  752c                  jne       short 0044F990h
  0044f964  66897710              mov       [edi+10h],si
  0044f968  66895f16              mov       [edi+16h],bx
  0044f96c  66c70560915e007800    mov       word [5E9160h],78h
  0044f975  66c70562915e008200    mov       word [5E9162h],82h
  0044f97e  66c70564915e00b400    mov       word [5E9164h],0B4h
  0044f987  66c70566915e000200    mov       word [5E9166h],2

LAB_0044f990:
  0044f990  66837f1602            cmp       word [edi+16h],2
  0044f995  7519                  jne       short 0044F9B0h
  0044f997  6800100000            push      1000h
  0044f99c  6800100000            push      1000h
  0044f9a1  6800020000            push      200h
  0044f9a6  6a27                  push      27h
  0044f9a8  e8f3d40e00            call      0053CEA0h
  0044f9ad  83c410                add       esp,10h

LAB_0044f9b0:
  0044f9b0  66397710              cmp       [edi+10h],si
  0044f9b4  0f8508010000          jne       near 0044FAC2h
  0044f9ba  8b6f74                mov       ebp,[edi+74h]
  0044f9bd  bea0ca5e00            mov       esi,5ECAA0h
  0044f9c2  3beb                  cmp       ebp,ebx
  0044f9c4  896c2428              mov       [esp+28h],ebp
  0044f9c8  7509                  jne       short 0044F9D3h
  0044f9ca  bd58e55d00            mov       ebp,5DE558h
  0044f9cf  896c2428              mov       [esp+28h],ebp

LAB_0044f9d3:
  0044f9d3  895c2410              mov       [esp+10h],ebx
  0044f9d7  bba0ca5e00            mov       ebx,5ECAA0h
  0044f9dc  c744241420000000      mov       dword [esp+14h],20h

LAB_0044f9e4:
  0044f9e4  8d44241c              lea       eax,[esp+1Ch]
  0044f9e8  8d4c2420              lea       ecx,[esp+20h]
  0044f9ec  50                    push      eax
  0044f9ed  8d54241c              lea       edx,[esp+1Ch]
  0044f9f1  51                    push      ecx
  0044f9f2  52                    push      edx
  0044f9f3  56                    push      esi
  0044f9f4  e8878f0e00            call      00538980h
  0044f9f9  8b06                  mov       eax,[esi]
  0044f9fb  83c410                add       esp,10h
  0044f9fe  a900000080            test      eax,80000000h
  0044fa03  0f8484000000          je        near 0044FA8Dh
  0044fa09  a900200040            test      eax,40002000h
  0044fa0e  757d                  jne       short 0044FA8Dh
  0044fa10  8b4674                mov       eax,[esi+74h]
  0044fa13  85c0                  test      eax,eax
  0044fa15  7576                  jne       short 0044FA8Dh
  0044fa17  8b442418              mov       eax,[esp+18h]
  0044fa1b  f7401400000003        test      dword [eax+14h],3000000h
  0044fa22  7469                  je        short 0044FA8Dh
  0044fa24  8b461c                mov       eax,[esi+1Ch]
  0044fa27  8b551c                mov       edx,[ebp+1Ch]
  0044fa2a  8b4e20                mov       ecx,[esi+20h]
  0044fa2d  2bc2                  sub       eax,edx
  0044fa2f  8b5520                mov       edx,[ebp+20h]
  0044fa32  2bca                  sub       ecx,edx
  0044fa34  8b5624                mov       edx,[esi+24h]
  0044fa37  2b5524                sub       edx,[ebp+24h]
  0044fa3a  c1f80c                sar       eax,0Ch
  0044fa3d  8be8                  mov       ebp,eax
  0044fa3f  c1f90c                sar       ecx,0Ch
  0044fa42  0fafe8                imul      ebp,eax
  0044fa45  8bc1                  mov       eax,ecx
  0044fa47  c1fa0c                sar       edx,0Ch
  0044fa4a  0fafc1                imul      eax,ecx
  0044fa4d  8bca                  mov       ecx,edx
  0044fa4f  03e8                  add       ebp,eax
  0044fa51  0fafca                imul      ecx,edx
  0044fa54  03e9                  add       ebp,ecx
  0044fa56  55                    push      ebp
  0044fa57  e8b45f0f00            call      00545A10h
  0044fa5c  3ddc050000            cmp       eax,5DCh
  0044fa61  7d26                  jge       short 0044FA89h
  0044fa63  8b576c                mov       edx,[edi+6Ch]
  0044fa66  6aff                  push      0FFFFFFFFh
  0044fa68  6a00                  push      0
  0044fa6a  689e000000            push      9Eh
  0044fa6f  895f78                mov       [edi+78h],ebx
  0044fa72  57                    push      edi
  0044fa73  8bf3                  mov       esi,ebx
  0044fa75  895374                mov       [ebx+74h],edx
  0044fa78  e863b00200            call      0047AAE0h
  0044fa7d  8b442420              mov       eax,[esp+20h]
  0044fa81  83c410                add       esp,10h
  0044fa84  40                    inc       eax
  0044fa85  89442410              mov       [esp+10h],eax

LAB_0044fa89:
  0044fa89  8b6c2428              mov       ebp,[esp+28h]

LAB_0044fa8d:
  0044fa8d  8b442414              mov       eax,[esp+14h]
  0044fa91  81c69c000000          add       esi,9Ch
  0044fa97  81c39c000000          add       ebx,9Ch
  0044fa9d  48                    dec       eax
  0044fa9e  89442414              mov       [esp+14h],eax
  0044faa2  0f853cffffff          jne       near 0044F9E4h
  0044faa8  66837c241000          cmp       word [esp+10h],0
  0044faae  750c                  jne       short 0044FABCh
  0044fab0  66c747106300          mov       word [edi+10h],63h
  0044fab6  66c747164600          mov       word [edi+16h],46h

LAB_0044fabc:
  0044fabc  66c747100200          mov       word [edi+10h],2

LAB_0044fac2:
  0044fac2  66837f1002            cmp       word [edi+10h],2
  0044fac7  750d                  jne       short 0044FAD6h
  0044fac9  66837f165a            cmp       word [edi+16h],5Ah
  0044face  7c06                  jl        short 0044FAD6h
  0044fad0  66c747106300          mov       word [edi+10h],63h

LAB_0044fad6:
  0044fad6  66ff4716              inc       word [edi+16h]
  0044fada  66837f1063            cmp       word [edi+10h],63h
  0044fadf  668b4716              mov       ax,[edi+16h]
  0044fae3  7520                  jne       short 0044FB05h
  0044fae5  663d5a00              cmp       ax,5Ah
  0044fae9  7c0d                  jl        short 0044FAF8h
  0044faeb  6a00                  push      0
  0044faed  6a00                  push      0
  0044faef  57                    push      edi
  0044faf0  e82b85ffff            call      00448020h
  0044faf5  83c40c                add       esp,0Ch

LAB_0044faf8:
  0044faf8  6a00                  push      0
  0044fafa  6a00                  push      0
  0044fafc  57                    push      edi
  0044fafd  e81e85ffff            call      00448020h
  0044fb02  83c40c                add       esp,0Ch

LAB_0044fb05:
  0044fb05  5f                    pop       edi
  0044fb06  5e                    pop       esi
  0044fb07  5d                    pop       ebp
  0044fb08  5b                    pop       ebx
  0044fb09  83c414                add       esp,14h
  0044fb0c  c3                    ret

12 cible(s) de saut interne : 0x0044f959, 0x0044f990, 0x0044f9b0, 0x0044f9d3, 0x0044f9e4, 0x0044fa89, 0x0044fa8d, 0x0044fabc, 0x0044fac2, 0x0044fad6, 0x0044faf8, 0x0044fb05
