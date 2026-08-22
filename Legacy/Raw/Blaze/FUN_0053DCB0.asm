Legacy/Binaries/Blaze.exe  base 0x00400000
0x0053dcb0  section .text  offset fichier 0x13d0b0  112 octets

  0053dcb0  a1b4876000            mov       eax,[6087B4h]
  0053dcb5  53                    push      ebx
  0053dcb6  8b0d20905e00          mov       ecx,[5E9020h]
  0053dcbc  56                    push      esi
  0053dcbd  8b352ce05d00          mov       esi,[5DE02Ch]
  0053dcc3  33db                  xor       ebx,ebx
  0053dcc5  83ce08                or        esi,8
  0053dcc8  881d44e05d00          mov       [5DE044h],bl
  0053dcce  89352ce05d00          mov       [5DE02Ch],esi
  0053dcd4  8b00                  mov       eax,[eax]
  0053dcd6  0500000f00            add       eax,0F0000h
  0053dcdb  33f6                  xor       esi,esi
  0053dcdd  3bcb                  cmp       ecx,ebx
  0053dcdf  7e36                  jle       short 0053DD17h
  0053dce1  57                    push      edi
  0053dce2  8d9048010000          lea       edx,[eax+148h]

LAB_0053dce8:
  0053dce8  668b0a                mov       cx,[edx]
  0053dceb  668b4202              mov       ax,[edx+2]
  0053dcef  66894a04              mov       [edx+4],cx
  0053dcf3  66894206              mov       [edx+6],ax
  0053dcf7  b920000000            mov       ecx,20h
  0053dcfc  33c0                  xor       eax,eax
  0053dcfe  8d7a38                lea       edi,[edx+38h]
  0053dd01  895af8                mov       [edx-8],ebx
  0053dd04  f3ab                  rep stosd
  0053dd06  a120905e00            mov       eax,[5E9020h]
  0053dd0b  46                    inc       esi
  0053dd0c  81c200200000          add       edx,2000h
  0053dd12  3bf0                  cmp       esi,eax
  0053dd14  7cd2                  jl        short 0053DCE8h
  0053dd16  5f                    pop       edi

LAB_0053dd17:
  0053dd17  5e                    pop       esi
  0053dd18  5b                    pop       ebx
  0053dd19  c3                    ret
  0053dd1a  90                    nop
  0053dd1b  90                    nop
  0053dd1c  90                    nop
  0053dd1d  90                    nop
  0053dd1e  90                    nop
  0053dd1f  90                    nop

2 cible(s) de saut interne : 0x0053dce8, 0x0053dd17
