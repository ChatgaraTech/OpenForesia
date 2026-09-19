Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004868e0  section .text  offset fichier 0x85ce0  240 octets

  004868e0  83ec0c                sub       esp,0Ch
  004868e3  53                    push      ebx
  004868e4  56                    push      esi
  004868e5  8b742418              mov       esi,[esp+18h]
  004868e9  57                    push      edi
  004868ea  66837e1000            cmp       word [esi+10h],0
  004868ef  8b7e6c                mov       edi,[esi+6Ch]
  004868f2  752d                  jne       short 00486921h
  004868f4  8b4644                mov       eax,[esi+44h]
  004868f7  8b4e4c                mov       ecx,[esi+4Ch]
  004868fa  f7d8                  neg       eax
  004868fc  f7d9                  neg       ecx
  004868fe  897e74                mov       [esi+74h],edi
  00486901  897e70                mov       [esi+70h],edi
  00486904  c7462800040008        mov       dword [esi+28h],8000400h
  0048690b  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00486912  66c7869a0000000000    mov       word [esi+9Ah],0
  0048691b  894644                mov       [esi+44h],eax
  0048691e  894e4c                mov       [esi+4Ch],ecx

LAB_00486921:
  00486921  8d54240c              lea       edx,[esp+0Ch]
  00486925  8d4644                lea       eax,[esi+44h]
  00486928  52                    push      edx
  00486929  8d4f30                lea       ecx,[edi+30h]
  0048692c  50                    push      eax
  0048692d  51                    push      ecx
  0048692e  e85df70a00            call      00536090h
  00486933  8b571c                mov       edx,[edi+1Ch]
  00486936  8b442418              mov       eax,[esp+18h]
  0048693a  8b4c241c              mov       ecx,[esp+1Ch]
  0048693e  8d5e1c                lea       ebx,[esi+1Ch]
  00486941  03d0                  add       edx,eax
  00486943  83c40c                add       esp,0Ch
  00486946  8913                  mov       [ebx],edx
  00486948  8b4720                mov       eax,[edi+20h]
  0048694b  8b542414              mov       edx,[esp+14h]
  0048694f  03c1                  add       eax,ecx
  00486951  894620                mov       [esi+20h],eax
  00486954  8b4f24                mov       ecx,[edi+24h]
  00486957  0fbf4634              movsx     eax,word [esi+34h]
  0048695b  03ca                  add       ecx,edx
  0048695d  8b565c                mov       edx,[esi+5Ch]
  00486960  894e24                mov       [esi+24h],ecx
  00486963  52                    push      edx
  00486964  0fbf4f32              movsx     ecx,word [edi+32h]
  00486968  50                    push      eax
  00486969  8d5650                lea       edx,[esi+50h]
  0048696c  51                    push      ecx
  0048696d  52                    push      edx
  0048696e  e85dc10a00            call      00532AD0h
  00486973  0fbf7e34              movsx     edi,word [esi+34h]
  00486977  66c746340000          mov       word [esi+34h],0
  0048697d  a1cc975e00            mov       eax,[5E97CCh]
  00486982  83c410                add       esp,10h
  00486985  8b88d4000000          mov       ecx,[eax+0D4h]
  0048698b  33c0                  xor       eax,eax
  0048698d  6a04                  push      4
  0048698f  6a00                  push      0
  00486991  8b11                  mov       edx,[ecx]
  00486993  8a420a                mov       al,[edx+0Ah]
  00486996  05c0000000            add       eax,0C0h
  0048699b  50                    push      eax
  0048699c  68101a5500            push      551A10h
  004869a1  53                    push      ebx
  004869a2  56                    push      esi
  004869a3  e89842ffff            call      0047AC40h
  004869a8  83c418                add       esp,18h
  004869ab  66ff4610              inc       word [esi+10h]
  004869af  66837e1014            cmp       word [esi+10h],14h
  004869b4  66897e34              mov       [esi+34h],di
  004869b8  7c06                  jl        short 004869C0h
  004869ba  c70600000000          mov       dword [esi],0

LAB_004869c0:
  004869c0  5f                    pop       edi
  004869c1  5e                    pop       esi
  004869c2  5b                    pop       ebx
  004869c3  83c40c                add       esp,0Ch
  004869c6  c3                    ret
  004869c7  90                    nop
  004869c8  90                    nop
  004869c9  90                    nop
  004869ca  90                    nop
  004869cb  90                    nop
  004869cc  90                    nop
  004869cd  90                    nop
  004869ce  90                    nop
  004869cf  90                    nop

2 cible(s) de saut interne : 0x00486921, 0x004869c0
