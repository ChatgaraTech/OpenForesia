Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00437aa0  section .text  offset fichier 0x36ea0  272 octets

  00437aa0  53                    push      ebx
  00437aa1  56                    push      esi
  00437aa2  8b74240c              mov       esi,[esp+0Ch]
  00437aa6  33db                  xor       ebx,ebx
  00437aa8  66395e10              cmp       [esi+10h],bx
  00437aac  0f85c9000000          jne       near 00437B7Bh
  00437ab2  c7463c680f5500        mov       dword [esi+3Ch],550F68h
  00437ab9  885e40                mov       [esi+40h],bl
  00437abc  e87ff61000            call      00547140h
  00437ac1  99                    cdq
  00437ac2  33c2                  xor       eax,edx
  00437ac4  2bc2                  sub       eax,edx
  00437ac6  25ff070000            and       eax,7FFh
  00437acb  33c2                  xor       eax,edx
  00437acd  2bc2                  sub       eax,edx
  00437acf  05000c0000            add       eax,0C00h
  00437ad4  66894628              mov       [esi+28h],ax
  00437ad8  6689462a              mov       [esi+2Ah],ax
  00437adc  803d2ce55d0010        cmp       byte [5DE52Ch],10h
  00437ae3  7531                  jne       short 00437B16h
  00437ae5  c6460a09              mov       byte [esi+0Ah],9
  00437ae9  e852f61000            call      00547140h
  00437aee  99                    cdq
  00437aef  b996000000            mov       ecx,96h
  00437af4  f7f9                  idiv      ecx
  00437af6  8b4e1c                mov       ecx,[esi+1Ch]
  00437af9  83ea4b                sub       edx,4Bh
  00437afc  c1e20c                shl       edx,0Ch
  00437aff  03ca                  add       ecx,edx
  00437b01  894e1c                mov       [esi+1Ch],ecx
  00437b04  e837f61000            call      00547140h
  00437b09  99                    cdq
  00437b0a  b996000000            mov       ecx,96h
  00437b0f  f7f9                  idiv      ecx
  00437b11  83ea4b                sub       edx,4Bh
  00437b14  eb35                  jmp       short 00437B4Bh

LAB_00437b16:
  00437b16  c6460a06              mov       byte [esi+0Ah],6
  00437b1a  e821f61000            call      00547140h
  00437b1f  99                    cdq
  00437b20  b92c010000            mov       ecx,12Ch
  00437b25  f7f9                  idiv      ecx
  00437b27  8b4e1c                mov       ecx,[esi+1Ch]
  00437b2a  81ea96000000          sub       edx,96h
  00437b30  c1e20c                shl       edx,0Ch
  00437b33  03ca                  add       ecx,edx
  00437b35  894e1c                mov       [esi+1Ch],ecx
  00437b38  e803f61000            call      00547140h
  00437b3d  99                    cdq
  00437b3e  b92c010000            mov       ecx,12Ch
  00437b43  f7f9                  idiv      ecx
  00437b45  81ea96000000          sub       edx,96h

LAB_00437b4b:
  00437b4b  8b4624                mov       eax,[esi+24h]
  00437b4e  c1e20c                shl       edx,0Ch
  00437b51  03c2                  add       eax,edx
  00437b53  894624                mov       [esi+24h],eax
  00437b56  e8e5f51000            call      00547140h
  00437b5b  99                    cdq
  00437b5c  b996000000            mov       ecx,96h
  00437b61  885e0b                mov       [esi+0Bh],bl
  00437b64  f7f9                  idiv      ecx
  00437b66  8b4620                mov       eax,[esi+20h]
  00437b69  66c746100100          mov       word [esi+10h],1
  00437b6f  66895e12              mov       [esi+12h],bx
  00437b73  c1e20c                shl       edx,0Ch
  00437b76  03c2                  add       eax,edx
  00437b78  894620                mov       [esi+20h],eax

LAB_00437b7b:
  00437b7b  66837e1001            cmp       word [esi+10h],1
  00437b80  7523                  jne       short 00437BA5h
  00437b82  66ff4612              inc       word [esi+12h]
  00437b86  66837e1202            cmp       word [esi+12h],2
  00437b8b  7c18                  jl        short 00437BA5h
  00437b8d  8a4e40                mov       cl,[esi+40h]
  00437b90  66895e12              mov       [esi+12h],bx
  00437b94  fec1                  inc       cl
  00437b96  8ac1                  mov       al,cl
  00437b98  884e40                mov       [esi+40h],cl
  00437b9b  3c04                  cmp       al,4
  00437b9d  7206                  jb        short 00437BA5h
  00437b9f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00437ba5:
  00437ba5  5e                    pop       esi
  00437ba6  5b                    pop       ebx
  00437ba7  c3                    ret
  00437ba8  90                    nop
  00437ba9  90                    nop
  00437baa  90                    nop
  00437bab  90                    nop
  00437bac  90                    nop
  00437bad  90                    nop
  00437bae  90                    nop
  00437baf  90                    nop

4 cible(s) de saut interne : 0x00437b16, 0x00437b4b, 0x00437b7b, 0x00437ba5
