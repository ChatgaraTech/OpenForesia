Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00430f10  section .text  offset fichier 0x30310  240 octets

  00430f10  56                    push      esi
  00430f11  8b742408              mov       esi,[esp+8]
  00430f15  66837e1000            cmp       word [esi+10h],0
  00430f1a  0f8589000000          jne       near 00430FA9h
  00430f20  66c746100100          mov       word [esi+10h],1
  00430f26  c7463c880b5500        mov       dword [esi+3Ch],550B88h
  00430f2d  c6460a06              mov       byte [esi+0Ah],6
  00430f31  c6460b00              mov       byte [esi+0Bh],0
  00430f35  e806621100            call      00547140h
  00430f3a  99                    cdq
  00430f3b  b9a0000000            mov       ecx,0A0h
  00430f40  f7f9                  idiv      ecx
  00430f42  8b461c                mov       eax,[esi+1Ch]
  00430f45  83ea50                sub       edx,50h
  00430f48  c1e20c                shl       edx,0Ch
  00430f4b  03c2                  add       eax,edx
  00430f4d  89461c                mov       [esi+1Ch],eax
  00430f50  e8eb611100            call      00547140h
  00430f55  99                    cdq
  00430f56  b9a0000000            mov       ecx,0A0h
  00430f5b  f7f9                  idiv      ecx
  00430f5d  8b4e24                mov       ecx,[esi+24h]
  00430f60  83ea50                sub       edx,50h
  00430f63  c1e20c                shl       edx,0Ch
  00430f66  03ca                  add       ecx,edx
  00430f68  894e24                mov       [esi+24h],ecx
  00430f6b  e8d0611100            call      00547140h
  00430f70  99                    cdq
  00430f71  b914000000            mov       ecx,14h
  00430f76  66c746140000          mov       word [esi+14h],0
  00430f7c  f7f9                  idiv      ecx
  00430f7e  8b4e20                mov       ecx,[esi+20h]
  00430f81  b8c4090000            mov       eax,9C4h
  00430f86  66894628              mov       [esi+28h],ax
  00430f8a  6689462a              mov       [esi+2Ah],ax
  00430f8e  c1e20c                shl       edx,0Ch
  00430f91  03ca                  add       ecx,edx
  00430f93  894e20                mov       [esi+20h],ecx
  00430f96  e8a5611100            call      00547140h
  00430f9b  99                    cdq
  00430f9c  b90a000000            mov       ecx,0Ah
  00430fa1  f7f9                  idiv      ecx
  00430fa3  03d1                  add       edx,ecx
  00430fa5  66895612              mov       [esi+12h],dx

LAB_00430fa9:
  00430fa9  66837e1001            cmp       word [esi+10h],1
  00430fae  754b                  jne       short 00430FFBh
  00430fb0  668b4612              mov       ax,[esi+12h]
  00430fb4  8b4e20                mov       ecx,[esi+20h]
  00430fb7  0fbfd0                movsx     edx,ax
  00430fba  c1e20c                shl       edx,0Ch
  00430fbd  03ca                  add       ecx,edx
  00430fbf  66ff4614              inc       word [esi+14h]
  00430fc3  66837e1402            cmp       word [esi+14h],2
  00430fc8  894e20                mov       [esi+20h],ecx
  00430fcb  750b                  jne       short 00430FD8h
  00430fcd  48                    dec       eax
  00430fce  66c746140000          mov       word [esi+14h],0
  00430fd4  66894612              mov       [esi+12h],ax

LAB_00430fd8:
  00430fd8  668b4612              mov       ax,[esi+12h]
  00430fdc  663d0700              cmp       ax,7
  00430fe0  7d0d                  jge       short 00430FEFh
  00430fe2  b938ffffff            mov       ecx,0FFFFFF38h
  00430fe7  66014e28              add       [esi+28h],cx
  00430feb  66014e2a              add       [esi+2Ah],cx

LAB_00430fef:
  00430fef  663d0300              cmp       ax,3
  00430ff3  7d06                  jge       short 00430FFBh
  00430ff5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00430ffb:
  00430ffb  5e                    pop       esi
  00430ffc  c3                    ret
  00430ffd  90                    nop
  00430ffe  90                    nop
  00430fff  90                    nop

4 cible(s) de saut interne : 0x00430fa9, 0x00430fd8, 0x00430fef, 0x00430ffb
