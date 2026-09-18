C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041d860  section .text  offset fichier 0x1cc60  179 octets

  0041d860  56                    push      esi
  0041d861  8b742408              mov       esi,[esp+8]
  0041d865  66837e1000            cmp       word [esi+10h],0
  0041d86a  7536                  jne       short 0041D8A2h
  0041d86c  66c746100100          mov       word [esi+10h],1
  0041d872  66c746120a00          mov       word [esi+12h],0Ah
  0041d878  a1cc975e00            mov       eax,[5E97CCh]
  0041d87d  6800100000            push      1000h
  0041d882  6800100000            push      1000h
  0041d887  6800020000            push      200h
  0041d88c  8b88a8000000          mov       ecx,[eax+0A8h]
  0041d892  33c0                  xor       eax,eax
  0041d894  8b11                  mov       edx,[ecx]
  0041d896  8a423e                mov       al,[edx+3Eh]
  0041d899  50                    push      eax
  0041d89a  e801f61100            call      0053CEA0h
  0041d89f  83c410                add       esp,10h

LAB_0041d8a2:
  0041d8a2  0fbf4e12              movsx     ecx,word [esi+12h]
  0041d8a6  890dace05d00          mov       [5DE0ACh],ecx
  0041d8ac  668b4612              mov       ax,[esi+12h]
  0041d8b0  6685c0                test      ax,ax
  0041d8b3  7e05                  jle       short 0041D8BAh
  0041d8b5  48                    dec       eax
  0041d8b6  66894612              mov       [esi+12h],ax

LAB_0041d8ba:
  0041d8ba  668b4612              mov       ax,[esi+12h]
  0041d8be  6685c0                test      ax,ax
  0041d8c1  7d05                  jge       short 0041D8C8h
  0041d8c3  40                    inc       eax
  0041d8c4  66894612              mov       [esi+12h],ax

LAB_0041d8c8:
  0041d8c8  668b4612              mov       ax,[esi+12h]
  0041d8cc  66f7d8                neg       ax
  0041d8cf  66894612              mov       [esi+12h],ax
  0041d8d3  753c                  jne       short 0041D911h
  0041d8d5  8b06                  mov       eax,[esi]
  0041d8d7  6a00                  push      0
  0041d8d9  25ffffff7f            and       eax,7FFFFFFFh
  0041d8de  68b0335600            push      5633B0h
  0041d8e3  8906                  mov       [esi],eax
  0041d8e5  33c0                  xor       eax,eax
  0041d8e7  a062905e00            mov       al,[5E9062h]
  0041d8ec  8b0dcc975e00          mov       ecx,[5E97CCh]
  0041d8f2  8d1480                lea       edx,[eax+eax*4]
  0041d8f5  c1e203                shl       edx,3
  0041d8f8  2bd0                  sub       edx,eax
  0041d8fa  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  0041d901  8b5170                mov       edx,[ecx+70h]
  0041d904  50                    push      eax
  0041d905  8b4204                mov       eax,[edx+4]
  0041d908  50                    push      eax
  0041d909  e8426a1200            call      00544350h
  0041d90e  83c410                add       esp,10h

LAB_0041d911:
  0041d911  5e                    pop       esi
  0041d912  c3                    ret

4 cible(s) de saut interne : 0x0041d8a2, 0x0041d8ba, 0x0041d8c8, 0x0041d911
