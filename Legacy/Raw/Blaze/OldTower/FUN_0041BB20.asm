C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041bb20  section .text  offset fichier 0x1af20  368 octets

  0041bb20  81ec9c000000          sub       esp,9Ch
  0041bb26  53                    push      ebx
  0041bb27  55                    push      ebp
  0041bb28  56                    push      esi
  0041bb29  57                    push      edi
  0041bb2a  b926000000            mov       ecx,26h
  0041bb2f  33c0                  xor       eax,eax
  0041bb31  8d7c2414              lea       edi,[esp+14h]
  0041bb35  c744241000000000      mov       dword [esp+10h],0
  0041bb3d  f3ab                  rep stosd
  0041bb3f  8d7c2410              lea       edi,[esp+10h]
  0041bb43  e808f4ffff            call      0041AF50h
  0041bb48  e8f32affff            call      0040E640h
  0041bb4d  8bb424b0000000        mov       esi,[esp+0B0h]
  0041bb54  b8a0ca5e00            mov       eax,5ECAA0h
  0041bb59  b920000000            mov       ecx,20h
  0041bb5e  bb01000000            mov       ebx,1
  0041bb63  bd02000000            mov       ebp,2

LAB_0041bb68:
  0041bb68  38580c                cmp       [eax+0Ch],bl
  0041bb6b  7527                  jne       short 0041BB94h
  0041bb6d  f70000000080          test      dword [eax],80000000h
  0041bb73  741f                  je        short 0041BB94h
  0041bb75  8b501c                mov       edx,[eax+1Ch]
  0041bb78  8bf8                  mov       edi,eax
  0041bb7a  89561c                mov       [esi+1Ch],edx
  0041bb7d  8b5020                mov       edx,[eax+20h]
  0041bb80  895620                mov       [esi+20h],edx
  0041bb83  8b5024                mov       edx,[eax+24h]
  0041bb86  895624                mov       [esi+24h],edx
  0041bb89  8b10                  mov       edx,[eax]
  0041bb8b  80ce10                or        dh,10h
  0041bb8e  66896818              mov       [eax+18h],bp
  0041bb92  8910                  mov       [eax],edx

LAB_0041bb94:
  0041bb94  059c000000            add       eax,9Ch
  0041bb99  49                    dec       ecx
  0041bb9a  75cc                  jne       short 0041BB68h
  0041bb9c  33c0                  xor       eax,eax
  0041bb9e  66394610              cmp       [esi+10h],ax
  0041bba2  751a                  jne       short 0041BBBEh
  0041bba4  66895e10              mov       [esi+10h],bx
  0041bba8  66894616              mov       [esi+16h],ax
  0041bbac  6689471a              mov       [edi+1Ah],ax
  0041bbb0  88470a                mov       [edi+0Ah],al
  0041bbb3  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0041bbba  66895f36              mov       [edi+36h],bx

LAB_0041bbbe:
  0041bbbe  66394616              cmp       [esi+16h],ax
  0041bbc2  7519                  jne       short 0041BBDDh
  0041bbc4  6800080000            push      800h
  0041bbc9  6800080000            push      800h
  0041bbce  68dc050000            push      5DCh
  0041bbd3  6a20                  push      20h
  0041bbd5  e8c6121200            call      0053CEA0h
  0041bbda  83c410                add       esp,10h

LAB_0041bbdd:
  0041bbdd  66395e10              cmp       [esi+10h],bx
  0041bbe1  7541                  jne       short 0041BC24h
  0041bbe3  66837e1600            cmp       word [esi+16h],0
  0041bbe8  753a                  jne       short 0041BC24h
  0041bbea  6aff                  push      0FFFFFFFFh
  0041bbec  6a00                  push      0
  0041bbee  68c2000000            push      0C2h
  0041bbf3  56                    push      esi
  0041bbf4  66c746143900          mov       word [esi+14h],39h
  0041bbfa  e8e1ee0500            call      0047AAE0h
  0041bbff  83c410                add       esp,10h
  0041bc02  66c746141c00          mov       word [esi+14h],1Ch
  0041bc08  6aff                  push      0FFFFFFFFh
  0041bc0a  6a00                  push      0
  0041bc0c  68c2000000            push      0C2h
  0041bc11  56                    push      esi
  0041bc12  e8c9ee0500            call      0047AAE0h
  0041bc17  83c410                add       esp,10h
  0041bc1a  66896e10              mov       [esi+10h],bp
  0041bc1e  66c746120000          mov       word [esi+12h],0

LAB_0041bc24:
  0041bc24  66ff4616              inc       word [esi+16h]
  0041bc28  66396e10              cmp       [esi+10h],bp
  0041bc2c  7553                  jne       short 0041BC81h
  0041bc2e  66ff4612              inc       word [esi+12h]
  0041bc32  66837e1212            cmp       word [esi+12h],12h
  0041bc37  7c07                  jl        short 0041BC40h
  0041bc39  8b07                  mov       eax,[edi]
  0041bc3b  80cc40                or        ah,40h
  0041bc3e  8907                  mov       [edi],eax

LAB_0041bc40:
  0041bc40  66837e1232            cmp       word [esi+12h],32h
  0041bc45  753a                  jne       short 0041BC81h
  0041bc47  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041bc4d  e85e7b0e00            call      005037B0h
  0041bc52  33c0                  xor       eax,eax
  0041bc54  55                    push      ebp
  0041bc55  a038e55d00            mov       al,[5DE538h]
  0041bc5a  8d0c80                lea       ecx,[eax+eax*4]
  0041bc5d  c1e103                shl       ecx,3
  0041bc60  2bc8                  sub       ecx,eax
  0041bc62  a1cc975e00            mov       eax,[5E97CCh]
  0041bc67  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0041bc6e  52                    push      edx
  0041bc6f  50                    push      eax
  0041bc70  e85b810f00            call      00513DD0h
  0041bc75  8b07                  mov       eax,[edi]
  0041bc77  83c40c                add       esp,0Ch
  0041bc7a  0d00400002            or        eax,2004000h
  0041bc7f  8907                  mov       [edi],eax

LAB_0041bc81:
  0041bc81  5f                    pop       edi
  0041bc82  5e                    pop       esi
  0041bc83  5d                    pop       ebp
  0041bc84  5b                    pop       ebx
  0041bc85  81c49c000000          add       esp,9Ch
  0041bc8b  c3                    ret
  0041bc8c  90                    nop
  0041bc8d  90                    nop
  0041bc8e  90                    nop
  0041bc8f  90                    nop

7 cible(s) de saut interne : 0x0041bb68, 0x0041bb94, 0x0041bbbe, 0x0041bbdd, 0x0041bc24, 0x0041bc40, 0x0041bc81
