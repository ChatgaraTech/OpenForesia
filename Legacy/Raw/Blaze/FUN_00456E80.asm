Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456e80  section .text  offset fichier 0x56280  183 octets

  00456e80  56                    push      esi
  00456e81  8b742408              mov       esi,[esp+8]
  00456e85  66837e1000            cmp       word [esi+10h],0
  00456e8a  752d                  jne       short 00456EB9h
  00456e8c  33c0                  xor       eax,eax
  00456e8e  66c746120000          mov       word [esi+12h],0
  00456e94  a038e55d00            mov       al,[5DE538h]
  00456e99  6a01                  push      1
  00456e9b  8d0c80                lea       ecx,[eax+eax*4]
  00456e9e  c1e103                shl       ecx,3
  00456ea1  2bc8                  sub       ecx,eax
  00456ea3  a1cc975e00            mov       eax,[5E97CCh]
  00456ea8  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00456eaf  52                    push      edx
  00456eb0  50                    push      eax
  00456eb1  e81acf0b00            call      00513DD0h
  00456eb6  83c40c                add       esp,0Ch

LAB_00456eb9:
  00456eb9  66837e1001            cmp       word [esi+10h],1
  00456ebe  7575                  jne       short 00456F35h
  00456ec0  33c0                  xor       eax,eax
  00456ec2  6a00                  push      0
  00456ec4  a038e55d00            mov       al,[5DE538h]
  00456ec9  8d0c80                lea       ecx,[eax+eax*4]
  00456ecc  c1e103                shl       ecx,3
  00456ecf  2bc8                  sub       ecx,eax
  00456ed1  a1cc975e00            mov       eax,[5E97CCh]
  00456ed6  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00456edd  52                    push      edx
  00456ede  50                    push      eax
  00456edf  e8ecce0b00            call      00513DD0h
  00456ee4  83c40c                add       esp,0Ch
  00456ee7  66837e1200            cmp       word [esi+12h],0
  00456eec  7531                  jne       short 00456F1Fh
  00456eee  8b0dcc975e00          mov       ecx,[5E97CCh]
  00456ef4  6a00                  push      0
  00456ef6  6a00                  push      0
  00456ef8  6800100000            push      1000h
  00456efd  8b91a8000000          mov       edx,[ecx+0A8h]
  00456f03  6800020000            push      200h
  00456f08  8b02                  mov       eax,[edx]
  00456f0a  660fb608              movzx     cx,byte [eax]
  00456f0e  51                    push      ecx
  00456f0f  56                    push      esi
  00456f10  e86b110100            call      00468080h
  00456f15  8b06                  mov       eax,[esi]
  00456f17  83c418                add       esp,18h
  00456f1a  80cc20                or        ah,20h
  00456f1d  8906                  mov       [esi],eax

LAB_00456f1f:
  00456f1f  66834634e7            add       word [esi+34h],0FFE7h
  00456f24  66ff4612              inc       word [esi+12h]
  00456f28  66837e121e            cmp       word [esi+12h],1Eh
  00456f2d  7506                  jne       short 00456F35h
  00456f2f  66c746106300          mov       word [esi+10h],63h

LAB_00456f35:
  00456f35  5e                    pop       esi
  00456f36  c3                    ret

3 cible(s) de saut interne : 0x00456eb9, 0x00456f1f, 0x00456f35
