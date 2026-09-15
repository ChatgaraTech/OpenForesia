Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00404b90  section .text  offset fichier 0x3f90  232 octets

  00404b90  a1bcca5e00            mov       eax,[5ECABCh]
  00404b95  56                    push      esi
  00404b96  8b742408              mov       esi,[esp+8]
  00404b9a  57                    push      edi
  00404b9b  8d7e1c                lea       edi,[esi+1Ch]
  00404b9e  66837e1000            cmp       word [esi+10h],0
  00404ba3  8907                  mov       [edi],eax
  00404ba5  8b0dc0ca5e00          mov       ecx,[5ECAC0h]
  00404bab  894e20                mov       [esi+20h],ecx
  00404bae  8b15c4ca5e00          mov       edx,[5ECAC4h]
  00404bb4  895624                mov       [esi+24h],edx
  00404bb7  750c                  jne       short 00404BC5h
  00404bb9  66c746100100          mov       word [esi+10h],1
  00404bbf  66c746161e00          mov       word [esi+16h],1Eh

LAB_00404bc5:
  00404bc5  6aff                  push      0FFFFFFFFh
  00404bc7  6a00                  push      0
  00404bc9  68ff000000            push      0FFh
  00404bce  6a00                  push      0
  00404bd0  57                    push      edi
  00404bd1  56                    push      esi
  00404bd2  e869600700            call      0047AC40h
  00404bd7  83c418                add       esp,18h
  00404bda  6aff                  push      0FFFFFFFFh
  00404bdc  6a00                  push      0
  00404bde  68ff000000            push      0FFh
  00404be3  6a00                  push      0
  00404be5  57                    push      edi
  00404be6  56                    push      esi
  00404be7  e854600700            call      0047AC40h
  00404bec  83c418                add       esp,18h
  00404bef  6aff                  push      0FFFFFFFFh
  00404bf1  6a00                  push      0
  00404bf3  68ff000000            push      0FFh
  00404bf8  6a00                  push      0
  00404bfa  57                    push      edi
  00404bfb  56                    push      esi
  00404bfc  e83f600700            call      0047AC40h
  00404c01  83c418                add       esp,18h
  00404c04  66837e161e            cmp       word [esi+16h],1Eh
  00404c09  7519                  jne       short 00404C24h
  00404c0b  6800100000            push      1000h
  00404c10  6800100000            push      1000h
  00404c15  6855020000            push      255h
  00404c1a  6a20                  push      20h
  00404c1c  e87f821300            call      0053CEA0h
  00404c21  83c410                add       esp,10h

LAB_00404c24:
  00404c24  66837e1616            cmp       word [esi+16h],16h
  00404c29  7519                  jne       short 00404C44h
  00404c2b  6800100000            push      1000h
  00404c30  6800100000            push      1000h
  00404c35  68aa020000            push      2AAh
  00404c3a  6a20                  push      20h
  00404c3c  e85f821300            call      0053CEA0h
  00404c41  83c410                add       esp,10h

LAB_00404c44:
  00404c44  66837e160e            cmp       word [esi+16h],0Eh
  00404c49  7519                  jne       short 00404C64h
  00404c4b  6800100000            push      1000h
  00404c50  6800100000            push      1000h
  00404c55  68d4020000            push      2D4h
  00404c5a  6a20                  push      20h
  00404c5c  e83f821300            call      0053CEA0h
  00404c61  83c410                add       esp,10h

LAB_00404c64:
  00404c64  66ff4e16              dec       word [esi+16h]
  00404c68  66837e1600            cmp       word [esi+16h],0
  00404c6d  7506                  jne       short 00404C75h
  00404c6f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00404c75:
  00404c75  5f                    pop       edi
  00404c76  5e                    pop       esi
  00404c77  c3                    ret

5 cible(s) de saut interne : 0x00404bc5, 0x00404c24, 0x00404c44, 0x00404c64, 0x00404c75
