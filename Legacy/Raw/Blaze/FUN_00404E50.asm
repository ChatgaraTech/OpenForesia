
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00404e50  section .text  offset fichier 0x4250  151 octets

  00404e50  53                    push      ebx
  00404e51  56                    push      esi
  00404e52  0fbf1d586c5600        movsx     ebx,word [566C58h]
  00404e59  0fbf355a6c5600        movsx     esi,word [566C5Ah]
  00404e60  81eb2c010000          sub       ebx,12Ch
  00404e66  81ee2c010000          sub       esi,12Ch
  00404e6c  85db                  test      ebx,ebx
  00404e6e  c705486c5600c0000000  mov       dword [566C48h],0C0h
  00404e78  7d02                  jge       short 00404E7Ch
  00404e7a  33db                  xor       ebx,ebx

LAB_00404e7c:
  00404e7c  85f6                  test      esi,esi
  00404e7e  7d02                  jge       short 00404E82h
  00404e80  33f6                  xor       esi,esi

LAB_00404e82:
  00404e82  68486c5600            push      566C48h
  00404e87  66891d586c5600        mov       [566C58h],bx
  00404e8e  6689355a6c5600        mov       [566C5Ah],si
  00404e95  e88e0b1400            call      00545A28h
  00404e9a  85db                  test      ebx,ebx
  00404e9c  7546                  jne       short 00404EE4h
  00404e9e  85f6                  test      esi,esi
  00404ea0  7542                  jne       short 00404EE4h
  00404ea2  a0fe965e00            mov       al,[5E96FEh]
  00404ea7  84c0                  test      al,al
  00404ea9  7414                  je        short 00404EBFh
  00404eab  3c01                  cmp       al,1
  00404ead  7507                  jne       short 00404EB6h
  00404eaf  bb01000000            mov       ebx,1
  00404eb4  eb09                  jmp       short 00404EBFh

LAB_00404eb6:
  00404eb6  3c02                  cmp       al,2
  00404eb8  7505                  jne       short 00404EBFh
  00404eba  bb02000000            mov       ebx,2

LAB_00404ebf:
  00404ebf  881d34e55d00          mov       [5DE534h],bl
  00404ec5  881d8c6c5600          mov       [566C8Ch],bl
  00404ecb  81e3ff000000          and       ebx,0FFh
  00404ed1  53                    push      ebx
  00404ed2  e8f94b1200            call      00529AD0h
  00404ed7  8b442410              mov       eax,[esp+10h]
  00404edb  83c404                add       esp,4
  00404ede  8120ffffff7f          and       dword [eax],7FFFFFFFh

LAB_00404ee4:
  00404ee4  5e                    pop       esi
  00404ee5  5b                    pop       ebx
  00404ee6  c3                    ret

5 cible(s) de saut interne : 0x00404e7c, 0x00404e82, 0x00404eb6, 0x00404ebf, 0x00404ee4
