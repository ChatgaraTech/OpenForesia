Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449720  section .text  offset fichier 0x48b20  61 octets

  00449720  56                    push      esi
  00449721  8b74240c              mov       esi,[esp+0Ch]
  00449725  57                    push      edi
  00449726  8b7c240c              mov       edi,[esp+0Ch]
  0044972a  56                    push      esi
  0044972b  57                    push      edi
  0044972c  e87fcdfeff            call      004364B0h
  00449731  83c408                add       esp,8
  00449734  6685c0                test      ax,ax
  00449737  751d                  jne       short 00449756h
  00449739  8b442414              mov       eax,[esp+14h]
  0044973d  50                    push      eax
  0044973e  56                    push      esi
  0044973f  57                    push      edi
  00449740  e8fb2f0f00            call      0053C740h
  00449745  83c40c                add       esp,0Ch
  00449748  33c9                  xor       ecx,ecx
  0044974a  6685c0                test      ax,ax
  0044974d  0f95c1                setne     cl
  00449750  668bc1                mov       ax,cx
  00449753  5f                    pop       edi
  00449754  5e                    pop       esi
  00449755  c3                    ret

LAB_00449756:
  00449756  5f                    pop       edi
  00449757  66b80100              mov       ax,1
  0044975b  5e                    pop       esi
  0044975c  c3                    ret

1 cible(s) de saut interne : 0x00449756
