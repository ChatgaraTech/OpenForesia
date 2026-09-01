Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044d030  section .text  offset fichier 0x4c430  315 octets

  0044d030  53                    push      ebx
  0044d031  56                    push      esi
  0044d032  8b74240c              mov       esi,[esp+0Ch]
  0044d036  33db                  xor       ebx,ebx
  0044d038  8b4678                mov       eax,[esi+78h]
  0044d03b  3bc3                  cmp       eax,ebx
  0044d03d  7505                  jne       short 0044D044h
  0044d03f  b858e55d00            mov       eax,5DE558h

LAB_0044d044:
  0044d044  8b481c                mov       ecx,[eax+1Ch]
  0044d047  66395e10              cmp       [esi+10h],bx
  0044d04b  894e1c                mov       [esi+1Ch],ecx
  0044d04e  8b5060                mov       edx,[eax+60h]
  0044d051  895620                mov       [esi+20h],edx
  0044d054  8b4024                mov       eax,[eax+24h]
  0044d057  894624                mov       [esi+24h],eax
  0044d05a  0f85aa000000          jne       near 0044D10Ah
  0044d060  b8a00f0000            mov       eax,0FA0h
  0044d065  c646041d              mov       byte [esi+4],1Dh
  0044d069  c6460a02              mov       byte [esi+0Ah],2
  0044d06d  c6460b01              mov       byte [esi+0Bh],1
  0044d071  66c746100100          mov       word [esi+10h],1
  0044d077  885e38                mov       [esi+38h],bl
  0044d07a  885e39                mov       [esi+39h],bl
  0044d07d  885e3a                mov       [esi+3Ah],bl
  0044d080  66894628              mov       [esi+28h],ax
  0044d084  6689462a              mov       [esi+2Ah],ax
  0044d088  6689462c              mov       [esi+2Ch],ax
  0044d08c  e8afa00f00            call      00547140h
  0044d091  99                    cdq
  0044d092  33c2                  xor       eax,edx
  0044d094  2bc2                  sub       eax,edx
  0044d096  25ff0f0000            and       eax,0FFFh
  0044d09b  33c2                  xor       eax,edx
  0044d09d  2bc2                  sub       eax,edx
  0044d09f  66894630              mov       [esi+30h],ax
  0044d0a3  e898a00f00            call      00547140h
  0044d0a8  99                    cdq
  0044d0a9  33c2                  xor       eax,edx
  0044d0ab  2bc2                  sub       eax,edx
  0044d0ad  25ff0f0000            and       eax,0FFFh
  0044d0b2  33c2                  xor       eax,edx
  0044d0b4  2bc2                  sub       eax,edx
  0044d0b6  66894632              mov       [esi+32h],ax
  0044d0ba  e881a00f00            call      00547140h
  0044d0bf  99                    cdq
  0044d0c0  33c2                  xor       eax,edx
  0044d0c2  2bc2                  sub       eax,edx
  0044d0c4  25ff0f0000            and       eax,0FFFh
  0044d0c9  33c2                  xor       eax,edx
  0044d0cb  2bc2                  sub       eax,edx
  0044d0cd  66894634              mov       [esi+34h],ax
  0044d0d1  e86aa00f00            call      00547140h
  0044d0d6  99                    cdq
  0044d0d7  b964000000            mov       ecx,64h
  0044d0dc  f7f9                  idiv      ecx
  0044d0de  83ea32                sub       edx,32h
  0044d0e1  66895612              mov       [esi+12h],dx
  0044d0e5  e856a00f00            call      00547140h
  0044d0ea  99                    cdq
  0044d0eb  b964000000            mov       ecx,64h
  0044d0f0  66895e16              mov       [esi+16h],bx
  0044d0f4  f7f9                  idiv      ecx
  0044d0f6  66c7462ec4ff          mov       word [esi+2Eh],0FFC4h
  0044d0fc  c7465cfa000000        mov       dword [esi+5Ch],0FAh
  0044d103  83ea32                sub       edx,32h
  0044d106  66895614              mov       [esi+14h],dx

LAB_0044d10a:
  0044d10a  66837e1001            cmp       word [esi+10h],1
  0044d10f  7549                  jne       short 0044D15Ah
  0044d111  668b4612              mov       ax,[esi+12h]
  0044d115  668b5614              mov       dx,[esi+14h]
  0044d119  66014630              add       [esi+30h],ax
  0044d11d  66294634              sub       [esi+34h],ax
  0044d121  8b465c                mov       eax,[esi+5Ch]
  0044d124  66015632              add       [esi+32h],dx
  0044d128  8b5620                mov       edx,[esi+20h]
  0044d12b  8bc8                  mov       ecx,eax
  0044d12d  c1e10c                shl       ecx,0Ch
  0044d130  83c0fe                add       eax,0FFFFFFFEh
  0044d133  03d1                  add       edx,ecx
  0044d135  83f81e                cmp       eax,1Eh
  0044d138  895620                mov       [esi+20h],edx
  0044d13b  89465c                mov       [esi+5Ch],eax
  0044d13e  7d07                  jge       short 0044D147h
  0044d140  c7465c1e000000        mov       dword [esi+5Ch],1Eh

LAB_0044d147:
  0044d147  66ff4616              inc       word [esi+16h]
  0044d14b  6a08                  push      8
  0044d14d  6a08                  push      8
  0044d14f  6a08                  push      8
  0044d151  56                    push      esi
  0044d152  e8897cfbff            call      00404DE0h
  0044d157  83c410                add       esp,10h

LAB_0044d15a:
  0044d15a  66817e168000          cmp       word [esi+16h],80h
  0044d160  7506                  jne       short 0044D168h
  0044d162  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044d168:
  0044d168  5e                    pop       esi
  0044d169  5b                    pop       ebx
  0044d16a  c3                    ret

5 cible(s) de saut interne : 0x0044d044, 0x0044d10a, 0x0044d147, 0x0044d15a, 0x0044d168
