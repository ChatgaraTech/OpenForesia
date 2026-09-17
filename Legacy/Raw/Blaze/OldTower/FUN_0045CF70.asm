C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045cf70  section .text  offset fichier 0x5c370  576 octets

  0045cf70  83ec18                sub       esp,18h
  0045cf73  53                    push      ebx
  0045cf74  55                    push      ebp
  0045cf75  56                    push      esi
  0045cf76  8b742428              mov       esi,[esp+28h]
  0045cf7a  57                    push      edi
  0045cf7b  56                    push      esi
  0045cf7c  e83f400000            call      00460FC0h
  0045cf81  33ff                  xor       edi,edi
  0045cf83  83c404                add       esp,4
  0045cf86  66397e10              cmp       [esi+10h],di
  0045cf8a  7512                  jne       short 0045CF9Eh
  0045cf8c  668b4614              mov       ax,[esi+14h]
  0045cf90  66c746100100          mov       word [esi+10h],1
  0045cf96  66894612              mov       [esi+12h],ax
  0045cf9a  66897e14              mov       [esi+14h],di

LAB_0045cf9e:
  0045cf9e  66ff4614              inc       word [esi+14h]
  0045cfa2  e899a10e00            call      00547140h
  0045cfa7  668b5e16              mov       bx,[esi+16h]
  0045cfab  b932000000            mov       ecx,32h
  0045cfb0  99                    cdq
  0045cfb1  f7f9                  idiv      ecx
  0045cfb3  668b4e14              mov       cx,[esi+14h]
  0045cfb7  0fbfc3                movsx     eax,bx
  0045cfba  0fbfe9                movsx     ebp,cx
  0045cfbd  8d0440                lea       eax,[eax+eax*2]
  0045cfc0  3be8                  cmp       ebp,eax
  0045cfc2  7d13                  jge       short 0045CFD7h
  0045cfc4  663bd7                cmp       dx,di
  0045cfc7  0f85a5010000          jne       near 0045D172h
  0045cfcd  6683f90a              cmp       cx,0Ah
  0045cfd1  0f8e9b010000          jle       near 0045D172h

LAB_0045cfd7:
  0045cfd7  663bdf                cmp       bx,di
  0045cfda  0f8492010000          je        near 0045D172h
  0045cfe0  8b4e1c                mov       ecx,[esi+1Ch]
  0045cfe3  8b5620                mov       edx,[esi+20h]
  0045cfe6  8b4624                mov       eax,[esi+24h]
  0045cfe9  894c2410              mov       [esp+10h],ecx
  0045cfed  8b4e50                mov       ecx,[esi+50h]
  0045cff0  89542414              mov       [esp+14h],edx
  0045cff4  8b5654                mov       edx,[esi+54h]
  0045cff7  894c241c              mov       [esp+1Ch],ecx
  0045cffb  668b4e32              mov       cx,[esi+32h]
  0045cfff  668146328000          add       word [esi+32h],80h
  0045d005  89542420              mov       [esp+20h],edx
  0045d009  89442418              mov       [esp+18h],eax
  0045d00d  0fbf5632              movsx     edx,word [esi+32h]
  0045d011  8b4658                mov       eax,[esi+58h]
  0045d014  52                    push      edx
  0045d015  66897e14              mov       [esi+14h],di
  0045d019  89442428              mov       [esp+28h],eax
  0045d01d  66894c2430            mov       [esp+30h],cx
  0045d022  e8f5890e00            call      00545A1Ch
  0045d027  8b4e1c                mov       ecx,[esi+1Ch]
  0045d02a  8d0440                lea       eax,[eax+eax*2]
  0045d02d  8d0480                lea       eax,[eax+eax*4]
  0045d030  c1e003                shl       eax,3
  0045d033  03c8                  add       ecx,eax
  0045d035  894e1c                mov       [esi+1Ch],ecx
  0045d038  0fbf4e32              movsx     ecx,word [esi+32h]
  0045d03c  51                    push      ecx
  0045d03d  e8d4890e00            call      00545A16h
  0045d042  8b4e24                mov       ecx,[esi+24h]
  0045d045  8d0440                lea       eax,[eax+eax*2]
  0045d048  8d1480                lea       edx,[eax+eax*4]
  0045d04b  8b4620                mov       eax,[esi+20h]
  0045d04e  c1e203                shl       edx,3
  0045d051  03ca                  add       ecx,edx
  0045d053  0500800c00            add       eax,0C8000h
  0045d058  894e24                mov       [esi+24h],ecx
  0045d05b  894620                mov       [esi+20h],eax

LAB_0045d05e:
  0045d05e  668b5e14              mov       bx,[esi+14h]
  0045d062  668b6e16              mov       bp,[esi+16h]
  0045d066  6a00                  push      0
  0045d068  6a00                  push      0
  0045d06a  6aff                  push      0FFFFFFFFh
  0045d06c  6a00                  push      0
  0045d06e  6a39                  push      39h
  0045d070  56                    push      esi
  0045d071  66897e14              mov       [esi+14h],di
  0045d075  66c746160a00          mov       word [esi+16h],0Ah
  0045d07b  e890b00000            call      00468110h
  0045d080  83c418                add       esp,18h
  0045d083  47                    inc       edi
  0045d084  6683ff03              cmp       di,3
  0045d088  66895e14              mov       [esi+14h],bx
  0045d08c  66896e16              mov       [esi+16h],bp
  0045d090  7ecc                  jle       short 0045D05Eh
  0045d092  8b442410              mov       eax,[esp+10h]
  0045d096  8b4c2414              mov       ecx,[esp+14h]
  0045d09a  89461c                mov       [esi+1Ch],eax
  0045d09d  8b44241c              mov       eax,[esp+1Ch]
  0045d0a1  8b542418              mov       edx,[esp+18h]
  0045d0a5  894650                mov       [esi+50h],eax
  0045d0a8  8b44242c              mov       eax,[esp+2Ch]
  0045d0ac  894e20                mov       [esi+20h],ecx
  0045d0af  8b4c2420              mov       ecx,[esp+20h]
  0045d0b3  0580080000            add       eax,880h
  0045d0b8  894e54                mov       [esi+54h],ecx
  0045d0bb  895624                mov       [esi+24h],edx
  0045d0be  8b542424              mov       edx,[esp+24h]
  0045d0c2  66894632              mov       [esi+32h],ax
  0045d0c6  0fbfc8                movsx     ecx,ax
  0045d0c9  51                    push      ecx
  0045d0ca  895658                mov       [esi+58h],edx
  0045d0cd  e84a890e00            call      00545A1Ch
  0045d0d2  8b6e1c                mov       ebp,[esi+1Ch]
  0045d0d5  8d0440                lea       eax,[eax+eax*2]
  0045d0d8  8d1480                lea       edx,[eax+eax*4]
  0045d0db  0fbf4632              movsx     eax,word [esi+32h]
  0045d0df  c1e203                shl       edx,3
  0045d0e2  03ea                  add       ebp,edx
  0045d0e4  50                    push      eax
  0045d0e5  896e1c                mov       [esi+1Ch],ebp
  0045d0e8  e829890e00            call      00545A16h
  0045d0ed  8b5624                mov       edx,[esi+24h]
  0045d0f0  8d0440                lea       eax,[eax+eax*2]
  0045d0f3  8d0c80                lea       ecx,[eax+eax*4]
  0045d0f6  8b4620                mov       eax,[esi+20h]
  0045d0f9  c1e103                shl       ecx,3
  0045d0fc  03d1                  add       edx,ecx
  0045d0fe  0500800c00            add       eax,0C8000h
  0045d103  895624                mov       [esi+24h],edx
  0045d106  894620                mov       [esi+20h],eax
  0045d109  33ff                  xor       edi,edi

LAB_0045d10b:
  0045d10b  668b5e14              mov       bx,[esi+14h]
  0045d10f  668b6e16              mov       bp,[esi+16h]
  0045d113  6a00                  push      0
  0045d115  6a00                  push      0
  0045d117  6aff                  push      0FFFFFFFFh
  0045d119  6a00                  push      0
  0045d11b  6a39                  push      39h
  0045d11d  56                    push      esi
  0045d11e  66897e14              mov       [esi+14h],di
  0045d122  66c746160a00          mov       word [esi+16h],0Ah
  0045d128  e8e3af0000            call      00468110h
  0045d12d  83c418                add       esp,18h
  0045d130  47                    inc       edi
  0045d131  6683ff03              cmp       di,3
  0045d135  66895e14              mov       [esi+14h],bx
  0045d139  66896e16              mov       [esi+16h],bp
  0045d13d  7ecc                  jle       short 0045D10Bh
  0045d13f  668b54242c            mov       dx,[esp+2Ch]
  0045d144  8b442410              mov       eax,[esp+10h]
  0045d148  8b4c2414              mov       ecx,[esp+14h]
  0045d14c  66895632              mov       [esi+32h],dx
  0045d150  8b542418              mov       edx,[esp+18h]
  0045d154  89461c                mov       [esi+1Ch],eax
  0045d157  8b44241c              mov       eax,[esp+1Ch]
  0045d15b  894e20                mov       [esi+20h],ecx
  0045d15e  8b4c2420              mov       ecx,[esp+20h]
  0045d162  895624                mov       [esi+24h],edx
  0045d165  8b542424              mov       edx,[esp+24h]
  0045d169  894650                mov       [esi+50h],eax
  0045d16c  894e54                mov       [esi+54h],ecx
  0045d16f  895658                mov       [esi+58h],edx

LAB_0045d172:
  0045d172  6683463244            add       word [esi+32h],44h
  0045d177  668b4632              mov       ax,[esi+32h]
  0045d17b  663d0010              cmp       ax,1000h
  0045d17f  7e09                  jle       short 0045D18Ah
  0045d181  0500f0ffff            add       eax,0FFFFF000h
  0045d186  66894632              mov       [esi+32h],ax

LAB_0045d18a:
  0045d18a  0fbf4632              movsx     eax,word [esi+32h]
  0045d18e  50                    push      eax
  0045d18f  e888880e00            call      00545A1Ch
  0045d194  8a4e12                mov       cl,[esi+12h]
  0045d197  5f                    pop       edi
  0045d198  d3e0                  shl       eax,cl
  0045d19a  8b4e20                mov       ecx,[esi+20h]
  0045d19d  03c8                  add       ecx,eax
  0045d19f  894e20                mov       [esi+20h],ecx
  0045d1a2  5e                    pop       esi
  0045d1a3  5d                    pop       ebp
  0045d1a4  5b                    pop       ebx
  0045d1a5  83c418                add       esp,18h
  0045d1a8  c3                    ret
  0045d1a9  90                    nop
  0045d1aa  90                    nop
  0045d1ab  90                    nop
  0045d1ac  90                    nop
  0045d1ad  90                    nop
  0045d1ae  90                    nop
  0045d1af  90                    nop

6 cible(s) de saut interne : 0x0045cf9e, 0x0045cfd7, 0x0045d05e, 0x0045d10b, 0x0045d172, 0x0045d18a
