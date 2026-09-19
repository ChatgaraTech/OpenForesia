Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466c30  section .text  offset fichier 0x66030  64 octets

  00466c30  83ec08                sub       esp,8
  00466c33  e808050e00            call      00547140h
  00466c38  99                    cdq
  00466c39  b906000000            mov       ecx,6
  00466c3e  f7f9                  idiv      ecx
  00466c40  6685d2                test      dx,dx
  00466c43  751f                  jne       short 00466C64h
  00466c45  8b44240c              mov       eax,[esp+0Ch]
  00466c49  6aff                  push      0FFFFFFFFh
  00466c4b  6a00                  push      0
  00466c4d  8d542408              lea       edx,[esp+8]
  00466c51  68f3000000            push      0F3h
  00466c56  8d481c                lea       ecx,[eax+1Ch]
  00466c59  52                    push      edx
  00466c5a  51                    push      ecx
  00466c5b  50                    push      eax
  00466c5c  e8df3f0100            call      0047AC40h
  00466c61  83c418                add       esp,18h

LAB_00466c64:
  00466c64  83c408                add       esp,8
  00466c67  c3                    ret
  00466c68  90                    nop
  00466c69  90                    nop
  00466c6a  90                    nop
  00466c6b  90                    nop
  00466c6c  90                    nop
  00466c6d  90                    nop
  00466c6e  90                    nop
  00466c6f  90                    nop

1 cible(s) de saut interne : 0x00466c64
