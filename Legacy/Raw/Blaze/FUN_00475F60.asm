
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475f60  section .text  offset fichier 0x75360  87 octets

  00475f60  56                    push      esi
  00475f61  8b742408              mov       esi,[esp+8]
  00475f65  56                    push      esi
  00475f66  e815f90b00            call      00535880h
  00475f6b  8a5640                mov       dl,[esi+40h]
  00475f6e  83c404                add       esp,4
  00475f71  fec2                  inc       dl
  00475f73  660fb6c2              movzx     ax,dl
  00475f77  663b461a              cmp       ax,[esi+1Ah]
  00475f7b  885640                mov       [esi+40h],dl
  00475f7e  7c04                  jl        short 00475F84h
  00475f80  c6464000              mov       byte [esi+40h],0

LAB_00475f84:
  00475f84  8b4638                mov       eax,[esi+38h]
  00475f87  a9ffffff00            test      eax,0FFFFFFh
  00475f8c  7421                  je        short 00475FAFh
  00475f8e  668b4e2c              mov       cx,[esi+2Ch]
  00475f92  05f0efefff            add       eax,0FFEFEFF0h
  00475f97  894638                mov       [esi+38h],eax
  00475f9a  668b4628              mov       ax,[esi+28h]
  00475f9e  8bd0                  mov       edx,eax
  00475fa0  66d3fa                sar       dx,cl
  00475fa3  2bc2                  sub       eax,edx
  00475fa5  66894628              mov       [esi+28h],ax
  00475fa9  6689462a              mov       [esi+2Ah],ax
  00475fad  5e                    pop       esi
  00475fae  c3                    ret

LAB_00475faf:
  00475faf  c70600000000          mov       dword [esi],0
  00475fb5  5e                    pop       esi
  00475fb6  c3                    ret

2 cible(s) de saut interne : 0x00475f84, 0x00475faf
