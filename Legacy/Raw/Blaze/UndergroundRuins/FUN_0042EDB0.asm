Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042edb0  section .text  offset fichier 0x2e1b0  240 octets

  0042edb0  53                    push      ebx
  0042edb1  56                    push      esi
  0042edb2  8b74240c              mov       esi,[esp+0Ch]
  0042edb6  33db                  xor       ebx,ebx
  0042edb8  66395e10              cmp       [esi+10h],bx
  0042edbc  0f85a8000000          jne       near 0042EE6Ah
  0042edc2  66c746100100          mov       word [esi+10h],1
  0042edc8  c7463c60095500        mov       dword [esi+3Ch],550960h
  0042edcf  c6460a09              mov       byte [esi+0Ah],9
  0042edd3  885e0b                mov       [esi+0Bh],bl
  0042edd6  e865831100            call      00547140h
  0042eddb  99                    cdq
  0042eddc  b928000000            mov       ecx,28h
  0042ede1  f7f9                  idiv      ecx
  0042ede3  8b461c                mov       eax,[esi+1Ch]
  0042ede6  83ea14                sub       edx,14h
  0042ede9  c1e20c                shl       edx,0Ch
  0042edec  03c2                  add       eax,edx
  0042edee  89461c                mov       [esi+1Ch],eax
  0042edf1  e84a831100            call      00547140h
  0042edf6  99                    cdq
  0042edf7  b928000000            mov       ecx,28h
  0042edfc  f7f9                  idiv      ecx
  0042edfe  8b4e24                mov       ecx,[esi+24h]
  0042ee01  83ea14                sub       edx,14h
  0042ee04  c1e20c                shl       edx,0Ch
  0042ee07  03ca                  add       ecx,edx
  0042ee09  894e24                mov       [esi+24h],ecx
  0042ee0c  e82f831100            call      00547140h
  0042ee11  99                    cdq
  0042ee12  b928000000            mov       ecx,28h
  0042ee17  f7f9                  idiv      ecx
  0042ee19  8b4620                mov       eax,[esi+20h]
  0042ee1c  83ea14                sub       edx,14h
  0042ee1f  c1e20c                shl       edx,0Ch
  0042ee22  03c2                  add       eax,edx
  0042ee24  894620                mov       [esi+20h],eax
  0042ee27  e814831100            call      00547140h
  0042ee2c  99                    cdq
  0042ee2d  33c2                  xor       eax,edx
  0042ee2f  2bc2                  sub       eax,edx
  0042ee31  25ff070000            and       eax,7FFh
  0042ee36  33c2                  xor       eax,edx
  0042ee38  2bc2                  sub       eax,edx
  0042ee3a  ba00140000            mov       edx,1400h
  0042ee3f  2bd0                  sub       edx,eax
  0042ee41  66895628              mov       [esi+28h],dx
  0042ee45  e8f6821100            call      00547140h
  0042ee4a  99                    cdq
  0042ee4b  33c2                  xor       eax,edx
  0042ee4d  b900140000            mov       ecx,1400h
  0042ee52  2bc2                  sub       eax,edx
  0042ee54  66895e12              mov       [esi+12h],bx
  0042ee58  25ff070000            and       eax,7FFh
  0042ee5d  885e40                mov       [esi+40h],bl
  0042ee60  33c2                  xor       eax,edx
  0042ee62  2bc2                  sub       eax,edx
  0042ee64  2bc8                  sub       ecx,eax
  0042ee66  66894e2a              mov       [esi+2Ah],cx

LAB_0042ee6a:
  0042ee6a  66837e1001            cmp       word [esi+10h],1
  0042ee6f  7523                  jne       short 0042EE94h
  0042ee71  66ff4612              inc       word [esi+12h]
  0042ee75  66837e1202            cmp       word [esi+12h],2
  0042ee7a  7518                  jne       short 0042EE94h
  0042ee7c  8a5640                mov       dl,[esi+40h]
  0042ee7f  66895e12              mov       [esi+12h],bx
  0042ee83  fec2                  inc       dl
  0042ee85  8ac2                  mov       al,dl
  0042ee87  885640                mov       [esi+40h],dl
  0042ee8a  3c04                  cmp       al,4
  0042ee8c  7506                  jne       short 0042EE94h
  0042ee8e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042ee94:
  0042ee94  5e                    pop       esi
  0042ee95  5b                    pop       ebx
  0042ee96  c3                    ret
  0042ee97  90                    nop
  0042ee98  90                    nop
  0042ee99  90                    nop
  0042ee9a  90                    nop
  0042ee9b  90                    nop
  0042ee9c  90                    nop
  0042ee9d  90                    nop
  0042ee9e  90                    nop
  0042ee9f  90                    nop

2 cible(s) de saut interne : 0x0042ee6a, 0x0042ee94
