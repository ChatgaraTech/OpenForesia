Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00431a20  section .text  offset fichier 0x30e20  144 octets

  00431a20  56                    push      esi
  00431a21  8b742408              mov       esi,[esp+8]
  00431a25  b801000000            mov       eax,1
  00431a2a  66837e1000            cmp       word [esi+10h],0
  00431a2f  750a                  jne       short 00431A3Bh
  00431a31  66894610              mov       [esi+10h],ax
  00431a35  66c746121400          mov       word [esi+12h],14h

LAB_00431a3b:
  00431a3b  66394610              cmp       [esi+10h],ax
  00431a3f  7529                  jne       short 00431A6Ah
  00431a41  66ff4e12              dec       word [esi+12h]
  00431a45  6aff                  push      0FFFFFFFFh
  00431a47  6a00                  push      0
  00431a49  68fc000000            push      0FCh
  00431a4e  8d461c                lea       eax,[esi+1Ch]
  00431a51  6a00                  push      0
  00431a53  50                    push      eax
  00431a54  56                    push      esi
  00431a55  e8e6910400            call      0047AC40h
  00431a5a  83c418                add       esp,18h
  00431a5d  66837e1200            cmp       word [esi+12h],0
  00431a62  7f06                  jg        short 00431A6Ah
  00431a64  66c746100200          mov       word [esi+10h],2

LAB_00431a6a:
  00431a6a  66837e1002            cmp       word [esi+10h],2
  00431a6f  7534                  jne       short 00431AA5h
  00431a71  6a00                  push      0
  00431a73  6a00                  push      0
  00431a75  6800200000            push      2000h
  00431a7a  68dc050000            push      5DCh
  00431a7f  6a26                  push      26h
  00431a81  56                    push      esi
  00431a82  e8f9650300            call      00468080h
  00431a87  83c418                add       esp,18h
  00431a8a  6aff                  push      0FFFFFFFFh
  00431a8c  6a00                  push      0
  00431a8e  68fe000000            push      0FEh
  00431a93  56                    push      esi
  00431a94  e847900400            call      0047AAE0h
  00431a99  8b06                  mov       eax,[esi]
  00431a9b  83c410                add       esp,10h
  00431a9e  25ffffff7f            and       eax,7FFFFFFFh
  00431aa3  8906                  mov       [esi],eax

LAB_00431aa5:
  00431aa5  5e                    pop       esi
  00431aa6  c3                    ret
  00431aa7  90                    nop
  00431aa8  90                    nop
  00431aa9  90                    nop
  00431aaa  90                    nop
  00431aab  90                    nop
  00431aac  90                    nop
  00431aad  90                    nop
  00431aae  90                    nop
  00431aaf  90                    nop

3 cible(s) de saut interne : 0x00431a3b, 0x00431a6a, 0x00431aa5
