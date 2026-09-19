Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043b310  section .text  offset fichier 0x3a710  240 octets

  0043b310  53                    push      ebx
  0043b311  56                    push      esi
  0043b312  8b74240c              mov       esi,[esp+0Ch]
  0043b316  33db                  xor       ebx,ebx
  0043b318  66395e10              cmp       [esi+10h],bx
  0043b31c  0f85a8000000          jne       near 0043B3CAh
  0043b322  66c746100100          mov       word [esi+10h],1
  0043b328  c7463c70105500        mov       dword [esi+3Ch],551070h
  0043b32f  c6460a09              mov       byte [esi+0Ah],9
  0043b333  885e0b                mov       [esi+0Bh],bl
  0043b336  e805be1000            call      00547140h
  0043b33b  99                    cdq
  0043b33c  b928000000            mov       ecx,28h
  0043b341  f7f9                  idiv      ecx
  0043b343  8b461c                mov       eax,[esi+1Ch]
  0043b346  83ea14                sub       edx,14h
  0043b349  c1e20c                shl       edx,0Ch
  0043b34c  03c2                  add       eax,edx
  0043b34e  89461c                mov       [esi+1Ch],eax
  0043b351  e8eabd1000            call      00547140h
  0043b356  99                    cdq
  0043b357  b928000000            mov       ecx,28h
  0043b35c  f7f9                  idiv      ecx
  0043b35e  8b4e24                mov       ecx,[esi+24h]
  0043b361  83ea14                sub       edx,14h
  0043b364  c1e20c                shl       edx,0Ch
  0043b367  03ca                  add       ecx,edx
  0043b369  894e24                mov       [esi+24h],ecx
  0043b36c  e8cfbd1000            call      00547140h
  0043b371  99                    cdq
  0043b372  b928000000            mov       ecx,28h
  0043b377  f7f9                  idiv      ecx
  0043b379  8b4620                mov       eax,[esi+20h]
  0043b37c  83ea14                sub       edx,14h
  0043b37f  c1e20c                shl       edx,0Ch
  0043b382  03c2                  add       eax,edx
  0043b384  894620                mov       [esi+20h],eax
  0043b387  e8b4bd1000            call      00547140h
  0043b38c  99                    cdq
  0043b38d  33c2                  xor       eax,edx
  0043b38f  2bc2                  sub       eax,edx
  0043b391  25ff070000            and       eax,7FFh
  0043b396  33c2                  xor       eax,edx
  0043b398  2bc2                  sub       eax,edx
  0043b39a  ba00140000            mov       edx,1400h
  0043b39f  2bd0                  sub       edx,eax
  0043b3a1  66895628              mov       [esi+28h],dx
  0043b3a5  e896bd1000            call      00547140h
  0043b3aa  99                    cdq
  0043b3ab  33c2                  xor       eax,edx
  0043b3ad  b900140000            mov       ecx,1400h
  0043b3b2  2bc2                  sub       eax,edx
  0043b3b4  66895e12              mov       [esi+12h],bx
  0043b3b8  25ff070000            and       eax,7FFh
  0043b3bd  885e40                mov       [esi+40h],bl
  0043b3c0  33c2                  xor       eax,edx
  0043b3c2  2bc2                  sub       eax,edx
  0043b3c4  2bc8                  sub       ecx,eax
  0043b3c6  66894e2a              mov       [esi+2Ah],cx

LAB_0043b3ca:
  0043b3ca  66837e1001            cmp       word [esi+10h],1
  0043b3cf  7523                  jne       short 0043B3F4h
  0043b3d1  66ff4612              inc       word [esi+12h]
  0043b3d5  66837e1202            cmp       word [esi+12h],2
  0043b3da  7518                  jne       short 0043B3F4h
  0043b3dc  8a5640                mov       dl,[esi+40h]
  0043b3df  66895e12              mov       [esi+12h],bx
  0043b3e3  fec2                  inc       dl
  0043b3e5  8ac2                  mov       al,dl
  0043b3e7  885640                mov       [esi+40h],dl
  0043b3ea  3c04                  cmp       al,4
  0043b3ec  7506                  jne       short 0043B3F4h
  0043b3ee  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043b3f4:
  0043b3f4  5e                    pop       esi
  0043b3f5  5b                    pop       ebx
  0043b3f6  c3                    ret
  0043b3f7  90                    nop
  0043b3f8  90                    nop
  0043b3f9  90                    nop
  0043b3fa  90                    nop
  0043b3fb  90                    nop
  0043b3fc  90                    nop
  0043b3fd  90                    nop
  0043b3fe  90                    nop
  0043b3ff  90                    nop

2 cible(s) de saut interne : 0x0043b3ca, 0x0043b3f4
