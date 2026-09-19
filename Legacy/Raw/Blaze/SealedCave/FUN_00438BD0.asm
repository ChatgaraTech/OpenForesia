Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00438bd0  section .text  offset fichier 0x37fd0  288 octets

  00438bd0  56                    push      esi
  00438bd1  8b742408              mov       esi,[esp+8]
  00438bd5  c7463c180f5500        mov       dword [esi+3Ch],550F18h
  00438bdc  e85fe51000            call      00547140h
  00438be1  99                    cdq
  00438be2  33c2                  xor       eax,edx
  00438be4  2bc2                  sub       eax,edx
  00438be6  25ff010000            and       eax,1FFh
  00438beb  33c2                  xor       eax,edx
  00438bed  2bc2                  sub       eax,edx
  00438bef  0500070000            add       eax,700h
  00438bf4  66894628              mov       [esi+28h],ax
  00438bf8  e843e51000            call      00547140h
  00438bfd  99                    cdq
  00438bfe  33c2                  xor       eax,edx
  00438c00  c6460a09              mov       byte [esi+0Ah],9
  00438c04  2bc2                  sub       eax,edx
  00438c06  c6460b00              mov       byte [esi+0Bh],0
  00438c0a  25ff010000            and       eax,1FFh
  00438c0f  33c2                  xor       eax,edx
  00438c11  2bc2                  sub       eax,edx
  00438c13  0500070000            add       eax,700h
  00438c18  66837e1001            cmp       word [esi+10h],1
  00438c1d  6689462a              mov       [esi+2Ah],ax
  00438c21  8a4616                mov       al,[esi+16h]
  00438c24  884640                mov       [esi+40h],al
  00438c27  7508                  jne       short 00438C31h
  00438c29  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00438c2f  5e                    pop       esi
  00438c30  c3                    ret

LAB_00438c31:
  00438c31  8b4e1c                mov       ecx,[esi+1Ch]
  00438c34  8b4650                mov       eax,[esi+50h]
  00438c37  8bd1                  mov       edx,ecx
  00438c39  57                    push      edi
  00438c3a  2bd0                  sub       edx,eax
  00438c3c  b8abaaaa2a            mov       eax,2AAAAAABh
  00438c41  f7ea                  imul      edx
  00438c43  0fbf7e12              movsx     edi,word [esi+12h]
  00438c47  8bc2                  mov       eax,edx
  00438c49  c1e81f                shr       eax,1Fh
  00438c4c  03d0                  add       edx,eax
  00438c4e  8b4654                mov       eax,[esi+54h]
  00438c51  0fafd7                imul      edx,edi
  00438c54  2bca                  sub       ecx,edx
  00438c56  894e1c                mov       [esi+1Ch],ecx
  00438c59  8b4e20                mov       ecx,[esi+20h]
  00438c5c  8bd1                  mov       edx,ecx
  00438c5e  2bd0                  sub       edx,eax
  00438c60  b8abaaaa2a            mov       eax,2AAAAAABh
  00438c65  f7ea                  imul      edx
  00438c67  8bc2                  mov       eax,edx
  00438c69  c1e81f                shr       eax,1Fh
  00438c6c  03d0                  add       edx,eax
  00438c6e  8b4658                mov       eax,[esi+58h]
  00438c71  0fafd7                imul      edx,edi
  00438c74  2bca                  sub       ecx,edx
  00438c76  894e20                mov       [esi+20h],ecx
  00438c79  8b4e24                mov       ecx,[esi+24h]
  00438c7c  8bd1                  mov       edx,ecx
  00438c7e  2bd0                  sub       edx,eax
  00438c80  b8abaaaa2a            mov       eax,2AAAAAABh
  00438c85  f7ea                  imul      edx
  00438c87  8bc2                  mov       eax,edx
  00438c89  c1e81f                shr       eax,1Fh
  00438c8c  03d0                  add       edx,eax
  00438c8e  0fafd7                imul      edx,edi
  00438c91  2bca                  sub       ecx,edx
  00438c93  894e24                mov       [esi+24h],ecx
  00438c96  e8a5e41000            call      00547140h
  00438c9b  99                    cdq
  00438c9c  b91e000000            mov       ecx,1Eh
  00438ca1  f7f9                  idiv      ecx
  00438ca3  8b461c                mov       eax,[esi+1Ch]
  00438ca6  83ea0f                sub       edx,0Fh
  00438ca9  c1e20c                shl       edx,0Ch
  00438cac  03c2                  add       eax,edx
  00438cae  89461c                mov       [esi+1Ch],eax
  00438cb1  e88ae41000            call      00547140h
  00438cb6  99                    cdq
  00438cb7  b928000000            mov       ecx,28h
  00438cbc  f7f9                  idiv      ecx
  00438cbe  8b4620                mov       eax,[esi+20h]
  00438cc1  83ea14                sub       edx,14h
  00438cc4  c1e20c                shl       edx,0Ch
  00438cc7  03c2                  add       eax,edx
  00438cc9  894620                mov       [esi+20h],eax
  00438ccc  e86fe41000            call      00547140h
  00438cd1  99                    cdq
  00438cd2  b91e000000            mov       ecx,1Eh
  00438cd7  66c746100100          mov       word [esi+10h],1
  00438cdd  f7f9                  idiv      ecx
  00438cdf  8b4624                mov       eax,[esi+24h]
  00438ce2  5f                    pop       edi
  00438ce3  83ea0f                sub       edx,0Fh
  00438ce6  c1e20c                shl       edx,0Ch
  00438ce9  03c2                  add       eax,edx
  00438ceb  894624                mov       [esi+24h],eax
  00438cee  5e                    pop       esi
  00438cef  c3                    ret

1 cible(s) de saut interne : 0x00438c31
