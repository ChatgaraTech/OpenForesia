Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046de80  section .text  offset fichier 0x6d280  320 octets

  0046de80  53                    push      ebx
  0046de81  55                    push      ebp
  0046de82  56                    push      esi
  0046de83  8b742410              mov       esi,[esp+10h]
  0046de87  33db                  xor       ebx,ebx
  0046de89  bd01000000            mov       ebp,1
  0046de8e  66395e10              cmp       [esi+10h],bx
  0046de92  7551                  jne       short 0046DEE5h
  0046de94  668b4e14              mov       cx,[esi+14h]
  0046de98  668b4632              mov       ax,[esi+32h]
  0046de9c  6681f90010            cmp       cx,1000h
  0046dea1  66896e10              mov       [esi+10h],bp
  0046dea5  66894612              mov       [esi+12h],ax
  0046dea9  7505                  jne       short 0046DEB0h
  0046deab  896e5c                mov       [esi+5Ch],ebp
  0046deae  eb03                  jmp       short 0046DEB3h

LAB_0046deb0:
  0046deb0  895e5c                mov       [esi+5Ch],ebx

LAB_0046deb3:
  0046deb3  03c8                  add       ecx,eax
  0046deb5  66895e16              mov       [esi+16h],bx
  0046deb9  66894e14              mov       [esi+14h],cx
  0046debd  a1cc975e00            mov       eax,[5E97CCh]
  0046dec2  53                    push      ebx
  0046dec3  53                    push      ebx
  0046dec4  8b88a8000000          mov       ecx,[eax+0A8h]
  0046deca  6800100000            push      1000h
  0046decf  6800040000            push      400h
  0046ded4  8b11                  mov       edx,[ecx]
  0046ded6  660fb6421f            movzx     ax,byte [edx+1Fh]
  0046dedb  50                    push      eax
  0046dedc  56                    push      esi
  0046dedd  e89ea1ffff            call      00468080h
  0046dee2  83c418                add       esp,18h

LAB_0046dee5:
  0046dee5  66396e10              cmp       [esi+10h],bp
  0046dee9  7561                  jne       short 0046DF4Ch
  0046deeb  668b4616              mov       ax,[esi+16h]
  0046deef  66014632              add       [esi+32h],ax
  0046def3  83c00a                add       eax,0Ah
  0046def6  66894616              mov       [esi+16h],ax
  0046defa  668b4614              mov       ax,[esi+14h]
  0046defe  66394632              cmp       [esi+32h],ax
  0046df02  7e48                  jle       short 0046DF4Ch
  0046df04  66894632              mov       [esi+32h],ax
  0046df08  8b465c                mov       eax,[esi+5Ch]
  0046df0b  3bc3                  cmp       eax,ebx
  0046df0d  7531                  jne       short 0046DF40h
  0046df0f  66c746100200          mov       word [esi+10h],2
  0046df15  8b0dcc975e00          mov       ecx,[5E97CCh]
  0046df1b  53                    push      ebx
  0046df1c  53                    push      ebx
  0046df1d  8b91a8000000          mov       edx,[ecx+0A8h]
  0046df23  6800100000            push      1000h
  0046df28  6800040000            push      400h
  0046df2d  8b02                  mov       eax,[edx]
  0046df2f  660fb6481f            movzx     cx,byte [eax+1Fh]
  0046df34  51                    push      ecx
  0046df35  56                    push      esi
  0046df36  e845a1ffff            call      00468080h
  0046df3b  83c418                add       esp,18h
  0046df3e  eb08                  jmp       short 0046DF48h

LAB_0046df40:
  0046df40  668b5612              mov       dx,[esi+12h]
  0046df44  66895632              mov       [esi+32h],dx

LAB_0046df48:
  0046df48  66895e16              mov       [esi+16h],bx

LAB_0046df4c:
  0046df4c  66837e1002            cmp       word [esi+10h],2
  0046df51  754d                  jne       short 0046DFA0h
  0046df53  668b4616              mov       ax,[esi+16h]
  0046df57  66294632              sub       [esi+32h],ax
  0046df5b  83c00a                add       eax,0Ah
  0046df5e  66894616              mov       [esi+16h],ax
  0046df62  668b4612              mov       ax,[esi+12h]
  0046df66  66394632              cmp       [esi+32h],ax
  0046df6a  7d34                  jge       short 0046DFA0h
  0046df6c  66894632              mov       [esi+32h],ax
  0046df70  66896e10              mov       [esi+10h],bp
  0046df74  a1cc975e00            mov       eax,[5E97CCh]
  0046df79  53                    push      ebx
  0046df7a  53                    push      ebx
  0046df7b  6800100000            push      1000h
  0046df80  8b88a8000000          mov       ecx,[eax+0A8h]
  0046df86  6800040000            push      400h
  0046df8b  8b11                  mov       edx,[ecx]
  0046df8d  660fb6421f            movzx     ax,byte [edx+1Fh]
  0046df92  50                    push      eax
  0046df93  56                    push      esi
  0046df94  e8e7a0ffff            call      00468080h
  0046df99  83c418                add       esp,18h
  0046df9c  66895e16              mov       [esi+16h],bx

LAB_0046dfa0:
  0046dfa0  53                    push      ebx
  0046dfa1  53                    push      ebx
  0046dfa2  53                    push      ebx
  0046dfa3  6a0a                  push      0Ah
  0046dfa5  56                    push      esi
  0046dfa6  e895fafeff            call      0045DA40h
  0046dfab  83c414                add       esp,14h
  0046dfae  5e                    pop       esi
  0046dfaf  5d                    pop       ebp
  0046dfb0  5b                    pop       ebx
  0046dfb1  c3                    ret
  0046dfb2  90                    nop
  0046dfb3  90                    nop
  0046dfb4  90                    nop
  0046dfb5  90                    nop
  0046dfb6  90                    nop
  0046dfb7  90                    nop
  0046dfb8  90                    nop
  0046dfb9  90                    nop
  0046dfba  90                    nop
  0046dfbb  90                    nop
  0046dfbc  90                    nop
  0046dfbd  90                    nop
  0046dfbe  90                    nop
  0046dfbf  90                    nop

7 cible(s) de saut interne : 0x0046deb0, 0x0046deb3, 0x0046dee5, 0x0046df40, 0x0046df48, 0x0046df4c, 0x0046dfa0
