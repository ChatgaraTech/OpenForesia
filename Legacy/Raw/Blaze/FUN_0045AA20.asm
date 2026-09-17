Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045aa20  section .text  offset fichier 0x59e20  112 octets

  0045aa20  a002975e00            mov       al,[5E9702h]
  0045aa25  56                    push      esi
  0045aa26  a808                  test      al,8
  0045aa28  755b                  jne       short 0045AA85h
  0045aa2a  8b742408              mov       esi,[esp+8]
  0045aa2e  b801000000            mov       eax,1
  0045aa33  66837e1000            cmp       word [esi+10h],0
  0045aa38  750a                  jne       short 0045AA44h
  0045aa3a  66c746120000          mov       word [esi+12h],0
  0045aa40  66894610              mov       [esi+10h],ax

LAB_0045aa44:
  0045aa44  66394610              cmp       [esi+10h],ax
  0045aa48  753b                  jne       short 0045AA85h
  0045aa4a  66ff4612              inc       word [esi+12h]
  0045aa4e  66837e120c            cmp       word [esi+12h],0Ch
  0045aa53  7530                  jne       short 0045AA85h
  0045aa55  a1cc975e00            mov       eax,[5E97CCh]
  0045aa5a  6a00                  push      0
  0045aa5c  6a00                  push      0
  0045aa5e  6800100000            push      1000h
  0045aa63  8b88a8000000          mov       ecx,[eax+0A8h]
  0045aa69  6800030000            push      300h
  0045aa6e  8b11                  mov       edx,[ecx]
  0045aa70  660fb64219            movzx     ax,byte [edx+19h]
  0045aa75  50                    push      eax
  0045aa76  56                    push      esi
  0045aa77  e804d60000            call      00468080h
  0045aa7c  83c418                add       esp,18h
  0045aa7f  66c746120000          mov       word [esi+12h],0

LAB_0045aa85:
  0045aa85  5e                    pop       esi
  0045aa86  c3                    ret
  0045aa87  90                    nop
  0045aa88  90                    nop
  0045aa89  90                    nop
  0045aa8a  90                    nop
  0045aa8b  90                    nop
  0045aa8c  90                    nop
  0045aa8d  90                    nop
  0045aa8e  90                    nop
  0045aa8f  90                    nop

2 cible(s) de saut interne : 0x0045aa44, 0x0045aa85
