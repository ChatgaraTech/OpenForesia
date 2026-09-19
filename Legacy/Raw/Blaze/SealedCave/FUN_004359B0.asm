Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004359b0  section .text  offset fichier 0x34db0  240 octets

  004359b0  53                    push      ebx
  004359b1  56                    push      esi
  004359b2  8b74240c              mov       esi,[esp+0Ch]
  004359b6  33db                  xor       ebx,ebx
  004359b8  66395e10              cmp       [esi+10h],bx
  004359bc  0f85a8000000          jne       near 00435A6Ah
  004359c2  66c746100100          mov       word [esi+10h],1
  004359c8  c7463c900e5500        mov       dword [esi+3Ch],550E90h
  004359cf  c6460a09              mov       byte [esi+0Ah],9
  004359d3  885e0b                mov       [esi+0Bh],bl
  004359d6  e865171100            call      00547140h
  004359db  99                    cdq
  004359dc  b928000000            mov       ecx,28h
  004359e1  f7f9                  idiv      ecx
  004359e3  8b461c                mov       eax,[esi+1Ch]
  004359e6  83ea14                sub       edx,14h
  004359e9  c1e20c                shl       edx,0Ch
  004359ec  03c2                  add       eax,edx
  004359ee  89461c                mov       [esi+1Ch],eax
  004359f1  e84a171100            call      00547140h
  004359f6  99                    cdq
  004359f7  b928000000            mov       ecx,28h
  004359fc  f7f9                  idiv      ecx
  004359fe  8b4e24                mov       ecx,[esi+24h]
  00435a01  83ea14                sub       edx,14h
  00435a04  c1e20c                shl       edx,0Ch
  00435a07  03ca                  add       ecx,edx
  00435a09  894e24                mov       [esi+24h],ecx
  00435a0c  e82f171100            call      00547140h
  00435a11  99                    cdq
  00435a12  b928000000            mov       ecx,28h
  00435a17  f7f9                  idiv      ecx
  00435a19  8b4620                mov       eax,[esi+20h]
  00435a1c  83ea14                sub       edx,14h
  00435a1f  c1e20c                shl       edx,0Ch
  00435a22  03c2                  add       eax,edx
  00435a24  894620                mov       [esi+20h],eax
  00435a27  e814171100            call      00547140h
  00435a2c  99                    cdq
  00435a2d  33c2                  xor       eax,edx
  00435a2f  2bc2                  sub       eax,edx
  00435a31  25ff070000            and       eax,7FFh
  00435a36  33c2                  xor       eax,edx
  00435a38  2bc2                  sub       eax,edx
  00435a3a  ba00140000            mov       edx,1400h
  00435a3f  2bd0                  sub       edx,eax
  00435a41  66895628              mov       [esi+28h],dx
  00435a45  e8f6161100            call      00547140h
  00435a4a  99                    cdq
  00435a4b  33c2                  xor       eax,edx
  00435a4d  b900140000            mov       ecx,1400h
  00435a52  2bc2                  sub       eax,edx
  00435a54  66895e12              mov       [esi+12h],bx
  00435a58  25ff070000            and       eax,7FFh
  00435a5d  885e40                mov       [esi+40h],bl
  00435a60  33c2                  xor       eax,edx
  00435a62  2bc2                  sub       eax,edx
  00435a64  2bc8                  sub       ecx,eax
  00435a66  66894e2a              mov       [esi+2Ah],cx

LAB_00435a6a:
  00435a6a  66837e1001            cmp       word [esi+10h],1
  00435a6f  7523                  jne       short 00435A94h
  00435a71  66ff4612              inc       word [esi+12h]
  00435a75  66837e1202            cmp       word [esi+12h],2
  00435a7a  7518                  jne       short 00435A94h
  00435a7c  8a5640                mov       dl,[esi+40h]
  00435a7f  66895e12              mov       [esi+12h],bx
  00435a83  fec2                  inc       dl
  00435a85  8ac2                  mov       al,dl
  00435a87  885640                mov       [esi+40h],dl
  00435a8a  3c04                  cmp       al,4
  00435a8c  7506                  jne       short 00435A94h
  00435a8e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00435a94:
  00435a94  5e                    pop       esi
  00435a95  5b                    pop       ebx
  00435a96  c3                    ret
  00435a97  90                    nop
  00435a98  90                    nop
  00435a99  90                    nop
  00435a9a  90                    nop
  00435a9b  90                    nop
  00435a9c  90                    nop
  00435a9d  90                    nop
  00435a9e  90                    nop
  00435a9f  90                    nop

2 cible(s) de saut interne : 0x00435a6a, 0x00435a94
