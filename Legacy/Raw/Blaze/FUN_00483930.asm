Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00483930  section .text  offset fichier 0x82d30  256 octets

  00483930  83ec0c                sub       esp,0Ch
  00483933  53                    push      ebx
  00483934  56                    push      esi
  00483935  8b742418              mov       esi,[esp+18h]
  00483939  33db                  xor       ebx,ebx
  0048393b  57                    push      edi
  0048393c  66395e10              cmp       [esi+10h],bx
  00483940  8b7e6c                mov       edi,[esi+6Ch]
  00483943  7555                  jne       short 0048399Ah
  00483945  8b4644                mov       eax,[esi+44h]
  00483948  8b4e4c                mov       ecx,[esi+4Ch]
  0048394b  668146320004          add       word [esi+32h],400h
  00483951  8d561c                lea       edx,[esi+1Ch]
  00483954  f7d8                  neg       eax
  00483956  f7d9                  neg       ecx
  00483958  52                    push      edx
  00483959  6860265500            push      552660h
  0048395e  897e74                mov       [esi+74h],edi
  00483961  897e70                mov       [esi+70h],edi
  00483964  66c746360400          mov       word [esi+36h],4
  0048396a  c6460407              mov       byte [esi+4],7
  0048396e  895e28                mov       [esi+28h],ebx
  00483971  66895e2c              mov       [esi+2Ch],bx
  00483975  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0048397c  66899e9a000000        mov       [esi+9Ah],bx
  00483983  66c746348003          mov       word [esi+34h],380h
  00483989  894644                mov       [esi+44h],eax
  0048398c  894e4c                mov       [esi+4Ch],ecx
  0048398f  885e09                mov       [esi+9],bl
  00483992  e849a20b00            call      0053DBE0h
  00483997  83c408                add       esp,8

LAB_0048399a:
  0048399a  f6461001              test      byte [esi+10h],1
  0048399e  7477                  je        short 00483A17h
  004839a0  8d44240c              lea       eax,[esp+0Ch]
  004839a4  8d4e44                lea       ecx,[esi+44h]
  004839a7  50                    push      eax
  004839a8  8d5730                lea       edx,[edi+30h]
  004839ab  51                    push      ecx
  004839ac  52                    push      edx
  004839ad  e8de260b00            call      00536090h
  004839b2  8b471c                mov       eax,[edi+1Ch]
  004839b5  8b4c2418              mov       ecx,[esp+18h]
  004839b9  8b54241c              mov       edx,[esp+1Ch]
  004839bd  03c1                  add       eax,ecx
  004839bf  89461c                mov       [esi+1Ch],eax
  004839c2  8b4f20                mov       ecx,[edi+20h]
  004839c5  03ca                  add       ecx,edx
  004839c7  83c40c                add       esp,0Ch
  004839ca  894e20                mov       [esi+20h],ecx
  004839cd  8b5724                mov       edx,[edi+24h]
  004839d0  8b4c2414              mov       ecx,[esp+14h]
  004839d4  6a64                  push      64h
  004839d6  03d1                  add       edx,ecx
  004839d8  68800f0000            push      0F80h
  004839dd  895624                mov       [esi+24h],edx
  004839e0  8d4e50                lea       ecx,[esi+50h]
  004839e3  0fbf4732              movsx     eax,word [edi+32h]
  004839e7  50                    push      eax
  004839e8  51                    push      ecx
  004839e9  e8e2f00a00            call      00532AD0h
  004839ee  8b15cc975e00          mov       edx,[5E97CCh]
  004839f4  83c410                add       esp,10h
  004839f7  8b82d0000000          mov       eax,[edx+0D0h]
  004839fd  33d2                  xor       edx,edx
  004839ff  6a02                  push      2
  00483a01  53                    push      ebx
  00483a02  8b08                  mov       ecx,[eax]
  00483a04  8a510b                mov       dl,[ecx+0Bh]
  00483a07  81c2c0000000          add       edx,0C0h
  00483a0d  52                    push      edx
  00483a0e  56                    push      esi
  00483a0f  e8cc70ffff            call      0047AAE0h
  00483a14  83c410                add       esp,10h

LAB_00483a17:
  00483a17  66ff4610              inc       word [esi+10h]
  00483a1b  66837e1008            cmp       word [esi+10h],8
  00483a20  7c02                  jl        short 00483A24h
  00483a22  891e                  mov       [esi],ebx

LAB_00483a24:
  00483a24  5f                    pop       edi
  00483a25  5e                    pop       esi
  00483a26  5b                    pop       ebx
  00483a27  83c40c                add       esp,0Ch
  00483a2a  c3                    ret
  00483a2b  90                    nop
  00483a2c  90                    nop
  00483a2d  90                    nop
  00483a2e  90                    nop
  00483a2f  90                    nop

3 cible(s) de saut interne : 0x0048399a, 0x00483a17, 0x00483a24
