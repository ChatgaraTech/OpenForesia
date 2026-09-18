C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460310  section .text  offset fichier 0x5f710  165 octets

  00460310  53                    push      ebx
  00460311  56                    push      esi
  00460312  8b74240c              mov       esi,[esp+0Ch]
  00460316  57                    push      edi
  00460317  66837e1000            cmp       word [esi+10h],0
  0046031c  7513                  jne       short 00460331h
  0046031e  8b06                  mov       eax,[esi]
  00460320  66c746100100          mov       word [esi+10h],1
  00460326  80cc40                or        ah,40h
  00460329  66c746120000          mov       word [esi+12h],0
  0046032f  8906                  mov       [esi],eax

LAB_00460331:
  00460331  66ff4612              inc       word [esi+12h]
  00460335  e8066e0e00            call      00547140h
  0046033a  99                    cdq
  0046033b  b91e000000            mov       ecx,1Eh
  00460340  f7f9                  idiv      ecx
  00460342  6683fa01              cmp       dx,1
  00460346  7507                  jne       short 0046034Fh
  00460348  66837e1232            cmp       word [esi+12h],32h
  0046034d  7d07                  jge       short 00460356h

LAB_0046034f:
  0046034f  66837e125a            cmp       word [esi+12h],5Ah
  00460354  7c5b                  jl        short 004603B1h

LAB_00460356:
  00460356  8b7e1c                mov       edi,[esi+1Ch]
  00460359  8b5e24                mov       ebx,[esi+24h]
  0046035c  66c746120000          mov       word [esi+12h],0
  00460362  e8d96d0e00            call      00547140h
  00460367  99                    cdq
  00460368  b9c8000000            mov       ecx,0C8h
  0046036d  f7f9                  idiv      ecx
  0046036f  8b461c                mov       eax,[esi+1Ch]
  00460372  83ea64                sub       edx,64h
  00460375  c1e20c                shl       edx,0Ch
  00460378  03c2                  add       eax,edx
  0046037a  89461c                mov       [esi+1Ch],eax
  0046037d  e8be6d0e00            call      00547140h
  00460382  99                    cdq
  00460383  b9c8000000            mov       ecx,0C8h
  00460388  6a00                  push      0
  0046038a  f7f9                  idiv      ecx
  0046038c  8b4e24                mov       ecx,[esi+24h]
  0046038f  6a00                  push      0
  00460391  6aff                  push      0FFFFFFFFh
  00460393  6a00                  push      0
  00460395  6a3c                  push      3Ch
  00460397  56                    push      esi
  00460398  83ea64                sub       edx,64h
  0046039b  c1e20c                shl       edx,0Ch
  0046039e  03ca                  add       ecx,edx
  004603a0  894e24                mov       [esi+24h],ecx
  004603a3  e8687d0000            call      00468110h
  004603a8  83c418                add       esp,18h
  004603ab  897e1c                mov       [esi+1Ch],edi
  004603ae  895e24                mov       [esi+24h],ebx

LAB_004603b1:
  004603b1  5f                    pop       edi
  004603b2  5e                    pop       esi
  004603b3  5b                    pop       ebx
  004603b4  c3                    ret

4 cible(s) de saut interne : 0x00460331, 0x0046034f, 0x00460356, 0x004603b1
