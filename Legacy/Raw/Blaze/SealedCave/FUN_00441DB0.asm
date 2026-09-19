Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00441db0  section .text  offset fichier 0x411b0  256 octets

  00441db0  56                    push      esi
  00441db1  8b742408              mov       esi,[esp+8]
  00441db5  33c0                  xor       eax,eax
  00441db7  66394610              cmp       [esi+10h],ax
  00441dbb  0f858e000000          jne       near 00441E4Fh
  00441dc1  8a0dc4de5e00          mov       cl,[5EDEC4h]
  00441dc7  c6460a03              mov       byte [esi+0Ah],3
  00441dcb  884e04                mov       [esi+4],cl
  00441dce  88460b                mov       [esi+0Bh],al
  00441dd1  66894628              mov       [esi+28h],ax
  00441dd5  6689462a              mov       [esi+2Ah],ax
  00441dd9  6689462c              mov       [esi+2Ch],ax
  00441ddd  e85e531000            call      00547140h
  00441de2  99                    cdq
  00441de3  33c2                  xor       eax,edx
  00441de5  2bc2                  sub       eax,edx
  00441de7  25ff0f0000            and       eax,0FFFh
  00441dec  33c2                  xor       eax,edx
  00441dee  2bc2                  sub       eax,edx
  00441df0  66894632              mov       [esi+32h],ax
  00441df4  e847531000            call      00547140h
  00441df9  99                    cdq
  00441dfa  33c2                  xor       eax,edx
  00441dfc  66c746100100          mov       word [esi+10h],1
  00441e02  2bc2                  sub       eax,edx
  00441e04  25ff0f0000            and       eax,0FFFh
  00441e09  33c2                  xor       eax,edx
  00441e0b  2bc2                  sub       eax,edx
  00441e0d  66894634              mov       [esi+34h],ax
  00441e11  e82a531000            call      00547140h
  00441e16  99                    cdq
  00441e17  b9c8000000            mov       ecx,0C8h
  00441e1c  f7f9                  idiv      ecx
  00441e1e  83ea64                sub       edx,64h
  00441e21  66895612              mov       [esi+12h],dx
  00441e25  e816531000            call      00547140h
  00441e2a  99                    cdq
  00441e2b  b9c8000000            mov       ecx,0C8h
  00441e30  f7f9                  idiv      ecx
  00441e32  83ea64                sub       edx,64h
  00441e35  66895614              mov       [esi+14h],dx
  00441e39  e802531000            call      00547140h
  00441e3e  99                    cdq
  00441e3f  b990010000            mov       ecx,190h
  00441e44  f7f9                  idiv      ecx
  00441e46  81c2c8000000          add       edx,0C8h
  00441e4c  89565c                mov       [esi+5Ch],edx

LAB_00441e4f:
  00441e4f  66837e1001            cmp       word [esi+10h],1
  00441e54  7552                  jne       short 00441EA8h
  00441e56  6afe                  push      0FFFFFFFEh
  00441e58  6afe                  push      0FFFFFFFEh
  00441e5a  6afe                  push      0FFFFFFFEh
  00441e5c  56                    push      esi
  00441e5d  e87e2ffcff            call      00404DE0h
  00441e62  668b4614              mov       ax,[esi+14h]
  00441e66  668b5612              mov       dx,[esi+12h]
  00441e6a  66014634              add       [esi+34h],ax
  00441e6e  668b465c              mov       ax,[esi+5Ch]
  00441e72  66014628              add       [esi+28h],ax
  00441e76  6601462a              add       [esi+2Ah],ax
  00441e7a  6601462c              add       [esi+2Ch],ax
  00441e7e  668b4e28              mov       cx,[esi+28h]
  00441e82  66015632              add       [esi+32h],dx
  00441e86  b830750000            mov       eax,7530h
  00441e8b  83c410                add       esp,10h
  00441e8e  663bc8                cmp       cx,ax
  00441e91  7c15                  jl        short 00441EA8h
  00441e93  66894628              mov       [esi+28h],ax
  00441e97  6689462a              mov       [esi+2Ah],ax
  00441e9b  6689462c              mov       [esi+2Ch],ax
  00441e9f  8b06                  mov       eax,[esi]
  00441ea1  25ffffff7f            and       eax,7FFFFFFFh
  00441ea6  8906                  mov       [esi],eax

LAB_00441ea8:
  00441ea8  5e                    pop       esi
  00441ea9  c3                    ret
  00441eaa  90                    nop
  00441eab  90                    nop
  00441eac  90                    nop
  00441ead  90                    nop
  00441eae  90                    nop
  00441eaf  90                    nop

2 cible(s) de saut interne : 0x00441e4f, 0x00441ea8
