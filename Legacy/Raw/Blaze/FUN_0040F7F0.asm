Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040f7f0  section .text  offset fichier 0xebf0  231 octets

  0040f7f0  53                    push      ebx
  0040f7f1  56                    push      esi
  0040f7f2  8b74240c              mov       esi,[esp+0Ch]
  0040f7f6  33db                  xor       ebx,ebx
  0040f7f8  66395e10              cmp       [esi+10h],bx
  0040f7fc  0f85a8000000          jne       near 0040F8AAh
  0040f802  66c746100100          mov       word [esi+10h],1
  0040f808  c7463ca8f75400        mov       dword [esi+3Ch],54F7A8h
  0040f80f  c6460a09              mov       byte [esi+0Ah],9
  0040f813  885e0b                mov       [esi+0Bh],bl
  0040f816  e825791300            call      00547140h
  0040f81b  99                    cdq
  0040f81c  b928000000            mov       ecx,28h
  0040f821  f7f9                  idiv      ecx
  0040f823  8b461c                mov       eax,[esi+1Ch]
  0040f826  83ea14                sub       edx,14h
  0040f829  c1e20c                shl       edx,0Ch
  0040f82c  03c2                  add       eax,edx
  0040f82e  89461c                mov       [esi+1Ch],eax
  0040f831  e80a791300            call      00547140h
  0040f836  99                    cdq
  0040f837  b928000000            mov       ecx,28h
  0040f83c  f7f9                  idiv      ecx
  0040f83e  8b4e24                mov       ecx,[esi+24h]
  0040f841  83ea14                sub       edx,14h
  0040f844  c1e20c                shl       edx,0Ch
  0040f847  03ca                  add       ecx,edx
  0040f849  894e24                mov       [esi+24h],ecx
  0040f84c  e8ef781300            call      00547140h
  0040f851  99                    cdq
  0040f852  b928000000            mov       ecx,28h
  0040f857  f7f9                  idiv      ecx
  0040f859  8b4620                mov       eax,[esi+20h]
  0040f85c  83ea14                sub       edx,14h
  0040f85f  c1e20c                shl       edx,0Ch
  0040f862  03c2                  add       eax,edx
  0040f864  894620                mov       [esi+20h],eax
  0040f867  e8d4781300            call      00547140h
  0040f86c  99                    cdq
  0040f86d  33c2                  xor       eax,edx
  0040f86f  2bc2                  sub       eax,edx
  0040f871  25ff070000            and       eax,7FFh
  0040f876  33c2                  xor       eax,edx
  0040f878  2bc2                  sub       eax,edx
  0040f87a  ba00140000            mov       edx,1400h
  0040f87f  2bd0                  sub       edx,eax
  0040f881  66895628              mov       [esi+28h],dx
  0040f885  e8b6781300            call      00547140h
  0040f88a  99                    cdq
  0040f88b  33c2                  xor       eax,edx
  0040f88d  b900140000            mov       ecx,1400h
  0040f892  2bc2                  sub       eax,edx
  0040f894  66895e12              mov       [esi+12h],bx
  0040f898  25ff070000            and       eax,7FFh
  0040f89d  885e40                mov       [esi+40h],bl
  0040f8a0  33c2                  xor       eax,edx
  0040f8a2  2bc2                  sub       eax,edx
  0040f8a4  2bc8                  sub       ecx,eax
  0040f8a6  66894e2a              mov       [esi+2Ah],cx

LAB_0040f8aa:
  0040f8aa  66837e1001            cmp       word [esi+10h],1
  0040f8af  7523                  jne       short 0040F8D4h
  0040f8b1  66ff4612              inc       word [esi+12h]
  0040f8b5  66837e1202            cmp       word [esi+12h],2
  0040f8ba  7518                  jne       short 0040F8D4h
  0040f8bc  8a5640                mov       dl,[esi+40h]
  0040f8bf  66895e12              mov       [esi+12h],bx
  0040f8c3  fec2                  inc       dl
  0040f8c5  8ac2                  mov       al,dl
  0040f8c7  885640                mov       [esi+40h],dl
  0040f8ca  3c04                  cmp       al,4
  0040f8cc  7506                  jne       short 0040F8D4h
  0040f8ce  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040f8d4:
  0040f8d4  5e                    pop       esi
  0040f8d5  5b                    pop       ebx
  0040f8d6  c3                    ret

2 cible(s) de saut interne : 0x0040f8aa, 0x0040f8d4
