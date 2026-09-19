Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00435e50  section .text  offset fichier 0x35250  288 octets

  00435e50  56                    push      esi
  00435e51  8b742408              mov       esi,[esp+8]
  00435e55  c7463c680e5500        mov       dword [esi+3Ch],550E68h
  00435e5c  e8df121100            call      00547140h
  00435e61  99                    cdq
  00435e62  33c2                  xor       eax,edx
  00435e64  2bc2                  sub       eax,edx
  00435e66  25ff010000            and       eax,1FFh
  00435e6b  33c2                  xor       eax,edx
  00435e6d  2bc2                  sub       eax,edx
  00435e6f  0500070000            add       eax,700h
  00435e74  66894628              mov       [esi+28h],ax
  00435e78  e8c3121100            call      00547140h
  00435e7d  99                    cdq
  00435e7e  33c2                  xor       eax,edx
  00435e80  c6460a09              mov       byte [esi+0Ah],9
  00435e84  2bc2                  sub       eax,edx
  00435e86  c6460b00              mov       byte [esi+0Bh],0
  00435e8a  25ff010000            and       eax,1FFh
  00435e8f  33c2                  xor       eax,edx
  00435e91  2bc2                  sub       eax,edx
  00435e93  0500070000            add       eax,700h
  00435e98  66837e1001            cmp       word [esi+10h],1
  00435e9d  6689462a              mov       [esi+2Ah],ax
  00435ea1  8a4616                mov       al,[esi+16h]
  00435ea4  884640                mov       [esi+40h],al
  00435ea7  7508                  jne       short 00435EB1h
  00435ea9  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00435eaf  5e                    pop       esi
  00435eb0  c3                    ret

LAB_00435eb1:
  00435eb1  8b4e1c                mov       ecx,[esi+1Ch]
  00435eb4  8b4650                mov       eax,[esi+50h]
  00435eb7  8bd1                  mov       edx,ecx
  00435eb9  57                    push      edi
  00435eba  2bd0                  sub       edx,eax
  00435ebc  b8abaaaa2a            mov       eax,2AAAAAABh
  00435ec1  f7ea                  imul      edx
  00435ec3  0fbf7e12              movsx     edi,word [esi+12h]
  00435ec7  8bc2                  mov       eax,edx
  00435ec9  c1e81f                shr       eax,1Fh
  00435ecc  03d0                  add       edx,eax
  00435ece  8b4654                mov       eax,[esi+54h]
  00435ed1  0fafd7                imul      edx,edi
  00435ed4  2bca                  sub       ecx,edx
  00435ed6  894e1c                mov       [esi+1Ch],ecx
  00435ed9  8b4e20                mov       ecx,[esi+20h]
  00435edc  8bd1                  mov       edx,ecx
  00435ede  2bd0                  sub       edx,eax
  00435ee0  b8abaaaa2a            mov       eax,2AAAAAABh
  00435ee5  f7ea                  imul      edx
  00435ee7  8bc2                  mov       eax,edx
  00435ee9  c1e81f                shr       eax,1Fh
  00435eec  03d0                  add       edx,eax
  00435eee  8b4658                mov       eax,[esi+58h]
  00435ef1  0fafd7                imul      edx,edi
  00435ef4  2bca                  sub       ecx,edx
  00435ef6  894e20                mov       [esi+20h],ecx
  00435ef9  8b4e24                mov       ecx,[esi+24h]
  00435efc  8bd1                  mov       edx,ecx
  00435efe  2bd0                  sub       edx,eax
  00435f00  b8abaaaa2a            mov       eax,2AAAAAABh
  00435f05  f7ea                  imul      edx
  00435f07  8bc2                  mov       eax,edx
  00435f09  c1e81f                shr       eax,1Fh
  00435f0c  03d0                  add       edx,eax
  00435f0e  0fafd7                imul      edx,edi
  00435f11  2bca                  sub       ecx,edx
  00435f13  894e24                mov       [esi+24h],ecx
  00435f16  e825121100            call      00547140h
  00435f1b  99                    cdq
  00435f1c  b91e000000            mov       ecx,1Eh
  00435f21  f7f9                  idiv      ecx
  00435f23  8b461c                mov       eax,[esi+1Ch]
  00435f26  83ea0f                sub       edx,0Fh
  00435f29  c1e20c                shl       edx,0Ch
  00435f2c  03c2                  add       eax,edx
  00435f2e  89461c                mov       [esi+1Ch],eax
  00435f31  e80a121100            call      00547140h
  00435f36  99                    cdq
  00435f37  b928000000            mov       ecx,28h
  00435f3c  f7f9                  idiv      ecx
  00435f3e  8b4620                mov       eax,[esi+20h]
  00435f41  83ea14                sub       edx,14h
  00435f44  c1e20c                shl       edx,0Ch
  00435f47  03c2                  add       eax,edx
  00435f49  894620                mov       [esi+20h],eax
  00435f4c  e8ef111100            call      00547140h
  00435f51  99                    cdq
  00435f52  b91e000000            mov       ecx,1Eh
  00435f57  66c746100100          mov       word [esi+10h],1
  00435f5d  f7f9                  idiv      ecx
  00435f5f  8b4624                mov       eax,[esi+24h]
  00435f62  5f                    pop       edi
  00435f63  83ea0f                sub       edx,0Fh
  00435f66  c1e20c                shl       edx,0Ch
  00435f69  03c2                  add       eax,edx
  00435f6b  894624                mov       [esi+24h],eax
  00435f6e  5e                    pop       esi
  00435f6f  c3                    ret

1 cible(s) de saut interne : 0x00435eb1
