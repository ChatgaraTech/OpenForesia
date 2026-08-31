Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00447df0  section .text  offset fichier 0x471f0  487 octets

  00447df0  53                    push      ebx
  00447df1  56                    push      esi
  00447df2  8b74240c              mov       esi,[esp+0Ch]
  00447df6  33db                  xor       ebx,ebx
  00447df8  57                    push      edi
  00447df9  53                    push      ebx
  00447dfa  53                    push      ebx
  00447dfb  56                    push      esi
  00447dfc  e83f020000            call      00448040h
  00447e01  83c40c                add       esp,0Ch
  00447e04  66395e10              cmp       [esi+10h],bx
  00447e08  750b                  jne       short 00447E15h
  00447e0a  53                    push      ebx
  00447e0b  53                    push      ebx
  00447e0c  56                    push      esi
  00447e0d  e89efeffff            call      00447CB0h
  00447e12  83c40c                add       esp,0Ch

LAB_00447e15:
  00447e15  8b4678                mov       eax,[esi+78h]
  00447e18  3bc3                  cmp       eax,ebx
  00447e1a  7505                  jne       short 00447E21h
  00447e1c  b858e55d00            mov       eax,5DE558h

LAB_00447e21:
  00447e21  8b481c                mov       ecx,[eax+1Ch]
  00447e24  8d7e1c                lea       edi,[esi+1Ch]
  00447e27  66395e10              cmp       [esi+10h],bx
  00447e2b  890f                  mov       [edi],ecx
  00447e2d  8b5020                mov       edx,[eax+20h]
  00447e30  895620                mov       [esi+20h],edx
  00447e33  8b4024                mov       eax,[eax+24h]
  00447e36  894624                mov       [esi+24h],eax
  00447e39  0f858d000000          jne       near 00447ECCh
  00447e3f  6aff                  push      0FFFFFFFFh
  00447e41  53                    push      ebx
  00447e42  68a9000000            push      0A9h
  00447e47  53                    push      ebx
  00447e48  b801000000            mov       eax,1
  00447e4d  57                    push      edi
  00447e4e  56                    push      esi
  00447e4f  66894610              mov       [esi+10h],ax
  00447e53  66895e12              mov       [esi+12h],bx
  00447e57  66c746145000          mov       word [esi+14h],50h
  00447e5d  66895e16              mov       [esi+16h],bx
  00447e61  6689869a000000        mov       [esi+9Ah],ax
  00447e68  e8d32d0300            call      0047AC40h
  00447e6d  83c418                add       esp,18h
  00447e70  6aff                  push      0FFFFFFFFh
  00447e72  53                    push      ebx
  00447e73  68a9000000            push      0A9h
  00447e78  53                    push      ebx
  00447e79  57                    push      edi
  00447e7a  56                    push      esi
  00447e7b  e8c02d0300            call      0047AC40h
  00447e80  83c418                add       esp,18h
  00447e83  6aff                  push      0FFFFFFFFh
  00447e85  53                    push      ebx
  00447e86  68a9000000            push      0A9h
  00447e8b  53                    push      ebx
  00447e8c  57                    push      edi
  00447e8d  56                    push      esi
  00447e8e  e8ad2d0300            call      0047AC40h
  00447e93  83c418                add       esp,18h
  00447e96  6aff                  push      0FFFFFFFFh
  00447e98  53                    push      ebx
  00447e99  68a9000000            push      0A9h
  00447e9e  53                    push      ebx
  00447e9f  57                    push      edi
  00447ea0  56                    push      esi
  00447ea1  e89a2d0300            call      0047AC40h
  00447ea6  8b4e74                mov       ecx,[esi+74h]
  00447ea9  8a5606                mov       dl,[esi+6]
  00447eac  83c418                add       esp,18h
  00447eaf  885106                mov       [ecx+6],dl
  00447eb2  8b4674                mov       eax,[esi+74h]
  00447eb5  8a4e07                mov       cl,[esi+7]
  00447eb8  53                    push      ebx
  00447eb9  884807                mov       [eax+7],cl
  00447ebc  8b5678                mov       edx,[esi+78h]
  00447ebf  8b4674                mov       eax,[esi+74h]
  00447ec2  52                    push      edx
  00447ec3  50                    push      eax
  00447ec4  e817010000            call      00447FE0h
  00447ec9  83c40c                add       esp,0Ch

LAB_00447ecc:
  00447ecc  66395e16              cmp       [esi+16h],bx
  00447ed0  7519                  jne       short 00447EEBh
  00447ed2  6800100000            push      1000h
  00447ed7  6800100000            push      1000h
  00447edc  6800020000            push      200h
  00447ee1  6a30                  push      30h
  00447ee3  e8b84f0f00            call      0053CEA0h
  00447ee8  83c410                add       esp,10h

LAB_00447eeb:
  00447eeb  66837e1602            cmp       word [esi+16h],2
  00447ef0  7519                  jne       short 00447F0Bh
  00447ef2  6800150000            push      1500h
  00447ef7  6800150000            push      1500h
  00447efc  6853060000            push      653h
  00447f01  6a2f                  push      2Fh
  00447f03  e8984f0f00            call      0053CEA0h
  00447f08  83c410                add       esp,10h

LAB_00447f0b:
  00447f0b  f6461601              test      byte [esi+16h],1
  00447f0f  742c                  je        short 00447F3Dh
  00447f11  6aff                  push      0FFFFFFFFh
  00447f13  53                    push      ebx
  00447f14  68a9000000            push      0A9h
  00447f19  53                    push      ebx
  00447f1a  57                    push      edi
  00447f1b  56                    push      esi
  00447f1c  66c746145000          mov       word [esi+14h],50h
  00447f22  e8192d0300            call      0047AC40h
  00447f27  83c418                add       esp,18h
  00447f2a  6aff                  push      0FFFFFFFFh
  00447f2c  53                    push      ebx
  00447f2d  68a9000000            push      0A9h
  00447f32  53                    push      ebx
  00447f33  57                    push      edi
  00447f34  56                    push      esi
  00447f35  e8062d0300            call      0047AC40h
  00447f3a  83c418                add       esp,18h

LAB_00447f3d:
  00447f3d  66837e1619            cmp       word [esi+16h],19h
  00447f42  7d79                  jge       short 00447FBDh
  00447f44  8b4e78                mov       ecx,[esi+78h]
  00447f47  c646043f              mov       byte [esi+4],3Fh
  00447f4b  c6460a09              mov       byte [esi+0Ah],9
  00447f4f  885e0b                mov       [esi+0Bh],bl
  00447f52  894e70                mov       [esi+70h],ecx
  00447f55  895e44                mov       [esi+44h],ebx
  00447f58  c7464800000500        mov       dword [esi+48h],50000h
  00447f5f  895e4c                mov       [esi+4Ch],ebx
  00447f62  e8d9f10f00            call      00547140h
  00447f67  99                    cdq
  00447f68  33c2                  xor       eax,edx
  00447f6a  2bc2                  sub       eax,edx
  00447f6c  25ff030000            and       eax,3FFh
  00447f71  33c2                  xor       eax,edx
  00447f73  2bc2                  sub       eax,edx
  00447f75  0500040000            add       eax,400h
  00447f7a  66894632              mov       [esi+32h],ax
  00447f7e  e8bdf10f00            call      00547140h
  00447f83  99                    cdq
  00447f84  33c2                  xor       eax,edx
  00447f86  6aff                  push      0FFFFFFFFh
  00447f88  2bc2                  sub       eax,edx
  00447f8a  53                    push      ebx
  00447f8b  25ff0f0000            and       eax,0FFFh
  00447f90  6a70                  push      70h
  00447f92  33c2                  xor       eax,edx
  00447f94  56                    push      esi
  00447f95  2bc2                  sub       eax,edx
  00447f97  66895e14              mov       [esi+14h],bx
  00447f9b  66894634              mov       [esi+34h],ax
  00447f9f  66c74612c800          mov       word [esi+12h],0C8h
  00447fa5  66c74628ff1f          mov       word [esi+28h],1FFFh
  00447fab  66895e2a              mov       [esi+2Ah],bx
  00447faf  66c7462c0001          mov       word [esi+2Ch],100h
  00447fb5  e8262b0300            call      0047AAE0h
  00447fba  83c410                add       esp,10h

LAB_00447fbd:
  00447fbd  66ff4616              inc       word [esi+16h]
  00447fc1  66837e1628            cmp       word [esi+16h],28h
  00447fc6  7e0b                  jle       short 00447FD3h
  00447fc8  53                    push      ebx
  00447fc9  53                    push      ebx
  00447fca  56                    push      esi
  00447fcb  e850000000            call      00448020h
  00447fd0  83c40c                add       esp,0Ch

LAB_00447fd3:
  00447fd3  5f                    pop       edi
  00447fd4  5e                    pop       esi
  00447fd5  5b                    pop       ebx
  00447fd6  c3                    ret

8 cible(s) de saut interne : 0x00447e15, 0x00447e21, 0x00447ecc, 0x00447eeb, 0x00447f0b, 0x00447f3d, 0x00447fbd, 0x00447fd3
