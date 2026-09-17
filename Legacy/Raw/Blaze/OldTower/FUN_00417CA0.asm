C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00417ca0  section .text  offset fichier 0x170a0  368 octets

  00417ca0  53                    push      ebx
  00417ca1  56                    push      esi
  00417ca2  8b74240c              mov       esi,[esp+0Ch]
  00417ca6  33db                  xor       ebx,ebx
  00417ca8  57                    push      edi
  00417ca9  bf01000000            mov       edi,1
  00417cae  66395e10              cmp       [esi+10h],bx
  00417cb2  7563                  jne       short 00417D17h
  00417cb4  66897e10              mov       [esi+10h],di
  00417cb8  c7463c58fa5400        mov       dword [esi+3Ch],54FA58h
  00417cbf  66895e28              mov       [esi+28h],bx
  00417cc3  66895e2a              mov       [esi+2Ah],bx
  00417cc7  c6460a09              mov       byte [esi+0Ah],9
  00417ccb  885e0b                mov       [esi+0Bh],bl
  00417cce  e86df41200            call      00547140h
  00417cd3  99                    cdq
  00417cd4  b996000000            mov       ecx,96h
  00417cd9  66c746140a00          mov       word [esi+14h],0Ah
  00417cdf  f7f9                  idiv      ecx
  00417ce1  8b4620                mov       eax,[esi+20h]
  00417ce4  8b4e24                mov       ecx,[esi+24h]
  00417ce7  894654                mov       [esi+54h],eax
  00417cea  894e58                mov       [esi+58h],ecx
  00417ced  885e40                mov       [esi+40h],bl
  00417cf0  81c2aa000000          add       edx,0AAh
  00417cf6  66895612              mov       [esi+12h],dx
  00417cfa  8b561c                mov       edx,[esi+1Ch]
  00417cfd  895650                mov       [esi+50h],edx
  00417d00  e83bf41200            call      00547140h
  00417d05  99                    cdq
  00417d06  33c2                  xor       eax,edx
  00417d08  2bc2                  sub       eax,edx
  00417d0a  25ff0f0000            and       eax,0FFFh
  00417d0f  33c2                  xor       eax,edx
  00417d11  2bc2                  sub       eax,edx
  00417d13  66894616              mov       [esi+16h],ax

LAB_00417d17:
  00417d17  66397e10              cmp       [esi+10h],di
  00417d1b  0f85e3000000          jne       near 00417E04h
  00417d21  b9f4010000            mov       ecx,1F4h
  00417d26  ba00100000            mov       edx,1000h
  00417d2b  66014e28              add       [esi+28h],cx
  00417d2f  668b4628              mov       ax,[esi+28h]
  00417d33  663bc2                cmp       ax,dx
  00417d36  7e04                  jle       short 00417D3Ch
  00417d38  66895628              mov       [esi+28h],dx

LAB_00417d3c:
  00417d3c  66014e2a              add       [esi+2Ah],cx
  00417d40  6639562a              cmp       [esi+2Ah],dx
  00417d44  7e04                  jle       short 00417D4Ah
  00417d46  6689562a              mov       [esi+2Ah],dx

LAB_00417d4a:
  00417d4a  0fbf5616              movsx     edx,word [esi+16h]
  00417d4e  52                    push      edx
  00417d4f  e8c8dc1200            call      00545A1Ch
  00417d54  0fbf4e12              movsx     ecx,word [esi+12h]
  00417d58  c1f902                sar       ecx,2
  00417d5b  0fafc1                imul      eax,ecx
  00417d5e  0fbf5632              movsx     edx,word [esi+32h]
  00417d62  8b4e50                mov       ecx,[esi+50h]
  00417d65  52                    push      edx
  00417d66  03c1                  add       eax,ecx
  00417d68  89461c                mov       [esi+1Ch],eax
  00417d6b  e8a6dc1200            call      00545A16h
  00417d70  0fbf4e12              movsx     ecx,word [esi+12h]
  00417d74  0fafc1                imul      eax,ecx
  00417d77  0fbf5616              movsx     edx,word [esi+16h]
  00417d7b  8b4e54                mov       ecx,[esi+54h]
  00417d7e  668b7e12              mov       di,[esi+12h]
  00417d82  03c1                  add       eax,ecx
  00417d84  52                    push      edx
  00417d85  894620                mov       [esi+20h],eax
  00417d88  e889dc1200            call      00545A16h
  00417d8d  0fbfcf                movsx     ecx,di
  00417d90  c1f902                sar       ecx,2
  00417d93  0fafc1                imul      eax,ecx
  00417d96  034658                add       eax,[esi+58h]
  00417d99  894624                mov       [esi+24h],eax
  00417d9c  668b4614              mov       ax,[esi+14h]
  00417da0  668bd0                mov       dx,ax
  00417da3  66d1fa                sar       dx,1
  00417da6  2bfa                  sub       edi,edx
  00417da8  40                    inc       eax
  00417da9  66894614              mov       [esi+14h],ax
  00417dad  668b4616              mov       ax,[esi+16h]
  00417db1  66056400              add       ax,64h
  00417db5  66897e12              mov       [esi+12h],di
  00417db9  25ff0f0000            and       eax,0FFFh
  00417dbe  66894616              mov       [esi+16h],ax
  00417dc2  e879f31200            call      00547140h
  00417dc7  99                    cdq
  00417dc8  b906000000            mov       ecx,6
  00417dcd  f7f9                  idiv      ecx
  00417dcf  8b4e1c                mov       ecx,[esi+1Ch]
  00417dd2  83ea03                sub       edx,3
  00417dd5  c1e20c                shl       edx,0Ch
  00417dd8  03ca                  add       ecx,edx
  00417dda  894e1c                mov       [esi+1Ch],ecx
  00417ddd  e85ef31200            call      00547140h
  00417de2  99                    cdq
  00417de3  b906000000            mov       ecx,6
  00417de8  f7f9                  idiv      ecx
  00417dea  8b4e24                mov       ecx,[esi+24h]
  00417ded  83ea03                sub       edx,3
  00417df0  c1e20c                shl       edx,0Ch
  00417df3  03ca                  add       ecx,edx
  00417df5  66395e12              cmp       [esi+12h],bx
  00417df9  894e24                mov       [esi+24h],ecx
  00417dfc  7d06                  jge       short 00417E04h
  00417dfe  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00417e04:
  00417e04  5f                    pop       edi
  00417e05  5e                    pop       esi
  00417e06  5b                    pop       ebx
  00417e07  c3                    ret
  00417e08  90                    nop
  00417e09  90                    nop
  00417e0a  90                    nop
  00417e0b  90                    nop
  00417e0c  90                    nop
  00417e0d  90                    nop
  00417e0e  90                    nop
  00417e0f  90                    nop

4 cible(s) de saut interne : 0x00417d17, 0x00417d3c, 0x00417d4a, 0x00417e04
