Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433050  section .text  offset fichier 0x32450  144 octets

  00433050  56                    push      esi
  00433051  8b742408              mov       esi,[esp+8]
  00433055  b801000000            mov       eax,1
  0043305a  66837e1000            cmp       word [esi+10h],0
  0043305f  750a                  jne       short 0043306Bh
  00433061  66894610              mov       [esi+10h],ax
  00433065  66c746121400          mov       word [esi+12h],14h

LAB_0043306b:
  0043306b  66394610              cmp       [esi+10h],ax
  0043306f  7529                  jne       short 0043309Ah
  00433071  66ff4e12              dec       word [esi+12h]
  00433075  6aff                  push      0FFFFFFFFh
  00433077  6a00                  push      0
  00433079  68fc000000            push      0FCh
  0043307e  8d461c                lea       eax,[esi+1Ch]
  00433081  6a00                  push      0
  00433083  50                    push      eax
  00433084  56                    push      esi
  00433085  e8b67b0400            call      0047AC40h
  0043308a  83c418                add       esp,18h
  0043308d  66837e1200            cmp       word [esi+12h],0
  00433092  7f06                  jg        short 0043309Ah
  00433094  66c746100200          mov       word [esi+10h],2

LAB_0043309a:
  0043309a  66837e1002            cmp       word [esi+10h],2
  0043309f  7534                  jne       short 004330D5h
  004330a1  6a00                  push      0
  004330a3  6a00                  push      0
  004330a5  6800200000            push      2000h
  004330aa  68dc050000            push      5DCh
  004330af  6a26                  push      26h
  004330b1  56                    push      esi
  004330b2  e8c94f0300            call      00468080h
  004330b7  83c418                add       esp,18h
  004330ba  6aff                  push      0FFFFFFFFh
  004330bc  6a00                  push      0
  004330be  68fe000000            push      0FEh
  004330c3  56                    push      esi
  004330c4  e8177a0400            call      0047AAE0h
  004330c9  8b06                  mov       eax,[esi]
  004330cb  83c410                add       esp,10h
  004330ce  25ffffff7f            and       eax,7FFFFFFFh
  004330d3  8906                  mov       [esi],eax

LAB_004330d5:
  004330d5  5e                    pop       esi
  004330d6  c3                    ret
  004330d7  90                    nop
  004330d8  90                    nop
  004330d9  90                    nop
  004330da  90                    nop
  004330db  90                    nop
  004330dc  90                    nop
  004330dd  90                    nop
  004330de  90                    nop
  004330df  90                    nop

3 cible(s) de saut interne : 0x0043306b, 0x0043309a, 0x004330d5
