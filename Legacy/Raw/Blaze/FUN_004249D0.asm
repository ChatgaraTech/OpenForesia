C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004249d0  section .text  offset fichier 0x23dd0  270 octets

  004249d0  53                    push      ebx
  004249d1  56                    push      esi
  004249d2  8b74240c              mov       esi,[esp+0Ch]
  004249d6  33db                  xor       ebx,ebx
  004249d8  66395e10              cmp       [esi+10h],bx
  004249dc  0f85b5000000          jne       near 00424A97h
  004249e2  8b5620                mov       edx,[esi+20h]
  004249e5  c7463c58035500        mov       dword [esi+3Ch],550358h
  004249ec  81c200000500          add       edx,50000h
  004249f2  885e40                mov       [esi+40h],bl
  004249f5  895620                mov       [esi+20h],edx
  004249f8  e843271200            call      00547140h
  004249fd  99                    cdq
  004249fe  b928000000            mov       ecx,28h
  00424a03  f7f9                  idiv      ecx
  00424a05  8b4e1c                mov       ecx,[esi+1Ch]
  00424a08  83ea14                sub       edx,14h
  00424a0b  c1e20c                shl       edx,0Ch
  00424a0e  03ca                  add       ecx,edx
  00424a10  894e1c                mov       [esi+1Ch],ecx
  00424a13  e828271200            call      00547140h
  00424a18  99                    cdq
  00424a19  b928000000            mov       ecx,28h
  00424a1e  f7f9                  idiv      ecx
  00424a20  8b4620                mov       eax,[esi+20h]
  00424a23  83ea14                sub       edx,14h
  00424a26  c1e20c                shl       edx,0Ch
  00424a29  03c2                  add       eax,edx
  00424a2b  894620                mov       [esi+20h],eax
  00424a2e  e80d271200            call      00547140h
  00424a33  99                    cdq
  00424a34  b928000000            mov       ecx,28h
  00424a39  66c746100100          mov       word [esi+10h],1
  00424a3f  f7f9                  idiv      ecx
  00424a41  8b4e24                mov       ecx,[esi+24h]
  00424a44  83ea14                sub       edx,14h
  00424a47  c1e20c                shl       edx,0Ch
  00424a4a  03ca                  add       ecx,edx
  00424a4c  894e24                mov       [esi+24h],ecx
  00424a4f  e8ec261200            call      00547140h
  00424a54  99                    cdq
  00424a55  b914000000            mov       ecx,14h
  00424a5a  66895e14              mov       [esi+14h],bx
  00424a5e  f7f9                  idiv      ecx
  00424a60  885e38                mov       [esi+38h],bl
  00424a63  885e39                mov       [esi+39h],bl
  00424a66  885e3a                mov       [esi+3Ah],bl
  00424a69  c6460a02              mov       byte [esi+0Ah],2
  00424a6d  885e0b                mov       [esi+0Bh],bl
  00424a70  83c204                add       edx,4
  00424a73  66895612              mov       [esi+12h],dx
  00424a77  e8c4261200            call      00547140h
  00424a7c  99                    cdq
  00424a7d  33c2                  xor       eax,edx
  00424a7f  2bc2                  sub       eax,edx
  00424a81  25ff030000            and       eax,3FFh
  00424a86  33c2                  xor       eax,edx
  00424a88  2bc2                  sub       eax,edx
  00424a8a  0500080000            add       eax,800h
  00424a8f  66894628              mov       [esi+28h],ax
  00424a93  6689462a              mov       [esi+2Ah],ax

LAB_00424a97:
  00424a97  66837e1001            cmp       word [esi+10h],1
  00424a9c  7510                  jne       short 00424AAEh
  00424a9e  66ff4e12              dec       word [esi+12h]
  00424aa2  66395e12              cmp       [esi+12h],bx
  00424aa6  7506                  jne       short 00424AAEh
  00424aa8  66c746100200          mov       word [esi+10h],2

LAB_00424aae:
  00424aae  66837e1002            cmp       word [esi+10h],2
  00424ab3  7526                  jne       short 00424ADBh
  00424ab5  668b4614              mov       ax,[esi+14h]
  00424ab9  8b5e20                mov       ebx,[esi+20h]
  00424abc  0fbfd0                movsx     edx,ax
  00424abf  8b4e48                mov       ecx,[esi+48h]
  00424ac2  c1e20c                shl       edx,0Ch
  00424ac5  2bda                  sub       ebx,edx
  00424ac7  40                    inc       eax
  00424ac8  66894614              mov       [esi+14h],ax
  00424acc  8bc3                  mov       eax,ebx
  00424ace  3bc1                  cmp       eax,ecx
  00424ad0  895e20                mov       [esi+20h],ebx
  00424ad3  7d06                  jge       short 00424ADBh
  00424ad5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00424adb:
  00424adb  5e                    pop       esi
  00424adc  5b                    pop       ebx
  00424add  c3                    ret

3 cible(s) de saut interne : 0x00424a97, 0x00424aae, 0x00424adb
