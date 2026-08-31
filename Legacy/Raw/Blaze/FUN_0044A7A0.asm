Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a7a0  section .text  offset fichier 0x49ba0  224 octets

  0044a7a0  56                    push      esi
  0044a7a1  8b742408              mov       esi,[esp+8]
  0044a7a5  6a00                  push      0
  0044a7a7  6a00                  push      0
  0044a7a9  56                    push      esi
  0044a7aa  e891d8ffff            call      00448040h
  0044a7af  83c40c                add       esp,0Ch
  0044a7b2  66837e1000            cmp       word [esi+10h],0
  0044a7b7  7538                  jne       short 0044A7F1h
  0044a7b9  6a00                  push      0
  0044a7bb  6a00                  push      0
  0044a7bd  56                    push      esi
  0044a7be  e8edd4ffff            call      00447CB0h
  0044a7c3  8b4674                mov       eax,[esi+74h]
  0044a7c6  83c40c                add       esp,0Ch
  0044a7c9  66837e1000            cmp       word [esi+10h],0
  0044a7ce  894678                mov       [esi+78h],eax
  0044a7d1  751e                  jne       short 0044A7F1h
  0044a7d3  6aff                  push      0FFFFFFFFh
  0044a7d5  6a00                  push      0
  0044a7d7  688e000000            push      8Eh
  0044a7dc  56                    push      esi
  0044a7dd  66c746100100          mov       word [esi+10h],1
  0044a7e3  e8f8020300            call      0047AAE0h
  0044a7e8  83c410                add       esp,10h
  0044a7eb  66c746160000          mov       word [esi+16h],0

LAB_0044a7f1:
  0044a7f1  66837e1001            cmp       word [esi+10h],1
  0044a7f6  7564                  jne       short 0044A85Ch
  0044a7f8  66837e1600            cmp       word [esi+16h],0
  0044a7fd  7519                  jne       short 0044A818h
  0044a7ff  6800100000            push      1000h
  0044a804  6800100000            push      1000h
  0044a809  6853060000            push      653h
  0044a80e  6a23                  push      23h
  0044a810  e88b260f00            call      0053CEA0h
  0044a815  83c410                add       esp,10h

LAB_0044a818:
  0044a818  66837e1601            cmp       word [esi+16h],1
  0044a81d  7519                  jne       short 0044A838h
  0044a81f  6800100000            push      1000h
  0044a824  6800100000            push      1000h
  0044a829  6855010000            push      155h
  0044a82e  6a16                  push      16h
  0044a830  e86b260f00            call      0053CEA0h
  0044a835  83c410                add       esp,10h

LAB_0044a838:
  0044a838  66837e1602            cmp       word [esi+16h],2
  0044a83d  7519                  jne       short 0044A858h
  0044a83f  6800100000            push      1000h
  0044a844  6800100000            push      1000h
  0044a849  68ff020000            push      2FFh
  0044a84e  6a0f                  push      0Fh
  0044a850  e84b260f00            call      0053CEA0h
  0044a855  83c410                add       esp,10h

LAB_0044a858:
  0044a858  66ff4616              inc       word [esi+16h]

LAB_0044a85c:
  0044a85c  66837e1002            cmp       word [esi+10h],2
  0044a861  750d                  jne       short 0044A870h
  0044a863  6a00                  push      0
  0044a865  6a00                  push      0
  0044a867  56                    push      esi
  0044a868  e8b3d7ffff            call      00448020h
  0044a86d  83c40c                add       esp,0Ch

LAB_0044a870:
  0044a870  5e                    pop       esi
  0044a871  c3                    ret
  0044a872  90                    nop
  0044a873  90                    nop
  0044a874  90                    nop
  0044a875  90                    nop
  0044a876  90                    nop
  0044a877  90                    nop
  0044a878  90                    nop
  0044a879  90                    nop
  0044a87a  90                    nop
  0044a87b  90                    nop
  0044a87c  90                    nop
  0044a87d  90                    nop
  0044a87e  90                    nop
  0044a87f  90                    nop

6 cible(s) de saut interne : 0x0044a7f1, 0x0044a818, 0x0044a838, 0x0044a858, 0x0044a85c, 0x0044a870
