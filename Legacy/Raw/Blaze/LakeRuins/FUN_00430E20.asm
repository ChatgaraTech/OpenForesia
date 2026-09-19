Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00430e20  section .text  offset fichier 0x30220  240 octets

  00430e20  53                    push      ebx
  00430e21  56                    push      esi
  00430e22  8b74240c              mov       esi,[esp+0Ch]
  00430e26  33db                  xor       ebx,ebx
  00430e28  66395e10              cmp       [esi+10h],bx
  00430e2c  0f85a8000000          jne       near 00430EDAh
  00430e32  66c746100100          mov       word [esi+10h],1
  00430e38  c7463c900b5500        mov       dword [esi+3Ch],550B90h
  00430e3f  c6460a09              mov       byte [esi+0Ah],9
  00430e43  885e0b                mov       [esi+0Bh],bl
  00430e46  e8f5621100            call      00547140h
  00430e4b  99                    cdq
  00430e4c  b928000000            mov       ecx,28h
  00430e51  f7f9                  idiv      ecx
  00430e53  8b461c                mov       eax,[esi+1Ch]
  00430e56  83ea14                sub       edx,14h
  00430e59  c1e20c                shl       edx,0Ch
  00430e5c  03c2                  add       eax,edx
  00430e5e  89461c                mov       [esi+1Ch],eax
  00430e61  e8da621100            call      00547140h
  00430e66  99                    cdq
  00430e67  b928000000            mov       ecx,28h
  00430e6c  f7f9                  idiv      ecx
  00430e6e  8b4e24                mov       ecx,[esi+24h]
  00430e71  83ea14                sub       edx,14h
  00430e74  c1e20c                shl       edx,0Ch
  00430e77  03ca                  add       ecx,edx
  00430e79  894e24                mov       [esi+24h],ecx
  00430e7c  e8bf621100            call      00547140h
  00430e81  99                    cdq
  00430e82  b928000000            mov       ecx,28h
  00430e87  f7f9                  idiv      ecx
  00430e89  8b4620                mov       eax,[esi+20h]
  00430e8c  83ea14                sub       edx,14h
  00430e8f  c1e20c                shl       edx,0Ch
  00430e92  03c2                  add       eax,edx
  00430e94  894620                mov       [esi+20h],eax
  00430e97  e8a4621100            call      00547140h
  00430e9c  99                    cdq
  00430e9d  33c2                  xor       eax,edx
  00430e9f  2bc2                  sub       eax,edx
  00430ea1  25ff070000            and       eax,7FFh
  00430ea6  33c2                  xor       eax,edx
  00430ea8  2bc2                  sub       eax,edx
  00430eaa  ba00140000            mov       edx,1400h
  00430eaf  2bd0                  sub       edx,eax
  00430eb1  66895628              mov       [esi+28h],dx
  00430eb5  e886621100            call      00547140h
  00430eba  99                    cdq
  00430ebb  33c2                  xor       eax,edx
  00430ebd  b900140000            mov       ecx,1400h
  00430ec2  2bc2                  sub       eax,edx
  00430ec4  66895e12              mov       [esi+12h],bx
  00430ec8  25ff070000            and       eax,7FFh
  00430ecd  885e40                mov       [esi+40h],bl
  00430ed0  33c2                  xor       eax,edx
  00430ed2  2bc2                  sub       eax,edx
  00430ed4  2bc8                  sub       ecx,eax
  00430ed6  66894e2a              mov       [esi+2Ah],cx

LAB_00430eda:
  00430eda  66837e1001            cmp       word [esi+10h],1
  00430edf  7523                  jne       short 00430F04h
  00430ee1  66ff4612              inc       word [esi+12h]
  00430ee5  66837e1202            cmp       word [esi+12h],2
  00430eea  7518                  jne       short 00430F04h
  00430eec  8a5640                mov       dl,[esi+40h]
  00430eef  66895e12              mov       [esi+12h],bx
  00430ef3  fec2                  inc       dl
  00430ef5  8ac2                  mov       al,dl
  00430ef7  885640                mov       [esi+40h],dl
  00430efa  3c04                  cmp       al,4
  00430efc  7506                  jne       short 00430F04h
  00430efe  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00430f04:
  00430f04  5e                    pop       esi
  00430f05  5b                    pop       ebx
  00430f06  c3                    ret
  00430f07  90                    nop
  00430f08  90                    nop
  00430f09  90                    nop
  00430f0a  90                    nop
  00430f0b  90                    nop
  00430f0c  90                    nop
  00430f0d  90                    nop
  00430f0e  90                    nop
  00430f0f  90                    nop

2 cible(s) de saut interne : 0x00430eda, 0x00430f04
