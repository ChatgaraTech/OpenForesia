Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442db0  section .text  offset fichier 0x421b0  96 octets

  00442db0  53                    push      ebx
  00442db1  56                    push      esi
  00442db2  57                    push      edi
  00442db3  bec0935e00            mov       esi,5E93C0h
  00442db8  bf04000000            mov       edi,4
  00442dbd  33db                  xor       ebx,ebx

LAB_00442dbf:
  00442dbf  f70600000080          test      dword [esi],80000000h
  00442dc5  742b                  je        short 00442DF2h
  00442dc7  53                    push      ebx
  00442dc8  6a64                  push      64h
  00442dca  53                    push      ebx
  00442dcb  881d9f905e00          mov       [5E909Fh],bl
  00442dd1  895e6c                mov       [esi+6Ch],ebx
  00442dd4  e8477b0f00            call      0053A920h
  00442dd9  83c408                add       esp,8
  00442ddc  50                    push      eax
  00442ddd  56                    push      esi
  00442dde  e83de10e00            call      00530F20h
  00442de3  83c40c                add       esp,0Ch
  00442de6  895e6c                mov       [esi+6Ch],ebx
  00442de9  56                    push      esi
  00442dea  e8e1e40e00            call      005312D0h
  00442def  83c404                add       esp,4

LAB_00442df2:
  00442df2  81c69c000000          add       esi,9Ch
  00442df8  4f                    dec       edi
  00442df9  75c4                  jne       short 00442DBFh
  00442dfb  5f                    pop       edi
  00442dfc  5e                    pop       esi
  00442dfd  33c0                  xor       eax,eax
  00442dff  5b                    pop       ebx
  00442e00  c3                    ret
  00442e01  90                    nop
  00442e02  90                    nop
  00442e03  90                    nop
  00442e04  90                    nop
  00442e05  90                    nop
  00442e06  90                    nop
  00442e07  90                    nop
  00442e08  90                    nop
  00442e09  90                    nop
  00442e0a  90                    nop
  00442e0b  90                    nop
  00442e0c  90                    nop
  00442e0d  90                    nop
  00442e0e  90                    nop
  00442e0f  90                    nop

2 cible(s) de saut interne : 0x00442dbf, 0x00442df2
