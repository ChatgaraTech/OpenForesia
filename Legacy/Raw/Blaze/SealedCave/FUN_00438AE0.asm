Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00438ae0  section .text  offset fichier 0x37ee0  240 octets

  00438ae0  53                    push      ebx
  00438ae1  56                    push      esi
  00438ae2  8b74240c              mov       esi,[esp+0Ch]
  00438ae6  33db                  xor       ebx,ebx
  00438ae8  66395e10              cmp       [esi+10h],bx
  00438aec  0f8589000000          jne       near 00438B7Bh
  00438af2  66c746100100          mov       word [esi+10h],1
  00438af8  c7463c300f5500        mov       dword [esi+3Ch],550F30h
  00438aff  c6460a06              mov       byte [esi+0Ah],6
  00438b03  885e0b                mov       [esi+0Bh],bl
  00438b06  e835e61000            call      00547140h
  00438b0b  99                    cdq
  00438b0c  b9a0000000            mov       ecx,0A0h
  00438b11  f7f9                  idiv      ecx
  00438b13  8b461c                mov       eax,[esi+1Ch]
  00438b16  83ea50                sub       edx,50h
  00438b19  c1e20c                shl       edx,0Ch
  00438b1c  03c2                  add       eax,edx
  00438b1e  89461c                mov       [esi+1Ch],eax
  00438b21  e81ae61000            call      00547140h
  00438b26  99                    cdq
  00438b27  b9a0000000            mov       ecx,0A0h
  00438b2c  f7f9                  idiv      ecx
  00438b2e  8b4e24                mov       ecx,[esi+24h]
  00438b31  83ea50                sub       edx,50h
  00438b34  c1e20c                shl       edx,0Ch
  00438b37  03ca                  add       ecx,edx
  00438b39  894e24                mov       [esi+24h],ecx
  00438b3c  e8ffe51000            call      00547140h
  00438b41  99                    cdq
  00438b42  b914000000            mov       ecx,14h
  00438b47  66895e14              mov       [esi+14h],bx
  00438b4b  f7f9                  idiv      ecx
  00438b4d  8b4e20                mov       ecx,[esi+20h]
  00438b50  b8c4090000            mov       eax,9C4h
  00438b55  66894628              mov       [esi+28h],ax
  00438b59  6689462a              mov       [esi+2Ah],ax
  00438b5d  c1e20c                shl       edx,0Ch
  00438b60  03ca                  add       ecx,edx
  00438b62  894e20                mov       [esi+20h],ecx
  00438b65  e8d6e51000            call      00547140h
  00438b6a  99                    cdq
  00438b6b  b90a000000            mov       ecx,0Ah
  00438b70  885e40                mov       [esi+40h],bl
  00438b73  f7f9                  idiv      ecx
  00438b75  03d1                  add       edx,ecx
  00438b77  66895612              mov       [esi+12h],dx

LAB_00438b7b:
  00438b7b  66837e1001            cmp       word [esi+10h],1
  00438b80  7549                  jne       short 00438BCBh
  00438b82  668b4612              mov       ax,[esi+12h]
  00438b86  8b4e20                mov       ecx,[esi+20h]
  00438b89  0fbfd0                movsx     edx,ax
  00438b8c  c1e20c                shl       edx,0Ch
  00438b8f  03ca                  add       ecx,edx
  00438b91  66ff4614              inc       word [esi+14h]
  00438b95  66837e1402            cmp       word [esi+14h],2
  00438b9a  894e20                mov       [esi+20h],ecx
  00438b9d  7509                  jne       short 00438BA8h
  00438b9f  48                    dec       eax
  00438ba0  66895e14              mov       [esi+14h],bx
  00438ba4  66894612              mov       [esi+12h],ax

LAB_00438ba8:
  00438ba8  668b4612              mov       ax,[esi+12h]
  00438bac  663d0700              cmp       ax,7
  00438bb0  7d0d                  jge       short 00438BBFh
  00438bb2  b938ffffff            mov       ecx,0FFFFFF38h
  00438bb7  66014e28              add       [esi+28h],cx
  00438bbb  66014e2a              add       [esi+2Ah],cx

LAB_00438bbf:
  00438bbf  663d0300              cmp       ax,3
  00438bc3  7d06                  jge       short 00438BCBh
  00438bc5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00438bcb:
  00438bcb  5e                    pop       esi
  00438bcc  5b                    pop       ebx
  00438bcd  c3                    ret
  00438bce  90                    nop
  00438bcf  90                    nop

4 cible(s) de saut interne : 0x00438b7b, 0x00438ba8, 0x00438bbf, 0x00438bcb
