Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00441570  section .text  offset fichier 0x40970  256 octets

  00441570  56                    push      esi
  00441571  8b742408              mov       esi,[esp+8]
  00441575  33c0                  xor       eax,eax
  00441577  66394610              cmp       [esi+10h],ax
  0044157b  0f858e000000          jne       near 0044160Fh
  00441581  8a0dc4de5e00          mov       cl,[5EDEC4h]
  00441587  c6460a03              mov       byte [esi+0Ah],3
  0044158b  884e04                mov       [esi+4],cl
  0044158e  88460b                mov       [esi+0Bh],al
  00441591  66894628              mov       [esi+28h],ax
  00441595  6689462a              mov       [esi+2Ah],ax
  00441599  6689462c              mov       [esi+2Ch],ax
  0044159d  e89e5b1000            call      00547140h
  004415a2  99                    cdq
  004415a3  33c2                  xor       eax,edx
  004415a5  2bc2                  sub       eax,edx
  004415a7  25ff0f0000            and       eax,0FFFh
  004415ac  33c2                  xor       eax,edx
  004415ae  2bc2                  sub       eax,edx
  004415b0  66894632              mov       [esi+32h],ax
  004415b4  e8875b1000            call      00547140h
  004415b9  99                    cdq
  004415ba  33c2                  xor       eax,edx
  004415bc  66c746100100          mov       word [esi+10h],1
  004415c2  2bc2                  sub       eax,edx
  004415c4  25ff0f0000            and       eax,0FFFh
  004415c9  33c2                  xor       eax,edx
  004415cb  2bc2                  sub       eax,edx
  004415cd  66894634              mov       [esi+34h],ax
  004415d1  e86a5b1000            call      00547140h
  004415d6  99                    cdq
  004415d7  b9c8000000            mov       ecx,0C8h
  004415dc  f7f9                  idiv      ecx
  004415de  83ea64                sub       edx,64h
  004415e1  66895612              mov       [esi+12h],dx
  004415e5  e8565b1000            call      00547140h
  004415ea  99                    cdq
  004415eb  b9c8000000            mov       ecx,0C8h
  004415f0  f7f9                  idiv      ecx
  004415f2  83ea64                sub       edx,64h
  004415f5  66895614              mov       [esi+14h],dx
  004415f9  e8425b1000            call      00547140h
  004415fe  99                    cdq
  004415ff  b920030000            mov       ecx,320h
  00441604  f7f9                  idiv      ecx
  00441606  81c290010000          add       edx,190h
  0044160c  89565c                mov       [esi+5Ch],edx

LAB_0044160f:
  0044160f  66837e1001            cmp       word [esi+10h],1
  00441614  7552                  jne       short 00441668h
  00441616  6afc                  push      0FFFFFFFCh
  00441618  6afc                  push      0FFFFFFFCh
  0044161a  6afc                  push      0FFFFFFFCh
  0044161c  56                    push      esi
  0044161d  e8be37fcff            call      00404DE0h
  00441622  668b4614              mov       ax,[esi+14h]
  00441626  668b5612              mov       dx,[esi+12h]
  0044162a  66014634              add       [esi+34h],ax
  0044162e  668b465c              mov       ax,[esi+5Ch]
  00441632  66014628              add       [esi+28h],ax
  00441636  6601462a              add       [esi+2Ah],ax
  0044163a  6601462c              add       [esi+2Ch],ax
  0044163e  668b4e28              mov       cx,[esi+28h]
  00441642  66015632              add       [esi+32h],dx
  00441646  b830750000            mov       eax,7530h
  0044164b  83c410                add       esp,10h
  0044164e  663bc8                cmp       cx,ax
  00441651  7c15                  jl        short 00441668h
  00441653  66894628              mov       [esi+28h],ax
  00441657  6689462a              mov       [esi+2Ah],ax
  0044165b  6689462c              mov       [esi+2Ch],ax
  0044165f  8b06                  mov       eax,[esi]
  00441661  25ffffff7f            and       eax,7FFFFFFFh
  00441666  8906                  mov       [esi],eax

LAB_00441668:
  00441668  5e                    pop       esi
  00441669  c3                    ret
  0044166a  90                    nop
  0044166b  90                    nop
  0044166c  90                    nop
  0044166d  90                    nop
  0044166e  90                    nop
  0044166f  90                    nop

2 cible(s) de saut interne : 0x0044160f, 0x00441668
