Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004491b0  section .text  offset fichier 0x485b0  61 octets

  004491b0  56                    push      esi
  004491b1  8b74240c              mov       esi,[esp+0Ch]
  004491b5  57                    push      edi
  004491b6  8b7c240c              mov       edi,[esp+0Ch]
  004491ba  56                    push      esi
  004491bb  57                    push      edi
  004491bc  e8efd2feff            call      004364B0h
  004491c1  83c408                add       esp,8
  004491c4  6685c0                test      ax,ax
  004491c7  751d                  jne       short 004491E6h
  004491c9  8b442414              mov       eax,[esp+14h]
  004491cd  50                    push      eax
  004491ce  56                    push      esi
  004491cf  57                    push      edi
  004491d0  e8bb1e0f00            call      0053B090h
  004491d5  83c40c                add       esp,0Ch
  004491d8  33c9                  xor       ecx,ecx
  004491da  6685c0                test      ax,ax
  004491dd  0f95c1                setne     cl
  004491e0  668bc1                mov       ax,cx
  004491e3  5f                    pop       edi
  004491e4  5e                    pop       esi
  004491e5  c3                    ret

LAB_004491e6:
  004491e6  5f                    pop       edi
  004491e7  66b80100              mov       ax,1
  004491eb  5e                    pop       esi
  004491ec  c3                    ret

1 cible(s) de saut interne : 0x004491e6
