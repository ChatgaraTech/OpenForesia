C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004725a0  section .text  offset fichier 0x719a0  144 octets

  004725a0  8b442408              mov       eax,[esp+8]
  004725a4  8b0d6c195700          mov       ecx,[57196Ch]
  004725aa  33d2                  xor       edx,edx
  004725ac  56                    push      esi
  004725ad  3bc2                  cmp       eax,edx
  004725af  742a                  je        short 004725DBh
  004725b1  3bca                  cmp       ecx,edx
  004725b3  7526                  jne       short 004725DBh
  004725b5  8b442408              mov       eax,[esp+8]
  004725b9  8b4c2410              mov       ecx,[esp+10h]
  004725bd  0fbf5034              movsx     edx,word [eax+34h]
  004725c1  8bc1                  mov       eax,ecx
  004725c3  890d68195700          mov       [571968h],ecx
  004725c9  2bc2                  sub       eax,edx
  004725cb  99                    cdq
  004725cc  83e203                and       edx,3
  004725cf  03c2                  add       eax,edx
  004725d1  c1f802                sar       eax,2
  004725d4  a36c195700            mov       [57196Ch],eax
  004725d9  5e                    pop       esi
  004725da  c3                    ret

LAB_004725db:
  004725db  3bc2                  cmp       eax,edx
  004725dd  7541                  jne       short 00472620h
  004725df  3bca                  cmp       ecx,edx
  004725e1  743d                  je        short 00472620h
  004725e3  8b442408              mov       eax,[esp+8]
  004725e7  66014834              add       [eax+34h],cx
  004725eb  8b356c195700          mov       esi,[57196Ch]
  004725f1  668b4834              mov       cx,[eax+34h]
  004725f5  3bf2                  cmp       esi,edx
  004725f7  0fbff1                movsx     esi,cx
  004725fa  8b0d68195700          mov       ecx,[571968h]
  00472600  7e10                  jle       short 00472612h
  00472602  3bf1                  cmp       esi,ecx
  00472604  7c1a                  jl        short 00472620h
  00472606  66894834              mov       [eax+34h],cx
  0047260a  89156c195700          mov       [57196Ch],edx
  00472610  5e                    pop       esi
  00472611  c3                    ret

LAB_00472612:
  00472612  3bf1                  cmp       esi,ecx
  00472614  7f0a                  jg        short 00472620h
  00472616  66894834              mov       [eax+34h],cx
  0047261a  89156c195700          mov       [57196Ch],edx

LAB_00472620:
  00472620  5e                    pop       esi
  00472621  c3                    ret
  00472622  90                    nop
  00472623  90                    nop
  00472624  90                    nop
  00472625  90                    nop
  00472626  90                    nop
  00472627  90                    nop
  00472628  90                    nop
  00472629  90                    nop
  0047262a  90                    nop
  0047262b  90                    nop
  0047262c  90                    nop
  0047262d  90                    nop
  0047262e  90                    nop
  0047262f  90                    nop

3 cible(s) de saut interne : 0x004725db, 0x00472612, 0x00472620
