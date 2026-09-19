Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433e10  section .text  offset fichier 0x33210  240 octets

  00433e10  53                    push      ebx
  00433e11  56                    push      esi
  00433e12  8b74240c              mov       esi,[esp+0Ch]
  00433e16  33db                  xor       ebx,ebx
  00433e18  66395e10              cmp       [esi+10h],bx
  00433e1c  0f85a8000000          jne       near 00433ECAh
  00433e22  66c746100100          mov       word [esi+10h],1
  00433e28  c7463c800d5500        mov       dword [esi+3Ch],550D80h
  00433e2f  c6460a09              mov       byte [esi+0Ah],9
  00433e33  885e0b                mov       [esi+0Bh],bl
  00433e36  e805331100            call      00547140h
  00433e3b  99                    cdq
  00433e3c  b928000000            mov       ecx,28h
  00433e41  f7f9                  idiv      ecx
  00433e43  8b461c                mov       eax,[esi+1Ch]
  00433e46  83ea14                sub       edx,14h
  00433e49  c1e20c                shl       edx,0Ch
  00433e4c  03c2                  add       eax,edx
  00433e4e  89461c                mov       [esi+1Ch],eax
  00433e51  e8ea321100            call      00547140h
  00433e56  99                    cdq
  00433e57  b928000000            mov       ecx,28h
  00433e5c  f7f9                  idiv      ecx
  00433e5e  8b4e24                mov       ecx,[esi+24h]
  00433e61  83ea14                sub       edx,14h
  00433e64  c1e20c                shl       edx,0Ch
  00433e67  03ca                  add       ecx,edx
  00433e69  894e24                mov       [esi+24h],ecx
  00433e6c  e8cf321100            call      00547140h
  00433e71  99                    cdq
  00433e72  b928000000            mov       ecx,28h
  00433e77  f7f9                  idiv      ecx
  00433e79  8b4620                mov       eax,[esi+20h]
  00433e7c  83ea14                sub       edx,14h
  00433e7f  c1e20c                shl       edx,0Ch
  00433e82  03c2                  add       eax,edx
  00433e84  894620                mov       [esi+20h],eax
  00433e87  e8b4321100            call      00547140h
  00433e8c  99                    cdq
  00433e8d  33c2                  xor       eax,edx
  00433e8f  2bc2                  sub       eax,edx
  00433e91  25ff070000            and       eax,7FFh
  00433e96  33c2                  xor       eax,edx
  00433e98  2bc2                  sub       eax,edx
  00433e9a  ba00140000            mov       edx,1400h
  00433e9f  2bd0                  sub       edx,eax
  00433ea1  66895628              mov       [esi+28h],dx
  00433ea5  e896321100            call      00547140h
  00433eaa  99                    cdq
  00433eab  33c2                  xor       eax,edx
  00433ead  b900140000            mov       ecx,1400h
  00433eb2  2bc2                  sub       eax,edx
  00433eb4  66895e12              mov       [esi+12h],bx
  00433eb8  25ff070000            and       eax,7FFh
  00433ebd  885e40                mov       [esi+40h],bl
  00433ec0  33c2                  xor       eax,edx
  00433ec2  2bc2                  sub       eax,edx
  00433ec4  2bc8                  sub       ecx,eax
  00433ec6  66894e2a              mov       [esi+2Ah],cx

LAB_00433eca:
  00433eca  66837e1001            cmp       word [esi+10h],1
  00433ecf  7523                  jne       short 00433EF4h
  00433ed1  66ff4612              inc       word [esi+12h]
  00433ed5  66837e1202            cmp       word [esi+12h],2
  00433eda  7518                  jne       short 00433EF4h
  00433edc  8a5640                mov       dl,[esi+40h]
  00433edf  66895e12              mov       [esi+12h],bx
  00433ee3  fec2                  inc       dl
  00433ee5  8ac2                  mov       al,dl
  00433ee7  885640                mov       [esi+40h],dl
  00433eea  3c04                  cmp       al,4
  00433eec  7506                  jne       short 00433EF4h
  00433eee  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00433ef4:
  00433ef4  5e                    pop       esi
  00433ef5  5b                    pop       ebx
  00433ef6  c3                    ret
  00433ef7  90                    nop
  00433ef8  90                    nop
  00433ef9  90                    nop
  00433efa  90                    nop
  00433efb  90                    nop
  00433efc  90                    nop
  00433efd  90                    nop
  00433efe  90                    nop
  00433eff  90                    nop

2 cible(s) de saut interne : 0x00433eca, 0x00433ef4
