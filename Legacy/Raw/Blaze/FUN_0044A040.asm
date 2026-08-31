Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a040  section .text  offset fichier 0x49440  61 octets

  0044a040  56                    push      esi
  0044a041  8b74240c              mov       esi,[esp+0Ch]
  0044a045  57                    push      edi
  0044a046  8b7c240c              mov       edi,[esp+0Ch]
  0044a04a  56                    push      esi
  0044a04b  57                    push      edi
  0044a04c  e85fc4feff            call      004364B0h
  0044a051  83c408                add       esp,8
  0044a054  6685c0                test      ax,ax
  0044a057  751d                  jne       short 0044A076h
  0044a059  8b442414              mov       eax,[esp+14h]
  0044a05d  50                    push      eax
  0044a05e  56                    push      esi
  0044a05f  57                    push      edi
  0044a060  e8cb270f00            call      0053C830h
  0044a065  83c40c                add       esp,0Ch
  0044a068  33c9                  xor       ecx,ecx
  0044a06a  6685c0                test      ax,ax
  0044a06d  0f95c1                setne     cl
  0044a070  668bc1                mov       ax,cx
  0044a073  5f                    pop       edi
  0044a074  5e                    pop       esi
  0044a075  c3                    ret

LAB_0044a076:
  0044a076  5f                    pop       edi
  0044a077  66b80100              mov       ax,1
  0044a07b  5e                    pop       esi
  0044a07c  c3                    ret

1 cible(s) de saut interne : 0x0044a076
