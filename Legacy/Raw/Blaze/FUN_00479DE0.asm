Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479de0  section .text  offset fichier 0x791e0  400 octets

  00479de0  51                    push      ecx
  00479de1  53                    push      ebx
  00479de2  55                    push      ebp
  00479de3  56                    push      esi
  00479de4  8b742414              mov       esi,[esp+14h]
  00479de8  57                    push      edi
  00479de9  8b06                  mov       eax,[esi]
  00479deb  a900000002            test      eax,2000000h
  00479df0  740c                  je        short 00479DFEh
  00479df2  c70600000000          mov       dword [esi],0
  00479df8  5f                    pop       edi
  00479df9  5e                    pop       esi
  00479dfa  5d                    pop       ebp
  00479dfb  5b                    pop       ebx
  00479dfc  59                    pop       ecx
  00479dfd  c3                    ret

LAB_00479dfe:
  00479dfe  668b4e10              mov       cx,[esi+10h]
  00479e02  33ed                  xor       ebp,ebp
  00479e04  663bcd                cmp       cx,bp
  00479e07  0f85af000000          jne       near 00479EBCh
  00479e0d  8b5674                mov       edx,[esi+74h]
  00479e10  8b461c                mov       eax,[esi+1Ch]
  00479e13  8b4e24                mov       ecx,[esi+24h]
  00479e16  52                    push      edx
  00479e17  0fbf7e32              movsx     edi,word [esi+32h]
  00479e1b  66c746100100          mov       word [esi+10h],1
  00479e21  8944241c              mov       [esp+1Ch],eax
  00479e25  894c2414              mov       [esp+14h],ecx
  00479e29  e892ed0b00            call      00538BC0h
  00479e2e  8b5660                mov       edx,[esi+60h]
  00479e31  8bd8                  mov       ebx,eax
  00479e33  83c404                add       esp,4
  00479e36  66896e12              mov       [esi+12h],bp
  00479e3a  8d431e                lea       eax,[ebx+1Eh]
  00479e3d  c6460439              mov       byte [esi+4],39h
  00479e41  c1e00c                shl       eax,0Ch
  00479e44  03c2                  add       eax,edx
  00479e46  bd03000000            mov       ebp,3
  00479e4b  d1fb                  sar       ebx,1
  00479e4d  668146320004          add       word [esi+32h],400h
  00479e53  894620                mov       [esi+20h],eax

LAB_00479e56:
  00479e56  57                    push      edi
  00479e57  e8c0bb0c00            call      00545A1Ch
  00479e5c  0fafc3                imul      eax,ebx
  00479e5f  8b4c2418              mov       ecx,[esp+18h]
  00479e63  57                    push      edi
  00479e64  03c1                  add       eax,ecx
  00479e66  89461c                mov       [esi+1Ch],eax
  00479e69  e8a8bb0c00            call      00545A16h
  00479e6e  0fafc3                imul      eax,ebx
  00479e71  8b542410              mov       edx,[esp+10h]
  00479e75  33c9                  xor       ecx,ecx
  00479e77  8a4e0b                mov       cl,[esi+0Bh]
  00479e7a  6aff                  push      0FFFFFFFFh
  00479e7c  51                    push      ecx
  00479e7d  03c2                  add       eax,edx
  00479e7f  6a7f                  push      7Fh
  00479e81  56                    push      esi
  00479e82  894624                mov       [esi+24h],eax
  00479e85  e8560c0000            call      0047AAE0h
  00479e8a  668146125505          add       word [esi+12h],555h
  00479e90  83c410                add       esp,10h
  00479e93  81c755050000          add       edi,555h
  00479e99  4d                    dec       ebp
  00479e9a  75ba                  jne       short 00479E56h
  00479e9c  8d561c                lea       edx,[esi+1Ch]
  00479e9f  52                    push      edx
  00479ea0  6868195500            push      551968h
  00479ea5  e8363d0c00            call      0053DBE0h
  00479eaa  8b7674                mov       esi,[esi+74h]
  00479ead  83c408                add       esp,8
  00479eb0  8b06                  mov       eax,[esi]
  00479eb2  0c80                  or        al,80h
  00479eb4  8906                  mov       [esi],eax
  00479eb6  5f                    pop       edi
  00479eb7  5e                    pop       esi
  00479eb8  5d                    pop       ebp
  00479eb9  5b                    pop       ebx
  00479eba  59                    pop       ecx
  00479ebb  c3                    ret

LAB_00479ebc:
  00479ebc  a802                  test      al,2
  00479ebe  0f849e000000          je        near 00479F62h
  00479ec4  a900008000            test      eax,800000h
  00479ec9  7477                  je        short 00479F42h
  00479ecb  8b4e74                mov       ecx,[esi+74h]
  00479ece  8b3db4876000          mov       edi,[6087B4h]
  00479ed4  33d2                  xor       edx,edx
  00479ed6  8a5108                mov       dl,[ecx+8]
  00479ed9  8b1f                  mov       ebx,[edi]
  00479edb  8d4a78                lea       ecx,[edx+78h]
  00479ede  8d3cd2                lea       edi,[edx+edx*8]
  00479ee1  c1e10d                shl       ecx,0Dh
  00479ee4  03cb                  add       ecx,ebx
  00479ee6  8d147a                lea       edx,[edx+edi*2]
  00479ee9  33db                  xor       ebx,ebx
  00479eeb  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  00479ef2  c1e304                shl       ebx,4
  00479ef5  668b93204a6000        mov       dx,[ebx+604A20h]
  00479efc  668591c41f0000        test      [ecx+1FC4h],dx
  00479f03  7507                  jne       short 00479F0Ch
  00479f05  0d00000002            or        eax,2000000h
  00479f0a  8906                  mov       [esi],eax

LAB_00479f0c:
  00479f0c  a1f02c5f00            mov       eax,[5F2CF0h]
  00479f11  33d2                  xor       edx,edx
  00479f13  bf05000000            mov       edi,5
  00479f18  f7f7                  div       edi
  00479f1a  85d2                  test      edx,edx
  00479f1c  7531                  jne       short 00479F4Fh
  00479f1e  668b814e010000        mov       ax,[ecx+14Eh]
  00479f25  663d0200              cmp       ax,2
  00479f29  7c0c                  jl        short 00479F37h
  00479f2b  83c0fe                add       eax,0FFFFFFFEh
  00479f2e  6689814e010000        mov       [ecx+14Eh],ax
  00479f35  eb18                  jmp       short 00479F4Fh

LAB_00479f37:
  00479f37  6689a94e010000        mov       [ecx+14Eh],bp
  00479f3e  8b06                  mov       eax,[esi]
  00479f40  eb06                  jmp       short 00479F48h

LAB_00479f42:
  00479f42  6683f978              cmp       cx,78h
  00479f46  7c07                  jl        short 00479F4Fh

LAB_00479f48:
  00479f48  0d00000002            or        eax,2000000h
  00479f4d  8906                  mov       [esi],eax

LAB_00479f4f:
  00479f4f  56                    push      esi
  00479f50  e8bbb1ffff            call      00475110h
  00479f55  83c404                add       esp,4
  00479f58  85c0                  test      eax,eax
  00479f5a  7406                  je        short 00479F62h
  00479f5c  810e00000002          or        dword [esi],2000000h

LAB_00479f62:
  00479f62  5f                    pop       edi
  00479f63  5e                    pop       esi
  00479f64  5d                    pop       ebp
  00479f65  5b                    pop       ebx
  00479f66  59                    pop       ecx
  00479f67  c3                    ret
  00479f68  90                    nop
  00479f69  90                    nop
  00479f6a  90                    nop
  00479f6b  90                    nop
  00479f6c  90                    nop
  00479f6d  90                    nop
  00479f6e  90                    nop
  00479f6f  90                    nop

9 cible(s) de saut interne : 0x00479dfe, 0x00479e56, 0x00479ebc, 0x00479f0c, 0x00479f37, 0x00479f42, 0x00479f48, 0x00479f4f, 0x00479f62
