Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00435f70  section .text  offset fichier 0x35370  256 octets

  00435f70  53                    push      ebx
  00435f71  56                    push      esi
  00435f72  8b74240c              mov       esi,[esp+0Ch]
  00435f76  33db                  xor       ebx,ebx
  00435f78  57                    push      edi
  00435f79  66395e10              cmp       [esi+10h],bx
  00435f7d  755b                  jne       short 00435FDAh
  00435f7f  66c746100100          mov       word [esi+10h],1
  00435f85  66895e12              mov       [esi+12h],bx
  00435f89  a1cc975e00            mov       eax,[5E97CCh]
  00435f8e  53                    push      ebx
  00435f8f  53                    push      ebx
  00435f90  68002a0000            push      2A00h
  00435f95  8b88a8000000          mov       ecx,[eax+0A8h]
  00435f9b  6800030000            push      300h
  00435fa0  8b11                  mov       edx,[ecx]
  00435fa2  660fb6422c            movzx     ax,byte [edx+2Ch]
  00435fa7  50                    push      eax
  00435fa8  56                    push      esi
  00435fa9  e8d2200300            call      00468080h
  00435fae  83c418                add       esp,18h
  00435fb1  c7465c01000000        mov       dword [esi+5Ch],1
  00435fb8  bf0b000000            mov       edi,0Bh

LAB_00435fbd:
  00435fbd  33c9                  xor       ecx,ecx
  00435fbf  33d2                  xor       edx,edx
  00435fc1  8a4e0a                mov       cl,[esi+0Ah]
  00435fc4  8a560b                mov       dl,[esi+0Bh]
  00435fc7  51                    push      ecx
  00435fc8  52                    push      edx
  00435fc9  68de000000            push      0DEh
  00435fce  56                    push      esi
  00435fcf  e80c4b0400            call      0047AAE0h
  00435fd4  83c410                add       esp,10h
  00435fd7  4f                    dec       edi
  00435fd8  75e3                  jne       short 00435FBDh

LAB_00435fda:
  00435fda  66837e1001            cmp       word [esi+10h],1
  00435fdf  757f                  jne       short 00436060h
  00435fe1  668b4612              mov       ax,[esi+12h]
  00435fe5  663bc3                cmp       ax,bx
  00435fe8  740c                  je        short 00435FF6h
  00435fea  663d0300              cmp       ax,3
  00435fee  7406                  je        short 00435FF6h
  00435ff0  663d0700              cmp       ax,7
  00435ff4  752e                  jne       short 00436024h

LAB_00435ff6:
  00435ff6  663bc3                cmp       ax,bx
  00435ff9  895e5c                mov       [esi+5Ch],ebx
  00435ffc  7506                  jne       short 00436004h
  00435ffe  66895e16              mov       [esi+16h],bx
  00436002  eb06                  jmp       short 0043600Ah

LAB_00436004:
  00436004  66c746160100          mov       word [esi+16h],1

LAB_0043600a:
  0043600a  33c0                  xor       eax,eax
  0043600c  33c9                  xor       ecx,ecx
  0043600e  8a460a                mov       al,[esi+0Ah]
  00436011  8a4e0b                mov       cl,[esi+0Bh]
  00436014  50                    push      eax
  00436015  51                    push      ecx
  00436016  68de000000            push      0DEh
  0043601b  56                    push      esi
  0043601c  e8bf4a0400            call      0047AAE0h
  00436021  83c410                add       esp,10h

LAB_00436024:
  00436024  66ff4612              inc       word [esi+12h]
  00436028  668b4612              mov       ax,[esi+12h]
  0043602c  b91e000000            mov       ecx,1Eh
  00436031  c60527e05d0001        mov       byte [5DE027h],1
  00436038  a801                  test      al,1
  0043603a  880d24e05d00          mov       [5DE024h],cl
  00436040  7506                  jne       short 00436048h
  00436042  881d24e05d00          mov       [5DE024h],bl

LAB_00436048:
  00436048  881d25e05d00          mov       [5DE025h],bl
  0043604e  881d26e05d00          mov       [5DE026h],bl
  00436054  66394e12              cmp       [esi+12h],cx
  00436058  7c06                  jl        short 00436060h
  0043605a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00436060:
  00436060  5f                    pop       edi
  00436061  5e                    pop       esi
  00436062  5b                    pop       ebx
  00436063  c3                    ret
  00436064  90                    nop
  00436065  90                    nop
  00436066  90                    nop
  00436067  90                    nop
  00436068  90                    nop
  00436069  90                    nop
  0043606a  90                    nop
  0043606b  90                    nop
  0043606c  90                    nop
  0043606d  90                    nop
  0043606e  90                    nop
  0043606f  90                    nop

8 cible(s) de saut interne : 0x00435fbd, 0x00435fda, 0x00435ff6, 0x00436004, 0x0043600a, 0x00436024, 0x00436048, 0x00436060
