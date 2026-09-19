Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004844a0  section .text  offset fichier 0x838a0  192 octets

  004844a0  53                    push      ebx
  004844a1  55                    push      ebp
  004844a2  56                    push      esi
  004844a3  8b742410              mov       esi,[esp+10h]
  004844a7  b800100000            mov       eax,1000h
  004844ac  57                    push      edi
  004844ad  8b6e6c                mov       ebp,[esi+6Ch]
  004844b0  894628                mov       [esi+28h],eax
  004844b3  6689462c              mov       [esi+2Ch],ax
  004844b7  33c0                  xor       eax,eax
  004844b9  6681463280fe          add       word [esi+32h],0FE80h
  004844bf  896e74                mov       [esi+74h],ebp
  004844c2  66c746360400          mov       word [esi+36h],4
  004844c8  894638                mov       [esi+38h],eax
  004844cb  c646041e              mov       byte [esi+4],1Eh
  004844cf  66894612              mov       [esi+12h],ax
  004844d3  c744241407000000      mov       dword [esp+14h],7

LAB_004844db:
  004844db  8b5c2414              mov       ebx,[esp+14h]
  004844df  0fbf7e32              movsx     edi,word [esi+32h]
  004844e3  80e301                and       bl,1
  004844e6  57                    push      edi
  004844e7  f6db                  neg       bl
  004844e9  1bdb                  sbb       ebx,ebx
  004844eb  83e3ce                and       ebx,0FFFFFFCEh
  004844ee  83c364                add       ebx,64h
  004844f1  e826150c00            call      00545A1Ch
  004844f6  0fafc3                imul      eax,ebx
  004844f9  8b4d1c                mov       ecx,[ebp+1Ch]
  004844fc  57                    push      edi
  004844fd  03c1                  add       eax,ecx
  004844ff  89461c                mov       [esi+1Ch],eax
  00484502  e80f150c00            call      00545A16h
  00484507  0fafc3                imul      eax,ebx
  0048450a  8b4d24                mov       ecx,[ebp+24h]
  0048450d  6a01                  push      1
  0048450f  03c1                  add       eax,ecx
  00484511  6a00                  push      0
  00484513  894624                mov       [esi+24h],eax
  00484516  a1cc975e00            mov       eax,[5E97CCh]
  0048451b  8b88d0000000          mov       ecx,[eax+0D0h]
  00484521  33c0                  xor       eax,eax
  00484523  8b11                  mov       edx,[ecx]
  00484525  8a4209                mov       al,[edx+9]
  00484528  05c0000000            add       eax,0C0h
  0048452d  50                    push      eax
  0048452e  56                    push      esi
  0048452f  e8ac65ffff            call      0047AAE0h
  00484534  8b442424              mov       eax,[esp+24h]
  00484538  668146120007          add       word [esi+12h],700h
  0048453e  83c410                add       esp,10h
  00484541  81c780000000          add       edi,80h
  00484547  48                    dec       eax
  00484548  66897e32              mov       [esi+32h],di
  0048454c  89442414              mov       [esp+14h],eax
  00484550  7589                  jne       short 004844DBh
  00484552  c70600000000          mov       dword [esi],0
  00484558  5f                    pop       edi
  00484559  5e                    pop       esi
  0048455a  5d                    pop       ebp
  0048455b  5b                    pop       ebx
  0048455c  c3                    ret
  0048455d  90                    nop
  0048455e  90                    nop
  0048455f  90                    nop

1 cible(s) de saut interne : 0x004844db
