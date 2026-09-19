Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470b00  section .text  offset fichier 0x6ff00  224 octets

  00470b00  a178195700            mov       eax,[571978h]
  00470b05  56                    push      esi
  00470b06  f6c410                test      ah,10h
  00470b09  0f84c0000000          je        near 00470BCFh
  00470b0f  f6c420                test      ah,20h
  00470b12  742e                  je        short 00470B42h
  00470b14  8b442408              mov       eax,[esp+8]
  00470b18  66ff4010              inc       word [eax+10h]
  00470b1c  668b4810              mov       cx,[eax+10h]
  00470b20  6683f901              cmp       cx,1
  00470b24  7507                  jne       short 00470B2Dh
  00470b26  8b10                  mov       edx,[eax]
  00470b28  80ce40                or        dh,40h
  00470b2b  8910                  mov       [eax],edx

LAB_00470b2d:
  00470b2d  6683f902              cmp       cx,2
  00470b31  7507                  jne       short 00470B3Ah
  00470b33  8b08                  mov       ecx,[eax]
  00470b35  80cd60                or        ch,60h
  00470b38  8908                  mov       [eax],ecx

LAB_00470b3a:
  00470b3a  66c740100000          mov       word [eax+10h],0
  00470b40  5e                    pop       esi
  00470b41  c3                    ret

LAB_00470b42:
  00470b42  8b742408              mov       esi,[esp+8]
  00470b46  66837e1000            cmp       word [esi+10h],0
  00470b4b  7530                  jne       short 00470B7Dh
  00470b4d  66c746100100          mov       word [esi+10h],1
  00470b53  a1cc975e00            mov       eax,[5E97CCh]
  00470b58  6a00                  push      0
  00470b5a  6a00                  push      0
  00470b5c  8b88a8000000          mov       ecx,[eax+0A8h]
  00470b62  6800180000            push      1800h
  00470b67  6800040000            push      400h
  00470b6c  8b11                  mov       edx,[ecx]
  00470b6e  660fb64239            movzx     ax,byte [edx+39h]
  00470b73  50                    push      eax
  00470b74  56                    push      esi
  00470b75  e80675ffff            call      00468080h
  00470b7a  83c418                add       esp,18h

LAB_00470b7d:
  00470b7d  66834634ba            add       word [esi+34h],0FFBAh
  00470b82  66837e3400            cmp       word [esi+34h],0
  00470b87  7d46                  jge       short 00470BCFh
  00470b89  66c746340000          mov       word [esi+34h],0
  00470b8f  8b0d78195700          mov       ecx,[571978h]
  00470b95  80cd20                or        ch,20h
  00470b98  6a00                  push      0
  00470b9a  890d78195700          mov       [571978h],ecx
  00470ba0  66c746100000          mov       word [esi+10h],0
  00470ba6  8b0dcc975e00          mov       ecx,[5E97CCh]
  00470bac  6a00                  push      0
  00470bae  6800180000            push      1800h
  00470bb3  6800040000            push      400h
  00470bb8  8b91a8000000          mov       edx,[ecx+0A8h]
  00470bbe  8b02                  mov       eax,[edx]
  00470bc0  660fb6483a            movzx     cx,byte [eax+3Ah]
  00470bc5  51                    push      ecx
  00470bc6  56                    push      esi
  00470bc7  e8b474ffff            call      00468080h
  00470bcc  83c418                add       esp,18h

LAB_00470bcf:
  00470bcf  5e                    pop       esi
  00470bd0  c3                    ret
  00470bd1  90                    nop
  00470bd2  90                    nop
  00470bd3  90                    nop
  00470bd4  90                    nop
  00470bd5  90                    nop
  00470bd6  90                    nop
  00470bd7  90                    nop
  00470bd8  90                    nop
  00470bd9  90                    nop
  00470bda  90                    nop
  00470bdb  90                    nop
  00470bdc  90                    nop
  00470bdd  90                    nop
  00470bde  90                    nop
  00470bdf  90                    nop

5 cible(s) de saut interne : 0x00470b2d, 0x00470b3a, 0x00470b42, 0x00470b7d, 0x00470bcf
