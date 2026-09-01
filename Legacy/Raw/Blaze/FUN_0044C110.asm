Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044c110  section .text  offset fichier 0x4b510  333 octets

  0044c110  53                    push      ebx
  0044c111  56                    push      esi
  0044c112  8b74240c              mov       esi,[esp+0Ch]
  0044c116  33db                  xor       ebx,ebx
  0044c118  8b4678                mov       eax,[esi+78h]
  0044c11b  3bc3                  cmp       eax,ebx
  0044c11d  7505                  jne       short 0044C124h
  0044c11f  b858e55d00            mov       eax,5DE558h

LAB_0044c124:
  0044c124  f70000000050          test      dword [eax],50000000h
  0044c12a  7522                  jne       short 0044C14Eh
  0044c12c  f70600000002          test      dword [esi],2000000h
  0044c132  751a                  jne       short 0044C14Eh
  0044c134  8b481c                mov       ecx,[eax+1Ch]
  0044c137  894e1c                mov       [esi+1Ch],ecx
  0044c13a  8b5020                mov       edx,[eax+20h]
  0044c13d  81c200600400          add       edx,46000h
  0044c143  895620                mov       [esi+20h],edx
  0044c146  8b4024                mov       eax,[eax+24h]
  0044c149  894624                mov       [esi+24h],eax
  0044c14c  eb06                  jmp       short 0044C154h

LAB_0044c14e:
  0044c14e  810e00000002          or        dword [esi],2000000h

LAB_0044c154:
  0044c154  66395e10              cmp       [esi+10h],bx
  0044c158  0f85a8000000          jne       near 0044C206h
  0044c15e  b8401f0000            mov       eax,1F40h
  0044c163  c646041d              mov       byte [esi+4],1Dh
  0044c167  c6460a03              mov       byte [esi+0Ah],3
  0044c16b  885e0b                mov       [esi+0Bh],bl
  0044c16e  66c746100100          mov       word [esi+10h],1
  0044c174  885e38                mov       [esi+38h],bl
  0044c177  885e39                mov       [esi+39h],bl
  0044c17a  885e3a                mov       [esi+3Ah],bl
  0044c17d  66894628              mov       [esi+28h],ax
  0044c181  6689462a              mov       [esi+2Ah],ax
  0044c185  6689462c              mov       [esi+2Ch],ax
  0044c189  e8b2af0f00            call      00547140h
  0044c18e  99                    cdq
  0044c18f  33c2                  xor       eax,edx
  0044c191  2bc2                  sub       eax,edx
  0044c193  25ff0f0000            and       eax,0FFFh
  0044c198  33c2                  xor       eax,edx
  0044c19a  2bc2                  sub       eax,edx
  0044c19c  66894630              mov       [esi+30h],ax
  0044c1a0  e89baf0f00            call      00547140h
  0044c1a5  99                    cdq
  0044c1a6  33c2                  xor       eax,edx
  0044c1a8  2bc2                  sub       eax,edx
  0044c1aa  25ff0f0000            and       eax,0FFFh
  0044c1af  33c2                  xor       eax,edx
  0044c1b1  2bc2                  sub       eax,edx
  0044c1b3  66894632              mov       [esi+32h],ax
  0044c1b7  e884af0f00            call      00547140h
  0044c1bc  99                    cdq
  0044c1bd  33c2                  xor       eax,edx
  0044c1bf  2bc2                  sub       eax,edx
  0044c1c1  25ff0f0000            and       eax,0FFFh
  0044c1c6  33c2                  xor       eax,edx
  0044c1c8  2bc2                  sub       eax,edx
  0044c1ca  66894634              mov       [esi+34h],ax
  0044c1ce  e86daf0f00            call      00547140h
  0044c1d3  99                    cdq
  0044c1d4  b92c010000            mov       ecx,12Ch
  0044c1d9  f7f9                  idiv      ecx
  0044c1db  81ea96000000          sub       edx,96h
  0044c1e1  66895612              mov       [esi+12h],dx
  0044c1e5  e856af0f00            call      00547140h
  0044c1ea  99                    cdq
  0044c1eb  b92c010000            mov       ecx,12Ch
  0044c1f0  66895e16              mov       [esi+16h],bx
  0044c1f4  f7f9                  idiv      ecx
  0044c1f6  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0044c1fc  81ea96000000          sub       edx,96h
  0044c202  66895614              mov       [esi+14h],dx

LAB_0044c206:
  0044c206  66837e1001            cmp       word [esi+10h],1
  0044c20b  754d                  jne       short 0044C25Ah
  0044c20d  668b4612              mov       ax,[esi+12h]
  0044c211  668b5614              mov       dx,[esi+14h]
  0044c215  66014630              add       [esi+30h],ax
  0044c219  66015632              add       [esi+32h],dx
  0044c21d  66294634              sub       [esi+34h],ax
  0044c221  66ff4616              inc       word [esi+16h]
  0044c225  66837e1607            cmp       word [esi+16h],7
  0044c22a  7f12                  jg        short 0044C23Eh
  0044c22c  6a32                  push      32h
  0044c22e  6a32                  push      32h
  0044c230  6a32                  push      32h
  0044c232  56                    push      esi
  0044c233  e8a88bfbff            call      00404DE0h
  0044c238  83c410                add       esp,10h
  0044c23b  5e                    pop       esi
  0044c23c  5b                    pop       ebx
  0044c23d  c3                    ret

LAB_0044c23e:
  0044c23e  6afc                  push      0FFFFFFFCh
  0044c240  6af9                  push      0FFFFFFF9h
  0044c242  6af9                  push      0FFFFFFF9h
  0044c244  56                    push      esi
  0044c245  e8968bfbff            call      00404DE0h
  0044c24a  8a463a                mov       al,[esi+3Ah]
  0044c24d  83c410                add       esp,10h
  0044c250  3ac3                  cmp       al,bl
  0044c252  7506                  jne       short 0044C25Ah
  0044c254  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044c25a:
  0044c25a  5e                    pop       esi
  0044c25b  5b                    pop       ebx
  0044c25c  c3                    ret

6 cible(s) de saut interne : 0x0044c124, 0x0044c14e, 0x0044c154, 0x0044c206, 0x0044c23e, 0x0044c25a
