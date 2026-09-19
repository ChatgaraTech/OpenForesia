Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042ed20  section .text  offset fichier 0x2e120  144 octets

  0042ed20  53                    push      ebx
  0042ed21  56                    push      esi
  0042ed22  8b74240c              mov       esi,[esp+0Ch]
  0042ed26  33db                  xor       ebx,ebx
  0042ed28  b901000000            mov       ecx,1
  0042ed2d  66395e10              cmp       [esi+10h],bx
  0042ed31  7539                  jne       short 0042ED6Ch
  0042ed33  66894e10              mov       [esi+10h],cx
  0042ed37  a0c4de5e00            mov       al,[5EDEC4h]
  0042ed3c  884604                mov       [esi+4],al
  0042ed3f  8b4620                mov       eax,[esi+20h]
  0042ed42  0500000500            add       eax,50000h
  0042ed47  c6460a03              mov       byte [esi+0Ah],3
  0042ed4b  894620                mov       [esi+20h],eax
  0042ed4e  b0b4                  mov       al,0B4h
  0042ed50  885e0b                mov       [esi+0Bh],bl
  0042ed53  66895e28              mov       [esi+28h],bx
  0042ed57  66895e2a              mov       [esi+2Ah],bx
  0042ed5b  66895e2c              mov       [esi+2Ch],bx
  0042ed5f  66895e34              mov       [esi+34h],bx
  0042ed63  884638                mov       [esi+38h],al
  0042ed66  884639                mov       [esi+39h],al
  0042ed69  88463a                mov       [esi+3Ah],al

LAB_0042ed6c:
  0042ed6c  66394e10              cmp       [esi+10h],cx
  0042ed70  752d                  jne       short 0042ED9Fh
  0042ed72  6af6                  push      0FFFFFFF6h
  0042ed74  6af6                  push      0FFFFFFF6h
  0042ed76  b878050000            mov       eax,578h
  0042ed7b  6af6                  push      0FFFFFFF6h
  0042ed7d  66014628              add       [esi+28h],ax
  0042ed81  6601462a              add       [esi+2Ah],ax
  0042ed85  6601462c              add       [esi+2Ch],ax
  0042ed89  56                    push      esi
  0042ed8a  e85160fdff            call      00404DE0h
  0042ed8f  8a4638                mov       al,[esi+38h]
  0042ed92  83c410                add       esp,10h
  0042ed95  3ac3                  cmp       al,bl
  0042ed97  7506                  jne       short 0042ED9Fh
  0042ed99  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042ed9f:
  0042ed9f  5e                    pop       esi
  0042eda0  5b                    pop       ebx
  0042eda1  c3                    ret
  0042eda2  90                    nop
  0042eda3  90                    nop
  0042eda4  90                    nop
  0042eda5  90                    nop
  0042eda6  90                    nop
  0042eda7  90                    nop
  0042eda8  90                    nop
  0042eda9  90                    nop
  0042edaa  90                    nop
  0042edab  90                    nop
  0042edac  90                    nop
  0042edad  90                    nop
  0042edae  90                    nop
  0042edaf  90                    nop

2 cible(s) de saut interne : 0x0042ed6c, 0x0042ed9f
