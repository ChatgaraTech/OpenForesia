Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040cda0  section .text  offset fichier 0xc1a0  256 octets

  0040cda0  53                    push      ebx
  0040cda1  56                    push      esi
  0040cda2  8b74240c              mov       esi,[esp+0Ch]
  0040cda6  33db                  xor       ebx,ebx
  0040cda8  57                    push      edi
  0040cda9  66395e10              cmp       [esi+10h],bx
  0040cdad  755b                  jne       short 0040CE0Ah
  0040cdaf  66c746100100          mov       word [esi+10h],1
  0040cdb5  66895e12              mov       [esi+12h],bx
  0040cdb9  a1cc975e00            mov       eax,[5E97CCh]
  0040cdbe  53                    push      ebx
  0040cdbf  53                    push      ebx
  0040cdc0  68002a0000            push      2A00h
  0040cdc5  8b88a8000000          mov       ecx,[eax+0A8h]
  0040cdcb  6800030000            push      300h
  0040cdd0  8b11                  mov       edx,[ecx]
  0040cdd2  660fb6422c            movzx     ax,byte [edx+2Ch]
  0040cdd7  50                    push      eax
  0040cdd8  56                    push      esi
  0040cdd9  e8a2b20500            call      00468080h
  0040cdde  83c418                add       esp,18h
  0040cde1  c7465c01000000        mov       dword [esi+5Ch],1
  0040cde8  bf0b000000            mov       edi,0Bh

LAB_0040cded:
  0040cded  33c9                  xor       ecx,ecx
  0040cdef  33d2                  xor       edx,edx
  0040cdf1  8a4e0a                mov       cl,[esi+0Ah]
  0040cdf4  8a560b                mov       dl,[esi+0Bh]
  0040cdf7  51                    push      ecx
  0040cdf8  52                    push      edx
  0040cdf9  68de000000            push      0DEh
  0040cdfe  56                    push      esi
  0040cdff  e8dcdc0600            call      0047AAE0h
  0040ce04  83c410                add       esp,10h
  0040ce07  4f                    dec       edi
  0040ce08  75e3                  jne       short 0040CDEDh

LAB_0040ce0a:
  0040ce0a  66837e1001            cmp       word [esi+10h],1
  0040ce0f  757f                  jne       short 0040CE90h
  0040ce11  668b4612              mov       ax,[esi+12h]
  0040ce15  663bc3                cmp       ax,bx
  0040ce18  740c                  je        short 0040CE26h
  0040ce1a  663d0300              cmp       ax,3
  0040ce1e  7406                  je        short 0040CE26h
  0040ce20  663d0700              cmp       ax,7
  0040ce24  752e                  jne       short 0040CE54h

LAB_0040ce26:
  0040ce26  663bc3                cmp       ax,bx
  0040ce29  895e5c                mov       [esi+5Ch],ebx
  0040ce2c  7506                  jne       short 0040CE34h
  0040ce2e  66895e16              mov       [esi+16h],bx
  0040ce32  eb06                  jmp       short 0040CE3Ah

LAB_0040ce34:
  0040ce34  66c746160100          mov       word [esi+16h],1

LAB_0040ce3a:
  0040ce3a  33c0                  xor       eax,eax
  0040ce3c  33c9                  xor       ecx,ecx
  0040ce3e  8a460a                mov       al,[esi+0Ah]
  0040ce41  8a4e0b                mov       cl,[esi+0Bh]
  0040ce44  50                    push      eax
  0040ce45  51                    push      ecx
  0040ce46  68de000000            push      0DEh
  0040ce4b  56                    push      esi
  0040ce4c  e88fdc0600            call      0047AAE0h
  0040ce51  83c410                add       esp,10h

LAB_0040ce54:
  0040ce54  66ff4612              inc       word [esi+12h]
  0040ce58  668b4612              mov       ax,[esi+12h]
  0040ce5c  b91e000000            mov       ecx,1Eh
  0040ce61  c60527e05d0001        mov       byte [5DE027h],1
  0040ce68  a801                  test      al,1
  0040ce6a  880d24e05d00          mov       [5DE024h],cl
  0040ce70  7506                  jne       short 0040CE78h
  0040ce72  881d24e05d00          mov       [5DE024h],bl

LAB_0040ce78:
  0040ce78  881d25e05d00          mov       [5DE025h],bl
  0040ce7e  881d26e05d00          mov       [5DE026h],bl
  0040ce84  66394e12              cmp       [esi+12h],cx
  0040ce88  7c06                  jl        short 0040CE90h
  0040ce8a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040ce90:
  0040ce90  5f                    pop       edi
  0040ce91  5e                    pop       esi
  0040ce92  5b                    pop       ebx
  0040ce93  c3                    ret
  0040ce94  90                    nop
  0040ce95  90                    nop
  0040ce96  90                    nop
  0040ce97  90                    nop
  0040ce98  90                    nop
  0040ce99  90                    nop
  0040ce9a  90                    nop
  0040ce9b  90                    nop
  0040ce9c  90                    nop
  0040ce9d  90                    nop
  0040ce9e  90                    nop
  0040ce9f  90                    nop

8 cible(s) de saut interne : 0x0040cded, 0x0040ce0a, 0x0040ce26, 0x0040ce34, 0x0040ce3a, 0x0040ce54, 0x0040ce78, 0x0040ce90
