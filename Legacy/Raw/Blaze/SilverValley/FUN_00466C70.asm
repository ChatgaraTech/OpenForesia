Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466c70  section .text  offset fichier 0x66070  256 octets

  00466c70  56                    push      esi
  00466c71  8b742408              mov       esi,[esp+8]
  00466c75  57                    push      edi
  00466c76  6a00                  push      0
  00466c78  6a00                  push      0
  00466c7a  6aff                  push      0FFFFFFFFh
  00466c7c  6a00                  push      0
  00466c7e  6a30                  push      30h
  00466c80  56                    push      esi
  00466c81  e88a140000            call      00468110h
  00466c86  83c418                add       esp,18h
  00466c89  6a00                  push      0
  00466c8b  6a00                  push      0
  00466c8d  6aff                  push      0FFFFFFFFh
  00466c8f  6a00                  push      0
  00466c91  6a30                  push      30h
  00466c93  56                    push      esi
  00466c94  e877140000            call      00468110h
  00466c99  83c418                add       esp,18h
  00466c9c  6a00                  push      0
  00466c9e  6a00                  push      0
  00466ca0  6aff                  push      0FFFFFFFFh
  00466ca2  6a00                  push      0
  00466ca4  6a30                  push      30h
  00466ca6  56                    push      esi
  00466ca7  e864140000            call      00468110h
  00466cac  83c418                add       esp,18h
  00466caf  6a00                  push      0
  00466cb1  6a00                  push      0
  00466cb3  6aff                  push      0FFFFFFFFh
  00466cb5  6a00                  push      0
  00466cb7  6a30                  push      30h
  00466cb9  56                    push      esi
  00466cba  e851140000            call      00468110h
  00466cbf  83c418                add       esp,18h
  00466cc2  6a00                  push      0
  00466cc4  6a00                  push      0
  00466cc6  6aff                  push      0FFFFFFFFh
  00466cc8  6a00                  push      0
  00466cca  6a30                  push      30h
  00466ccc  56                    push      esi
  00466ccd  e83e140000            call      00468110h
  00466cd2  83c418                add       esp,18h
  00466cd5  6a00                  push      0
  00466cd7  6a00                  push      0
  00466cd9  6aff                  push      0FFFFFFFFh
  00466cdb  6a00                  push      0
  00466cdd  6a30                  push      30h
  00466cdf  56                    push      esi
  00466ce0  e82b140000            call      00468110h
  00466ce5  83c418                add       esp,18h
  00466ce8  6800000040            push      40000000h
  00466ced  6a10                  push      10h
  00466cef  56                    push      esi
  00466cf0  e80be30c00            call      00535000h
  00466cf5  83c40c                add       esp,0Ch
  00466cf8  66837e1000            cmp       word [esi+10h],0
  00466cfd  8bf8                  mov       edi,eax
  00466cff  751b                  jne       short 00466D1Ch
  00466d01  e86a000000            call      00466D70h
  00466d06  663bf8                cmp       di,ax
  00466d09  7511                  jne       short 00466D1Ch
  00466d0b  6685ff                test      di,di
  00466d0e  740c                  je        short 00466D1Ch
  00466d10  66c746100100          mov       word [esi+10h],1
  00466d16  66c746120000          mov       word [esi+12h],0

LAB_00466d1c:
  00466d1c  66837e1001            cmp       word [esi+10h],1
  00466d21  7547                  jne       short 00466D6Ah
  00466d23  e848000000            call      00466D70h
  00466d28  663bf8                cmp       di,ax
  00466d2b  7537                  jne       short 00466D64h
  00466d2d  6685ff                test      di,di
  00466d30  7432                  je        short 00466D64h
  00466d32  66ff4612              inc       word [esi+12h]
  00466d36  66817e125802          cmp       word [esi+12h],258h
  00466d3c  7c2c                  jl        short 00466D6Ah
  00466d3e  66c746100200          mov       word [esi+10h],2
  00466d44  a1cc975e00            mov       eax,[5E97CCh]
  00466d49  6a00                  push      0
  00466d4b  68b0335600            push      5633B0h
  00466d50  8b4870                mov       ecx,[eax+70h]
  00466d53  6a00                  push      0
  00466d55  8b5104                mov       edx,[ecx+4]
  00466d58  52                    push      edx
  00466d59  e8f2d50d00            call      00544350h
  00466d5e  83c410                add       esp,10h
  00466d61  5f                    pop       edi
  00466d62  5e                    pop       esi
  00466d63  c3                    ret

LAB_00466d64:
  00466d64  66c746100000          mov       word [esi+10h],0

LAB_00466d6a:
  00466d6a  5f                    pop       edi
  00466d6b  5e                    pop       esi
  00466d6c  c3                    ret
  00466d6d  90                    nop
  00466d6e  90                    nop
  00466d6f  90                    nop

3 cible(s) de saut interne : 0x00466d1c, 0x00466d64, 0x00466d6a
