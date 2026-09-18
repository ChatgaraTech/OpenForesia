C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00461c60  section .text  offset fichier 0x61060  401 octets

  00461c60  53                    push      ebx
  00461c61  56                    push      esi
  00461c62  8b74240c              mov       esi,[esp+0Ch]
  00461c66  57                    push      edi
  00461c67  56                    push      esi
  00461c68  e853f3ffff            call      00460FC0h
  00461c6d  33db                  xor       ebx,ebx
  00461c6f  83c404                add       esp,4
  00461c72  66395e10              cmp       [esi+10h],bx
  00461c76  753f                  jne       short 00461CB7h
  00461c78  8b4620                mov       eax,[esi+20h]
  00461c7b  66c746100100          mov       word [esi+10h],1
  00461c81  c1f80c                sar       eax,0Ch
  00461c84  66c746120a00          mov       word [esi+12h],0Ah
  00461c8a  66894614              mov       [esi+14h],ax
  00461c8e  8b0dcc975e00          mov       ecx,[5E97CCh]
  00461c94  53                    push      ebx
  00461c95  53                    push      ebx
  00461c96  6800100000            push      1000h
  00461c9b  8b91a8000000          mov       edx,[ecx+0A8h]
  00461ca1  6800040000            push      400h
  00461ca6  8b02                  mov       eax,[edx]
  00461ca8  660fb64821            movzx     cx,byte [eax+21h]
  00461cad  51                    push      ecx
  00461cae  56                    push      esi
  00461caf  e8cc630000            call      00468080h
  00461cb4  83c418                add       esp,18h

LAB_00461cb7:
  00461cb7  66837e1001            cmp       word [esi+10h],1
  00461cbc  bf02000000            mov       edi,2
  00461cc1  757d                  jne       short 00461D40h
  00461cc3  66017e12              add       [esi+12h],di
  00461cc7  8b4e20                mov       ecx,[esi+20h]
  00461cca  0fbf4612              movsx     eax,word [esi+12h]
  00461cce  8bd0                  mov       edx,eax
  00461cd0  53                    push      ebx
  00461cd1  f7d8                  neg       eax
  00461cd3  c1e00c                shl       eax,0Ch
  00461cd6  c1e20c                shl       edx,0Ch
  00461cd9  50                    push      eax
  00461cda  2bca                  sub       ecx,edx
  00461cdc  53                    push      ebx
  00461cdd  56                    push      esi
  00461cde  894e20                mov       [esi+20h],ecx
  00461ce1  e89a28ffff            call      00454580h
  00461ce6  0fbf4614              movsx     eax,word [esi+14h]
  00461cea  8b4e20                mov       ecx,[esi+20h]
  00461ced  83c410                add       esp,10h
  00461cf0  8d9060ffffff          lea       edx,[eax-0A0h]
  00461cf6  c1e20c                shl       edx,0Ch
  00461cf9  3bca                  cmp       ecx,edx
  00461cfb  7f07                  jg        short 00461D04h
  00461cfd  8b16                  mov       edx,[esi]
  00461cff  80e6df                and       dh,0DFh
  00461d02  8916                  mov       [esi],edx

LAB_00461d04:
  00461d04  05fcfeffff            add       eax,0FFFFFEFCh
  00461d09  c1e00c                shl       eax,0Ch
  00461d0c  3bc8                  cmp       ecx,eax
  00461d0e  7f30                  jg        short 00461D40h
  00461d10  53                    push      ebx
  00461d11  53                    push      ebx
  00461d12  6800080000            push      800h
  00461d17  6853060000            push      653h
  00461d1c  6a0b                  push      0Bh
  00461d1e  56                    push      esi
  00461d1f  66897e10              mov       [esi+10h],di
  00461d23  66895e12              mov       [esi+12h],bx
  00461d27  894620                mov       [esi+20h],eax
  00461d2a  e851630000            call      00468080h
  00461d2f  83c418                add       esp,18h
  00461d32  53                    push      ebx
  00461d33  53                    push      ebx
  00461d34  53                    push      ebx
  00461d35  6a0f                  push      0Fh
  00461d37  56                    push      esi
  00461d38  e803bdffff            call      0045DA40h
  00461d3d  83c414                add       esp,14h

LAB_00461d40:
  00461d40  66397e10              cmp       [esi+10h],di
  00461d44  b803000000            mov       eax,3
  00461d49  7513                  jne       short 00461D5Eh
  00461d4b  66ff4612              inc       word [esi+12h]
  00461d4f  66837e120f            cmp       word [esi+12h],0Fh
  00461d54  7508                  jne       short 00461D5Eh
  00461d56  66894610              mov       [esi+10h],ax
  00461d5a  66895e12              mov       [esi+12h],bx

LAB_00461d5e:
  00461d5e  66394610              cmp       [esi+10h],ax
  00461d62  bf04000000            mov       edi,4
  00461d67  7536                  jne       short 00461D9Fh
  00461d69  8b5620                mov       edx,[esi+20h]
  00461d6c  66ff4612              inc       word [esi+12h]
  00461d70  53                    push      ebx
  00461d71  6800a00000            push      0A000h
  00461d76  81c200a00000          add       edx,0A000h
  00461d7c  53                    push      ebx
  00461d7d  56                    push      esi
  00461d7e  895620                mov       [esi+20h],edx
  00461d81  e8fa27ffff            call      00454580h
  00461d86  0fbf4614              movsx     eax,word [esi+14h]
  00461d8a  8b4e20                mov       ecx,[esi+20h]
  00461d8d  83c410                add       esp,10h
  00461d90  c1e00c                shl       eax,0Ch
  00461d93  3bc8                  cmp       ecx,eax
  00461d95  7c08                  jl        short 00461D9Fh
  00461d97  66897e10              mov       [esi+10h],di
  00461d9b  66895e12              mov       [esi+12h],bx

LAB_00461d9f:
  00461d9f  66397e10              cmp       [esi+10h],di
  00461da3  7548                  jne       short 00461DEDh
  00461da5  66ff4612              inc       word [esi+12h]
  00461da9  66837e120f            cmp       word [esi+12h],0Fh
  00461dae  753d                  jne       short 00461DEDh
  00461db0  0fbf4e14              movsx     ecx,word [esi+14h]
  00461db4  c1e10c                shl       ecx,0Ch
  00461db7  66c746100100          mov       word [esi+10h],1
  00461dbd  66895e12              mov       [esi+12h],bx
  00461dc1  894e20                mov       [esi+20h],ecx
  00461dc4  8b15cc975e00          mov       edx,[5E97CCh]
  00461dca  53                    push      ebx
  00461dcb  53                    push      ebx
  00461dcc  8b82a8000000          mov       eax,[edx+0A8h]
  00461dd2  6800100000            push      1000h
  00461dd7  6800080000            push      800h
  00461ddc  8b08                  mov       ecx,[eax]
  00461dde  660fb65121            movzx     dx,byte [ecx+21h]
  00461de3  52                    push      edx
  00461de4  56                    push      esi
  00461de5  e896620000            call      00468080h
  00461dea  83c418                add       esp,18h

LAB_00461ded:
  00461ded  5f                    pop       edi
  00461dee  5e                    pop       esi
  00461def  5b                    pop       ebx
  00461df0  c3                    ret

6 cible(s) de saut interne : 0x00461cb7, 0x00461d04, 0x00461d40, 0x00461d5e, 0x00461d9f, 0x00461ded
