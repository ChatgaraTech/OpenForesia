Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00435d60  section .text  offset fichier 0x35160  240 octets

  00435d60  53                    push      ebx
  00435d61  56                    push      esi
  00435d62  8b74240c              mov       esi,[esp+0Ch]
  00435d66  33db                  xor       ebx,ebx
  00435d68  66395e10              cmp       [esi+10h],bx
  00435d6c  0f8589000000          jne       near 00435DFBh
  00435d72  66c746100100          mov       word [esi+10h],1
  00435d78  c7463c800e5500        mov       dword [esi+3Ch],550E80h
  00435d7f  c6460a06              mov       byte [esi+0Ah],6
  00435d83  885e0b                mov       [esi+0Bh],bl
  00435d86  e8b5131100            call      00547140h
  00435d8b  99                    cdq
  00435d8c  b9a0000000            mov       ecx,0A0h
  00435d91  f7f9                  idiv      ecx
  00435d93  8b461c                mov       eax,[esi+1Ch]
  00435d96  83ea50                sub       edx,50h
  00435d99  c1e20c                shl       edx,0Ch
  00435d9c  03c2                  add       eax,edx
  00435d9e  89461c                mov       [esi+1Ch],eax
  00435da1  e89a131100            call      00547140h
  00435da6  99                    cdq
  00435da7  b9a0000000            mov       ecx,0A0h
  00435dac  f7f9                  idiv      ecx
  00435dae  8b4e24                mov       ecx,[esi+24h]
  00435db1  83ea50                sub       edx,50h
  00435db4  c1e20c                shl       edx,0Ch
  00435db7  03ca                  add       ecx,edx
  00435db9  894e24                mov       [esi+24h],ecx
  00435dbc  e87f131100            call      00547140h
  00435dc1  99                    cdq
  00435dc2  b914000000            mov       ecx,14h
  00435dc7  66895e14              mov       [esi+14h],bx
  00435dcb  f7f9                  idiv      ecx
  00435dcd  8b4e20                mov       ecx,[esi+20h]
  00435dd0  b8c4090000            mov       eax,9C4h
  00435dd5  66894628              mov       [esi+28h],ax
  00435dd9  6689462a              mov       [esi+2Ah],ax
  00435ddd  c1e20c                shl       edx,0Ch
  00435de0  03ca                  add       ecx,edx
  00435de2  894e20                mov       [esi+20h],ecx
  00435de5  e856131100            call      00547140h
  00435dea  99                    cdq
  00435deb  b90a000000            mov       ecx,0Ah
  00435df0  885e40                mov       [esi+40h],bl
  00435df3  f7f9                  idiv      ecx
  00435df5  03d1                  add       edx,ecx
  00435df7  66895612              mov       [esi+12h],dx

LAB_00435dfb:
  00435dfb  66837e1001            cmp       word [esi+10h],1
  00435e00  7549                  jne       short 00435E4Bh
  00435e02  668b4612              mov       ax,[esi+12h]
  00435e06  8b4e20                mov       ecx,[esi+20h]
  00435e09  0fbfd0                movsx     edx,ax
  00435e0c  c1e20c                shl       edx,0Ch
  00435e0f  03ca                  add       ecx,edx
  00435e11  66ff4614              inc       word [esi+14h]
  00435e15  66837e1402            cmp       word [esi+14h],2
  00435e1a  894e20                mov       [esi+20h],ecx
  00435e1d  7509                  jne       short 00435E28h
  00435e1f  48                    dec       eax
  00435e20  66895e14              mov       [esi+14h],bx
  00435e24  66894612              mov       [esi+12h],ax

LAB_00435e28:
  00435e28  668b4612              mov       ax,[esi+12h]
  00435e2c  663d0700              cmp       ax,7
  00435e30  7d0d                  jge       short 00435E3Fh
  00435e32  b938ffffff            mov       ecx,0FFFFFF38h
  00435e37  66014e28              add       [esi+28h],cx
  00435e3b  66014e2a              add       [esi+2Ah],cx

LAB_00435e3f:
  00435e3f  663d0300              cmp       ax,3
  00435e43  7d06                  jge       short 00435E4Bh
  00435e45  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00435e4b:
  00435e4b  5e                    pop       esi
  00435e4c  5b                    pop       ebx
  00435e4d  c3                    ret
  00435e4e  90                    nop
  00435e4f  90                    nop

4 cible(s) de saut interne : 0x00435dfb, 0x00435e28, 0x00435e3f, 0x00435e4b
