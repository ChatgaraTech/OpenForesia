Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044ae90  section .text  offset fichier 0x4a290  224 octets

  0044ae90  56                    push      esi
  0044ae91  8b742408              mov       esi,[esp+8]
  0044ae95  6a00                  push      0
  0044ae97  6a00                  push      0
  0044ae99  56                    push      esi
  0044ae9a  e8a1d1ffff            call      00448040h
  0044ae9f  83c40c                add       esp,0Ch
  0044aea2  66837e1000            cmp       word [esi+10h],0
  0044aea7  7538                  jne       short 0044AEE1h
  0044aea9  6a00                  push      0
  0044aeab  6a00                  push      0
  0044aead  56                    push      esi
  0044aeae  e8fdcdffff            call      00447CB0h
  0044aeb3  8b4674                mov       eax,[esi+74h]
  0044aeb6  83c40c                add       esp,0Ch
  0044aeb9  66837e1000            cmp       word [esi+10h],0
  0044aebe  894678                mov       [esi+78h],eax
  0044aec1  751e                  jne       short 0044AEE1h
  0044aec3  6aff                  push      0FFFFFFFFh
  0044aec5  6a00                  push      0
  0044aec7  688f000000            push      8Fh
  0044aecc  56                    push      esi
  0044aecd  66c746100100          mov       word [esi+10h],1
  0044aed3  e808fc0200            call      0047AAE0h
  0044aed8  83c410                add       esp,10h
  0044aedb  66c746160000          mov       word [esi+16h],0

LAB_0044aee1:
  0044aee1  66837e1001            cmp       word [esi+10h],1
  0044aee6  7564                  jne       short 0044AF4Ch
  0044aee8  66837e1600            cmp       word [esi+16h],0
  0044aeed  7519                  jne       short 0044AF08h
  0044aeef  6800100000            push      1000h
  0044aef4  6800100000            push      1000h
  0044aef9  6853060000            push      653h
  0044aefe  6a23                  push      23h
  0044af00  e89b1f0f00            call      0053CEA0h
  0044af05  83c410                add       esp,10h

LAB_0044af08:
  0044af08  66837e1601            cmp       word [esi+16h],1
  0044af0d  7519                  jne       short 0044AF28h
  0044af0f  6800100000            push      1000h
  0044af14  6800100000            push      1000h
  0044af19  6855010000            push      155h
  0044af1e  6a16                  push      16h
  0044af20  e87b1f0f00            call      0053CEA0h
  0044af25  83c410                add       esp,10h

LAB_0044af28:
  0044af28  66837e1602            cmp       word [esi+16h],2
  0044af2d  7519                  jne       short 0044AF48h
  0044af2f  6800100000            push      1000h
  0044af34  6800100000            push      1000h
  0044af39  6800010000            push      100h
  0044af3e  6a31                  push      31h
  0044af40  e85b1f0f00            call      0053CEA0h
  0044af45  83c410                add       esp,10h

LAB_0044af48:
  0044af48  66ff4616              inc       word [esi+16h]

LAB_0044af4c:
  0044af4c  66837e1002            cmp       word [esi+10h],2
  0044af51  750d                  jne       short 0044AF60h
  0044af53  6a00                  push      0
  0044af55  6a00                  push      0
  0044af57  56                    push      esi
  0044af58  e8c3d0ffff            call      00448020h
  0044af5d  83c40c                add       esp,0Ch

LAB_0044af60:
  0044af60  5e                    pop       esi
  0044af61  c3                    ret
  0044af62  90                    nop
  0044af63  90                    nop
  0044af64  90                    nop
  0044af65  90                    nop
  0044af66  90                    nop
  0044af67  90                    nop
  0044af68  90                    nop
  0044af69  90                    nop
  0044af6a  90                    nop
  0044af6b  90                    nop
  0044af6c  90                    nop
  0044af6d  90                    nop
  0044af6e  90                    nop
  0044af6f  90                    nop

6 cible(s) de saut interne : 0x0044aee1, 0x0044af08, 0x0044af28, 0x0044af48, 0x0044af4c, 0x0044af60
