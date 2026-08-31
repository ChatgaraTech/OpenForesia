
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d330  section .text  offset fichier 0x7c730  310 octets

  0047d330  51                    push      ecx
  0047d331  53                    push      ebx
  0047d332  55                    push      ebp
  0047d333  56                    push      esi
  0047d334  8b742414              mov       esi,[esp+14h]
  0047d338  57                    push      edi
  0047d339  8b4674                mov       eax,[esi+74h]
  0047d33c  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047d343  50                    push      eax
  0047d344  e877b80b00            call      00538BC0h
  0047d349  8b4e20                mov       ecx,[esi+20h]
  0047d34c  24fe                  and       al,0FEh
  0047d34e  c1e00b                shl       eax,0Bh
  0047d351  83c404                add       esp,4
  0047d354  03c8                  add       ecx,eax
  0047d356  8d461c                lea       eax,[esi+1Ch]
  0047d359  894e20                mov       [esi+20h],ecx
  0047d35c  50                    push      eax
  0047d35d  6800120000            push      1200h
  0047d362  6853060000            push      653h
  0047d367  6a26                  push      26h
  0047d369  e8b2070c00            call      0053DB20h
  0047d36e  668146320004          add       word [esi+32h],400h
  0047d374  83c410                add       esp,10h
  0047d377  0fbf4e32              movsx     ecx,word [esi+32h]
  0047d37b  894c2410              mov       [esp+10h],ecx
  0047d37f  33ed                  xor       ebp,ebp
  0047d381  c744241803000000      mov       dword [esp+18h],3

LAB_0047d389:
  0047d389  e8b29d0c00            call      00547140h
  0047d38e  8b542410              mov       edx,[esp+10h]
  0047d392  25c0010000            and       eax,1C0h
  0047d397  2bc5                  sub       eax,ebp
  0047d399  2d00010000            sub       eax,100h
  0047d39e  8be8                  mov       ebp,eax
  0047d3a0  03d5                  add       edx,ebp
  0047d3a2  66895632              mov       [esi+32h],dx
  0047d3a6  e8959d0c00            call      00547140h
  0047d3ab  8bf8                  mov       edi,eax
  0047d3ad  b8d34d6210            mov       eax,10624DD3h
  0047d3b2  81e7ff000000          and       edi,0FFh
  0047d3b8  81c700010000          add       edi,100h
  0047d3be  8bcf                  mov       ecx,edi
  0047d3c0  c1e10c                shl       ecx,0Ch
  0047d3c3  f7e9                  imul      ecx
  0047d3c5  c1fa04                sar       edx,4
  0047d3c8  8bc2                  mov       eax,edx
  0047d3ca  c1e81f                shr       eax,1Fh
  0047d3cd  03d0                  add       edx,eax
  0047d3cf  66895628              mov       [esi+28h],dx
  0047d3d3  e8689d0c00            call      00547140h
  0047d3d8  25ff0f0000            and       eax,0FFFh
  0047d3dd  0500200000            add       eax,2000h
  0047d3e2  6689462a              mov       [esi+2Ah],ax
  0047d3e6  e8559d0c00            call      00547140h
  0047d3eb  25ff0f0000            and       eax,0FFFh
  0047d3f0  0500280000            add       eax,2800h
  0047d3f5  6689462c              mov       [esi+2Ch],ax
  0047d3f9  e8429d0c00            call      00547140h
  0047d3fe  99                    cdq
  0047d3ff  b903000000            mov       ecx,3
  0047d404  6aff                  push      0FFFFFFFFh
  0047d406  f7f9                  idiv      ecx
  0047d408  80c208                add       dl,8
  0047d40b  885604                mov       [esi+4],dl
  0047d40e  33d2                  xor       edx,edx
  0047d410  8a560b                mov       dl,[esi+0Bh]
  0047d413  52                    push      edx
  0047d414  6a75                  push      75h
  0047d416  56                    push      esi
  0047d417  e8c4d6ffff            call      0047AAE0h
  0047d41c  0fbf5e32              movsx     ebx,word [esi+32h]
  0047d420  83c410                add       esp,10h
  0047d423  81eb00040000          sub       ebx,400h
  0047d429  53                    push      ebx
  0047d42a  e8ed850c00            call      00545A1Ch
  0047d42f  0fafc7                imul      eax,edi
  0047d432  8b4e1c                mov       ecx,[esi+1Ch]
  0047d435  53                    push      ebx
  0047d436  03c8                  add       ecx,eax
  0047d438  894e1c                mov       [esi+1Ch],ecx
  0047d43b  e8d6850c00            call      00545A16h
  0047d440  0fafc7                imul      eax,edi
  0047d443  8b4e24                mov       ecx,[esi+24h]
  0047d446  03c8                  add       ecx,eax
  0047d448  8b442418              mov       eax,[esp+18h]
  0047d44c  48                    dec       eax
  0047d44d  894e24                mov       [esi+24h],ecx
  0047d450  89442418              mov       [esp+18h],eax
  0047d454  0f852fffffff          jne       near 0047D389h
  0047d45a  c70600000000          mov       dword [esi],0
  0047d460  5f                    pop       edi
  0047d461  5e                    pop       esi
  0047d462  5d                    pop       ebp
  0047d463  5b                    pop       ebx
  0047d464  59                    pop       ecx
  0047d465  c3                    ret

1 cible(s) de saut interne : 0x0047d389
