Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042ff50  section .text  offset fichier 0x2f350  240 octets

  0042ff50  56                    push      esi
  0042ff51  8b742408              mov       esi,[esp+8]
  0042ff55  66837e1000            cmp       word [esi+10h],0
  0042ff5a  0f8589000000          jne       near 0042FFE9h
  0042ff60  66c746100100          mov       word [esi+10h],1
  0042ff66  c7463c88095500        mov       dword [esi+3Ch],550988h
  0042ff6d  c6460a06              mov       byte [esi+0Ah],6
  0042ff71  c6460b00              mov       byte [esi+0Bh],0
  0042ff75  e8c6711100            call      00547140h
  0042ff7a  99                    cdq
  0042ff7b  b9a0000000            mov       ecx,0A0h
  0042ff80  f7f9                  idiv      ecx
  0042ff82  8b461c                mov       eax,[esi+1Ch]
  0042ff85  83ea50                sub       edx,50h
  0042ff88  c1e20c                shl       edx,0Ch
  0042ff8b  03c2                  add       eax,edx
  0042ff8d  89461c                mov       [esi+1Ch],eax
  0042ff90  e8ab711100            call      00547140h
  0042ff95  99                    cdq
  0042ff96  b9a0000000            mov       ecx,0A0h
  0042ff9b  f7f9                  idiv      ecx
  0042ff9d  8b4e24                mov       ecx,[esi+24h]
  0042ffa0  83ea50                sub       edx,50h
  0042ffa3  c1e20c                shl       edx,0Ch
  0042ffa6  03ca                  add       ecx,edx
  0042ffa8  894e24                mov       [esi+24h],ecx
  0042ffab  e890711100            call      00547140h
  0042ffb0  99                    cdq
  0042ffb1  b914000000            mov       ecx,14h
  0042ffb6  66c746140000          mov       word [esi+14h],0
  0042ffbc  f7f9                  idiv      ecx
  0042ffbe  8b4e20                mov       ecx,[esi+20h]
  0042ffc1  b8c4090000            mov       eax,9C4h
  0042ffc6  66894628              mov       [esi+28h],ax
  0042ffca  6689462a              mov       [esi+2Ah],ax
  0042ffce  c1e20c                shl       edx,0Ch
  0042ffd1  03ca                  add       ecx,edx
  0042ffd3  894e20                mov       [esi+20h],ecx
  0042ffd6  e865711100            call      00547140h
  0042ffdb  99                    cdq
  0042ffdc  b90a000000            mov       ecx,0Ah
  0042ffe1  f7f9                  idiv      ecx
  0042ffe3  03d1                  add       edx,ecx
  0042ffe5  66895612              mov       [esi+12h],dx

LAB_0042ffe9:
  0042ffe9  66837e1001            cmp       word [esi+10h],1
  0042ffee  754b                  jne       short 0043003Bh
  0042fff0  668b4612              mov       ax,[esi+12h]
  0042fff4  8b4e20                mov       ecx,[esi+20h]
  0042fff7  0fbfd0                movsx     edx,ax
  0042fffa  c1e20c                shl       edx,0Ch
  0042fffd  03ca                  add       ecx,edx
  0042ffff  66ff4614              inc       word [esi+14h]
  00430003  66837e1402            cmp       word [esi+14h],2
  00430008  894e20                mov       [esi+20h],ecx
  0043000b  750b                  jne       short 00430018h
  0043000d  48                    dec       eax
  0043000e  66c746140000          mov       word [esi+14h],0
  00430014  66894612              mov       [esi+12h],ax

LAB_00430018:
  00430018  668b4612              mov       ax,[esi+12h]
  0043001c  663d0700              cmp       ax,7
  00430020  7d0d                  jge       short 0043002Fh
  00430022  b938ffffff            mov       ecx,0FFFFFF38h
  00430027  66014e28              add       [esi+28h],cx
  0043002b  66014e2a              add       [esi+2Ah],cx

LAB_0043002f:
  0043002f  663d0300              cmp       ax,3
  00430033  7d06                  jge       short 0043003Bh
  00430035  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043003b:
  0043003b  5e                    pop       esi
  0043003c  c3                    ret
  0043003d  90                    nop
  0043003e  90                    nop
  0043003f  90                    nop

4 cible(s) de saut interne : 0x0042ffe9, 0x00430018, 0x0043002f, 0x0043003b
