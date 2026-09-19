Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442290  section .text  offset fichier 0x41690  320 octets

  00442290  53                    push      ebx
  00442291  56                    push      esi
  00442292  8b74240c              mov       esi,[esp+0Ch]
  00442296  bb01000000            mov       ebx,1
  0044229b  66837e1000            cmp       word [esi+10h],0
  004422a0  0f8583000000          jne       near 00442329h
  004422a6  8b5620                mov       edx,[esi+20h]
  004422a9  66895e10              mov       [esi+10h],bx
  004422ad  81c200200300          add       edx,32000h
  004422b3  c6460400              mov       byte [esi+4],0
  004422b7  c6460a03              mov       byte [esi+0Ah],3
  004422bb  885e0b                mov       [esi+0Bh],bl
  004422be  66c746120000          mov       word [esi+12h],0
  004422c4  895620                mov       [esi+20h],edx
  004422c7  66c7462a0000          mov       word [esi+2Ah],0
  004422cd  e86e4e1000            call      00547140h
  004422d2  99                    cdq
  004422d3  b9f4010000            mov       ecx,1F4h
  004422d8  f7f9                  idiv      ecx
  004422da  81c200020000          add       edx,200h
  004422e0  66895628              mov       [esi+28h],dx
  004422e4  e8574e1000            call      00547140h
  004422e9  99                    cdq
  004422ea  b9f4010000            mov       ecx,1F4h
  004422ef  f7f9                  idiv      ecx
  004422f1  81c200020000          add       edx,200h
  004422f7  6689562c              mov       [esi+2Ch],dx
  004422fb  e8404e1000            call      00547140h
  00442300  99                    cdq
  00442301  33c2                  xor       eax,edx
  00442303  2bc2                  sub       eax,edx
  00442305  25ff030000            and       eax,3FFh
  0044230a  33c2                  xor       eax,edx
  0044230c  2bc2                  sub       eax,edx
  0044230e  66894634              mov       [esi+34h],ax
  00442312  e8294e1000            call      00547140h
  00442317  99                    cdq
  00442318  33c2                  xor       eax,edx
  0044231a  2bc2                  sub       eax,edx
  0044231c  25ff0f0000            and       eax,0FFFh
  00442321  33c2                  xor       eax,edx
  00442323  2bc2                  sub       eax,edx
  00442325  66894632              mov       [esi+32h],ax

LAB_00442329:
  00442329  66395e10              cmp       [esi+10h],bx
  0044232d  0f8596000000          jne       near 004423C9h
  00442333  66837e1200            cmp       word [esi+12h],0
  00442338  7519                  jne       short 00442353h
  0044233a  6800100000            push      1000h
  0044233f  6800100000            push      1000h
  00442344  6852070000            push      752h
  00442349  6a0f                  push      0Fh
  0044234b  e850ab0f00            call      0053CEA0h
  00442350  83c410                add       esp,10h

LAB_00442353:
  00442353  66395e12              cmp       [esi+12h],bx
  00442357  7519                  jne       short 00442372h
  00442359  6800100000            push      1000h
  0044235e  6800100000            push      1000h
  00442363  6800080000            push      800h
  00442368  6a0f                  push      0Fh
  0044236a  e831ab0f00            call      0053CEA0h
  0044236f  83c410                add       esp,10h

LAB_00442372:
  00442372  6681462ab80b          add       word [esi+2Ah],0BB8h
  00442378  668b462a              mov       ax,[esi+2Ah]
  0044237c  66ff4612              inc       word [esi+12h]
  00442380  663da861              cmp       ax,61A8h
  00442384  7e06                  jle       short 0044238Ch
  00442386  66c7462aa861          mov       word [esi+2Ah],61A8h

LAB_0044238c:
  0044238c  66837e1646            cmp       word [esi+16h],46h
  00442391  7c36                  jl        short 004423C9h
  00442393  660fb64638            movzx     ax,byte [esi+38h]
  00442398  660fb64e39            movzx     cx,byte [esi+39h]
  0044239d  660fb6563a            movzx     dx,byte [esi+3Ah]
  004423a2  83e80f                sub       eax,0Fh
  004423a5  83e90f                sub       ecx,0Fh
  004423a8  83ea0f                sub       edx,0Fh
  004423ab  6685c0                test      ax,ax
  004423ae  7f10                  jg        short 004423C0h
  004423b0  8b1e                  mov       ebx,[esi]
  004423b2  33c0                  xor       eax,eax
  004423b4  33c9                  xor       ecx,ecx
  004423b6  33d2                  xor       edx,edx
  004423b8  81e3ffffff7f          and       ebx,7FFFFFFFh
  004423be  891e                  mov       [esi],ebx

LAB_004423c0:
  004423c0  884638                mov       [esi+38h],al
  004423c3  884e39                mov       [esi+39h],cl
  004423c6  88563a                mov       [esi+3Ah],dl

LAB_004423c9:
  004423c9  66ff4616              inc       word [esi+16h]
  004423cd  5e                    pop       esi
  004423ce  5b                    pop       ebx
  004423cf  c3                    ret

6 cible(s) de saut interne : 0x00442329, 0x00442353, 0x00442372, 0x0044238c, 0x004423c0, 0x004423c9
