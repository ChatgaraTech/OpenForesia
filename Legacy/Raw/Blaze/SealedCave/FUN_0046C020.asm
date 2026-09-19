Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046c020  section .text  offset fichier 0x6b420  320 octets

  0046c020  53                    push      ebx
  0046c021  55                    push      ebp
  0046c022  56                    push      esi
  0046c023  8b742410              mov       esi,[esp+10h]
  0046c027  33db                  xor       ebx,ebx
  0046c029  bd01000000            mov       ebp,1
  0046c02e  66395e10              cmp       [esi+10h],bx
  0046c032  7551                  jne       short 0046C085h
  0046c034  668b4e14              mov       cx,[esi+14h]
  0046c038  668b4632              mov       ax,[esi+32h]
  0046c03c  6681f90010            cmp       cx,1000h
  0046c041  66896e10              mov       [esi+10h],bp
  0046c045  66894612              mov       [esi+12h],ax
  0046c049  7505                  jne       short 0046C050h
  0046c04b  896e5c                mov       [esi+5Ch],ebp
  0046c04e  eb03                  jmp       short 0046C053h

LAB_0046c050:
  0046c050  895e5c                mov       [esi+5Ch],ebx

LAB_0046c053:
  0046c053  03c8                  add       ecx,eax
  0046c055  66895e16              mov       [esi+16h],bx
  0046c059  66894e14              mov       [esi+14h],cx
  0046c05d  a1cc975e00            mov       eax,[5E97CCh]
  0046c062  53                    push      ebx
  0046c063  53                    push      ebx
  0046c064  8b88a8000000          mov       ecx,[eax+0A8h]
  0046c06a  6800100000            push      1000h
  0046c06f  6800040000            push      400h
  0046c074  8b11                  mov       edx,[ecx]
  0046c076  660fb6421f            movzx     ax,byte [edx+1Fh]
  0046c07b  50                    push      eax
  0046c07c  56                    push      esi
  0046c07d  e8febfffff            call      00468080h
  0046c082  83c418                add       esp,18h

LAB_0046c085:
  0046c085  66396e10              cmp       [esi+10h],bp
  0046c089  7561                  jne       short 0046C0ECh
  0046c08b  668b4616              mov       ax,[esi+16h]
  0046c08f  66014632              add       [esi+32h],ax
  0046c093  83c00a                add       eax,0Ah
  0046c096  66894616              mov       [esi+16h],ax
  0046c09a  668b4614              mov       ax,[esi+14h]
  0046c09e  66394632              cmp       [esi+32h],ax
  0046c0a2  7e48                  jle       short 0046C0ECh
  0046c0a4  66894632              mov       [esi+32h],ax
  0046c0a8  8b465c                mov       eax,[esi+5Ch]
  0046c0ab  3bc3                  cmp       eax,ebx
  0046c0ad  7531                  jne       short 0046C0E0h
  0046c0af  66c746100200          mov       word [esi+10h],2
  0046c0b5  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046c0bb  53                    push      ebx
  0046c0bc  53                    push      ebx
  0046c0bd  8b91a8000000          mov       edx,[ecx+0A8h]
  0046c0c3  6800100000            push      1000h
  0046c0c8  6800040000            push      400h
  0046c0cd  8b02                  mov       eax,[edx]
  0046c0cf  660fb6481f            movzx     cx,byte [eax+1Fh]
  0046c0d4  51                    push      ecx
  0046c0d5  56                    push      esi
  0046c0d6  e8a5bfffff            call      00468080h
  0046c0db  83c418                add       esp,18h
  0046c0de  eb08                  jmp       short 0046C0E8h

LAB_0046c0e0:
  0046c0e0  668b5612              mov       dx,[esi+12h]
  0046c0e4  66895632              mov       [esi+32h],dx

LAB_0046c0e8:
  0046c0e8  66895e16              mov       [esi+16h],bx

LAB_0046c0ec:
  0046c0ec  66837e1002            cmp       word [esi+10h],2
  0046c0f1  754d                  jne       short 0046C140h
  0046c0f3  668b4616              mov       ax,[esi+16h]
  0046c0f7  66294632              sub       [esi+32h],ax
  0046c0fb  83c00a                add       eax,0Ah
  0046c0fe  66894616              mov       [esi+16h],ax
  0046c102  668b4612              mov       ax,[esi+12h]
  0046c106  66394632              cmp       [esi+32h],ax
  0046c10a  7d34                  jge       short 0046C140h
  0046c10c  66894632              mov       [esi+32h],ax
  0046c110  66896e10              mov       [esi+10h],bp
  0046c114  a1cc975e00            mov       eax,[5E97CCh]
  0046c119  53                    push      ebx
  0046c11a  53                    push      ebx
  0046c11b  6800100000            push      1000h
  0046c120  8b88a8000000          mov       ecx,[eax+0A8h]
  0046c126  6800040000            push      400h
  0046c12b  8b11                  mov       edx,[ecx]
  0046c12d  660fb6421f            movzx     ax,byte [edx+1Fh]
  0046c132  50                    push      eax
  0046c133  56                    push      esi
  0046c134  e847bfffff            call      00468080h
  0046c139  83c418                add       esp,18h
  0046c13c  66895e16              mov       [esi+16h],bx

LAB_0046c140:
  0046c140  53                    push      ebx
  0046c141  53                    push      ebx
  0046c142  53                    push      ebx
  0046c143  6a0a                  push      0Ah
  0046c145  56                    push      esi
  0046c146  e8f518ffff            call      0045DA40h
  0046c14b  83c414                add       esp,14h
  0046c14e  5e                    pop       esi
  0046c14f  5d                    pop       ebp
  0046c150  5b                    pop       ebx
  0046c151  c3                    ret
  0046c152  90                    nop
  0046c153  90                    nop
  0046c154  90                    nop
  0046c155  90                    nop
  0046c156  90                    nop
  0046c157  90                    nop
  0046c158  90                    nop
  0046c159  90                    nop
  0046c15a  90                    nop
  0046c15b  90                    nop
  0046c15c  90                    nop
  0046c15d  90                    nop
  0046c15e  90                    nop
  0046c15f  90                    nop

7 cible(s) de saut interne : 0x0046c050, 0x0046c053, 0x0046c085, 0x0046c0e0, 0x0046c0e8, 0x0046c0ec, 0x0046c140
