Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00403c10  section .text  offset fichier 0x3010  324 octets

  00403c10  83ec08                sub       esp,8
  00403c13  53                    push      ebx
  00403c14  56                    push      esi
  00403c15  8b742414              mov       esi,[esp+14h]
  00403c19  57                    push      edi
  00403c1a  8b4e6c                mov       ecx,[esi+6Ch]
  00403c1d  85c9                  test      ecx,ecx
  00403c1f  0f8428010000          je        near 00403D4Dh
  00403c25  8b411c                mov       eax,[ecx+1Ch]
  00403c28  8d5e1c                lea       ebx,[esi+1Ch]
  00403c2b  66837e1000            cmp       word [esi+10h],0
  00403c30  8903                  mov       [ebx],eax
  00403c32  8b5120                mov       edx,[ecx+20h]
  00403c35  895620                mov       [esi+20h],edx
  00403c38  8b4124                mov       eax,[ecx+24h]
  00403c3b  894624                mov       [esi+24h],eax
  00403c3e  0f85bf000000          jne       near 00403D03h
  00403c44  0fbf7e18              movsx     edi,word [esi+18h]
  00403c48  b889888888            mov       eax,88888889h
  00403c4d  66c746100100          mov       word [esi+10h],1
  00403c53  f7ef                  imul      edi
  00403c55  03d7                  add       edx,edi
  00403c57  c1fa04                sar       edx,4
  00403c5a  8bc2                  mov       eax,edx
  00403c5c  c1e81f                shr       eax,1Fh
  00403c5f  8d440202              lea       eax,[edx+eax+2]
  00403c63  663d1000              cmp       ax,10h
  00403c67  66894612              mov       [esi+12h],ax
  00403c6b  7c06                  jl        short 00403C73h
  00403c6d  66c746120f00          mov       word [esi+12h],0Fh

LAB_00403c73:
  00403c73  83cfff                or        edi,0FFFFFFFFh
  00403c76  66897e14              mov       [esi+14h],di
  00403c7a  8b11                  mov       edx,[ecx]
  00403c7c  81e20000fc00          and       edx,0FC0000h
  00403c82  81fa00008000          cmp       edx,800000h
  00403c88  7506                  jne       short 00403C90h
  00403c8a  66c746149600          mov       word [esi+14h],96h

LAB_00403c90:
  00403c90  8b01                  mov       eax,[ecx]
  00403c92  250000fc00            and       eax,0FC0000h
  00403c97  3d00004000            cmp       eax,400000h
  00403c9c  7407                  je        short 00403CA5h
  00403c9e  3d00006000            cmp       eax,600000h
  00403ca3  754a                  jne       short 00403CEFh

LAB_00403ca5:
  00403ca5  8d442418              lea       eax,[esp+18h]
  00403ca9  8d54240c              lea       edx,[esp+0Ch]
  00403cad  50                    push      eax
  00403cae  8d442414              lea       eax,[esp+14h]
  00403cb2  52                    push      edx
  00403cb3  50                    push      eax
  00403cb4  51                    push      ecx
  00403cb5  e8c64c1300            call      00538980h
  00403cba  8b442428              mov       eax,[esp+28h]
  00403cbe  83c410                add       esp,10h
  00403cc1  0fbf481c              movsx     ecx,word [eax+1Ch]
  00403cc5  0fbf501e              movsx     edx,word [eax+1Eh]
  00403cc9  0fbf4020              movsx     eax,word [eax+20h]
  00403ccd  03ca                  add       ecx,edx
  00403ccf  03c8                  add       ecx,eax
  00403cd1  b856555555            mov       eax,55555556h
  00403cd6  f7e9                  imul      ecx
  00403cd8  8bca                  mov       ecx,edx
  00403cda  c1e91f                shr       ecx,1Fh
  00403cdd  03d1                  add       edx,ecx
  00403cdf  6683fa04              cmp       dx,4
  00403ce3  66895614              mov       [esi+14h],dx
  00403ce7  7f06                  jg        short 00403CEFh
  00403ce9  66c746140500          mov       word [esi+14h],5

LAB_00403cef:
  00403cef  66397e14              cmp       [esi+14h],di
  00403cf3  7506                  jne       short 00403CFBh
  00403cf5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00403cfb:
  00403cfb  668b561a              mov       dx,[esi+1Ah]
  00403cff  66895616              mov       [esi+16h],dx

LAB_00403d03:
  00403d03  66837e1001            cmp       word [esi+10h],1
  00403d08  bf02000000            mov       edi,2
  00403d0d  7527                  jne       short 00403D36h
  00403d0f  66ff4e12              dec       word [esi+12h]
  00403d13  6aff                  push      0FFFFFFFFh
  00403d15  6a00                  push      0
  00403d17  6a32                  push      32h
  00403d19  6a00                  push      0
  00403d1b  53                    push      ebx
  00403d1c  56                    push      esi
  00403d1d  e81e6f0700            call      0047AC40h
  00403d22  83c418                add       esp,18h
  00403d25  66837e1200            cmp       word [esi+12h],0
  00403d2a  7d0a                  jge       short 00403D36h
  00403d2c  66897e10              mov       [esi+10h],di
  00403d30  66c746121e00          mov       word [esi+12h],1Eh

LAB_00403d36:
  00403d36  66397e10              cmp       [esi+10h],di
  00403d3a  7511                  jne       short 00403D4Dh
  00403d3c  66ff4e12              dec       word [esi+12h]
  00403d40  66837e1200            cmp       word [esi+12h],0
  00403d45  7f06                  jg        short 00403D4Dh
  00403d47  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00403d4d:
  00403d4d  5f                    pop       edi
  00403d4e  5e                    pop       esi
  00403d4f  5b                    pop       ebx
  00403d50  83c408                add       esp,8
  00403d53  c3                    ret

8 cible(s) de saut interne : 0x00403c73, 0x00403c90, 0x00403ca5, 0x00403cef, 0x00403cfb, 0x00403d03, 0x00403d36, 0x00403d4d
