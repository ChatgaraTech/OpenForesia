Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449df0  section .text  offset fichier 0x491f0  61 octets

  00449df0  56                    push      esi
  00449df1  8b74240c              mov       esi,[esp+0Ch]
  00449df5  57                    push      edi
  00449df6  8b7c240c              mov       edi,[esp+0Ch]
  00449dfa  56                    push      esi
  00449dfb  57                    push      edi
  00449dfc  e8afc6feff            call      004364B0h
  00449e01  83c408                add       esp,8
  00449e04  6685c0                test      ax,ax
  00449e07  751d                  jne       short 00449E26h
  00449e09  8b442414              mov       eax,[esp+14h]
  00449e0d  50                    push      eax
  00449e0e  56                    push      esi
  00449e0f  57                    push      edi
  00449e10  e8cb290f00            call      0053C7E0h
  00449e15  83c40c                add       esp,0Ch
  00449e18  33c9                  xor       ecx,ecx
  00449e1a  6685c0                test      ax,ax
  00449e1d  0f95c1                setne     cl
  00449e20  668bc1                mov       ax,cx
  00449e23  5f                    pop       edi
  00449e24  5e                    pop       esi
  00449e25  c3                    ret

LAB_00449e26:
  00449e26  5f                    pop       edi
  00449e27  66b80100              mov       ax,1
  00449e2b  5e                    pop       esi
  00449e2c  c3                    ret

1 cible(s) de saut interne : 0x00449e26
