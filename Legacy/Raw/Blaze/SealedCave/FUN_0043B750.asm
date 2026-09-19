Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043b750  section .text  offset fichier 0x3ab50  288 octets

  0043b750  56                    push      esi
  0043b751  8b742408              mov       esi,[esp+8]
  0043b755  c7463c48105500        mov       dword [esi+3Ch],551048h
  0043b75c  e8dfb91000            call      00547140h
  0043b761  99                    cdq
  0043b762  33c2                  xor       eax,edx
  0043b764  2bc2                  sub       eax,edx
  0043b766  25ff010000            and       eax,1FFh
  0043b76b  33c2                  xor       eax,edx
  0043b76d  2bc2                  sub       eax,edx
  0043b76f  0500070000            add       eax,700h
  0043b774  66894628              mov       [esi+28h],ax
  0043b778  e8c3b91000            call      00547140h
  0043b77d  99                    cdq
  0043b77e  33c2                  xor       eax,edx
  0043b780  c6460a09              mov       byte [esi+0Ah],9
  0043b784  2bc2                  sub       eax,edx
  0043b786  c6460b00              mov       byte [esi+0Bh],0
  0043b78a  25ff010000            and       eax,1FFh
  0043b78f  33c2                  xor       eax,edx
  0043b791  2bc2                  sub       eax,edx
  0043b793  0500070000            add       eax,700h
  0043b798  66837e1001            cmp       word [esi+10h],1
  0043b79d  6689462a              mov       [esi+2Ah],ax
  0043b7a1  8a4616                mov       al,[esi+16h]
  0043b7a4  884640                mov       [esi+40h],al
  0043b7a7  7508                  jne       short 0043B7B1h
  0043b7a9  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043b7af  5e                    pop       esi
  0043b7b0  c3                    ret

LAB_0043b7b1:
  0043b7b1  8b4e1c                mov       ecx,[esi+1Ch]
  0043b7b4  8b4650                mov       eax,[esi+50h]
  0043b7b7  8bd1                  mov       edx,ecx
  0043b7b9  57                    push      edi
  0043b7ba  2bd0                  sub       edx,eax
  0043b7bc  b8abaaaa2a            mov       eax,2AAAAAABh
  0043b7c1  f7ea                  imul      edx
  0043b7c3  0fbf7e12              movsx     edi,word [esi+12h]
  0043b7c7  8bc2                  mov       eax,edx
  0043b7c9  c1e81f                shr       eax,1Fh
  0043b7cc  03d0                  add       edx,eax
  0043b7ce  8b4654                mov       eax,[esi+54h]
  0043b7d1  0fafd7                imul      edx,edi
  0043b7d4  2bca                  sub       ecx,edx
  0043b7d6  894e1c                mov       [esi+1Ch],ecx
  0043b7d9  8b4e20                mov       ecx,[esi+20h]
  0043b7dc  8bd1                  mov       edx,ecx
  0043b7de  2bd0                  sub       edx,eax
  0043b7e0  b8abaaaa2a            mov       eax,2AAAAAABh
  0043b7e5  f7ea                  imul      edx
  0043b7e7  8bc2                  mov       eax,edx
  0043b7e9  c1e81f                shr       eax,1Fh
  0043b7ec  03d0                  add       edx,eax
  0043b7ee  8b4658                mov       eax,[esi+58h]
  0043b7f1  0fafd7                imul      edx,edi
  0043b7f4  2bca                  sub       ecx,edx
  0043b7f6  894e20                mov       [esi+20h],ecx
  0043b7f9  8b4e24                mov       ecx,[esi+24h]
  0043b7fc  8bd1                  mov       edx,ecx
  0043b7fe  2bd0                  sub       edx,eax
  0043b800  b8abaaaa2a            mov       eax,2AAAAAABh
  0043b805  f7ea                  imul      edx
  0043b807  8bc2                  mov       eax,edx
  0043b809  c1e81f                shr       eax,1Fh
  0043b80c  03d0                  add       edx,eax
  0043b80e  0fafd7                imul      edx,edi
  0043b811  2bca                  sub       ecx,edx
  0043b813  894e24                mov       [esi+24h],ecx
  0043b816  e825b91000            call      00547140h
  0043b81b  99                    cdq
  0043b81c  b91e000000            mov       ecx,1Eh
  0043b821  f7f9                  idiv      ecx
  0043b823  8b461c                mov       eax,[esi+1Ch]
  0043b826  83ea0f                sub       edx,0Fh
  0043b829  c1e20c                shl       edx,0Ch
  0043b82c  03c2                  add       eax,edx
  0043b82e  89461c                mov       [esi+1Ch],eax
  0043b831  e80ab91000            call      00547140h
  0043b836  99                    cdq
  0043b837  b928000000            mov       ecx,28h
  0043b83c  f7f9                  idiv      ecx
  0043b83e  8b4620                mov       eax,[esi+20h]
  0043b841  83ea14                sub       edx,14h
  0043b844  c1e20c                shl       edx,0Ch
  0043b847  03c2                  add       eax,edx
  0043b849  894620                mov       [esi+20h],eax
  0043b84c  e8efb81000            call      00547140h
  0043b851  99                    cdq
  0043b852  b91e000000            mov       ecx,1Eh
  0043b857  66c746100100          mov       word [esi+10h],1
  0043b85d  f7f9                  idiv      ecx
  0043b85f  8b4624                mov       eax,[esi+24h]
  0043b862  5f                    pop       edi
  0043b863  83ea0f                sub       edx,0Fh
  0043b866  c1e20c                shl       edx,0Ch
  0043b869  03c2                  add       eax,edx
  0043b86b  894624                mov       [esi+24h],eax
  0043b86e  5e                    pop       esi
  0043b86f  c3                    ret

1 cible(s) de saut interne : 0x0043b7b1
