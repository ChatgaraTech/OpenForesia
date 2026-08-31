Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448210  section .text  offset fichier 0x47610  61 octets

  00448210  56                    push      esi
  00448211  8b74240c              mov       esi,[esp+0Ch]
  00448215  57                    push      edi
  00448216  8b7c240c              mov       edi,[esp+0Ch]
  0044821a  56                    push      esi
  0044821b  57                    push      edi
  0044821c  e88fe2feff            call      004364B0h
  00448221  83c408                add       esp,8
  00448224  6685c0                test      ax,ax
  00448227  751d                  jne       short 00448246h
  00448229  8b442414              mov       eax,[esp+14h]
  0044822d  50                    push      eax
  0044822e  56                    push      esi
  0044822f  57                    push      edi
  00448230  e89b390f00            call      0053BBD0h
  00448235  83c40c                add       esp,0Ch
  00448238  33c9                  xor       ecx,ecx
  0044823a  6685c0                test      ax,ax
  0044823d  0f95c1                setne     cl
  00448240  668bc1                mov       ax,cx
  00448243  5f                    pop       edi
  00448244  5e                    pop       esi
  00448245  c3                    ret

LAB_00448246:
  00448246  5f                    pop       edi
  00448247  66b80100              mov       ax,1
  0044824b  5e                    pop       esi
  0044824c  c3                    ret

1 cible(s) de saut interne : 0x00448246
