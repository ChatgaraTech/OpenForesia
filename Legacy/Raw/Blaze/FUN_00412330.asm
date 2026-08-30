; 0x00412330 — la braise qui monte, famille A (78 instructions, absente de Blaze.c).
; Copies : 0x00415860 (planche 0x0054f9e8) ; famille B : 0x00412810 / 0x00415e00 (+0x0a=2, vert et bleu -1 de plus).

C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00412330  section .text  offset fichier 0x11730  302 octets

  00412330  53                    push      ebx
  00412331  56                    push      esi
  00412332  8b74240c              mov       esi,[esp+0Ch]
  00412336  33db                  xor       ebx,ebx
  00412338  57                    push      edi
  00412339  bf01000000            mov       edi,1
  0041233e  66395e10              cmp       [esi+10h],bx
  00412342  0f8587000000          jne       near 004123CFh
  00412348  b800200000            mov       eax,2000h
  0041234d  66897e10              mov       [esi+10h],di
  00412351  c6460a09              mov       byte [esi+0Ah],9
  00412355  885e0b                mov       [esi+0Bh],bl
  00412358  c7463cf0f85400        mov       dword [esi+3Ch],54F8F0h
  0041235f  885e40                mov       [esi+40h],bl
  00412362  66894628              mov       [esi+28h],ax
  00412366  6689462a              mov       [esi+2Ah],ax
  0041236a  e8d14d1300            call      00547140h
  0041236f  99                    cdq
  00412370  33c2                  xor       eax,edx
  00412372  2bc2                  sub       eax,edx
  00412374  25ff0f0000            and       eax,0FFFh
  00412379  33c2                  xor       eax,edx
  0041237b  2bc2                  sub       eax,edx
  0041237d  66894612              mov       [esi+12h],ax
  00412381  e8ba4d1300            call      00547140h
  00412386  99                    cdq
  00412387  b90a000000            mov       ecx,0Ah
  0041238c  f7f9                  idiv      ecx
  0041238e  03d1                  add       edx,ecx
  00412390  66895614              mov       [esi+14h],dx
  00412394  e8a74d1300            call      00547140h
  00412399  99                    cdq
  0041239a  b905000000            mov       ecx,5
  0041239f  895e5c                mov       [esi+5Ch],ebx
  004123a2  f7f9                  idiv      ecx
  004123a4  8b4620                mov       eax,[esi+20h]
  004123a7  8b4e24                mov       ecx,[esi+24h]
  004123aa  894654                mov       [esi+54h],eax
  004123ad  b080                  mov       al,80h
  004123af  894e58                mov       [esi+58h],ecx
  004123b2  66899e9a000000        mov       [esi+9Ah],bx
  004123b9  884638                mov       [esi+38h],al
  004123bc  884639                mov       [esi+39h],al
  004123bf  88463a                mov       [esi+3Ah],al
  004123c2  83c203                add       edx,3
  004123c5  66895616              mov       [esi+16h],dx
  004123c9  8b561c                mov       edx,[esi+1Ch]
  004123cc  895650                mov       [esi+50h],edx

LAB_004123cf:
  004123cf  66397e10              cmp       [esi+10h],di
  004123d3  0f8581000000          jne       near 0041245Ah
  004123d9  0fbf5612              movsx     edx,word [esi+12h]
  004123dd  52                    push      edx
  004123de  e839361300            call      00545A1Ch
  004123e3  0faf465c              imul      eax,[esi+5Ch]
  004123e7  8b7e50                mov       edi,[esi+50h]
  004123ea  03c7                  add       eax,edi
  004123ec  8b7e5c                mov       edi,[esi+5Ch]
  004123ef  89461c                mov       [esi+1Ch],eax
  004123f2  0fbf4612              movsx     eax,word [esi+12h]
  004123f6  50                    push      eax
  004123f7  e81a361300            call      00545A16h
  004123fc  0fafc7                imul      eax,edi
  004123ff  8b4e58                mov       ecx,[esi+58h]
  00412402  8b5620                mov       edx,[esi+20h]
  00412405  03c1                  add       eax,ecx
  00412407  6afb                  push      0FFFFFFFBh
  00412409  0fbf4e16              movsx     ecx,word [esi+16h]
  0041240d  c1e10c                shl       ecx,0Ch
  00412410  03d1                  add       edx,ecx
  00412412  6afb                  push      0FFFFFFFBh
  00412414  895620                mov       [esi+20h],edx
  00412417  6afb                  push      0FFFFFFFBh
  00412419  0fbf5614              movsx     edx,word [esi+14h]
  0041241d  03d7                  add       edx,edi
  0041241f  66ff869a000000        inc       word [esi+9Ah]
  00412426  56                    push      esi
  00412427  894624                mov       [esi+24h],eax
  0041242a  89565c                mov       [esi+5Ch],edx
  0041242d  e8ae29ffff            call      00404DE0h
  00412432  83c410                add       esp,10h
  00412435  6683be9a00000003      cmp       word [esi+9Ah],3
  0041243d  7e1b                  jle       short 0041245Ah
  0041243f  8a4e40                mov       cl,[esi+40h]
  00412442  66899e9a000000        mov       [esi+9Ah],bx
  00412449  fec1                  inc       cl
  0041244b  8ac1                  mov       al,cl
  0041244d  884e40                mov       [esi+40h],cl
  00412450  3c04                  cmp       al,4
  00412452  7506                  jne       short 0041245Ah
  00412454  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041245a:
  0041245a  5f                    pop       edi
  0041245b  5e                    pop       esi
  0041245c  5b                    pop       ebx
  0041245d  c3                    ret

2 cible(s) de saut interne : 0x004123cf, 0x0041245a
