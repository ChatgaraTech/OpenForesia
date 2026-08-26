Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004566c0  section .text  offset fichier 0x55ac0  110 octets

  004566c0  56                    push      esi
  004566c1  8b742408              mov       esi,[esp+8]
  004566c5  66837e1000            cmp       word [esi+10h],0
  004566ca  750f                  jne       short 004566DBh
  004566cc  66c746100100          mov       word [esi+10h],1
  004566d2  66c786980000000000    mov       word [esi+98h],0

LAB_004566db:
  004566db  6683be9800000000      cmp       word [esi+98h],0
  004566e3  752d                  jne       short 00456712h
  004566e5  a1cc975e00            mov       eax,[5E97CCh]
  004566ea  6a00                  push      0
  004566ec  68b80b0000            push      0BB8h
  004566f1  6800060000            push      600h
  004566f6  8b88a8000000          mov       ecx,[eax+0A8h]
  004566fc  6800040000            push      400h
  00456701  8b11                  mov       edx,[ecx]
  00456703  660fb64215            movzx     ax,byte [edx+15h]
  00456708  50                    push      eax
  00456709  56                    push      esi
  0045670a  e8d1190000            call      004580E0h
  0045670f  83c418                add       esp,18h

LAB_00456712:
  00456712  66ff8698000000        inc       word [esi+98h]
  00456719  6683be9800000014      cmp       word [esi+98h],14h
  00456721  7509                  jne       short 0045672Ch
  00456723  66c786980000000000    mov       word [esi+98h],0

LAB_0045672c:
  0045672c  5e                    pop       esi
  0045672d  c3                    ret

3 cible(s) de saut interne : 0x004566db, 0x00456712, 0x0045672c
