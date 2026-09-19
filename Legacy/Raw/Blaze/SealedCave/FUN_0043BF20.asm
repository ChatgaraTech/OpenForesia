Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043bf20  section .text  offset fichier 0x3b320  48 octets

  0043bf20  56                    push      esi
  0043bf21  8b742408              mov       esi,[esp+8]
  0043bf25  66837e1000            cmp       word [esi+10h],0
  0043bf2a  7512                  jne       short 0043BF3Eh
  0043bf2c  6aff                  push      0FFFFFFFFh
  0043bf2e  6a00                  push      0
  0043bf30  68ff000000            push      0FFh
  0043bf35  56                    push      esi
  0043bf36  e8a5eb0300            call      0047AAE0h
  0043bf3b  83c410                add       esp,10h

LAB_0043bf3e:
  0043bf3e  8b06                  mov       eax,[esi]
  0043bf40  25ffffff7f            and       eax,7FFFFFFFh
  0043bf45  8906                  mov       [esi],eax
  0043bf47  5e                    pop       esi
  0043bf48  c3                    ret
  0043bf49  90                    nop
  0043bf4a  90                    nop
  0043bf4b  90                    nop
  0043bf4c  90                    nop
  0043bf4d  90                    nop
  0043bf4e  90                    nop
  0043bf4f  90                    nop

1 cible(s) de saut interne : 0x0043bf3e
