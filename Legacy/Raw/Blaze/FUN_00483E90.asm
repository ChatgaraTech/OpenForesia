C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483e90  section .text  offset fichier 0x83290  146 octets

  00483e90  8b442404              mov       eax,[esp+4]
  00483e94  6683781000            cmp       word [eax+10h],0
  00483e99  752b                  jne       short 00483EC6h
  00483e9b  b900040000            mov       ecx,400h
  00483ea0  66c740100100          mov       word [eax+10h],1
  00483ea6  66014832              add       [eax+32h],cx
  00483eaa  66014834              add       [eax+34h],cx
  00483eae  c7402800000020        mov       dword [eax+28h],20000000h
  00483eb5  66c7402c0020          mov       word [eax+2Ch],2000h
  00483ebb  c7403800000000        mov       dword [eax+38h],0
  00483ec2  c6400435              mov       byte [eax+4],35h

LAB_00483ec6:
  00483ec6  0fbf4828              movsx     ecx,word [eax+28h]
  00483eca  ba00300000            mov       edx,3000h
  00483ecf  2bd1                  sub       edx,ecx
  00483ed1  c1fa04                sar       edx,4
  00483ed4  03ca                  add       ecx,edx
  00483ed6  66894828              mov       [eax+28h],cx
  00483eda  8b4820                mov       ecx,[eax+20h]
  00483edd  81c100400100          add       ecx,14000h
  00483ee3  894820                mov       [eax+20h],ecx
  00483ee6  8b08                  mov       ecx,[eax]
  00483ee8  f7c100000002          test      ecx,2000000h
  00483eee  7415                  je        short 00483F05h
  00483ef0  8b4838                mov       ecx,[eax+38h]
  00483ef3  81c1f8f7f7ff          add       ecx,0FFF7F7F8h
  00483ef9  894838                mov       [eax+38h],ecx
  00483efc  7523                  jne       short 00483F21h
  00483efe  c70000000000          mov       dword [eax],0
  00483f04  c3                    ret

LAB_00483f05:
  00483f05  8b5038                mov       edx,[eax+38h]
  00483f08  81c208080800          add       edx,80808h
  00483f0e  81fa80808000          cmp       edx,808080h
  00483f14  895038                mov       [eax+38h],edx
  00483f17  7508                  jne       short 00483F21h
  00483f19  81c900000002          or        ecx,2000000h
  00483f1f  8908                  mov       [eax],ecx

LAB_00483f21:
  00483f21  c3                    ret

3 cible(s) de saut interne : 0x00483ec6, 0x00483f05, 0x00483f21
