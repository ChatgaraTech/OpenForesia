C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004840e0  section .text  offset fichier 0x834e0  163 octets

  004840e0  8b442404              mov       eax,[esp+4]
  004840e4  56                    push      esi
  004840e5  57                    push      edi
  004840e6  8b08                  mov       ecx,[eax]
  004840e8  668140328000          add       word [eax+32h],80h
  004840ee  8b5070                mov       edx,[eax+70h]
  004840f1  f7c100000002          test      ecx,2000000h
  004840f7  742a                  je        short 00484123h
  004840f9  0fbf4828              movsx     ecx,word [eax+28h]
  004840fd  8bd1                  mov       edx,ecx
  004840ff  c1fa03                sar       edx,3
  00484102  2bca                  sub       ecx,edx
  00484104  66894828              mov       [eax+28h],cx
  00484108  6689482c              mov       [eax+2Ch],cx
  0048410c  8b4838                mov       ecx,[eax+38h]
  0048410f  81c1f8f7f7ff          add       ecx,0FFF7F7F8h
  00484115  894838                mov       [eax+38h],ecx
  00484118  7566                  jne       short 00484180h
  0048411a  c70000000000          mov       dword [eax],0
  00484120  5f                    pop       edi
  00484121  5e                    pop       esi
  00484122  c3                    ret

LAB_00484123:
  00484123  f6c102                test      cl,2
  00484126  741f                  je        short 00484147h
  00484128  66ff4010              inc       word [eax+10h]
  0048412c  6683781028            cmp       word [eax+10h],28h
  00484131  7c4d                  jl        short 00484180h
  00484133  81c900000002          or        ecx,2000000h
  00484139  8908                  mov       [eax],ecx
  0048413b  8b02                  mov       eax,[edx]
  0048413d  0d00000002            or        eax,2000000h
  00484142  8902                  mov       [edx],eax
  00484144  5f                    pop       edi
  00484145  5e                    pop       esi
  00484146  c3                    ret

LAB_00484147:
  00484147  0fbf7028              movsx     esi,word [eax+28h]
  0048414b  bf00500000            mov       edi,5000h
  00484150  2bfe                  sub       edi,esi
  00484152  c1ff03                sar       edi,3
  00484155  03f7                  add       esi,edi
  00484157  8b7838                mov       edi,[eax+38h]
  0048415a  81c708080800          add       edi,80808h
  00484160  66897028              mov       [eax+28h],si
  00484164  6689702c              mov       [eax+2Ch],si
  00484168  8bf7                  mov       esi,edi
  0048416a  81fe80808000          cmp       esi,808080h
  00484170  897838                mov       [eax+38h],edi
  00484173  750b                  jne       short 00484180h
  00484175  83c902                or        ecx,2
  00484178  8908                  mov       [eax],ecx
  0048417a  8b02                  mov       eax,[edx]
  0048417c  0c02                  or        al,2
  0048417e  8902                  mov       [edx],eax

LAB_00484180:
  00484180  5f                    pop       edi
  00484181  5e                    pop       esi
  00484182  c3                    ret

3 cible(s) de saut interne : 0x00484123, 0x00484147, 0x00484180
