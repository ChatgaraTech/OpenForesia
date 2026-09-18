C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00425350  section .text  offset fichier 0x24750  188 octets

  00425350  8b442404              mov       eax,[esp+4]
  00425354  53                    push      ebx
  00425355  ba01000000            mov       edx,1
  0042535a  6683781000            cmp       word [eax+10h],0
  0042535f  7532                  jne       short 00425393h
  00425361  b9b80b0000            mov       ecx,0BB8h
  00425366  66895010              mov       [eax+10h],dx
  0042536a  66894828              mov       [eax+28h],cx
  0042536e  6689482a              mov       [eax+2Ah],cx
  00425372  6689482c              mov       [eax+2Ch],cx
  00425376  b1ff                  mov       cl,0FFh
  00425378  c6400403              mov       byte [eax+4],3
  0042537c  c6400a02              mov       byte [eax+0Ah],2
  00425380  c6400b00              mov       byte [eax+0Bh],0
  00425384  66c740128403          mov       word [eax+12h],384h
  0042538a  884838                mov       [eax+38h],cl
  0042538d  884839                mov       [eax+39h],cl
  00425390  88483a                mov       [eax+3Ah],cl

LAB_00425393:
  00425393  66395010              cmp       [eax+10h],dx
  00425397  7571                  jne       short 0042540Ah
  00425399  660fb64838            movzx     cx,byte [eax+38h]
  0042539e  660fb65039            movzx     dx,byte [eax+39h]
  004253a3  660fb6583a            movzx     bx,byte [eax+3Ah]
  004253a8  83e90c                sub       ecx,0Ch
  004253ab  83ea0c                sub       edx,0Ch
  004253ae  83eb0c                sub       ebx,0Ch
  004253b1  6685c9                test      cx,cx
  004253b4  7d06                  jge       short 004253BCh
  004253b6  33c9                  xor       ecx,ecx
  004253b8  33d2                  xor       edx,edx
  004253ba  33db                  xor       ebx,ebx

LAB_004253bc:
  004253bc  884838                mov       [eax+38h],cl
  004253bf  668b4812              mov       cx,[eax+12h]
  004253c3  66014828              add       [eax+28h],cx
  004253c7  6601482a              add       [eax+2Ah],cx
  004253cb  6601482c              add       [eax+2Ch],cx
  004253cf  83c128                add       ecx,28h
  004253d2  6681f9e803            cmp       cx,3E8h
  004253d7  885039                mov       [eax+39h],dl
  004253da  88583a                mov       [eax+3Ah],bl
  004253dd  66894812              mov       [eax+12h],cx
  004253e1  7c06                  jl        short 004253E9h
  004253e3  66c74012e803          mov       word [eax+12h],3E8h

LAB_004253e9:
  004253e9  b930750000            mov       ecx,7530h
  004253ee  66394828              cmp       [eax+28h],cx
  004253f2  7c16                  jl        short 0042540Ah
  004253f4  66894828              mov       [eax+28h],cx
  004253f8  6689482a              mov       [eax+2Ah],cx
  004253fc  6689482c              mov       [eax+2Ch],cx
  00425400  8b08                  mov       ecx,[eax]
  00425402  81e1ffffff7f          and       ecx,7FFFFFFFh
  00425408  8908                  mov       [eax],ecx

LAB_0042540a:
  0042540a  5b                    pop       ebx
  0042540b  c3                    ret

4 cible(s) de saut interne : 0x00425393, 0x004253bc, 0x004253e9, 0x0042540a
