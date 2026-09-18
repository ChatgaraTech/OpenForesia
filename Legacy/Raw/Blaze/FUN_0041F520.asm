C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041f520  section .text  offset fichier 0x1e920  215 octets

  0041f520  53                    push      ebx
  0041f521  56                    push      esi
  0041f522  8b74240c              mov       esi,[esp+0Ch]
  0041f526  57                    push      edi
  0041f527  8b7e74                mov       edi,[esi+74h]
  0041f52a  8b5e6c                mov       ebx,[esi+6Ch]
  0041f52d  0fbf4e32              movsx     ecx,word [esi+32h]
  0041f531  8b4354                mov       eax,[ebx+54h]
  0041f534  8b5720                mov       edx,[edi+20h]
  0041f537  03c2                  add       eax,edx
  0041f539  8b565c                mov       edx,[esi+5Ch]
  0041f53c  894620                mov       [esi+20h],eax
  0041f53f  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  0041f546  50                    push      eax
  0041f547  e8d0641200            call      00545A1Ch
  0041f54c  8b5350                mov       edx,[ebx+50h]
  0041f54f  8d0440                lea       eax,[eax+eax*2]
  0041f552  8d0c80                lea       ecx,[eax+eax*4]
  0041f555  8b471c                mov       eax,[edi+1Ch]
  0041f558  03ca                  add       ecx,edx
  0041f55a  0fbf5632              movsx     edx,word [esi+32h]
  0041f55e  03c8                  add       ecx,eax
  0041f560  8b465c                mov       eax,[esi+5Ch]
  0041f563  894e1c                mov       [esi+1Ch],ecx
  0041f566  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  0041f56d  51                    push      ecx
  0041f56e  e8a3641200            call      00545A16h
  0041f573  8b4f24                mov       ecx,[edi+24h]
  0041f576  8d0440                lea       eax,[eax+eax*2]
  0041f579  bf01000000            mov       edi,1
  0041f57e  8d1480                lea       edx,[eax+eax*4]
  0041f581  8b4358                mov       eax,[ebx+58h]
  0041f584  03d0                  add       edx,eax
  0041f586  33db                  xor       ebx,ebx
  0041f588  03d1                  add       edx,ecx
  0041f58a  66395e10              cmp       [esi+10h],bx
  0041f58e  895624                mov       [esi+24h],edx
  0041f591  7535                  jne       short 0041F5C8h
  0041f593  c7463c68025500        mov       dword [esi+3Ch],550268h
  0041f59a  e8a17b1200            call      00547140h
  0041f59f  99                    cdq
  0041f5a0  b903000000            mov       ecx,3
  0041f5a5  c6460a02              mov       byte [esi+0Ah],2
  0041f5a9  f7f9                  idiv      ecx
  0041f5ab  b8a00f0000            mov       eax,0FA0h
  0041f5b0  885e0b                mov       [esi+0Bh],bl
  0041f5b3  66894628              mov       [esi+28h],ax
  0041f5b7  6689462a              mov       [esi+2Ah],ax
  0041f5bb  66897e10              mov       [esi+10h],di
  0041f5bf  66c746120b00          mov       word [esi+12h],0Bh
  0041f5c5  885640                mov       [esi+40h],dl

LAB_0041f5c8:
  0041f5c8  66397e10              cmp       [esi+10h],di
  0041f5cc  7525                  jne       short 0041F5F3h
  0041f5ce  8a5640                mov       dl,[esi+40h]
  0041f5d1  66ff4e12              dec       word [esi+12h]
  0041f5d5  668b4612              mov       ax,[esi+12h]
  0041f5d9  fec2                  inc       dl
  0041f5db  8aca                  mov       cl,dl
  0041f5dd  885640                mov       [esi+40h],dl
  0041f5e0  80f903                cmp       cl,3
  0041f5e3  7203                  jb        short 0041F5E8h
  0041f5e5  885e40                mov       [esi+40h],bl

LAB_0041f5e8:
  0041f5e8  663bc3                cmp       ax,bx
  0041f5eb  7f06                  jg        short 0041F5F3h
  0041f5ed  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041f5f3:
  0041f5f3  5f                    pop       edi
  0041f5f4  5e                    pop       esi
  0041f5f5  5b                    pop       ebx
  0041f5f6  c3                    ret

3 cible(s) de saut interne : 0x0041f5c8, 0x0041f5e8, 0x0041f5f3
