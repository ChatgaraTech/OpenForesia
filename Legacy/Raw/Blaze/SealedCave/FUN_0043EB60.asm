Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043eb60  section .text  offset fichier 0x3df60  256 octets

  0043eb60  53                    push      ebx
  0043eb61  56                    push      esi
  0043eb62  8b74240c              mov       esi,[esp+0Ch]
  0043eb66  33db                  xor       ebx,ebx
  0043eb68  57                    push      edi
  0043eb69  66395e10              cmp       [esi+10h],bx
  0043eb6d  755b                  jne       short 0043EBCAh
  0043eb6f  66c746100100          mov       word [esi+10h],1
  0043eb75  66895e12              mov       [esi+12h],bx
  0043eb79  a1cc975e00            mov       eax,[5E97CCh]
  0043eb7e  53                    push      ebx
  0043eb7f  53                    push      ebx
  0043eb80  68002a0000            push      2A00h
  0043eb85  8b88a8000000          mov       ecx,[eax+0A8h]
  0043eb8b  6800030000            push      300h
  0043eb90  8b11                  mov       edx,[ecx]
  0043eb92  660fb6422c            movzx     ax,byte [edx+2Ch]
  0043eb97  50                    push      eax
  0043eb98  56                    push      esi
  0043eb99  e8e2940200            call      00468080h
  0043eb9e  83c418                add       esp,18h
  0043eba1  c7465c01000000        mov       dword [esi+5Ch],1
  0043eba8  bf0b000000            mov       edi,0Bh

LAB_0043ebad:
  0043ebad  33c9                  xor       ecx,ecx
  0043ebaf  33d2                  xor       edx,edx
  0043ebb1  8a4e0a                mov       cl,[esi+0Ah]
  0043ebb4  8a560b                mov       dl,[esi+0Bh]
  0043ebb7  51                    push      ecx
  0043ebb8  52                    push      edx
  0043ebb9  68e1000000            push      0E1h
  0043ebbe  56                    push      esi
  0043ebbf  e81cbf0300            call      0047AAE0h
  0043ebc4  83c410                add       esp,10h
  0043ebc7  4f                    dec       edi
  0043ebc8  75e3                  jne       short 0043EBADh

LAB_0043ebca:
  0043ebca  66837e1001            cmp       word [esi+10h],1
  0043ebcf  757f                  jne       short 0043EC50h
  0043ebd1  668b4612              mov       ax,[esi+12h]
  0043ebd5  663bc3                cmp       ax,bx
  0043ebd8  740c                  je        short 0043EBE6h
  0043ebda  663d0300              cmp       ax,3
  0043ebde  7406                  je        short 0043EBE6h
  0043ebe0  663d0700              cmp       ax,7
  0043ebe4  752e                  jne       short 0043EC14h

LAB_0043ebe6:
  0043ebe6  663bc3                cmp       ax,bx
  0043ebe9  895e5c                mov       [esi+5Ch],ebx
  0043ebec  7506                  jne       short 0043EBF4h
  0043ebee  66895e16              mov       [esi+16h],bx
  0043ebf2  eb06                  jmp       short 0043EBFAh

LAB_0043ebf4:
  0043ebf4  66c746160100          mov       word [esi+16h],1

LAB_0043ebfa:
  0043ebfa  33c0                  xor       eax,eax
  0043ebfc  33c9                  xor       ecx,ecx
  0043ebfe  8a460a                mov       al,[esi+0Ah]
  0043ec01  8a4e0b                mov       cl,[esi+0Bh]
  0043ec04  50                    push      eax
  0043ec05  51                    push      ecx
  0043ec06  68e1000000            push      0E1h
  0043ec0b  56                    push      esi
  0043ec0c  e8cfbe0300            call      0047AAE0h
  0043ec11  83c410                add       esp,10h

LAB_0043ec14:
  0043ec14  66ff4612              inc       word [esi+12h]
  0043ec18  668b4612              mov       ax,[esi+12h]
  0043ec1c  b91e000000            mov       ecx,1Eh
  0043ec21  c60527e05d0001        mov       byte [5DE027h],1
  0043ec28  a801                  test      al,1
  0043ec2a  880d24e05d00          mov       [5DE024h],cl
  0043ec30  7506                  jne       short 0043EC38h
  0043ec32  881d24e05d00          mov       [5DE024h],bl

LAB_0043ec38:
  0043ec38  881d25e05d00          mov       [5DE025h],bl
  0043ec3e  881d26e05d00          mov       [5DE026h],bl
  0043ec44  66394e12              cmp       [esi+12h],cx
  0043ec48  7c06                  jl        short 0043EC50h
  0043ec4a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043ec50:
  0043ec50  5f                    pop       edi
  0043ec51  5e                    pop       esi
  0043ec52  5b                    pop       ebx
  0043ec53  c3                    ret
  0043ec54  90                    nop
  0043ec55  90                    nop
  0043ec56  90                    nop
  0043ec57  90                    nop
  0043ec58  90                    nop
  0043ec59  90                    nop
  0043ec5a  90                    nop
  0043ec5b  90                    nop
  0043ec5c  90                    nop
  0043ec5d  90                    nop
  0043ec5e  90                    nop
  0043ec5f  90                    nop

8 cible(s) de saut interne : 0x0043ebad, 0x0043ebca, 0x0043ebe6, 0x0043ebf4, 0x0043ebfa, 0x0043ec14, 0x0043ec38, 0x0043ec50
