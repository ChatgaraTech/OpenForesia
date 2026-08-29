C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00447280  section .text  offset fichier 0x46680  255 octets

  00447280  83ec08                sub       esp,8
  00447283  53                    push      ebx
  00447284  56                    push      esi
  00447285  8b742414              mov       esi,[esp+14h]
  00447289  57                    push      edi
  0044728a  66837e1001            cmp       word [esi+10h],1
  0044728f  7521                  jne       short 004472B2h
  00447291  8d461c                lea       eax,[esi+1Ch]
  00447294  50                    push      eax
  00447295  6800120000            push      1200h
  0044729a  68520b0000            push      0B52h
  0044729f  6a09                  push      9
  004472a1  e87a680f00            call      0053DB20h
  004472a6  8b06                  mov       eax,[esi]
  004472a8  83c410                add       esp,10h
  004472ab  25ffffff7f            and       eax,7FFFFFFFh
  004472b0  8906                  mov       [esi],eax

LAB_004472b2:
  004472b2  66837e1000            cmp       word [esi+10h],0
  004472b7  0f85bb000000          jne       near 00447378h
  004472bd  8b4670                mov       eax,[esi+70h]
  004472c0  85c0                  test      eax,eax
  004472c2  0f84b0000000          je        near 00447378h
  004472c8  8d4c2418              lea       ecx,[esp+18h]
  004472cc  8d54240c              lea       edx,[esp+0Ch]
  004472d0  51                    push      ecx
  004472d1  8d4c2414              lea       ecx,[esp+14h]
  004472d5  52                    push      edx
  004472d6  51                    push      ecx
  004472d7  50                    push      eax
  004472d8  e8a3160f00            call      00538980h
  004472dd  8b4c2428              mov       ecx,[esp+28h]
  004472e1  83c410                add       esp,10h
  004472e4  668b4120              mov       ax,[ecx+20h]
  004472e8  6603411e              add       ax,[ecx+1Eh]
  004472ec  6603411c              add       ax,[ecx+1Ch]
  004472f0  663d5e01              cmp       ax,15Eh
  004472f4  66894614              mov       [esi+14h],ax
  004472f8  7e06                  jle       short 00447300h
  004472fa  66c746145e01          mov       word [esi+14h],15Eh

LAB_00447300:
  00447300  b864000000            mov       eax,64h
  00447305  66394614              cmp       [esi+14h],ax
  00447309  7d04                  jge       short 0044730Fh
  0044730b  66894614              mov       [esi+14h],ax

LAB_0044730f:
  0044730f  8b4620                mov       eax,[esi+20h]
  00447312  6aff                  push      0FFFFFFFFh
  00447314  6a00                  push      0
  00447316  0500a00500            add       eax,5A000h
  0044731b  68a4000000            push      0A4h
  00447320  56                    push      esi
  00447321  66c74614b400          mov       word [esi+14h],0B4h
  00447327  894620                mov       [esi+20h],eax
  0044732a  e8b1370300            call      0047AAE0h
  0044732f  83c410                add       esp,10h
  00447332  8d5e1c                lea       ebx,[esi+1Ch]
  00447335  33ff                  xor       edi,edi

LAB_00447337:
  00447337  6aff                  push      0FFFFFFFFh
  00447339  6a00                  push      0
  0044733b  6898000000            push      98h
  00447340  6a00                  push      0
  00447342  53                    push      ebx
  00447343  56                    push      esi
  00447344  66897e16              mov       [esi+16h],di
  00447348  e8f3380300            call      0047AC40h
  0044734d  81c700020000          add       edi,200h
  00447353  83c418                add       esp,18h
  00447356  6681ff000e            cmp       di,0E00h
  0044735b  7eda                  jle       short 00447337h
  0044735d  53                    push      ebx
  0044735e  6800120000            push      1200h
  00447363  6820030000            push      320h
  00447368  6a05                  push      5
  0044736a  e8b1670f00            call      0053DB20h
  0044736f  83c410                add       esp,10h
  00447372  66c746100100          mov       word [esi+10h],1

LAB_00447378:
  00447378  5f                    pop       edi
  00447379  5e                    pop       esi
  0044737a  5b                    pop       ebx
  0044737b  83c408                add       esp,8
  0044737e  c3                    ret

5 cible(s) de saut interne : 0x004472b2, 0x00447300, 0x0044730f, 0x00447337, 0x00447378
