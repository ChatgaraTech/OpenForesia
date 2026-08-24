Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00512ef0  section .text  offset fichier 0x1122f0  248 octets

  00512ef0  83ec14                sub       esp,14h
  00512ef3  56                    push      esi
  00512ef4  8b74241c              mov       esi,[esp+1Ch]
  00512ef8  57                    push      edi
  00512ef9  68d8315600            push      5631D8h
  00512efe  56                    push      esi
  00512eff  89355cb85d00          mov       [5DB85Ch],esi
  00512f05  ff15f0856000          call      dword [6085F0h]
  00512f0b  33c0                  xor       eax,eax
  00512f0d  b932000000            mov       ecx,32h
  00512f12  bf58b75d00            mov       edi,5DB758h
  00512f17  50                    push      eax
  00512f18  68e4b85d00            push      5DB8E4h
  00512f1d  50                    push      eax
  00512f1e  f3ab                  rep stosd
  00512f20  e8432d0300            call      00545C68h
  00512f25  a1e4b85d00            mov       eax,[5DB8E4h]
  00512f2a  85c0                  test      eax,eax
  00512f2c  0f84b6000000          je        near 00512FE8h
  00512f32  33c9                  xor       ecx,ecx
  00512f34  33d2                  xor       edx,edx
  00512f36  890d88b85d00          mov       [5DB888h],ecx
  00512f3c  89542408              mov       [esp+8],edx
  00512f40  890d8cb85d00          mov       [5DB88Ch],ecx
  00512f46  8954240c              mov       [esp+0Ch],edx
  00512f4a  890d90b85d00          mov       [5DB890h],ecx
  00512f50  89542410              mov       [esp+10h],edx
  00512f54  890d94b85d00          mov       [5DB894h],ecx
  00512f5a  6a02                  push      2
  00512f5c  66890d98b85d00        mov       [5DB898h],cx
  00512f63  b901000000            mov       ecx,1
  00512f68  66890d88b85d00        mov       [5DB888h],cx
  00512f6f  894c2410              mov       [esp+10h],ecx
  00512f73  8b08                  mov       ecx,[eax]
  00512f75  89542418              mov       [esp+18h],edx
  00512f79  56                    push      esi
  00512f7a  50                    push      eax
  00512f7b  66c7058ab85d000200    mov       word [5DB88Ah],2
  00512f84  c7058cb85d0022560000  mov       dword [5DB88Ch],5622h
  00512f8e  66c70594b85d000400    mov       word [5DB894h],4
  00512f97  c70590b85d0088580100  mov       dword [5DB890h],15888h
  00512fa1  66c70596b85d001000    mov       word [5DB896h],10h
  00512faa  89542424              mov       [esp+24h],edx
  00512fae  c744241414000000      mov       dword [esp+14h],14h
  00512fb6  ff5118                call      dword [ecx+18h]
  00512fb9  85c0                  test      eax,eax
  00512fbb  7c2b                  jl        short 00512FE8h
  00512fbd  a1e4b85d00            mov       eax,[5DB8E4h]
  00512fc2  6a00                  push      0
  00512fc4  8d4c240c              lea       ecx,[esp+0Ch]
  00512fc8  6854b75d00            push      5DB754h
  00512fcd  8b10                  mov       edx,[eax]
  00512fcf  51                    push      ecx
  00512fd0  50                    push      eax
  00512fd1  ff520c                call      dword [edx+0Ch]
  00512fd4  85c0                  test      eax,eax
  00512fd6  7c10                  jl        short 00512FE8h
  00512fd8  a154b75d00            mov       eax,[5DB754h]
  00512fdd  6888b85d00            push      5DB888h
  00512fe2  50                    push      eax
  00512fe3  8b10                  mov       edx,[eax]
  00512fe5  ff5238                call      dword [edx+38h]
