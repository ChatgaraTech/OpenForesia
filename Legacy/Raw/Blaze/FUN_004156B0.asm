; 0x004156b0 — la projection large (77 instructions, absente de Blaze.c).
; Trois copies : 0x00415c50 (+0x0a=2), 0x00412240 (planche 0x0054f8f0), 0x00412720 (les deux).

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004156b0  section .text  offset fichier 0x14ab0  230 octets

  004156b0  53                    push      ebx
  004156b1  56                    push      esi
  004156b2  8b74240c              mov       esi,[esp+0Ch]
  004156b6  33db                  xor       ebx,ebx
  004156b8  66395e10              cmp       [esi+10h],bx
  004156bc  0f85a7000000          jne       near 00415769h
  004156c2  c7463ce8f95400        mov       dword [esi+3Ch],54F9E8h
  004156c9  885e40                mov       [esi+40h],bl
  004156cc  e86f1a1300            call      00547140h
  004156d1  99                    cdq
  004156d2  33c2                  xor       eax,edx
  004156d4  2bc2                  sub       eax,edx
  004156d6  25ff070000            and       eax,7FFh
  004156db  33c2                  xor       eax,edx
  004156dd  2bc2                  sub       eax,edx
  004156df  05000c0000            add       eax,0C00h
  004156e4  66894628              mov       [esi+28h],ax
  004156e8  e8531a1300            call      00547140h
  004156ed  99                    cdq
  004156ee  33c2                  xor       eax,edx
  004156f0  2bc2                  sub       eax,edx
  004156f2  25ff070000            and       eax,7FFh
  004156f7  33c2                  xor       eax,edx
  004156f9  2bc2                  sub       eax,edx
  004156fb  05000c0000            add       eax,0C00h
  00415700  6689462a              mov       [esi+2Ah],ax
  00415704  e8371a1300            call      00547140h
  00415709  99                    cdq
  0041570a  b92c010000            mov       ecx,12Ch
  0041570f  f7f9                  idiv      ecx
  00415711  8b4e1c                mov       ecx,[esi+1Ch]
  00415714  81ea96000000          sub       edx,96h
  0041571a  c1e20c                shl       edx,0Ch
  0041571d  03ca                  add       ecx,edx
  0041571f  894e1c                mov       [esi+1Ch],ecx
  00415722  e8191a1300            call      00547140h
  00415727  99                    cdq
  00415728  b92c010000            mov       ecx,12Ch
  0041572d  f7f9                  idiv      ecx
  0041572f  8b4624                mov       eax,[esi+24h]
  00415732  81ea96000000          sub       edx,96h
  00415738  c1e20c                shl       edx,0Ch
  0041573b  03c2                  add       eax,edx
  0041573d  894624                mov       [esi+24h],eax
  00415740  e8fb191300            call      00547140h
  00415745  99                    cdq
  00415746  b996000000            mov       ecx,96h
  0041574b  c6460a09              mov       byte [esi+0Ah],9
  0041574f  f7f9                  idiv      ecx
  00415751  8b4620                mov       eax,[esi+20h]
  00415754  885e0b                mov       [esi+0Bh],bl
  00415757  66c746100100          mov       word [esi+10h],1
  0041575d  66895e12              mov       [esi+12h],bx
  00415761  c1e20c                shl       edx,0Ch
  00415764  03c2                  add       eax,edx
  00415766  894620                mov       [esi+20h],eax

LAB_00415769:
  00415769  66837e1001            cmp       word [esi+10h],1
  0041576e  7523                  jne       short 00415793h
  00415770  66ff4612              inc       word [esi+12h]
  00415774  66837e1202            cmp       word [esi+12h],2
  00415779  7c18                  jl        short 00415793h
  0041577b  8a4e40                mov       cl,[esi+40h]
  0041577e  66895e12              mov       [esi+12h],bx
  00415782  fec1                  inc       cl
  00415784  8ac1                  mov       al,cl
  00415786  884e40                mov       [esi+40h],cl
  00415789  3c04                  cmp       al,4
  0041578b  7206                  jb        short 00415793h
  0041578d  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00415793:
  00415793  5e                    pop       esi
  00415794  5b                    pop       ebx
  00415795  c3                    ret

2 cible(s) de saut interne : 0x00415769, 0x00415793
