Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044d8e0  section .text  offset fichier 0x4cce0  208 octets

  0044d8e0  53                    push      ebx
  0044d8e1  56                    push      esi
  0044d8e2  8b74240c              mov       esi,[esp+0Ch]
  0044d8e6  33db                  xor       ebx,ebx
  0044d8e8  66395e10              cmp       [esi+10h],bx
  0044d8ec  756e                  jne       short 0044D95Ch
  0044d8ee  0fbf4632              movsx     eax,word [esi+32h]
  0044d8f2  50                    push      eax
  0044d8f3  c646042c              mov       byte [esi+4],2Ch
  0044d8f7  c6460a09              mov       byte [esi+0Ah],9
  0044d8fb  885e0b                mov       [esi+0Bh],bl
  0044d8fe  66c746100100          mov       word [esi+10h],1
  0044d904  885e38                mov       [esi+38h],bl
  0044d907  885e39                mov       [esi+39h],bl
  0044d90a  885e3a                mov       [esi+3Ah],bl
  0044d90d  e80a810f00            call      00545A1Ch
  0044d912  8b561c                mov       edx,[esi+1Ch]
  0044d915  8d0480                lea       eax,[eax+eax*4]
  0044d918  8d0c80                lea       ecx,[eax+eax*4]
  0044d91b  d1e1                  shl       ecx,1
  0044d91d  03d1                  add       edx,ecx
  0044d91f  89561c                mov       [esi+1Ch],edx
  0044d922  0fbf5632              movsx     edx,word [esi+32h]
  0044d926  52                    push      edx
  0044d927  e8ea800f00            call      00545A16h
  0044d92c  8b4e24                mov       ecx,[esi+24h]
  0044d92f  8d0480                lea       eax,[eax+eax*4]
  0044d932  66895e16              mov       [esi+16h],bx
  0044d936  8d0480                lea       eax,[eax+eax*4]
  0044d939  d1e0                  shl       eax,1
  0044d93b  03c8                  add       ecx,eax
  0044d93d  8b4620                mov       eax,[esi+20h]
  0044d940  0500600400            add       eax,46000h
  0044d945  894e24                mov       [esi+24h],ecx
  0044d948  894620                mov       [esi+20h],eax
  0044d94b  b800100000            mov       eax,1000h
  0044d950  66894628              mov       [esi+28h],ax
  0044d954  6689462a              mov       [esi+2Ah],ax
  0044d958  6689462c              mov       [esi+2Ch],ax

LAB_0044d95c:
  0044d95c  66837e1001            cmp       word [esi+10h],1
  0044d961  754a                  jne       short 0044D9ADh
  0044d963  66ff4616              inc       word [esi+16h]
  0044d967  66837e160a            cmp       word [esi+16h],0Ah
  0044d96c  7e1f                  jle       short 0044D98Dh
  0044d96e  6af6                  push      0FFFFFFF6h
  0044d970  6af6                  push      0FFFFFFF6h
  0044d972  6af6                  push      0FFFFFFF6h
  0044d974  56                    push      esi
  0044d975  e86674fbff            call      00404DE0h
  0044d97a  8a4638                mov       al,[esi+38h]
  0044d97d  83c410                add       esp,10h
  0044d980  3ac3                  cmp       al,bl
  0044d982  7529                  jne       short 0044D9ADh
  0044d984  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0044d98a  5e                    pop       esi
  0044d98b  5b                    pop       ebx
  0044d98c  c3                    ret

LAB_0044d98d:
  0044d98d  6a14                  push      14h
  0044d98f  6a14                  push      14h
  0044d991  b864000000            mov       eax,64h
  0044d996  6a14                  push      14h
  0044d998  66014628              add       [esi+28h],ax
  0044d99c  6601462a              add       [esi+2Ah],ax
  0044d9a0  6601462c              add       [esi+2Ch],ax
  0044d9a4  56                    push      esi
  0044d9a5  e83674fbff            call      00404DE0h
  0044d9aa  83c410                add       esp,10h

LAB_0044d9ad:
  0044d9ad  5e                    pop       esi
  0044d9ae  5b                    pop       ebx
  0044d9af  c3                    ret

3 cible(s) de saut interne : 0x0044d95c, 0x0044d98d, 0x0044d9ad
