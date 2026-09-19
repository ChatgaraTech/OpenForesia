Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046c160  section .text  offset fichier 0x6b560  256 octets

  0046c160  53                    push      ebx
  0046c161  56                    push      esi
  0046c162  8b74240c              mov       esi,[esp+0Ch]
  0046c166  33db                  xor       ebx,ebx
  0046c168  57                    push      edi
  0046c169  66395e10              cmp       [esi+10h],bx
  0046c16d  7521                  jne       short 0046C190h
  0046c16f  66837e1405            cmp       word [esi+14h],5
  0046c174  66c746100100          mov       word [esi+10h],1
  0046c17a  66895e12              mov       [esi+12h],bx
  0046c17e  895e5c                mov       [esi+5Ch],ebx
  0046c181  7f06                  jg        short 0046C189h
  0046c183  66c746140600          mov       word [esi+14h],6

LAB_0046c189:
  0046c189  66899e98000000        mov       [esi+98h],bx

LAB_0046c190:
  0046c190  66837e1001            cmp       word [esi+10h],1
  0046c195  0f85be000000          jne       near 0046C259h
  0046c19b  8b7e5c                mov       edi,[esi+5Ch]
  0046c19e  3bfb                  cmp       edi,ebx
  0046c1a0  7504                  jne       short 0046C1A6h
  0046c1a2  0fbf7e14              movsx     edi,word [esi+14h]

LAB_0046c1a6:
  0046c1a6  0fbf4612              movsx     eax,word [esi+12h]
  0046c1aa  50                    push      eax
  0046c1ab  e86c980d00            call      00545A1Ch
  0046c1b0  99                    cdq
  0046c1b1  f7ff                  idiv      edi
  0046c1b3  668b4e16              mov       cx,[esi+16h]
  0046c1b7  66014e12              add       [esi+12h],cx
  0046c1bb  66894634              mov       [esi+34h],ax
  0046c1bf  668b4612              mov       ax,[esi+12h]
  0046c1c3  663d0008              cmp       ax,800h
  0046c1c7  7e3b                  jle       short 0046C204h
  0046c1c9  66399e98000000        cmp       [esi+98h],bx
  0046c1d0  7532                  jne       short 0046C204h
  0046c1d2  66c786980000000100    mov       word [esi+98h],1
  0046c1db  8b15cc975e00          mov       edx,[5E97CCh]
  0046c1e1  53                    push      ebx
  0046c1e2  53                    push      ebx
  0046c1e3  8b82a8000000          mov       eax,[edx+0A8h]
  0046c1e9  6800100000            push      1000h
  0046c1ee  6800050000            push      500h
  0046c1f3  8b08                  mov       ecx,[eax]
  0046c1f5  660fb65123            movzx     dx,byte [ecx+23h]
  0046c1fa  52                    push      edx
  0046c1fb  56                    push      esi
  0046c1fc  e87fbeffff            call      00468080h
  0046c201  83c418                add       esp,18h

LAB_0046c204:
  0046c204  66817e120010          cmp       word [esi+12h],1000h
  0046c20a  7c4d                  jl        short 0046C259h
  0046c20c  a1cc975e00            mov       eax,[5E97CCh]
  0046c211  53                    push      ebx
  0046c212  53                    push      ebx
  0046c213  6800100000            push      1000h
  0046c218  8b88a8000000          mov       ecx,[eax+0A8h]
  0046c21e  6800050000            push      500h
  0046c223  8b11                  mov       edx,[ecx]
  0046c225  660fb64223            movzx     ax,byte [edx+23h]
  0046c22a  50                    push      eax
  0046c22b  56                    push      esi
  0046c22c  e84fbeffff            call      00468080h
  0046c231  8b465c                mov       eax,[esi+5Ch]
  0046c234  6681461200f0          add       word [esi+12h],0F000h
  0046c23a  83c418                add       esp,18h
  0046c23d  3bc3                  cmp       eax,ebx
  0046c23f  66899e98000000        mov       [esi+98h],bx
  0046c246  7407                  je        short 0046C24Fh
  0046c248  895e5c                mov       [esi+5Ch],ebx
  0046c24b  5f                    pop       edi
  0046c24c  5e                    pop       esi
  0046c24d  5b                    pop       ebx
  0046c24e  c3                    ret

LAB_0046c24f:
  0046c24f  0fbf4e14              movsx     ecx,word [esi+14h]
  0046c253  83e905                sub       ecx,5
  0046c256  894e5c                mov       [esi+5Ch],ecx

LAB_0046c259:
  0046c259  5f                    pop       edi
  0046c25a  5e                    pop       esi
  0046c25b  5b                    pop       ebx
  0046c25c  c3                    ret
  0046c25d  90                    nop
  0046c25e  90                    nop
  0046c25f  90                    nop

6 cible(s) de saut interne : 0x0046c189, 0x0046c190, 0x0046c1a6, 0x0046c204, 0x0046c24f, 0x0046c259
