C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046a5a0  section .text  offset fichier 0x699a0  144 octets

  0046a5a0  8b442408              mov       eax,[esp+8]
  0046a5a4  8b0d1c185700          mov       ecx,[57181Ch]
  0046a5aa  33d2                  xor       edx,edx
  0046a5ac  56                    push      esi
  0046a5ad  3bc2                  cmp       eax,edx
  0046a5af  742a                  je        short 0046A5DBh
  0046a5b1  3bca                  cmp       ecx,edx
  0046a5b3  7526                  jne       short 0046A5DBh
  0046a5b5  8b442408              mov       eax,[esp+8]
  0046a5b9  8b4c2410              mov       ecx,[esp+10h]
  0046a5bd  0fbf5034              movsx     edx,word [eax+34h]
  0046a5c1  8bc1                  mov       eax,ecx
  0046a5c3  890d18185700          mov       [571818h],ecx
  0046a5c9  2bc2                  sub       eax,edx
  0046a5cb  99                    cdq
  0046a5cc  83e203                and       edx,3
  0046a5cf  03c2                  add       eax,edx
  0046a5d1  c1f802                sar       eax,2
  0046a5d4  a31c185700            mov       [57181Ch],eax
  0046a5d9  5e                    pop       esi
  0046a5da  c3                    ret

LAB_0046a5db:
  0046a5db  3bc2                  cmp       eax,edx
  0046a5dd  7541                  jne       short 0046A620h
  0046a5df  3bca                  cmp       ecx,edx
  0046a5e1  743d                  je        short 0046A620h
  0046a5e3  8b442408              mov       eax,[esp+8]
  0046a5e7  66014834              add       [eax+34h],cx
  0046a5eb  8b351c185700          mov       esi,[57181Ch]
  0046a5f1  668b4834              mov       cx,[eax+34h]
  0046a5f5  3bf2                  cmp       esi,edx
  0046a5f7  0fbff1                movsx     esi,cx
  0046a5fa  8b0d18185700          mov       ecx,[571818h]
  0046a600  7e10                  jle       short 0046A612h
  0046a602  3bf1                  cmp       esi,ecx
  0046a604  7c1a                  jl        short 0046A620h
  0046a606  66894834              mov       [eax+34h],cx
  0046a60a  89151c185700          mov       [57181Ch],edx
  0046a610  5e                    pop       esi
  0046a611  c3                    ret

LAB_0046a612:
  0046a612  3bf1                  cmp       esi,ecx
  0046a614  7f0a                  jg        short 0046A620h
  0046a616  66894834              mov       [eax+34h],cx
  0046a61a  89151c185700          mov       [57181Ch],edx

LAB_0046a620:
  0046a620  5e                    pop       esi
  0046a621  c3                    ret
  0046a622  90                    nop
  0046a623  90                    nop
  0046a624  90                    nop
  0046a625  90                    nop
  0046a626  90                    nop
  0046a627  90                    nop
  0046a628  90                    nop
  0046a629  90                    nop
  0046a62a  90                    nop
  0046a62b  90                    nop
  0046a62c  90                    nop
  0046a62d  90                    nop
  0046a62e  90                    nop
  0046a62f  90                    nop

3 cible(s) de saut interne : 0x0046a5db, 0x0046a612, 0x0046a620
