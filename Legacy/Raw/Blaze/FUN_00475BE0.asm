
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475be0  section .text  offset fichier 0x74fe0  224 octets

  00475be0  83ec08                sub       esp,8
  00475be3  56                    push      esi
  00475be4  8b742410              mov       esi,[esp+10h]
  00475be8  66837e1001            cmp       word [esi+10h],1
  00475bed  7518                  jne       short 00475C07h
  00475bef  8d461c                lea       eax,[esi+1Ch]
  00475bf2  50                    push      eax
  00475bf3  6800120000            push      1200h
  00475bf8  6800010000            push      100h
  00475bfd  6a31                  push      31h
  00475bff  e81c7f0c00            call      0053DB20h
  00475c04  83c410                add       esp,10h

LAB_00475c07:
  00475c07  8b4678                mov       eax,[esi+78h]
  00475c0a  8b481c                mov       ecx,[eax+1Ch]
  00475c0d  894e1c                mov       [esi+1Ch],ecx
  00475c10  8b5020                mov       edx,[eax+20h]
  00475c13  81c200000500          add       edx,50000h
  00475c19  895620                mov       [esi+20h],edx
  00475c1c  8b4024                mov       eax,[eax+24h]
  00475c1f  8b5628                mov       edx,[esi+28h]
  00475c22  6683462c80            add       word [esi+2Ch],0FF80h
  00475c27  894624                mov       [esi+24h],eax
  00475c2a  8b4638                mov       eax,[esi+38h]
  00475c2d  81c280ff7fff          add       edx,0FF7FFF80h
  00475c33  05f8f7f7ff            add       eax,0FFF7F7F8h
  00475c38  895628                mov       [esi+28h],edx
  00475c3b  894638                mov       [esi+38h],eax
  00475c3e  7574                  jne       short 00475CB4h
  00475c40  6a27                  push      27h
  00475c42  6820de5e00            push      5EDE20h
  00475c47  56                    push      esi
  00475c48  c70600004080          mov       dword [esi],80400000h
  00475c4e  e88df60b00            call      005352E0h
  00475c53  83c40c                add       esp,0Ch
  00475c56  8d4c2410              lea       ecx,[esp+10h]
  00475c5a  8d542404              lea       edx,[esp+4]
  00475c5e  8d442408              lea       eax,[esp+8]
  00475c62  51                    push      ecx
  00475c63  52                    push      edx
  00475c64  50                    push      eax
  00475c65  6820de5e00            push      5EDE20h
  00475c6a  c60528de5e0020        mov       byte [5EDE28h],20h
  00475c71  e80a2d0c00            call      00538980h
  00475c76  8b4c2420              mov       ecx,[esp+20h]
  00475c7a  8b542414              mov       edx,[esp+14h]
  00475c7e  8b442418              mov       eax,[esp+18h]
  00475c82  83c410                add       esp,10h
  00475c85  51                    push      ecx
  00475c86  52                    push      edx
  00475c87  50                    push      eax
  00475c88  e8532d0c00            call      005389E0h
  00475c8d  668b4e06              mov       cx,[esi+6]
  00475c91  83c40c                add       esp,0Ch
  00475c94  66890d26de5e00        mov       [5EDE26h],cx
  00475c9b  8b5678                mov       edx,[esi+78h]
  00475c9e  6a00                  push      0
  00475ca0  52                    push      edx
  00475ca1  6820de5e00            push      5EDE20h
  00475ca6  e8b5590c00            call      0053B660h
  00475cab  83c40c                add       esp,0Ch
  00475cae  c70600000000          mov       dword [esi],0

LAB_00475cb4:
  00475cb4  66ff4610              inc       word [esi+10h]
  00475cb8  5e                    pop       esi
  00475cb9  83c408                add       esp,8
  00475cbc  c3                    ret
  00475cbd  90                    nop
  00475cbe  90                    nop
  00475cbf  90                    nop

2 cible(s) de saut interne : 0x00475c07, 0x00475cb4


