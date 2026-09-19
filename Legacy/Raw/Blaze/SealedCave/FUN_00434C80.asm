Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00434c80  section .text  offset fichier 0x34080  144 octets

  00434c80  56                    push      esi
  00434c81  8b742408              mov       esi,[esp+8]
  00434c85  b801000000            mov       eax,1
  00434c8a  66837e1000            cmp       word [esi+10h],0
  00434c8f  750a                  jne       short 00434C9Bh
  00434c91  66894610              mov       [esi+10h],ax
  00434c95  66c746121400          mov       word [esi+12h],14h

LAB_00434c9b:
  00434c9b  66394610              cmp       [esi+10h],ax
  00434c9f  7529                  jne       short 00434CCAh
  00434ca1  66ff4e12              dec       word [esi+12h]
  00434ca5  6aff                  push      0FFFFFFFFh
  00434ca7  6a00                  push      0
  00434ca9  68fc000000            push      0FCh
  00434cae  8d461c                lea       eax,[esi+1Ch]
  00434cb1  6a00                  push      0
  00434cb3  50                    push      eax
  00434cb4  56                    push      esi
  00434cb5  e8865f0400            call      0047AC40h
  00434cba  83c418                add       esp,18h
  00434cbd  66837e1200            cmp       word [esi+12h],0
  00434cc2  7f06                  jg        short 00434CCAh
  00434cc4  66c746100200          mov       word [esi+10h],2

LAB_00434cca:
  00434cca  66837e1002            cmp       word [esi+10h],2
  00434ccf  7534                  jne       short 00434D05h
  00434cd1  6a00                  push      0
  00434cd3  6a00                  push      0
  00434cd5  6800200000            push      2000h
  00434cda  68dc050000            push      5DCh
  00434cdf  6a26                  push      26h
  00434ce1  56                    push      esi
  00434ce2  e899330300            call      00468080h
  00434ce7  83c418                add       esp,18h
  00434cea  6aff                  push      0FFFFFFFFh
  00434cec  6a00                  push      0
  00434cee  68fe000000            push      0FEh
  00434cf3  56                    push      esi
  00434cf4  e8e75d0400            call      0047AAE0h
  00434cf9  8b06                  mov       eax,[esi]
  00434cfb  83c410                add       esp,10h
  00434cfe  25ffffff7f            and       eax,7FFFFFFFh
  00434d03  8906                  mov       [esi],eax

LAB_00434d05:
  00434d05  5e                    pop       esi
  00434d06  c3                    ret
  00434d07  90                    nop
  00434d08  90                    nop
  00434d09  90                    nop
  00434d0a  90                    nop
  00434d0b  90                    nop
  00434d0c  90                    nop
  00434d0d  90                    nop
  00434d0e  90                    nop
  00434d0f  90                    nop

3 cible(s) de saut interne : 0x00434c9b, 0x00434cca, 0x00434d05
