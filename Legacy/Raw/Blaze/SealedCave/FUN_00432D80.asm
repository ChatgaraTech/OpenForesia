Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00432d80  section .text  offset fichier 0x32180  288 octets

  00432d80  56                    push      esi
  00432d81  8b742408              mov       esi,[esp+8]
  00432d85  c7463c680c5500        mov       dword [esi+3Ch],550C68h
  00432d8c  e8af431100            call      00547140h
  00432d91  99                    cdq
  00432d92  33c2                  xor       eax,edx
  00432d94  2bc2                  sub       eax,edx
  00432d96  25ff010000            and       eax,1FFh
  00432d9b  33c2                  xor       eax,edx
  00432d9d  2bc2                  sub       eax,edx
  00432d9f  0500070000            add       eax,700h
  00432da4  66894628              mov       [esi+28h],ax
  00432da8  e893431100            call      00547140h
  00432dad  99                    cdq
  00432dae  33c2                  xor       eax,edx
  00432db0  c6460a09              mov       byte [esi+0Ah],9
  00432db4  2bc2                  sub       eax,edx
  00432db6  c6460b00              mov       byte [esi+0Bh],0
  00432dba  25ff010000            and       eax,1FFh
  00432dbf  33c2                  xor       eax,edx
  00432dc1  2bc2                  sub       eax,edx
  00432dc3  0500070000            add       eax,700h
  00432dc8  66837e1001            cmp       word [esi+10h],1
  00432dcd  6689462a              mov       [esi+2Ah],ax
  00432dd1  8a4616                mov       al,[esi+16h]
  00432dd4  884640                mov       [esi+40h],al
  00432dd7  7508                  jne       short 00432DE1h
  00432dd9  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00432ddf  5e                    pop       esi
  00432de0  c3                    ret

LAB_00432de1:
  00432de1  8b4e1c                mov       ecx,[esi+1Ch]
  00432de4  8b4650                mov       eax,[esi+50h]
  00432de7  8bd1                  mov       edx,ecx
  00432de9  57                    push      edi
  00432dea  2bd0                  sub       edx,eax
  00432dec  b8abaaaa2a            mov       eax,2AAAAAABh
  00432df1  f7ea                  imul      edx
  00432df3  0fbf7e12              movsx     edi,word [esi+12h]
  00432df7  8bc2                  mov       eax,edx
  00432df9  c1e81f                shr       eax,1Fh
  00432dfc  03d0                  add       edx,eax
  00432dfe  8b4654                mov       eax,[esi+54h]
  00432e01  0fafd7                imul      edx,edi
  00432e04  2bca                  sub       ecx,edx
  00432e06  894e1c                mov       [esi+1Ch],ecx
  00432e09  8b4e20                mov       ecx,[esi+20h]
  00432e0c  8bd1                  mov       edx,ecx
  00432e0e  2bd0                  sub       edx,eax
  00432e10  b8abaaaa2a            mov       eax,2AAAAAABh
  00432e15  f7ea                  imul      edx
  00432e17  8bc2                  mov       eax,edx
  00432e19  c1e81f                shr       eax,1Fh
  00432e1c  03d0                  add       edx,eax
  00432e1e  8b4658                mov       eax,[esi+58h]
  00432e21  0fafd7                imul      edx,edi
  00432e24  2bca                  sub       ecx,edx
  00432e26  894e20                mov       [esi+20h],ecx
  00432e29  8b4e24                mov       ecx,[esi+24h]
  00432e2c  8bd1                  mov       edx,ecx
  00432e2e  2bd0                  sub       edx,eax
  00432e30  b8abaaaa2a            mov       eax,2AAAAAABh
  00432e35  f7ea                  imul      edx
  00432e37  8bc2                  mov       eax,edx
  00432e39  c1e81f                shr       eax,1Fh
  00432e3c  03d0                  add       edx,eax
  00432e3e  0fafd7                imul      edx,edi
  00432e41  2bca                  sub       ecx,edx
  00432e43  894e24                mov       [esi+24h],ecx
  00432e46  e8f5421100            call      00547140h
  00432e4b  99                    cdq
  00432e4c  b91e000000            mov       ecx,1Eh
  00432e51  f7f9                  idiv      ecx
  00432e53  8b461c                mov       eax,[esi+1Ch]
  00432e56  83ea0f                sub       edx,0Fh
  00432e59  c1e20c                shl       edx,0Ch
  00432e5c  03c2                  add       eax,edx
  00432e5e  89461c                mov       [esi+1Ch],eax
  00432e61  e8da421100            call      00547140h
  00432e66  99                    cdq
  00432e67  b928000000            mov       ecx,28h
  00432e6c  f7f9                  idiv      ecx
  00432e6e  8b4620                mov       eax,[esi+20h]
  00432e71  83ea14                sub       edx,14h
  00432e74  c1e20c                shl       edx,0Ch
  00432e77  03c2                  add       eax,edx
  00432e79  894620                mov       [esi+20h],eax
  00432e7c  e8bf421100            call      00547140h
  00432e81  99                    cdq
  00432e82  b91e000000            mov       ecx,1Eh
  00432e87  66c746100100          mov       word [esi+10h],1
  00432e8d  f7f9                  idiv      ecx
  00432e8f  8b4624                mov       eax,[esi+24h]
  00432e92  5f                    pop       edi
  00432e93  83ea0f                sub       edx,0Fh
  00432e96  c1e20c                shl       edx,0Ch
  00432e99  03c2                  add       eax,edx
  00432e9b  894624                mov       [esi+24h],eax
  00432e9e  5e                    pop       esi
  00432e9f  c3                    ret

1 cible(s) de saut interne : 0x00432de1
