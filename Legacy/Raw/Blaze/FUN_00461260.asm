C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00461260  section .text  offset fichier 0x60660  301 octets

  00461260  a16ce05d00            mov       eax,[5DE06Ch]
  00461265  53                    push      ebx
  00461266  55                    push      ebp
  00461267  56                    push      esi
  00461268  3d00c0e0ff            cmp       eax,0FFE0C000h
  0046126d  57                    push      edi
  0046126e  0f8c14010000          jl        near 00461388h
  00461274  8b742414              mov       esi,[esp+14h]
  00461278  668b4632              mov       ax,[esi+32h]
  0046127c  8b7e1c                mov       edi,[esi+1Ch]
  0046127f  8b5e20                mov       ebx,[esi+20h]
  00461282  8b6e24                mov       ebp,[esi+24h]
  00461285  6689442414            mov       [esp+14h],ax
  0046128a  33c0                  xor       eax,eax
  0046128c  50                    push      eax
  0046128d  50                    push      eax
  0046128e  6aff                  push      0FFFFFFFFh
  00461290  50                    push      eax
  00461291  6a3b                  push      3Bh
  00461293  56                    push      esi
  00461294  66894614              mov       [esi+14h],ax
  00461298  c7461c00c07600        mov       dword [esi+1Ch],76C000h
  0046129f  c746200060f0ff        mov       dword [esi+20h],0FFF06000h
  004612a6  c7462400a0b1fe        mov       dword [esi+24h],0FEB1A000h
  004612ad  66894632              mov       [esi+32h],ax
  004612b1  e85a6e0000            call      00468110h
  004612b6  83c418                add       esp,18h
  004612b9  c7461c00a08c00        mov       dword [esi+1Ch],8CA000h
  004612c0  c746200060f0ff        mov       dword [esi+20h],0FFF06000h
  004612c7  c7462400c0cdfe        mov       dword [esi+24h],0FECDC000h
  004612ce  6a00                  push      0
  004612d0  6a00                  push      0
  004612d2  6aff                  push      0FFFFFFFFh
  004612d4  6a00                  push      0
  004612d6  6a3b                  push      3Bh
  004612d8  56                    push      esi
  004612d9  66c746320004          mov       word [esi+32h],400h
  004612df  e82c6e0000            call      00468110h
  004612e4  83c418                add       esp,18h
  004612e7  c7461c00a08c00        mov       dword [esi+1Ch],8CA000h
  004612ee  c746200060f0ff        mov       dword [esi+20h],0FFF06000h
  004612f5  c7462400c09bfe        mov       dword [esi+24h],0FE9BC000h
  004612fc  6a00                  push      0
  004612fe  6a00                  push      0
  00461300  6aff                  push      0FFFFFFFFh
  00461302  6a00                  push      0
  00461304  6a3b                  push      3Bh
  00461306  56                    push      esi
  00461307  66c746320004          mov       word [esi+32h],400h
  0046130d  e8fe6d0000            call      00468110h
  00461312  668b4c242c            mov       cx,[esp+2Ch]
  00461317  897e1c                mov       [esi+1Ch],edi
  0046131a  33ff                  xor       edi,edi
  0046131c  83c418                add       esp,18h
  0046131f  66397e10              cmp       [esi+10h],di
  00461323  895e20                mov       [esi+20h],ebx
  00461326  896e24                mov       [esi+24h],ebp
  00461329  66894e32              mov       [esi+32h],cx
  0046132d  750d                  jne       short 0046133Ch
  0046132f  66c746100100          mov       word [esi+10h],1
  00461335  6689be98000000        mov       [esi+98h],di

LAB_0046133c:
  0046133c  6639be98000000        cmp       [esi+98h],di
  00461343  752b                  jne       short 00461370h
  00461345  8b15cc975e00          mov       edx,[5E97CCh]
  0046134b  6800080000            push      800h
  00461350  6800080000            push      800h
  00461355  6800050000            push      500h
  0046135a  8b82a8000000          mov       eax,[edx+0A8h]
  00461360  33d2                  xor       edx,edx
  00461362  8b08                  mov       ecx,[eax]
  00461364  8a5117                mov       dl,[ecx+17h]
  00461367  52                    push      edx
  00461368  e833bb0d00            call      0053CEA0h
  0046136d  83c410                add       esp,10h

LAB_00461370:
  00461370  66ff8698000000        inc       word [esi+98h]
  00461377  6683be9800000014      cmp       word [esi+98h],14h
  0046137f  7507                  jne       short 00461388h
  00461381  6689be98000000        mov       [esi+98h],di

LAB_00461388:
  00461388  5f                    pop       edi
  00461389  5e                    pop       esi
  0046138a  5d                    pop       ebp
  0046138b  5b                    pop       ebx
  0046138c  c3                    ret

3 cible(s) de saut interne : 0x0046133c, 0x00461370, 0x00461388
