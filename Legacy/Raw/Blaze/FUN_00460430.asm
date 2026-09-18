C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460430  section .text  offset fichier 0x5f830  641 octets

  00460430  83ec0c                sub       esp,0Ch
  00460433  53                    push      ebx
  00460434  55                    push      ebp
  00460435  56                    push      esi
  00460436  8b74241c              mov       esi,[esp+1Ch]
  0046043a  57                    push      edi
  0046043b  33ff                  xor       edi,edi
  0046043d  66397e10              cmp       [esi+10h],di
  00460441  750d                  jne       short 00460450h
  00460443  66c746100100          mov       word [esi+10h],1
  00460449  6689be98000000        mov       [esi+98h],di

LAB_00460450:
  00460450  6639be98000000        cmp       [esi+98h],di
  00460457  752a                  jne       short 00460483h
  00460459  a1cc975e00            mov       eax,[5E97CCh]
  0046045e  6800080000            push      800h
  00460463  6800080000            push      800h
  00460468  6800070000            push      700h
  0046046d  8b88a8000000          mov       ecx,[eax+0A8h]
  00460473  33c0                  xor       eax,eax
  00460475  8b11                  mov       edx,[ecx]
  00460477  8a4217                mov       al,[edx+17h]
  0046047a  50                    push      eax
  0046047b  e820ca0d00            call      0053CEA0h
  00460480  83c410                add       esp,10h

LAB_00460483:
  00460483  66ff8698000000        inc       word [esi+98h]
  0046048a  6683be9800000014      cmp       word [esi+98h],14h
  00460492  7e07                  jle       short 0046049Bh
  00460494  6689be98000000        mov       [esi+98h],di

LAB_0046049b:
  0046049b  8b4e1c                mov       ecx,[esi+1Ch]
  0046049e  8b5620                mov       edx,[esi+20h]
  004604a1  8b4624                mov       eax,[esi+24h]
  004604a4  57                    push      edi
  004604a5  57                    push      edi
  004604a6  6aff                  push      0FFFFFFFFh
  004604a8  894c242c              mov       [esp+2Ch],ecx
  004604ac  668b4e32              mov       cx,[esi+32h]
  004604b0  57                    push      edi
  004604b1  bd00405100            mov       ebp,514000h
  004604b6  bb00040000            mov       ebx,400h
  004604bb  6a3b                  push      3Bh
  004604bd  56                    push      esi
  004604be  89542428              mov       [esp+28h],edx
  004604c2  8944242c              mov       [esp+2Ch],eax
  004604c6  66894c2430            mov       [esp+30h],cx
  004604cb  66897e14              mov       [esi+14h],di
  004604cf  896e1c                mov       [esi+1Ch],ebp
  004604d2  c7462000a0fbff        mov       dword [esi+20h],0FFFBA000h
  004604d9  c746240040caff        mov       dword [esi+24h],0FFCA4000h
  004604e0  66895e32              mov       [esi+32h],bx
  004604e4  e8277c0000            call      00468110h
  004604e9  83c418                add       esp,18h
  004604ec  bf000c0000            mov       edi,0C00h
  004604f1  66897e32              mov       [esi+32h],di
  004604f5  6a00                  push      0
  004604f7  6a00                  push      0
  004604f9  6aff                  push      0FFFFFFFFh
  004604fb  6a00                  push      0
  004604fd  6a3b                  push      3Bh
  004604ff  56                    push      esi
  00460500  e80b7c0000            call      00468110h
  00460505  83c418                add       esp,18h
  00460508  896e1c                mov       [esi+1Ch],ebp
  0046050b  bd00c0f9ff            mov       ebp,0FFF9C000h
  00460510  c746240020dbff        mov       dword [esi+24h],0FFDB2000h
  00460517  6a00                  push      0
  00460519  6a00                  push      0
  0046051b  6aff                  push      0FFFFFFFFh
  0046051d  6a00                  push      0
  0046051f  6a3b                  push      3Bh
  00460521  56                    push      esi
  00460522  896e20                mov       [esi+20h],ebp
  00460525  66895e32              mov       [esi+32h],bx
  00460529  e8e27b0000            call      00468110h
  0046052e  83c418                add       esp,18h
  00460531  66897e32              mov       [esi+32h],di
  00460535  6a00                  push      0
  00460537  6a00                  push      0
  00460539  6aff                  push      0FFFFFFFFh
  0046053b  6a00                  push      0
  0046053d  6a3b                  push      3Bh
  0046053f  56                    push      esi
  00460540  e8cb7b0000            call      00468110h
  00460545  83c418                add       esp,18h
  00460548  c7461c00c07cff        mov       dword [esi+1Ch],0FF7CC000h
  0046054f  896e20                mov       [esi+20h],ebp
  00460552  c7462400001900        mov       dword [esi+24h],190000h
  00460559  6a00                  push      0
  0046055b  6a00                  push      0
  0046055d  6aff                  push      0FFFFFFFFh
  0046055f  6a00                  push      0
  00460561  6a3b                  push      3Bh
  00460563  56                    push      esi
  00460564  66895e32              mov       [esi+32h],bx
  00460568  e8a37b0000            call      00468110h
  0046056d  83c418                add       esp,18h
  00460570  66897e32              mov       [esi+32h],di
  00460574  6a00                  push      0
  00460576  6a00                  push      0
  00460578  6aff                  push      0FFFFFFFFh
  0046057a  6a00                  push      0
  0046057c  6a3b                  push      3Bh
  0046057e  56                    push      esi
  0046057f  e88c7b0000            call      00468110h
  00460584  83c418                add       esp,18h
  00460587  c7461c00405100        mov       dword [esi+1Ch],514000h
  0046058e  896e20                mov       [esi+20h],ebp
  00460591  c7462400c09400        mov       dword [esi+24h],94C000h
  00460598  66895e32              mov       [esi+32h],bx
  0046059c  6a00                  push      0
  0046059e  6a00                  push      0
  004605a0  6aff                  push      0FFFFFFFFh
  004605a2  6a00                  push      0
  004605a4  6a3b                  push      3Bh
  004605a6  56                    push      esi
  004605a7  e8647b0000            call      00468110h
  004605ac  83c418                add       esp,18h
  004605af  66897e32              mov       [esi+32h],di
  004605b3  6a00                  push      0
  004605b5  6a00                  push      0
  004605b7  6aff                  push      0FFFFFFFFh
  004605b9  6a00                  push      0
  004605bb  6a3b                  push      3Bh
  004605bd  56                    push      esi
  004605be  e84d7b0000            call      00468110h
  004605c3  83c418                add       esp,18h
  004605c6  c7461c00405100        mov       dword [esi+1Ch],514000h
  004605cd  c7462000a0fbff        mov       dword [esi+20h],0FFFBA000h
  004605d4  c746240040a600        mov       dword [esi+24h],0A64000h
  004605db  6a00                  push      0
  004605dd  6a00                  push      0
  004605df  6aff                  push      0FFFFFFFFh
  004605e1  6a00                  push      0
  004605e3  6a3b                  push      3Bh
  004605e5  56                    push      esi
  004605e6  66895e32              mov       [esi+32h],bx
  004605ea  e8217b0000            call      00468110h
  004605ef  83c418                add       esp,18h
  004605f2  66897e32              mov       [esi+32h],di
  004605f6  6a00                  push      0
  004605f8  6a00                  push      0
  004605fa  6aff                  push      0FFFFFFFFh
  004605fc  6a00                  push      0
  004605fe  6a3b                  push      3Bh
  00460600  56                    push      esi
  00460601  e80a7b0000            call      00468110h
  00460606  83c418                add       esp,18h
  00460609  c7461c00e04200        mov       dword [esi+1Ch],42E000h
  00460610  896e20                mov       [esi+20h],ebp
  00460613  c7462400c04400        mov       dword [esi+24h],44C000h
  0046061a  6a00                  push      0
  0046061c  6a00                  push      0
  0046061e  6aff                  push      0FFFFFFFFh
  00460620  6a00                  push      0
  00460622  6a3b                  push      3Bh
  00460624  56                    push      esi
  00460625  66c746320008          mov       word [esi+32h],800h
  0046062b  e8e07a0000            call      00468110h
  00460630  83c418                add       esp,18h
  00460633  66c746320000          mov       word [esi+32h],0
  00460639  6a00                  push      0
  0046063b  6a00                  push      0
  0046063d  6aff                  push      0FFFFFFFFh
  0046063f  6a00                  push      0
  00460641  6a3b                  push      3Bh
  00460643  56                    push      esi
  00460644  e8c77a0000            call      00468110h
  00460649  83c418                add       esp,18h
  0046064c  c7461c0040d4ff        mov       dword [esi+1Ch],0FFD44000h
  00460653  896e20                mov       [esi+20h],ebp
  00460656  c7462400403800        mov       dword [esi+24h],384000h
  0046065d  6a00                  push      0
  0046065f  6a00                  push      0
  00460661  6aff                  push      0FFFFFFFFh
  00460663  6a00                  push      0
  00460665  6a3b                  push      3Bh
  00460667  56                    push      esi
  00460668  66895e32              mov       [esi+32h],bx
  0046066c  e89f7a0000            call      00468110h
  00460671  83c418                add       esp,18h
  00460674  66897e32              mov       [esi+32h],di
  00460678  6a00                  push      0
  0046067a  6a00                  push      0
  0046067c  6aff                  push      0FFFFFFFFh
  0046067e  6a00                  push      0
  00460680  6a3b                  push      3Bh
  00460682  56                    push      esi
  00460683  e8887a0000            call      00468110h
  00460688  8b542438              mov       edx,[esp+38h]
  0046068c  83c418                add       esp,18h
  0046068f  89561c                mov       [esi+1Ch],edx
  00460692  8b442410              mov       eax,[esp+10h]
  00460696  8b4c2414              mov       ecx,[esp+14h]
  0046069a  668b542418            mov       dx,[esp+18h]
  0046069f  894620                mov       [esi+20h],eax
  004606a2  894e24                mov       [esi+24h],ecx
  004606a5  66895632              mov       [esi+32h],dx
  004606a9  5f                    pop       edi
  004606aa  5e                    pop       esi
  004606ab  5d                    pop       ebp
  004606ac  5b                    pop       ebx
  004606ad  83c40c                add       esp,0Ch
  004606b0  c3                    ret

3 cible(s) de saut interne : 0x00460450, 0x00460483, 0x0046049b
