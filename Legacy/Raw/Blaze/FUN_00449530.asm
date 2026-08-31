Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449530  section .text  offset fichier 0x48930  61 octets

  00449530  56                    push      esi
  00449531  8b74240c              mov       esi,[esp+0Ch]
  00449535  57                    push      edi
  00449536  8b7c240c              mov       edi,[esp+0Ch]
  0044953a  56                    push      esi
  0044953b  57                    push      edi
  0044953c  e86fcffeff            call      004364B0h
  00449541  83c408                add       esp,8
  00449544  6685c0                test      ax,ax
  00449547  751d                  jne       short 00449566h
  00449549  8b442414              mov       eax,[esp+14h]
  0044954d  50                    push      eax
  0044954e  56                    push      esi
  0044954f  57                    push      edi
  00449550  e8db350f00            call      0053CB30h
  00449555  83c40c                add       esp,0Ch
  00449558  33c9                  xor       ecx,ecx
  0044955a  6685c0                test      ax,ax
  0044955d  0f95c1                setne     cl
  00449560  668bc1                mov       ax,cx
  00449563  5f                    pop       edi
  00449564  5e                    pop       esi
  00449565  c3                    ret

LAB_00449566:
  00449566  5f                    pop       edi
  00449567  66b80100              mov       ax,1
  0044956b  5e                    pop       esi
  0044956c  c3                    ret

1 cible(s) de saut interne : 0x00449566
