Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f970  section .text  offset fichier 0x2ed70  160 octets

  0042f970  53                    push      ebx
  0042f971  56                    push      esi
  0042f972  8b74240c              mov       esi,[esp+0Ch]
  0042f976  33db                  xor       ebx,ebx
  0042f978  b801000000            mov       eax,1
  0042f97d  66395e10              cmp       [esi+10h],bx
  0042f981  752e                  jne       short 0042F9B1h
  0042f983  b900080000            mov       ecx,800h
  0042f988  66894610              mov       [esi+10h],ax
  0042f98c  c7463cd00a5500        mov       dword [esi+3Ch],550AD0h
  0042f993  885e40                mov       [esi+40h],bl
  0042f996  88460a                mov       [esi+0Ah],al
  0042f999  885e0b                mov       [esi+0Bh],bl
  0042f99c  66894e28              mov       [esi+28h],cx
  0042f9a0  66894e2a              mov       [esi+2Ah],cx
  0042f9a4  66895e12              mov       [esi+12h],bx
  0042f9a8  885e38                mov       [esi+38h],bl
  0042f9ab  885e39                mov       [esi+39h],bl
  0042f9ae  885e3a                mov       [esi+3Ah],bl

LAB_0042f9b1:
  0042f9b1  66394610              cmp       [esi+10h],ax
  0042f9b5  754d                  jne       short 0042FA04h
  0042f9b7  807e4002              cmp       byte [esi+40h],2
  0042f9bb  770f                  ja        short 0042F9CCh
  0042f9bd  6a06                  push      6
  0042f9bf  6a06                  push      6
  0042f9c1  6a06                  push      6
  0042f9c3  56                    push      esi
  0042f9c4  e81754fdff            call      00404DE0h
  0042f9c9  83c410                add       esp,10h

LAB_0042f9cc:
  0042f9cc  807e4003              cmp       byte [esi+40h],3
  0042f9d0  720f                  jb        short 0042F9E1h
  0042f9d2  6af6                  push      0FFFFFFF6h
  0042f9d4  6af6                  push      0FFFFFFF6h
  0042f9d6  6af6                  push      0FFFFFFF6h
  0042f9d8  56                    push      esi
  0042f9d9  e80254fdff            call      00404DE0h
  0042f9de  83c410                add       esp,10h

LAB_0042f9e1:
  0042f9e1  66ff4612              inc       word [esi+12h]
  0042f9e5  66837e1204            cmp       word [esi+12h],4
  0042f9ea  7c18                  jl        short 0042FA04h
  0042f9ec  8a4e40                mov       cl,[esi+40h]
  0042f9ef  66895e12              mov       [esi+12h],bx
  0042f9f3  fec1                  inc       cl
  0042f9f5  8ac1                  mov       al,cl
  0042f9f7  884e40                mov       [esi+40h],cl
  0042f9fa  3c07                  cmp       al,7
  0042f9fc  7506                  jne       short 0042FA04h
  0042f9fe  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042fa04:
  0042fa04  5e                    pop       esi
  0042fa05  5b                    pop       ebx
  0042fa06  c3                    ret
  0042fa07  90                    nop
  0042fa08  90                    nop
  0042fa09  90                    nop
  0042fa0a  90                    nop
  0042fa0b  90                    nop
  0042fa0c  90                    nop
  0042fa0d  90                    nop
  0042fa0e  90                    nop
  0042fa0f  90                    nop

4 cible(s) de saut interne : 0x0042f9b1, 0x0042f9cc, 0x0042f9e1, 0x0042fa04
