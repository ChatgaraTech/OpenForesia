C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00425270  section .text  offset fichier 0x24670  214 octets

  00425270  53                    push      ebx
  00425271  56                    push      esi
  00425272  8b74240c              mov       esi,[esp+0Ch]
  00425276  33db                  xor       ebx,ebx
  00425278  66395e10              cmp       [esi+10h],bx
  0042527c  754f                  jne       short 004252CDh
  0042527e  8b5620                mov       edx,[esi+20h]
  00425281  6aff                  push      0FFFFFFFFh
  00425283  53                    push      ebx
  00425284  81c200400600          add       edx,64000h
  0042528a  b0fa                  mov       al,0FAh
  0042528c  68c8000000            push      0C8h
  00425291  56                    push      esi
  00425292  c6460402              mov       byte [esi+4],2
  00425296  c6460a09              mov       byte [esi+0Ah],9
  0042529a  885e0b                mov       [esi+0Bh],bl
  0042529d  66895e28              mov       [esi+28h],bx
  004252a1  66895e2a              mov       [esi+2Ah],bx
  004252a5  66895e2c              mov       [esi+2Ch],bx
  004252a9  895620                mov       [esi+20h],edx
  004252ac  66895e12              mov       [esi+12h],bx
  004252b0  66c746100100          mov       word [esi+10h],1
  004252b6  884638                mov       [esi+38h],al
  004252b9  884639                mov       [esi+39h],al
  004252bc  88463a                mov       [esi+3Ah],al
  004252bf  e81c580500            call      0047AAE0h
  004252c4  83c410                add       esp,10h
  004252c7  66c74612bc02          mov       word [esi+12h],2BCh

LAB_004252cd:
  004252cd  66837e1001            cmp       word [esi+10h],1
  004252d2  756f                  jne       short 00425343h
  004252d4  660fb64638            movzx     ax,byte [esi+38h]
  004252d9  660fb64e39            movzx     cx,byte [esi+39h]
  004252de  660fb6563a            movzx     dx,byte [esi+3Ah]
  004252e3  83e80c                sub       eax,0Ch
  004252e6  83e90c                sub       ecx,0Ch
  004252e9  83ea0c                sub       edx,0Ch
  004252ec  663bc3                cmp       ax,bx
  004252ef  7d06                  jge       short 004252F7h
  004252f1  33c0                  xor       eax,eax
  004252f3  33c9                  xor       ecx,ecx
  004252f5  33d2                  xor       edx,edx

LAB_004252f7:
  004252f7  884638                mov       [esi+38h],al
  004252fa  668b4612              mov       ax,[esi+12h]
  004252fe  66014628              add       [esi+28h],ax
  00425302  6601462a              add       [esi+2Ah],ax
  00425306  6601462c              add       [esi+2Ch],ax
  0042530a  83c014                add       eax,14h
  0042530d  663de803              cmp       ax,3E8h
  00425311  884e39                mov       [esi+39h],cl
  00425314  88563a                mov       [esi+3Ah],dl
  00425317  66894612              mov       [esi+12h],ax
  0042531b  7c06                  jl        short 00425323h
  0042531d  66c74612e803          mov       word [esi+12h],3E8h

LAB_00425323:
  00425323  b830750000            mov       eax,7530h
  00425328  66394628              cmp       [esi+28h],ax
  0042532c  7c15                  jl        short 00425343h
  0042532e  66894628              mov       [esi+28h],ax
  00425332  6689462a              mov       [esi+2Ah],ax
  00425336  6689462c              mov       [esi+2Ch],ax
  0042533a  8b06                  mov       eax,[esi]
  0042533c  25ffffff7f            and       eax,7FFFFFFFh
  00425341  8906                  mov       [esi],eax

LAB_00425343:
  00425343  5e                    pop       esi
  00425344  5b                    pop       ebx
  00425345  c3                    ret

4 cible(s) de saut interne : 0x004252cd, 0x004252f7, 0x00425323, 0x00425343
