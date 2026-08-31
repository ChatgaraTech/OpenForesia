Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044a320  section .text  offset fichier 0x49720  386 octets

  0044a320  53                    push      ebx
  0044a321  56                    push      esi
  0044a322  8b74240c              mov       esi,[esp+0Ch]
  0044a326  33db                  xor       ebx,ebx
  0044a328  57                    push      edi
  0044a329  53                    push      ebx
  0044a32a  53                    push      ebx
  0044a32b  56                    push      esi
  0044a32c  e80fddffff            call      00448040h
  0044a331  83c40c                add       esp,0Ch
  0044a334  66395e10              cmp       [esi+10h],bx
  0044a338  bf01000000            mov       edi,1
  0044a33d  0f859f000000          jne       near 0044A3E2h
  0044a343  c7463ca0155500        mov       dword [esi+3Ch],5515A0h
  0044a34a  885e40                mov       [esi+40h],bl
  0044a34d  c6460a06              mov       byte [esi+0Ah],6
  0044a351  885e0b                mov       [esi+0Bh],bl
  0044a354  66897e10              mov       [esi+10h],di
  0044a358  e8e3cd0f00            call      00547140h
  0044a35d  99                    cdq
  0044a35e  33c2                  xor       eax,edx
  0044a360  8b4e20                mov       ecx,[esi+20h]
  0044a363  2bc2                  sub       eax,edx
  0044a365  81c100800700          add       ecx,78000h
  0044a36b  25ff030000            and       eax,3FFh
  0044a370  894e54                mov       [esi+54h],ecx
  0044a373  33c2                  xor       eax,edx
  0044a375  66895e14              mov       [esi+14h],bx
  0044a379  2bc2                  sub       eax,edx
  0044a37b  8b5624                mov       edx,[esi+24h]
  0044a37e  0500080000            add       eax,800h
  0044a383  895658                mov       [esi+58h],edx
  0044a386  66894628              mov       [esi+28h],ax
  0044a38a  6689462a              mov       [esi+2Ah],ax
  0044a38e  8b461c                mov       eax,[esi+1Ch]
  0044a391  c6463a96              mov       byte [esi+3Ah],96h
  0044a395  894650                mov       [esi+50h],eax
  0044a398  b0ff                  mov       al,0FFh
  0044a39a  884638                mov       [esi+38h],al
  0044a39d  884639                mov       [esi+39h],al
  0044a3a0  e89bcd0f00            call      00547140h
  0044a3a5  99                    cdq
  0044a3a6  b90a000000            mov       ecx,0Ah
  0044a3ab  895e44                mov       [esi+44h],ebx
  0044a3ae  f7f9                  idiv      ecx
  0044a3b0  83c21e                add       edx,1Eh
  0044a3b3  89565c                mov       [esi+5Ch],edx
  0044a3b6  e885cd0f00            call      00547140h
  0044a3bb  99                    cdq
  0044a3bc  33c2                  xor       eax,edx
  0044a3be  2bc2                  sub       eax,edx
  0044a3c0  25ff0f0000            and       eax,0FFFh
  0044a3c5  33c2                  xor       eax,edx
  0044a3c7  2bc2                  sub       eax,edx
  0044a3c9  89464c                mov       [esi+4Ch],eax
  0044a3cc  e86fcd0f00            call      00547140h
  0044a3d1  99                    cdq
  0044a3d2  b9b0040000            mov       ecx,4B0h
  0044a3d7  f7f9                  idiv      ecx
  0044a3d9  81eac8000000          sub       edx,0C8h
  0044a3df  895648                mov       [esi+48h],edx

LAB_0044a3e2:
  0044a3e2  66397e10              cmp       [esi+10h],di
  0044a3e6  0f85b2000000          jne       near 0044A49Eh
  0044a3ec  8b5648                mov       edx,[esi+48h]
  0044a3ef  b864000000            mov       eax,64h
  0044a3f4  66014628              add       [esi+28h],ax
  0044a3f8  6601462a              add       [esi+2Ah],ax
  0044a3fc  52                    push      edx
  0044a3fd  e814b60f00            call      00545A16h
  0044a402  8bf8                  mov       edi,eax
  0044a404  8b464c                mov       eax,[esi+4Ch]
  0044a407  0faf7e44              imul      edi,[esi+44h]
  0044a40b  50                    push      eax
  0044a40c  c1ff0c                sar       edi,0Ch
  0044a40f  e808b60f00            call      00545A1Ch
  0044a414  0faff8                imul      edi,eax
  0044a417  8b4650                mov       eax,[esi+50h]
  0044a41a  8b4e48                mov       ecx,[esi+48h]
  0044a41d  03f8                  add       edi,eax
  0044a41f  51                    push      ecx
  0044a420  897e1c                mov       [esi+1Ch],edi
  0044a423  e8eeb50f00            call      00545A16h
  0044a428  8bf8                  mov       edi,eax
  0044a42a  8b564c                mov       edx,[esi+4Ch]
  0044a42d  0faf7e44              imul      edi,[esi+44h]
  0044a431  52                    push      edx
  0044a432  c1ff0c                sar       edi,0Ch
  0044a435  e8dcb50f00            call      00545A16h
  0044a43a  0faff8                imul      edi,eax
  0044a43d  8b5658                mov       edx,[esi+58h]
  0044a440  8b4648                mov       eax,[esi+48h]
  0044a443  03fa                  add       edi,edx
  0044a445  50                    push      eax
  0044a446  897e24                mov       [esi+24h],edi
  0044a449  8b7e44                mov       edi,[esi+44h]
  0044a44c  e8cbb50f00            call      00545A1Ch
  0044a451  0fafc7                imul      eax,edi
  0044a454  034654                add       eax,[esi+54h]
  0044a457  894620                mov       [esi+20h],eax
  0044a45a  8b465c                mov       eax,[esi+5Ch]
  0044a45d  8bd0                  mov       edx,eax
  0044a45f  8d0c38                lea       ecx,[eax+edi]
  0044a462  c1fa02                sar       edx,2
  0044a465  894e44                mov       [esi+44h],ecx
  0044a468  8a4e40                mov       cl,[esi+40h]
  0044a46b  2bc2                  sub       eax,edx
  0044a46d  66ff4614              inc       word [esi+14h]
  0044a471  fec1                  inc       cl
  0044a473  89465c                mov       [esi+5Ch],eax
  0044a476  8ac1                  mov       al,cl
  0044a478  884e40                mov       [esi+40h],cl
  0044a47b  3c03                  cmp       al,3
  0044a47d  7203                  jb        short 0044A482h
  0044a47f  885e40                mov       [esi+40h],bl

LAB_0044a482:
  0044a482  6aec                  push      0FFFFFFECh
  0044a484  6aec                  push      0FFFFFFECh
  0044a486  6aec                  push      0FFFFFFECh
  0044a488  56                    push      esi
  0044a489  e852a9fbff            call      00404DE0h
  0044a48e  8a4638                mov       al,[esi+38h]
  0044a491  83c410                add       esp,10h
  0044a494  3ac3                  cmp       al,bl
  0044a496  7506                  jne       short 0044A49Eh
  0044a498  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044a49e:
  0044a49e  5f                    pop       edi
  0044a49f  5e                    pop       esi
  0044a4a0  5b                    pop       ebx
  0044a4a1  c3                    ret

3 cible(s) de saut interne : 0x0044a3e2, 0x0044a482, 0x0044a49e
