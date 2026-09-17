C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045c000  section .text  offset fichier 0x5b400  144 octets

  0045c000  8b15bcde5e00          mov       edx,[5EDEBCh]
  0045c006  56                    push      esi
  0045c007  8b742408              mov       esi,[esp+8]
  0045c00b  57                    push      edi
  0045c00c  8b02                  mov       eax,[edx]
  0045c00e  8b4e1c                mov       ecx,[esi+1Ch]
  0045c011  8b7e20                mov       edi,[esi+20h]
  0045c014  c1f90c                sar       ecx,0Ch
  0045c017  c1f80c                sar       eax,0Ch
  0045c01a  2bc1                  sub       eax,ecx
  0045c01c  8b4a04                mov       ecx,[edx+4]
  0045c01f  8b5208                mov       edx,[edx+8]
  0045c022  c1ff0c                sar       edi,0Ch
  0045c025  c1f90c                sar       ecx,0Ch
  0045c028  2bcf                  sub       ecx,edi
  0045c02a  8b7e24                mov       edi,[esi+24h]
  0045c02d  c1ff0c                sar       edi,0Ch
  0045c030  c1fa0c                sar       edx,0Ch
  0045c033  2bd7                  sub       edx,edi
  0045c035  8bf8                  mov       edi,eax
  0045c037  0faff8                imul      edi,eax
  0045c03a  8bc1                  mov       eax,ecx
  0045c03c  0fafc1                imul      eax,ecx
  0045c03f  8bca                  mov       ecx,edx
  0045c041  03f8                  add       edi,eax
  0045c043  0fafca                imul      ecx,edx
  0045c046  03f9                  add       edi,ecx
  0045c048  57                    push      edi
  0045c049  e8c2990e00            call      00545A10h
  0045c04e  33c9                  xor       ecx,ecx
  0045c050  3d08070000            cmp       eax,708h
  0045c055  7d26                  jge       short 0045C07Dh
  0045c057  0fbf542418            movsx     edx,word [esp+18h]
  0045c05c  0fbf4c2410            movsx     ecx,word [esp+10h]
  0045c061  0fbf442414            movsx     eax,word [esp+14h]
  0045c066  52                    push      edx
  0045c067  81c1c0000000          add       ecx,0C0h
  0045c06d  50                    push      eax
  0045c06e  51                    push      ecx
  0045c06f  56                    push      esi
  0045c070  e85be90100            call      0047A9D0h
  0045c075  83c410                add       esp,10h
  0045c078  b901000000            mov       ecx,1

LAB_0045c07d:
  0045c07d  5f                    pop       edi
  0045c07e  668bc1                mov       ax,cx
  0045c081  5e                    pop       esi
  0045c082  c3                    ret
  0045c083  90                    nop
  0045c084  90                    nop
  0045c085  90                    nop
  0045c086  90                    nop
  0045c087  90                    nop
  0045c088  90                    nop
  0045c089  90                    nop
  0045c08a  90                    nop
  0045c08b  90                    nop
  0045c08c  90                    nop
  0045c08d  90                    nop
  0045c08e  90                    nop
  0045c08f  90                    nop

1 cible(s) de saut interne : 0x0045c07d
