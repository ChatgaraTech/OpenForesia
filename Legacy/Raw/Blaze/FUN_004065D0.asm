
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004065d0  section .text  offset fichier 0x59d0  176 octets

  004065d0  53                    push      ebx
  004065d1  56                    push      esi
  004065d2  8b74240c              mov       esi,[esp+0Ch]
  004065d6  33db                  xor       ebx,ebx
  004065d8  8b466c                mov       eax,[esi+6Ch]
  004065db  3bc3                  cmp       eax,ebx
  004065dd  741a                  je        short 004065F9h
  004065df  8b481c                mov       ecx,[eax+1Ch]
  004065e2  894e1c                mov       [esi+1Ch],ecx
  004065e5  8b5060                mov       edx,[eax+60h]
  004065e8  895620                mov       [esi+20h],edx
  004065eb  8b4824                mov       ecx,[eax+24h]
  004065ee  894e24                mov       [esi+24h],ecx
  004065f1  668b5010              mov       dx,[eax+10h]
  004065f5  66895612              mov       [esi+12h],dx

LAB_004065f9:
  004065f9  66395e10              cmp       [esi+10h],bx
  004065fd  b801000000            mov       eax,1
  00406602  7524                  jne       short 00406628h
  00406604  66894610              mov       [esi+10h],ax
  00406608  c7463cc0f25400        mov       dword [esi+3Ch],54F2C0h
  0040660f  885e40                mov       [esi+40h],bl
  00406612  c6460a03              mov       byte [esi+0Ah],3
  00406616  885e0b                mov       [esi+0Bh],bl
  00406619  66c7462ef6ff          mov       word [esi+2Eh],0FFF6h
  0040661f  885e38                mov       [esi+38h],bl
  00406622  885e39                mov       [esi+39h],bl
  00406625  885e3a                mov       [esi+3Ah],bl

LAB_00406628:
  00406628  66394610              cmp       [esi+10h],ax
  0040662c  7541                  jne       short 0040666Fh
  0040662e  8b4e60                mov       ecx,[esi+60h]
  00406631  66394612              cmp       [esi+12h],ax
  00406635  894e20                mov       [esi+20h],ecx
  00406638  750f                  jne       short 00406649h
  0040663a  6a02                  push      2
  0040663c  6a02                  push      2
  0040663e  6a02                  push      2
  00406640  56                    push      esi
  00406641  e89ae7ffff            call      00404DE0h
  00406646  83c410                add       esp,10h

LAB_00406649:
  00406649  66837e1202            cmp       word [esi+12h],2
  0040664e  750f                  jne       short 0040665Fh
  00406650  6afd                  push      0FFFFFFFDh
  00406652  6afd                  push      0FFFFFFFDh
  00406654  6afd                  push      0FFFFFFFDh
  00406656  56                    push      esi
  00406657  e884e7ffff            call      00404DE0h
  0040665c  83c410                add       esp,10h

LAB_0040665f:
  0040665f  66837e1265            cmp       word [esi+12h],65h
  00406664  7509                  jne       short 0040666Fh
  00406666  885e38                mov       [esi+38h],bl
  00406669  885e39                mov       [esi+39h],bl
  0040666c  885e3a                mov       [esi+3Ah],bl

LAB_0040666f:
  0040666f  5e                    pop       esi
  00406670  5b                    pop       ebx
  00406671  c3                    ret
  00406672  90                    nop
  00406673  90                    nop
  00406674  90                    nop
  00406675  90                    nop
  00406676  90                    nop
  00406677  90                    nop
  00406678  90                    nop
  00406679  90                    nop
  0040667a  90                    nop
  0040667b  90                    nop
  0040667c  90                    nop
  0040667d  90                    nop
  0040667e  90                    nop
  0040667f  90                    nop

5 cible(s) de saut interne : 0x004065f9, 0x00406628, 0x00406649, 0x0040665f, 0x0040666f


