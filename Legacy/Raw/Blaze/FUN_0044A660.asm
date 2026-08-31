Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a660  section .text  offset fichier 0x49a60  61 octets

  0044a660  56                    push      esi
  0044a661  8b74240c              mov       esi,[esp+0Ch]
  0044a665  57                    push      edi
  0044a666  8b7c240c              mov       edi,[esp+0Ch]
  0044a66a  56                    push      esi
  0044a66b  57                    push      edi
  0044a66c  e83fbefeff            call      004364B0h
  0044a671  83c408                add       esp,8
  0044a674  6685c0                test      ax,ax
  0044a677  751d                  jne       short 0044A696h
  0044a679  8b442414              mov       eax,[esp+14h]
  0044a67d  50                    push      eax
  0044a67e  56                    push      esi
  0044a67f  57                    push      edi
  0044a680  e89b260f00            call      0053CD20h
  0044a685  83c40c                add       esp,0Ch
  0044a688  33c9                  xor       ecx,ecx
  0044a68a  6685c0                test      ax,ax
  0044a68d  0f95c1                setne     cl
  0044a690  668bc1                mov       ax,cx
  0044a693  5f                    pop       edi
  0044a694  5e                    pop       esi
  0044a695  c3                    ret

LAB_0044a696:
  0044a696  5f                    pop       edi
  0044a697  66b80100              mov       ax,1
  0044a69b  5e                    pop       esi
  0044a69c  c3                    ret

1 cible(s) de saut interne : 0x0044a696
