Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044db30  section .text  offset fichier 0x4cf30  304 octets

  0044db30  53                    push      ebx
  0044db31  56                    push      esi
  0044db32  8b74240c              mov       esi,[esp+0Ch]
  0044db36  33db                  xor       ebx,ebx
  0044db38  8b4678                mov       eax,[esi+78h]
  0044db3b  3bc3                  cmp       eax,ebx
  0044db3d  7505                  jne       short 0044DB44h
  0044db3f  b858e55d00            mov       eax,5DE558h

LAB_0044db44:
  0044db44  8b481c                mov       ecx,[eax+1Ch]
  0044db47  894e1c                mov       [esi+1Ch],ecx
  0044db4a  8b5020                mov       edx,[eax+20h]
  0044db4d  81c200200300          add       edx,32000h
  0044db53  66395e10              cmp       [esi+10h],bx
  0044db57  895620                mov       [esi+20h],edx
  0044db5a  8b4024                mov       eax,[eax+24h]
  0044db5d  894624                mov       [esi+24h],eax
  0044db60  0f85a3000000          jne       near 0044DC09h
  0044db66  b8a00f0000            mov       eax,0FA0h
  0044db6b  c646041d              mov       byte [esi+4],1Dh
  0044db6f  c6460a02              mov       byte [esi+0Ah],2
  0044db73  c6460b01              mov       byte [esi+0Bh],1
  0044db77  66c746100100          mov       word [esi+10h],1
  0044db7d  885e38                mov       [esi+38h],bl
  0044db80  885e39                mov       [esi+39h],bl
  0044db83  885e3a                mov       [esi+3Ah],bl
  0044db86  66894628              mov       [esi+28h],ax
  0044db8a  6689462a              mov       [esi+2Ah],ax
  0044db8e  6689462c              mov       [esi+2Ch],ax
  0044db92  e8a9950f00            call      00547140h
  0044db97  99                    cdq
  0044db98  33c2                  xor       eax,edx
  0044db9a  2bc2                  sub       eax,edx
  0044db9c  25ff0f0000            and       eax,0FFFh
  0044dba1  33c2                  xor       eax,edx
  0044dba3  2bc2                  sub       eax,edx
  0044dba5  66894630              mov       [esi+30h],ax
  0044dba9  e892950f00            call      00547140h
  0044dbae  99                    cdq
  0044dbaf  33c2                  xor       eax,edx
  0044dbb1  2bc2                  sub       eax,edx
  0044dbb3  25ff0f0000            and       eax,0FFFh
  0044dbb8  33c2                  xor       eax,edx
  0044dbba  2bc2                  sub       eax,edx
  0044dbbc  66894632              mov       [esi+32h],ax
  0044dbc0  e87b950f00            call      00547140h
  0044dbc5  99                    cdq
  0044dbc6  33c2                  xor       eax,edx
  0044dbc8  2bc2                  sub       eax,edx
  0044dbca  25ff0f0000            and       eax,0FFFh
  0044dbcf  33c2                  xor       eax,edx
  0044dbd1  2bc2                  sub       eax,edx
  0044dbd3  66894634              mov       [esi+34h],ax
  0044dbd7  e864950f00            call      00547140h
  0044dbdc  99                    cdq
  0044dbdd  b964000000            mov       ecx,64h
  0044dbe2  f7f9                  idiv      ecx
  0044dbe4  83ea32                sub       edx,32h
  0044dbe7  66895612              mov       [esi+12h],dx
  0044dbeb  e850950f00            call      00547140h
  0044dbf0  99                    cdq
  0044dbf1  b964000000            mov       ecx,64h
  0044dbf6  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0044dbfc  f7f9                  idiv      ecx
  0044dbfe  66895e16              mov       [esi+16h],bx
  0044dc02  83ea32                sub       edx,32h
  0044dc05  66895614              mov       [esi+14h],dx

LAB_0044dc09:
  0044dc09  66837e1001            cmp       word [esi+10h],1
  0044dc0e  754d                  jne       short 0044DC5Dh
  0044dc10  668b4612              mov       ax,[esi+12h]
  0044dc14  668b5614              mov       dx,[esi+14h]
  0044dc18  66014630              add       [esi+30h],ax
  0044dc1c  66015632              add       [esi+32h],dx
  0044dc20  66294634              sub       [esi+34h],ax
  0044dc24  66ff4616              inc       word [esi+16h]
  0044dc28  66837e1605            cmp       word [esi+16h],5
  0044dc2d  7d12                  jge       short 0044DC41h
  0044dc2f  6a28                  push      28h
  0044dc31  6a28                  push      28h
  0044dc33  6a28                  push      28h
  0044dc35  56                    push      esi
  0044dc36  e8a571fbff            call      00404DE0h
  0044dc3b  83c410                add       esp,10h
  0044dc3e  5e                    pop       esi
  0044dc3f  5b                    pop       ebx
  0044dc40  c3                    ret

LAB_0044dc41:
  0044dc41  6ae2                  push      0FFFFFFE2h
  0044dc43  6ae2                  push      0FFFFFFE2h
  0044dc45  6ae2                  push      0FFFFFFE2h
  0044dc47  56                    push      esi
  0044dc48  e89371fbff            call      00404DE0h
  0044dc4d  8a4638                mov       al,[esi+38h]
  0044dc50  83c410                add       esp,10h
  0044dc53  3ac3                  cmp       al,bl
  0044dc55  7506                  jne       short 0044DC5Dh
  0044dc57  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044dc5d:
  0044dc5d  5e                    pop       esi
  0044dc5e  5b                    pop       ebx
  0044dc5f  c3                    ret

4 cible(s) de saut interne : 0x0044db44, 0x0044dc09, 0x0044dc41, 0x0044dc5d
