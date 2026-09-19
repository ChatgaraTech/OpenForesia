Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00432c90  section .text  offset fichier 0x32090  240 octets

  00432c90  53                    push      ebx
  00432c91  56                    push      esi
  00432c92  8b74240c              mov       esi,[esp+0Ch]
  00432c96  33db                  xor       ebx,ebx
  00432c98  66395e10              cmp       [esi+10h],bx
  00432c9c  0f8589000000          jne       near 00432D2Bh
  00432ca2  66c746100100          mov       word [esi+10h],1
  00432ca8  c7463c800c5500        mov       dword [esi+3Ch],550C80h
  00432caf  c6460a06              mov       byte [esi+0Ah],6
  00432cb3  885e0b                mov       [esi+0Bh],bl
  00432cb6  e885441100            call      00547140h
  00432cbb  99                    cdq
  00432cbc  b9a0000000            mov       ecx,0A0h
  00432cc1  f7f9                  idiv      ecx
  00432cc3  8b461c                mov       eax,[esi+1Ch]
  00432cc6  83ea50                sub       edx,50h
  00432cc9  c1e20c                shl       edx,0Ch
  00432ccc  03c2                  add       eax,edx
  00432cce  89461c                mov       [esi+1Ch],eax
  00432cd1  e86a441100            call      00547140h
  00432cd6  99                    cdq
  00432cd7  b9a0000000            mov       ecx,0A0h
  00432cdc  f7f9                  idiv      ecx
  00432cde  8b4e24                mov       ecx,[esi+24h]
  00432ce1  83ea50                sub       edx,50h
  00432ce4  c1e20c                shl       edx,0Ch
  00432ce7  03ca                  add       ecx,edx
  00432ce9  894e24                mov       [esi+24h],ecx
  00432cec  e84f441100            call      00547140h
  00432cf1  99                    cdq
  00432cf2  b914000000            mov       ecx,14h
  00432cf7  66895e14              mov       [esi+14h],bx
  00432cfb  f7f9                  idiv      ecx
  00432cfd  8b4e20                mov       ecx,[esi+20h]
  00432d00  b8c4090000            mov       eax,9C4h
  00432d05  66894628              mov       [esi+28h],ax
  00432d09  6689462a              mov       [esi+2Ah],ax
  00432d0d  c1e20c                shl       edx,0Ch
  00432d10  03ca                  add       ecx,edx
  00432d12  894e20                mov       [esi+20h],ecx
  00432d15  e826441100            call      00547140h
  00432d1a  99                    cdq
  00432d1b  b90a000000            mov       ecx,0Ah
  00432d20  885e40                mov       [esi+40h],bl
  00432d23  f7f9                  idiv      ecx
  00432d25  03d1                  add       edx,ecx
  00432d27  66895612              mov       [esi+12h],dx

LAB_00432d2b:
  00432d2b  66837e1001            cmp       word [esi+10h],1
  00432d30  7549                  jne       short 00432D7Bh
  00432d32  668b4612              mov       ax,[esi+12h]
  00432d36  8b4e20                mov       ecx,[esi+20h]
  00432d39  0fbfd0                movsx     edx,ax
  00432d3c  c1e20c                shl       edx,0Ch
  00432d3f  03ca                  add       ecx,edx
  00432d41  66ff4614              inc       word [esi+14h]
  00432d45  66837e1402            cmp       word [esi+14h],2
  00432d4a  894e20                mov       [esi+20h],ecx
  00432d4d  7509                  jne       short 00432D58h
  00432d4f  48                    dec       eax
  00432d50  66895e14              mov       [esi+14h],bx
  00432d54  66894612              mov       [esi+12h],ax

LAB_00432d58:
  00432d58  668b4612              mov       ax,[esi+12h]
  00432d5c  663d0700              cmp       ax,7
  00432d60  7d0d                  jge       short 00432D6Fh
  00432d62  b938ffffff            mov       ecx,0FFFFFF38h
  00432d67  66014e28              add       [esi+28h],cx
  00432d6b  66014e2a              add       [esi+2Ah],cx

LAB_00432d6f:
  00432d6f  663d0300              cmp       ax,3
  00432d73  7d06                  jge       short 00432D7Bh
  00432d75  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00432d7b:
  00432d7b  5e                    pop       esi
  00432d7c  5b                    pop       ebx
  00432d7d  c3                    ret
  00432d7e  90                    nop
  00432d7f  90                    nop

4 cible(s) de saut interne : 0x00432d2b, 0x00432d58, 0x00432d6f, 0x00432d7b
