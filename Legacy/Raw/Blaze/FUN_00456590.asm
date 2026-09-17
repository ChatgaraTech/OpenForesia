Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456590  section .text  offset fichier 0x55990  304 octets

  00456590  53                    push      ebx
  00456591  56                    push      esi
  00456592  8b74240c              mov       esi,[esp+0Ch]
  00456596  57                    push      edi
  00456597  33ff                  xor       edi,edi
  00456599  b803000000            mov       eax,3
  0045659e  66397e10              cmp       [esi+10h],di
  004565a2  751b                  jne       short 004565BFh
  004565a4  8b4e20                mov       ecx,[esi+20h]
  004565a7  66894610              mov       [esi+10h],ax
  004565ab  c1f90c                sar       ecx,0Ch
  004565ae  66897e12              mov       [esi+12h],di
  004565b2  66894e14              mov       [esi+14h],cx
  004565b6  66c746168000          mov       word [esi+16h],80h
  004565bc  897e44                mov       [esi+44h],edi

LAB_004565bf:
  004565bf  66394610              cmp       [esi+10h],ax
  004565c3  7515                  jne       short 004565DAh
  004565c5  66ff4612              inc       word [esi+12h]
  004565c9  66837e1228            cmp       word [esi+12h],28h
  004565ce  7e0a                  jle       short 004565DAh
  004565d0  66c746100100          mov       word [esi+10h],1
  004565d6  66897e12              mov       [esi+12h],di

LAB_004565da:
  004565da  66837e1001            cmp       word [esi+10h],1
  004565df  753c                  jne       short 0045661Dh
  004565e1  66397e12              cmp       [esi+12h],di
  004565e5  7522                  jne       short 00456609h
  004565e7  bb06000000            mov       ebx,6

LAB_004565ec:
  004565ec  66397e32              cmp       [esi+32h],di
  004565f0  57                    push      edi
  004565f1  57                    push      edi
  004565f2  6aff                  push      0FFFFFFFFh
  004565f4  57                    push      edi
  004565f5  7504                  jne       short 004565FBh
  004565f7  6a31                  push      31h
  004565f9  eb02                  jmp       short 004565FDh

LAB_004565fb:
  004565fb  6a32                  push      32h

LAB_004565fd:
  004565fd  56                    push      esi
  004565fe  e80d1b0100            call      00468110h
  00456603  83c418                add       esp,18h
  00456606  4b                    dec       ebx
  00456607  75e3                  jne       short 004565ECh

LAB_00456609:
  00456609  668146120002          add       word [esi+12h],200h
  0045660f  66817e12401f          cmp       word [esi+12h],1F40h
  00456615  7e06                  jle       short 0045661Dh
  00456617  66c746100200          mov       word [esi+10h],2

LAB_0045661d:
  0045661d  66837e1002            cmp       word [esi+10h],2
  00456622  7526                  jne       short 0045664Ah
  00456624  66ff4612              inc       word [esi+12h]
  00456628  e8130b0f00            call      00547140h
  0045662d  99                    cdq
  0045662e  b92d000000            mov       ecx,2Dh
  00456633  f7f9                  idiv      ecx
  00456635  66394e12              cmp       [esi+12h],cx
  00456639  7405                  je        short 00456640h
  0045663b  83fa01                cmp       edx,1
  0045663e  750a                  jne       short 0045664Ah

LAB_00456640:
  00456640  66c746100100          mov       word [esi+10h],1
  00456646  66897e12              mov       [esi+12h],di

LAB_0045664a:
  0045664a  66837e1063            cmp       word [esi+10h],63h
  0045664f  7561                  jne       short 004566B2h
  00456651  e8ea0a0f00            call      00547140h
  00456656  99                    cdq
  00456657  33c2                  xor       eax,edx
  00456659  2bc2                  sub       eax,edx
  0045665b  83e001                and       eax,1
  0045665e  33c2                  xor       eax,edx
  00456660  2bc2                  sub       eax,edx
  00456662  89442410              mov       [esp+10h],eax
  00456666  7510                  jne       short 00456678h
  00456668  57                    push      edi
  00456669  57                    push      edi
  0045666a  6aff                  push      0FFFFFFFFh
  0045666c  57                    push      edi
  0045666d  6a30                  push      30h
  0045666f  56                    push      esi
  00456670  e89b1a0100            call      00468110h
  00456675  83c418                add       esp,18h

LAB_00456678:
  00456678  e8c30a0f00            call      00547140h
  0045667d  99                    cdq
  0045667e  b91e000000            mov       ecx,1Eh
  00456683  f7f9                  idiv      ecx
  00456685  3bd7                  cmp       edx,edi
  00456687  7529                  jne       short 004566B2h
  00456689  8b15cc975e00          mov       edx,[5E97CCh]
  0045668f  57                    push      edi
  00456690  57                    push      edi
  00456691  6800100000            push      1000h
  00456696  8b82a8000000          mov       eax,[edx+0A8h]
  0045669c  6800040000            push      400h
  004566a1  8b08                  mov       ecx,[eax]
  004566a3  660fb65110            movzx     dx,byte [ecx+10h]
  004566a8  52                    push      edx
  004566a9  56                    push      esi
  004566aa  e8d1190100            call      00468080h
  004566af  83c418                add       esp,18h

LAB_004566b2:
  004566b2  5f                    pop       edi
  004566b3  5e                    pop       esi
  004566b4  5b                    pop       ebx
  004566b5  c3                    ret
  004566b6  90                    nop
  004566b7  90                    nop
  004566b8  90                    nop
  004566b9  90                    nop
  004566ba  90                    nop
  004566bb  90                    nop
  004566bc  90                    nop
  004566bd  90                    nop
  004566be  90                    nop
  004566bf  90                    nop

11 cible(s) de saut interne : 0x004565bf, 0x004565da, 0x004565ec, 0x004565fb, 0x004565fd, 0x00456609, 0x0045661d, 0x00456640, 0x0045664a, 0x00456678, 0x004566b2
