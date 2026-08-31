Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449f90  section .text  offset fichier 0x49390  176 octets

  00449f90  56                    push      esi
  00449f91  8b742408              mov       esi,[esp+8]
  00449f95  6a00                  push      0
  00449f97  6a00                  push      0
  00449f99  56                    push      esi
  00449f9a  e8a1e0ffff            call      00448040h
  00449f9f  83c40c                add       esp,0Ch
  00449fa2  66837e1000            cmp       word [esi+10h],0
  00449fa7  750d                  jne       short 00449FB6h
  00449fa9  6a00                  push      0
  00449fab  6a00                  push      0
  00449fad  56                    push      esi
  00449fae  e8fddcffff            call      00447CB0h
  00449fb3  83c40c                add       esp,0Ch

LAB_00449fb6:
  00449fb6  8b4678                mov       eax,[esi+78h]
  00449fb9  85c0                  test      eax,eax
  00449fbb  7505                  jne       short 00449FC2h
  00449fbd  b858e55d00            mov       eax,5DE558h

LAB_00449fc2:
  00449fc2  8b481c                mov       ecx,[eax+1Ch]
  00449fc5  66837e1000            cmp       word [esi+10h],0
  00449fca  894e1c                mov       [esi+1Ch],ecx
  00449fcd  8b5020                mov       edx,[eax+20h]
  00449fd0  895620                mov       [esi+20h],edx
  00449fd3  8b4024                mov       eax,[eax+24h]
  00449fd6  894624                mov       [esi+24h],eax
  00449fd9  751e                  jne       short 00449FF9h
  00449fdb  6aff                  push      0FFFFFFFFh
  00449fdd  6a00                  push      0
  00449fdf  68a5000000            push      0A5h
  00449fe4  56                    push      esi
  00449fe5  66c746100100          mov       word [esi+10h],1
  00449feb  66c746160000          mov       word [esi+16h],0
  00449ff1  e8ea0a0300            call      0047AAE0h
  00449ff6  83c410                add       esp,10h

LAB_00449ff9:
  00449ff9  66ff4616              inc       word [esi+16h]
  00449ffd  66837e161e            cmp       word [esi+16h],1Eh
  0044a002  7c31                  jl        short 0044A035h
  0044a004  8b4e74                mov       ecx,[esi+74h]
  0044a007  8a5606                mov       dl,[esi+6]
  0044a00a  6a00                  push      0
  0044a00c  885106                mov       [ecx+6],dl
  0044a00f  8b4674                mov       eax,[esi+74h]
  0044a012  8a4e07                mov       cl,[esi+7]
  0044a015  884807                mov       [eax+7],cl
  0044a018  8b5678                mov       edx,[esi+78h]
  0044a01b  8b4674                mov       eax,[esi+74h]
  0044a01e  52                    push      edx
  0044a01f  50                    push      eax
  0044a020  e81b000000            call      0044A040h
  0044a025  83c40c                add       esp,0Ch
  0044a028  6a00                  push      0
  0044a02a  6a00                  push      0
  0044a02c  56                    push      esi
  0044a02d  e8eedfffff            call      00448020h
  0044a032  83c40c                add       esp,0Ch

LAB_0044a035:
  0044a035  5e                    pop       esi
  0044a036  c3                    ret
  0044a037  90                    nop
  0044a038  90                    nop
  0044a039  90                    nop
  0044a03a  90                    nop
  0044a03b  90                    nop
  0044a03c  90                    nop
  0044a03d  90                    nop
  0044a03e  90                    nop
  0044a03f  90                    nop

4 cible(s) de saut interne : 0x00449fb6, 0x00449fc2, 0x00449ff9, 0x0044a035
