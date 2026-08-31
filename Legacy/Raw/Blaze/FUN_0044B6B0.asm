Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044b6b0  section .text  offset fichier 0x4aab0  61 octets

  0044b6b0  56                    push      esi
  0044b6b1  8b74240c              mov       esi,[esp+0Ch]
  0044b6b5  57                    push      edi
  0044b6b6  8b7c240c              mov       edi,[esp+0Ch]
  0044b6ba  56                    push      esi
  0044b6bb  57                    push      edi
  0044b6bc  e8efadfeff            call      004364B0h
  0044b6c1  83c408                add       esp,8
  0044b6c4  6685c0                test      ax,ax
  0044b6c7  751d                  jne       short 0044B6E6h
  0044b6c9  8b442414              mov       eax,[esp+14h]
  0044b6cd  50                    push      eax
  0044b6ce  56                    push      esi
  0044b6cf  57                    push      edi
  0044b6d0  e8bb110f00            call      0053C890h
  0044b6d5  83c40c                add       esp,0Ch
  0044b6d8  33c9                  xor       ecx,ecx
  0044b6da  6685c0                test      ax,ax
  0044b6dd  0f95c1                setne     cl
  0044b6e0  668bc1                mov       ax,cx
  0044b6e3  5f                    pop       edi
  0044b6e4  5e                    pop       esi
  0044b6e5  c3                    ret

LAB_0044b6e6:
  0044b6e6  5f                    pop       edi
  0044b6e7  66b80100              mov       ax,1
  0044b6eb  5e                    pop       esi
  0044b6ec  c3                    ret

1 cible(s) de saut interne : 0x0044b6e6
