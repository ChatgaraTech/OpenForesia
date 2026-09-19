Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00469af0  section .text  offset fichier 0x68ef0  208 octets

  00469af0  a128185700            mov       eax,[571828h]
  00469af5  56                    push      esi
  00469af6  f6c404                test      ah,4
  00469af9  0f84b2000000          je        near 00469BB1h
  00469aff  f6c408                test      ah,8
  00469b02  7422                  je        short 00469B26h
  00469b04  8b442408              mov       eax,[esp+8]
  00469b08  66ff4010              inc       word [eax+10h]
  00469b0c  6683781002            cmp       word [eax+10h],2
  00469b11  0f859a000000          jne       near 00469BB1h
  00469b17  8b08                  mov       ecx,[eax]
  00469b19  66c740100000          mov       word [eax+10h],0
  00469b1f  80cd60                or        ch,60h
  00469b22  8908                  mov       [eax],ecx
  00469b24  5e                    pop       esi
  00469b25  c3                    ret

LAB_00469b26:
  00469b26  8b742408              mov       esi,[esp+8]
  00469b2a  66837e1000            cmp       word [esi+10h],0
  00469b2f  7530                  jne       short 00469B61h
  00469b31  a1cc975e00            mov       eax,[5E97CCh]
  00469b36  6a00                  push      0
  00469b38  6a00                  push      0
  00469b3a  6800180000            push      1800h
  00469b3f  8b88a8000000          mov       ecx,[eax+0A8h]
  00469b45  6800040000            push      400h
  00469b4a  8b11                  mov       edx,[ecx]
  00469b4c  660fb64239            movzx     ax,byte [edx+39h]
  00469b51  50                    push      eax
  00469b52  56                    push      esi
  00469b53  e828e5ffff            call      00468080h
  00469b58  83c418                add       esp,18h
  00469b5b  66c746100100          mov       word [esi+10h],1

LAB_00469b61:
  00469b61  66834634ba            add       word [esi+34h],0FFBAh
  00469b66  66837e3400            cmp       word [esi+34h],0
  00469b6b  7d44                  jge       short 00469BB1h
  00469b6d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00469b73  6a00                  push      0
  00469b75  6a00                  push      0
  00469b77  6800180000            push      1800h
  00469b7c  8b91a8000000          mov       edx,[ecx+0A8h]
  00469b82  6800040000            push      400h
  00469b87  8b02                  mov       eax,[edx]
  00469b89  660fb6483a            movzx     cx,byte [eax+3Ah]
  00469b8e  51                    push      ecx
  00469b8f  56                    push      esi
  00469b90  e8ebe4ffff            call      00468080h
  00469b95  66c746340000          mov       word [esi+34h],0
  00469b9b  a128185700            mov       eax,[571828h]
  00469ba0  83c418                add       esp,18h
  00469ba3  80cc08                or        ah,8
  00469ba6  a328185700            mov       [571828h],eax
  00469bab  66c746100000          mov       word [esi+10h],0

LAB_00469bb1:
  00469bb1  5e                    pop       esi
  00469bb2  c3                    ret
  00469bb3  90                    nop
  00469bb4  90                    nop
  00469bb5  90                    nop
  00469bb6  90                    nop
  00469bb7  90                    nop
  00469bb8  90                    nop
  00469bb9  90                    nop
  00469bba  90                    nop
  00469bbb  90                    nop
  00469bbc  90                    nop
  00469bbd  90                    nop
  00469bbe  90                    nop
  00469bbf  90                    nop

3 cible(s) de saut interne : 0x00469b26, 0x00469b61, 0x00469bb1
