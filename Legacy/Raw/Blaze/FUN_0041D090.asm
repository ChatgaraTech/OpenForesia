C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041d090  section .text  offset fichier 0x1c490  272 octets

  0041d090  81ec9c000000          sub       esp,9Ch
  0041d096  55                    push      ebp
  0041d097  56                    push      esi
  0041d098  8bb424a8000000        mov       esi,[esp+0A8h]
  0041d09f  57                    push      edi
  0041d0a0  b926000000            mov       ecx,26h
  0041d0a5  33c0                  xor       eax,eax
  0041d0a7  8d7c2410              lea       edi,[esp+10h]
  0041d0ab  c744240c00000000      mov       dword [esp+0Ch],0
  0041d0b3  f3ab                  rep stosd
  0041d0b5  8b4674                mov       eax,[esi+74h]
  0041d0b8  8d7c240c              lea       edi,[esp+0Ch]
  0041d0bc  85c0                  test      eax,eax
  0041d0be  8d6c240c              lea       ebp,[esp+0Ch]
  0041d0c2  7402                  je        short 0041D0C6h
  0041d0c4  8be8                  mov       ebp,eax

LAB_0041d0c6:
  0041d0c6  8b466c                mov       eax,[esi+6Ch]
  0041d0c9  85c0                  test      eax,eax
  0041d0cb  7402                  je        short 0041D0CFh
  0041d0cd  8bf8                  mov       edi,eax

LAB_0041d0cf:
  0041d0cf  8b4754                mov       eax,[edi+54h]
  0041d0d2  8b5520                mov       edx,[ebp+20h]
  0041d0d5  0fbf4e32              movsx     ecx,word [esi+32h]
  0041d0d9  03c2                  add       eax,edx
  0041d0db  8b565c                mov       edx,[esi+5Ch]
  0041d0de  894620                mov       [esi+20h],eax
  0041d0e1  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  0041d0e8  50                    push      eax
  0041d0e9  e82e891200            call      00545A1Ch
  0041d0ee  8b5750                mov       edx,[edi+50h]
  0041d0f1  8d0440                lea       eax,[eax+eax*2]
  0041d0f4  8d0c80                lea       ecx,[eax+eax*4]
  0041d0f7  8b451c                mov       eax,[ebp+1Ch]
  0041d0fa  03ca                  add       ecx,edx
  0041d0fc  0fbf5632              movsx     edx,word [esi+32h]
  0041d100  03c8                  add       ecx,eax
  0041d102  8b465c                mov       eax,[esi+5Ch]
  0041d105  894e1c                mov       [esi+1Ch],ecx
  0041d108  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0041d10f  51                    push      ecx
  0041d110  e801891200            call      00545A16h
  0041d115  8b4d24                mov       ecx,[ebp+24h]
  0041d118  8d0440                lea       eax,[eax+eax*2]
  0041d11b  8d1480                lea       edx,[eax+eax*4]
  0041d11e  8b4758                mov       eax,[edi+58h]
  0041d121  03d0                  add       edx,eax
  0041d123  bf01000000            mov       edi,1
  0041d128  03d1                  add       edx,ecx
  0041d12a  66837e1000            cmp       word [esi+10h],0
  0041d12f  895624                mov       [esi+24h],edx
  0041d132  7536                  jne       short 0041D16Ah
  0041d134  c7463c60005500        mov       dword [esi+3Ch],550060h
  0041d13b  e800a01200            call      00547140h
  0041d140  99                    cdq
  0041d141  b903000000            mov       ecx,3
  0041d146  c6460a02              mov       byte [esi+0Ah],2
  0041d14a  f7f9                  idiv      ecx
  0041d14c  b8a00f0000            mov       eax,0FA0h
  0041d151  c6460b00              mov       byte [esi+0Bh],0
  0041d155  66894628              mov       [esi+28h],ax
  0041d159  6689462a              mov       [esi+2Ah],ax
  0041d15d  66897e10              mov       [esi+10h],di
  0041d161  66c746120b00          mov       word [esi+12h],0Bh
  0041d167  885640                mov       [esi+40h],dl

LAB_0041d16a:
  0041d16a  66397e10              cmp       [esi+10h],di
  0041d16e  7526                  jne       short 0041D196h
  0041d170  8a5640                mov       dl,[esi+40h]
  0041d173  66ff4e12              dec       word [esi+12h]
  0041d177  668b4612              mov       ax,[esi+12h]
  0041d17b  fec2                  inc       dl
  0041d17d  8aca                  mov       cl,dl
  0041d17f  885640                mov       [esi+40h],dl
  0041d182  80f903                cmp       cl,3
  0041d185  7204                  jb        short 0041D18Bh
  0041d187  c6464000              mov       byte [esi+40h],0

LAB_0041d18b:
  0041d18b  6685c0                test      ax,ax
  0041d18e  7f06                  jg        short 0041D196h
  0041d190  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041d196:
  0041d196  5f                    pop       edi
  0041d197  5e                    pop       esi
  0041d198  5d                    pop       ebp
  0041d199  81c49c000000          add       esp,9Ch
  0041d19f  c3                    ret

5 cible(s) de saut interne : 0x0041d0c6, 0x0041d0cf, 0x0041d16a, 0x0041d18b, 0x0041d196
