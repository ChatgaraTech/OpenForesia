Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043b660  section .text  offset fichier 0x3aa60  240 octets

  0043b660  53                    push      ebx
  0043b661  56                    push      esi
  0043b662  8b74240c              mov       esi,[esp+0Ch]
  0043b666  33db                  xor       ebx,ebx
  0043b668  66395e10              cmp       [esi+10h],bx
  0043b66c  0f8589000000          jne       near 0043B6FBh
  0043b672  66c746100100          mov       word [esi+10h],1
  0043b678  c7463c60105500        mov       dword [esi+3Ch],551060h
  0043b67f  c6460a06              mov       byte [esi+0Ah],6
  0043b683  885e0b                mov       [esi+0Bh],bl
  0043b686  e8b5ba1000            call      00547140h
  0043b68b  99                    cdq
  0043b68c  b9a0000000            mov       ecx,0A0h
  0043b691  f7f9                  idiv      ecx
  0043b693  8b461c                mov       eax,[esi+1Ch]
  0043b696  83ea50                sub       edx,50h
  0043b699  c1e20c                shl       edx,0Ch
  0043b69c  03c2                  add       eax,edx
  0043b69e  89461c                mov       [esi+1Ch],eax
  0043b6a1  e89aba1000            call      00547140h
  0043b6a6  99                    cdq
  0043b6a7  b9a0000000            mov       ecx,0A0h
  0043b6ac  f7f9                  idiv      ecx
  0043b6ae  8b4e24                mov       ecx,[esi+24h]
  0043b6b1  83ea50                sub       edx,50h
  0043b6b4  c1e20c                shl       edx,0Ch
  0043b6b7  03ca                  add       ecx,edx
  0043b6b9  894e24                mov       [esi+24h],ecx
  0043b6bc  e87fba1000            call      00547140h
  0043b6c1  99                    cdq
  0043b6c2  b914000000            mov       ecx,14h
  0043b6c7  66895e14              mov       [esi+14h],bx
  0043b6cb  f7f9                  idiv      ecx
  0043b6cd  8b4e20                mov       ecx,[esi+20h]
  0043b6d0  b8c4090000            mov       eax,9C4h
  0043b6d5  66894628              mov       [esi+28h],ax
  0043b6d9  6689462a              mov       [esi+2Ah],ax
  0043b6dd  c1e20c                shl       edx,0Ch
  0043b6e0  03ca                  add       ecx,edx
  0043b6e2  894e20                mov       [esi+20h],ecx
  0043b6e5  e856ba1000            call      00547140h
  0043b6ea  99                    cdq
  0043b6eb  b90a000000            mov       ecx,0Ah
  0043b6f0  885e40                mov       [esi+40h],bl
  0043b6f3  f7f9                  idiv      ecx
  0043b6f5  03d1                  add       edx,ecx
  0043b6f7  66895612              mov       [esi+12h],dx

LAB_0043b6fb:
  0043b6fb  66837e1001            cmp       word [esi+10h],1
  0043b700  7549                  jne       short 0043B74Bh
  0043b702  668b4612              mov       ax,[esi+12h]
  0043b706  8b4e20                mov       ecx,[esi+20h]
  0043b709  0fbfd0                movsx     edx,ax
  0043b70c  c1e20c                shl       edx,0Ch
  0043b70f  03ca                  add       ecx,edx
  0043b711  66ff4614              inc       word [esi+14h]
  0043b715  66837e1402            cmp       word [esi+14h],2
  0043b71a  894e20                mov       [esi+20h],ecx
  0043b71d  7509                  jne       short 0043B728h
  0043b71f  48                    dec       eax
  0043b720  66895e14              mov       [esi+14h],bx
  0043b724  66894612              mov       [esi+12h],ax

LAB_0043b728:
  0043b728  668b4612              mov       ax,[esi+12h]
  0043b72c  663d0700              cmp       ax,7
  0043b730  7d0d                  jge       short 0043B73Fh
  0043b732  b938ffffff            mov       ecx,0FFFFFF38h
  0043b737  66014e28              add       [esi+28h],cx
  0043b73b  66014e2a              add       [esi+2Ah],cx

LAB_0043b73f:
  0043b73f  663d0300              cmp       ax,3
  0043b743  7d06                  jge       short 0043B74Bh
  0043b745  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043b74b:
  0043b74b  5e                    pop       esi
  0043b74c  5b                    pop       ebx
  0043b74d  c3                    ret
  0043b74e  90                    nop
  0043b74f  90                    nop

4 cible(s) de saut interne : 0x0043b6fb, 0x0043b728, 0x0043b73f, 0x0043b74b
