
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041f600  section .text  offset fichier 0x1ea00  352 octets

  0041f600  53                    push      ebx
  0041f601  55                    push      ebp
  0041f602  56                    push      esi
  0041f603  8b742410              mov       esi,[esp+10h]
  0041f607  57                    push      edi
  0041f608  8b5e6c                mov       ebx,[esi+6Ch]
  0041f60b  8d6e1c                lea       ebp,[esi+1Ch]
  0041f60e  8b431c                mov       eax,[ebx+1Ch]
  0041f611  894500                mov       [ebp],eax
  0041f614  8b4b20                mov       ecx,[ebx+20h]
  0041f617  894e20                mov       [esi+20h],ecx
  0041f61a  8b5324                mov       edx,[ebx+24h]
  0041f61d  895624                mov       [esi+24h],edx
  0041f620  8b03                  mov       eax,[ebx]
  0041f622  a900000040            test      eax,40000000h
  0041f627  0f85f2000000          jne       near 0041F71Fh
  0041f62d  a900000080            test      eax,80000000h
  0041f632  0f84e7000000          je        near 0041F71Fh
  0041f638  66837e1000            cmp       word [esi+10h],0
  0041f63d  7535                  jne       short 0041F674h
  0041f63f  c7464400000000        mov       dword [esi+44h],0
  0041f646  bf0b000000            mov       edi,0Bh

LAB_0041f64b:
  0041f64b  6aff                  push      0FFFFFFFFh
  0041f64d  6a00                  push      0
  0041f64f  68ed000000            push      0EDh
  0041f654  6a00                  push      0
  0041f656  55                    push      ebp
  0041f657  56                    push      esi
  0041f658  e8e3b50500            call      0047AC40h
  0041f65d  83c418                add       esp,18h
  0041f660  4f                    dec       edi
  0041f661  75e8                  jne       short 0041F64Bh
  0041f663  b801000000            mov       eax,1
  0041f668  66c746120000          mov       word [esi+12h],0
  0041f66e  66894610              mov       [esi+10h],ax
  0041f672  eb05                  jmp       short 0041F679h

LAB_0041f674:
  0041f674  b801000000            mov       eax,1

LAB_0041f679:
  0041f679  66394610              cmp       [esi+10h],ax
  0041f67d  bf02000000            mov       edi,2
  0041f682  7552                  jne       short 0041F6D6h
  0041f684  66ff4612              inc       word [esi+12h]
  0041f688  66837e1232            cmp       word [esi+12h],32h
  0041f68d  7e06                  jle       short 0041F695h
  0041f68f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041f695:
  0041f695  394644                cmp       [esi+44h],eax
  0041f698  753c                  jne       short 0041F6D6h
  0041f69a  66897e10              mov       [esi+10h],di
  0041f69e  e89d7a1200            call      00547140h
  0041f6a3  99                    cdq
  0041f6a4  b90a000000            mov       ecx,0Ah
  0041f6a9  6a00                  push      0
  0041f6ab  f7f9                  idiv      ecx
  0041f6ad  66c746140000          mov       word [esi+14h],0
  0041f6b3  83c20f                add       edx,0Fh
  0041f6b6  66895612              mov       [esi+12h],dx
  0041f6ba  8b5678                mov       edx,[esi+78h]
  0041f6bd  52                    push      edx
  0041f6be  53                    push      ebx
  0041f6bf  e81ccf1100            call      0053C5E0h
  0041f6c4  83c40c                add       esp,0Ch
  0041f6c7  85c0                  test      eax,eax
  0041f6c9  740b                  je        short 0041F6D6h
  0041f6cb  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041f6d1  5f                    pop       edi
  0041f6d2  5e                    pop       esi
  0041f6d3  5d                    pop       ebp
  0041f6d4  5b                    pop       ebx
  0041f6d5  c3                    ret

LAB_0041f6d6:
  0041f6d6  66397e10              cmp       [esi+10h],di
  0041f6da  757b                  jne       short 0041F757h
  0041f6dc  66ff4614              inc       word [esi+14h]
  0041f6e0  66837e1403            cmp       word [esi+14h],3
  0041f6e5  7570                  jne       short 0041F757h
  0041f6e7  66837e1200            cmp       word [esi+12h],0
  0041f6ec  66c746140000          mov       word [esi+14h],0
  0041f6f2  7e15                  jle       short 0041F709h
  0041f6f4  6aff                  push      0FFFFFFFFh
  0041f6f6  6a00                  push      0
  0041f6f8  68ed000000            push      0EDh
  0041f6fd  6a00                  push      0
  0041f6ff  55                    push      ebp
  0041f700  56                    push      esi
  0041f701  e83ab50500            call      0047AC40h
  0041f706  83c418                add       esp,18h

LAB_0041f709:
  0041f709  66ff4e12              dec       word [esi+12h]
  0041f70d  66837e12dd            cmp       word [esi+12h],0FFDDh
  0041f712  7f43                  jg        short 0041F757h
  0041f714  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041f71a  5f                    pop       edi
  0041f71b  5e                    pop       esi
  0041f71c  5d                    pop       ebp
  0041f71d  5b                    pop       ebx
  0041f71e  c3                    ret

LAB_0041f71f:
  0041f71f  8b0e                  mov       ecx,[esi]
  0041f721  81e1ffffff7f          and       ecx,7FFFFFFFh
  0041f727  890e                  mov       [esi],ecx
  0041f729  668bb69a000000        mov       si,[esi+9Ah]
  0041f730  6685f6                test      si,si
  0041f733  7406                  je        short 0041F73Bh
  0041f735  6683fe01              cmp       si,1
  0041f739  751c                  jne       short 0041F757h

LAB_0041f73b:
  0041f73b  c60527e05d0001        mov       byte [5DE027h],1
  0041f742  c60524e05d0000        mov       byte [5DE024h],0
  0041f749  c60525e05d0000        mov       byte [5DE025h],0
  0041f750  c60526e05d0000        mov       byte [5DE026h],0

LAB_0041f757:
  0041f757  5f                    pop       edi
  0041f758  5e                    pop       esi
  0041f759  5d                    pop       ebp
  0041f75a  5b                    pop       ebx
  0041f75b  c3                    ret

9 cible(s) de saut interne : 0x0041f64b, 0x0041f674, 0x0041f679, 0x0041f695, 0x0041f6d6, 0x0041f709, 0x0041f71f, 0x0041f73b, 0x0041f757
