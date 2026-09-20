C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00471a10  section .text  offset fichier 0x70e10  960 octets

  00471a10  a130195700            mov       eax,[571930h]
  00471a15  55                    push      ebp
  00471a16  56                    push      esi
  00471a17  57                    push      edi
  00471a18  85c0                  test      eax,eax
  00471a1a  0f85a3030000          jne       near 00471DC3h
  00471a20  8b3d74195700          mov       edi,[571974h]
  00471a26  8b742410              mov       esi,[esp+10h]
  00471a2a  81ffcc100000          cmp       edi,10CCh
  00471a30  bd01000000            mov       ebp,1
  00471a35  7225                  jb        short 00471A5Ch
  00471a37  81ff94110000          cmp       edi,1194h
  00471a3d  771d                  ja        short 00471A5Ch
  00471a3f  392d3c195700          cmp       [57193Ch],ebp
  00471a45  7515                  jne       short 00471A5Ch
  00471a47  687cfcffff            push      0FFFFFC7Ch
  00471a4c  55                    push      ebp
  00471a4d  56                    push      esi
  00471a4e  e87d030000            call      00471DD0h
  00471a53  8b3d74195700          mov       edi,[571974h]
  00471a59  83c40c                add       esp,0Ch

LAB_00471a5c:
  00471a5c  f6057819570004        test      byte [571978h],4
  00471a63  0f8561010000          jne       near 00471BCAh
  00471a69  81fffe150000          cmp       edi,15FEh
  00471a6f  7225                  jb        short 00471A96h
  00471a71  81ffa8160000          cmp       edi,16A8h
  00471a77  771d                  ja        short 00471A96h
  00471a79  392d3c195700          cmp       [57193Ch],ebp
  00471a7f  7515                  jne       short 00471A96h
  00471a81  68acf4ffff            push      0FFFFF4ACh
  00471a86  55                    push      ebp
  00471a87  56                    push      esi
  00471a88  e843030000            call      00471DD0h
  00471a8d  8b3d74195700          mov       edi,[571974h]
  00471a93  83c40c                add       esp,0Ch

LAB_00471a96:
  00471a96  81fff6220000          cmp       edi,22F6h
  00471a9c  7226                  jb        short 00471AC4h
  00471a9e  81fff0230000          cmp       edi,23F0h
  00471aa4  771e                  ja        short 00471AC4h
  00471aa6  392d3c195700          cmp       [57193Ch],ebp
  00471aac  7516                  jne       short 00471AC4h
  00471aae  686ceeffff            push      0FFFFEE6Ch
  00471ab3  6aff                  push      0FFFFFFFFh
  00471ab5  56                    push      esi
  00471ab6  e815030000            call      00471DD0h
  00471abb  8b3d74195700          mov       edi,[571974h]
  00471ac1  83c40c                add       esp,0Ch

LAB_00471ac4:
  00471ac4  81ff543d0000          cmp       edi,3D54h
  00471aca  7226                  jb        short 00471AF2h
  00471acc  81ff623e0000          cmp       edi,3E62h
  00471ad2  771e                  ja        short 00471AF2h
  00471ad4  392d3c195700          cmp       [57193Ch],ebp
  00471ada  7516                  jne       short 00471AF2h
  00471adc  6804100000            push      1004h
  00471ae1  6aff                  push      0FFFFFFFFh
  00471ae3  56                    push      esi
  00471ae4  e8e7020000            call      00471DD0h
  00471ae9  8b3d74195700          mov       edi,[571974h]
  00471aef  83c40c                add       esp,0Ch

LAB_00471af2:
  00471af2  81fffa4b0000          cmp       edi,4BFAh
  00471af8  721d                  jb        short 00471B17h
  00471afa  33c0                  xor       eax,eax
  00471afc  892d30195700          mov       [571930h],ebp
  00471b02  a374195700            mov       [571974h],eax
  00471b07  66894616              mov       [esi+16h],ax
  00471b0b  66c74610d101          mov       word [esi+10h],1D1h
  00471b11  8b3d74195700          mov       edi,[571974h]

LAB_00471b17:
  00471b17  668b4616              mov       ax,[esi+16h]
  00471b1b  663d1800              cmp       ax,18h
  00471b1f  7424                  je        short 00471B45h
  00471b21  663d2400              cmp       ax,24h
  00471b25  741e                  je        short 00471B45h
  00471b27  663d2c00              cmp       ax,2Ch
  00471b2b  7418                  je        short 00471B45h
  00471b2d  663d2d00              cmp       ax,2Dh
  00471b31  7412                  je        short 00471B45h
  00471b33  663d2f00              cmp       ax,2Fh
  00471b37  740c                  je        short 00471B45h
  00471b39  663d3600              cmp       ax,36h
  00471b3d  7c31                  jl        short 00471B70h
  00471b3f  663d3800              cmp       ax,38h
  00471b43  7f2b                  jg        short 00471B70h

LAB_00471b45:
  00471b45  0fbf4e10              movsx     ecx,word [esi+10h]
  00471b49  8bc1                  mov       eax,ecx
  00471b4b  99                    cdq
  00471b4c  2bc2                  sub       eax,edx
  00471b4e  d1f8                  sar       eax,1
  00471b50  d1e0                  shl       eax,1
  00471b52  3bc8                  cmp       ecx,eax
  00471b54  751a                  jne       short 00471B70h
  00471b56  a120195700            mov       eax,[571920h]
  00471b5b  40                    inc       eax
  00471b5c  83f832                cmp       eax,32h
  00471b5f  a320195700            mov       [571920h],eax
  00471b64  7c0a                  jl        short 00471B70h
  00471b66  c7052019570032000000  mov       dword [571920h],32h

LAB_00471b70:
  00471b70  668b4616              mov       ax,[esi+16h]
  00471b74  663d1f00              cmp       ax,1Fh
  00471b78  7c06                  jl        short 00471B80h
  00471b7a  663d2200              cmp       ax,22h
  00471b7e  7e12                  jle       short 00471B92h

LAB_00471b80:
  00471b80  663d2600              cmp       ax,26h
  00471b84  7c06                  jl        short 00471B8Ch
  00471b86  663d2a00              cmp       ax,2Ah
  00471b8a  7e06                  jle       short 00471B92h

LAB_00471b8c:
  00471b8c  663d3a00              cmp       ax,3Ah
  00471b90  752b                  jne       short 00471BBDh

LAB_00471b92:
  00471b92  0fbf4e10              movsx     ecx,word [esi+10h]
  00471b96  8bc1                  mov       eax,ecx
  00471b98  99                    cdq
  00471b99  2bc2                  sub       eax,edx
  00471b9b  d1f8                  sar       eax,1
  00471b9d  d1e0                  shl       eax,1
  00471b9f  3bc8                  cmp       ecx,eax
  00471ba1  751a                  jne       short 00471BBDh
  00471ba3  a120195700            mov       eax,[571920h]
  00471ba8  48                    dec       eax
  00471ba9  83f805                cmp       eax,5
  00471bac  a320195700            mov       [571920h],eax
  00471bb1  7f0a                  jg        short 00471BBDh
  00471bb3  c7052019570005000000  mov       dword [571920h],5

LAB_00471bbd:
  00471bbd  f6057819570004        test      byte [571978h],4
  00471bc4  0f847e010000          je        near 00471D48h

LAB_00471bca:
  00471bca  81ffae1a0000          cmp       edi,1AAEh
  00471bd0  7225                  jb        short 00471BF7h
  00471bd2  81ff581b0000          cmp       edi,1B58h
  00471bd8  771d                  ja        short 00471BF7h
  00471bda  392d3c195700          cmp       [57193Ch],ebp
  00471be0  7515                  jne       short 00471BF7h
  00471be2  685cf9ffff            push      0FFFFF95Ch
  00471be7  55                    push      ebp
  00471be8  56                    push      esi
  00471be9  e8e2010000            call      00471DD0h
  00471bee  8b3d74195700          mov       edi,[571974h]
  00471bf4  83c40c                add       esp,0Ch

LAB_00471bf7:
  00471bf7  81fff0230000          cmp       edi,23F0h
  00471bfd  7226                  jb        short 00471C25h
  00471bff  81ffb8240000          cmp       edi,24B8h
  00471c05  771e                  ja        short 00471C25h
  00471c07  392d3c195700          cmp       [57193Ch],ebp
  00471c0d  7516                  jne       short 00471C25h
  00471c0f  6854f2ffff            push      0FFFFF254h
  00471c14  6aff                  push      0FFFFFFFFh
  00471c16  56                    push      esi
  00471c17  e8b4010000            call      00471DD0h
  00471c1c  8b3d74195700          mov       edi,[571974h]
  00471c22  83c40c                add       esp,0Ch

LAB_00471c25:
  00471c25  81ff5c2b0000          cmp       edi,2B5Ch
  00471c2b  7226                  jb        short 00471C53h
  00471c2d  81ff882c0000          cmp       edi,2C88h
  00471c33  771e                  ja        short 00471C53h
  00471c35  392d3c195700          cmp       [57193Ch],ebp
  00471c3b  7516                  jne       short 00471C53h
  00471c3d  68f4010000            push      1F4h
  00471c42  6aff                  push      0FFFFFFFFh
  00471c44  56                    push      esi
  00471c45  e886010000            call      00471DD0h
  00471c4a  8b3d74195700          mov       edi,[571974h]
  00471c50  83c40c                add       esp,0Ch

LAB_00471c53:
  00471c53  81ffa2300000          cmp       edi,30A2h
  00471c59  7225                  jb        short 00471C80h
  00471c5b  81ff9c310000          cmp       edi,319Ch
  00471c61  771d                  ja        short 00471C80h
  00471c63  392d3c195700          cmp       [57193Ch],ebp
  00471c69  7515                  jne       short 00471C80h
  00471c6b  6894f8ffff            push      0FFFFF894h
  00471c70  55                    push      ebp
  00471c71  56                    push      esi
  00471c72  e859010000            call      00471DD0h
  00471c77  8b3d74195700          mov       edi,[571974h]
  00471c7d  83c40c                add       esp,0Ch

LAB_00471c80:
  00471c80  81ffe03d0000          cmp       edi,3DE0h
  00471c86  7226                  jb        short 00471CAEh
  00471c88  81ff803e0000          cmp       edi,3E80h
  00471c8e  771e                  ja        short 00471CAEh
  00471c90  392d3c195700          cmp       [57193Ch],ebp
  00471c96  7516                  jne       short 00471CAEh
  00471c98  6804100000            push      1004h
  00471c9d  6aff                  push      0FFFFFFFFh
  00471c9f  56                    push      esi
  00471ca0  e82b010000            call      00471DD0h
  00471ca5  8b3d74195700          mov       edi,[571974h]
  00471cab  83c40c                add       esp,0Ch

LAB_00471cae:
  00471cae  81ff68420000          cmp       edi,4268h
  00471cb4  7217                  jb        short 00471CCDh
  00471cb6  33c0                  xor       eax,eax
  00471cb8  892d30195700          mov       [571930h],ebp
  00471cbe  a374195700            mov       [571974h],eax
  00471cc3  66894616              mov       [esi+16h],ax
  00471cc7  66c74610d101          mov       word [esi+10h],1D1h

LAB_00471ccd:
  00471ccd  668b4616              mov       ax,[esi+16h]
  00471cd1  663d2200              cmp       ax,22h
  00471cd5  740c                  je        short 00471CE3h
  00471cd7  663d2300              cmp       ax,23h
  00471cdb  7406                  je        short 00471CE3h
  00471cdd  663d2400              cmp       ax,24h
  00471ce1  752b                  jne       short 00471D0Eh

LAB_00471ce3:
  00471ce3  0fbf4e10              movsx     ecx,word [esi+10h]
  00471ce7  8bc1                  mov       eax,ecx
  00471ce9  99                    cdq
  00471cea  2bc2                  sub       eax,edx
  00471cec  d1f8                  sar       eax,1
  00471cee  d1e0                  shl       eax,1
  00471cf0  3bc8                  cmp       ecx,eax
  00471cf2  751a                  jne       short 00471D0Eh
  00471cf4  a120195700            mov       eax,[571920h]
  00471cf9  40                    inc       eax
  00471cfa  83f828                cmp       eax,28h
  00471cfd  a320195700            mov       [571920h],eax
  00471d02  7c0a                  jl        short 00471D0Eh
  00471d04  c7052019570028000000  mov       dword [571920h],28h

LAB_00471d0e:
  00471d0e  66837e161c            cmp       word [esi+16h],1Ch
  00471d13  7533                  jne       short 00471D48h
  00471d15  0fbf4e10              movsx     ecx,word [esi+10h]
  00471d19  b856555555            mov       eax,55555556h
  00471d1e  f7e9                  imul      ecx
  00471d20  8bc2                  mov       eax,edx
  00471d22  c1e81f                shr       eax,1Fh
  00471d25  03d0                  add       edx,eax
  00471d27  8d1452                lea       edx,[edx+edx*2]
  00471d2a  3bca                  cmp       ecx,edx
  00471d2c  751a                  jne       short 00471D48h
  00471d2e  a120195700            mov       eax,[571920h]
  00471d33  48                    dec       eax
  00471d34  83f805                cmp       eax,5
  00471d37  a320195700            mov       [571920h],eax
  00471d3c  7f0a                  jg        short 00471D48h
  00471d3e  c7052019570005000000  mov       dword [571920h],5

LAB_00471d48:
  00471d48  668b4616              mov       ax,[esi+16h]
  00471d4c  663bc5                cmp       ax,bp
  00471d4f  740c                  je        short 00471D5Dh
  00471d51  663d0500              cmp       ax,5
  00471d55  7406                  je        short 00471D5Dh
  00471d57  663d0900              cmp       ax,9
  00471d5b  752b                  jne       short 00471D88h

LAB_00471d5d:
  00471d5d  0fbf4e10              movsx     ecx,word [esi+10h]
  00471d61  8bc1                  mov       eax,ecx
  00471d63  99                    cdq
  00471d64  2bc2                  sub       eax,edx
  00471d66  d1f8                  sar       eax,1
  00471d68  d1e0                  shl       eax,1
  00471d6a  3bc8                  cmp       ecx,eax
  00471d6c  751a                  jne       short 00471D88h
  00471d6e  a120195700            mov       eax,[571920h]
  00471d73  40                    inc       eax
  00471d74  83f832                cmp       eax,32h
  00471d77  a320195700            mov       [571920h],eax
  00471d7c  7c0a                  jl        short 00471D88h
  00471d7e  c7052019570032000000  mov       dword [571920h],32h

LAB_00471d88:
  00471d88  668b4616              mov       ax,[esi+16h]
  00471d8c  663d0300              cmp       ax,3
  00471d90  7406                  je        short 00471D98h
  00471d92  663d0700              cmp       ax,7
  00471d96  752b                  jne       short 00471DC3h

LAB_00471d98:
  00471d98  0fbf4e10              movsx     ecx,word [esi+10h]
  00471d9c  8bc1                  mov       eax,ecx
  00471d9e  99                    cdq
  00471d9f  2bc2                  sub       eax,edx
  00471da1  d1f8                  sar       eax,1
  00471da3  d1e0                  shl       eax,1
  00471da5  3bc8                  cmp       ecx,eax
  00471da7  751a                  jne       short 00471DC3h
  00471da9  a120195700            mov       eax,[571920h]
  00471dae  48                    dec       eax
  00471daf  83f805                cmp       eax,5
  00471db2  a320195700            mov       [571920h],eax
  00471db7  7f0a                  jg        short 00471DC3h
  00471db9  c7052019570005000000  mov       dword [571920h],5

LAB_00471dc3:
  00471dc3  5f                    pop       edi
  00471dc4  5e                    pop       esi
  00471dc5  5d                    pop       ebp
  00471dc6  c3                    ret
  00471dc7  90                    nop
  00471dc8  90                    nop
  00471dc9  90                    nop
  00471dca  90                    nop
  00471dcb  90                    nop
  00471dcc  90                    nop
  00471dcd  90                    nop
  00471dce  90                    nop
  00471dcf  90                    nop

25 cible(s) de saut interne : 0x00471a5c, 0x00471a96, 0x00471ac4, 0x00471af2, 0x00471b17, 0x00471b45, 0x00471b70, 0x00471b80, 0x00471b8c, 0x00471b92, 0x00471bbd, 0x00471bca, 0x00471bf7, 0x00471c25, 0x00471c53, 0x00471c80, 0x00471cae, 0x00471ccd, 0x00471ce3, 0x00471d0e, 0x00471d48, 0x00471d5d, 0x00471d88, 0x00471d98, 0x00471dc3
