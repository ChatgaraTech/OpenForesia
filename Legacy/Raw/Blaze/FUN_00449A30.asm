Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449a30  section .text  offset fichier 0x48e30  61 octets

  00449a30  56                    push      esi
  00449a31  8b74240c              mov       esi,[esp+0Ch]
  00449a35  57                    push      edi
  00449a36  8b7c240c              mov       edi,[esp+0Ch]
  00449a3a  56                    push      esi
  00449a3b  57                    push      edi
  00449a3c  e86fcafeff            call      004364B0h
  00449a41  83c408                add       esp,8
  00449a44  6685c0                test      ax,ax
  00449a47  751d                  jne       short 00449A66h
  00449a49  8b442414              mov       eax,[esp+14h]
  00449a4d  50                    push      eax
  00449a4e  56                    push      esi
  00449a4f  57                    push      edi
  00449a50  e83b2d0f00            call      0053C790h
  00449a55  83c40c                add       esp,0Ch
  00449a58  33c9                  xor       ecx,ecx
  00449a5a  6685c0                test      ax,ax
  00449a5d  0f95c1                setne     cl
  00449a60  668bc1                mov       ax,cx
  00449a63  5f                    pop       edi
  00449a64  5e                    pop       esi
  00449a65  c3                    ret

LAB_00449a66:
  00449a66  5f                    pop       edi
  00449a67  66b80100              mov       ax,1
  00449a6b  5e                    pop       esi
  00449a6c  c3                    ret

1 cible(s) de saut interne : 0x00449a66
