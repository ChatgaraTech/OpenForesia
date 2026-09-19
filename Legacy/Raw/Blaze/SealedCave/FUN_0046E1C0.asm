Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e1c0  section .text  offset fichier 0x6d5c0  240 octets

  0046e1c0  56                    push      esi
  0046e1c1  8b742408              mov       esi,[esp+8]
  0046e1c5  66837e1001            cmp       word [esi+10h],1
  0046e1ca  7518                  jne       short 0046E1E4h
  0046e1cc  6aff                  push      0FFFFFFFFh
  0046e1ce  6a00                  push      0
  0046e1d0  68fe000000            push      0FEh
  0046e1d5  56                    push      esi
  0046e1d6  e805c90000            call      0047AAE0h
  0046e1db  83c410                add       esp,10h
  0046e1de  66c746100200          mov       word [esi+10h],2

LAB_0046e1e4:
  0046e1e4  66837e1000            cmp       word [esi+10h],0
  0046e1e9  7526                  jne       short 0046E211h
  0046e1eb  8b4620                mov       eax,[esi+20h]
  0046e1ee  66c746100100          mov       word [esi+10h],1
  0046e1f4  c1f80c                sar       eax,0Ch
  0046e1f7  66894612              mov       [esi+12h],ax
  0046e1fb  e8408f0d00            call      00547140h
  0046e200  99                    cdq
  0046e201  33c2                  xor       eax,edx
  0046e203  2bc2                  sub       eax,edx
  0046e205  25ff0f0000            and       eax,0FFFh
  0046e20a  33c2                  xor       eax,edx
  0046e20c  2bc2                  sub       eax,edx
  0046e20e  89465c                mov       [esi+5Ch],eax

LAB_0046e211:
  0046e211  66837e1002            cmp       word [esi+10h],2
  0046e216  0f8589000000          jne       near 0046E2A5h
  0046e21c  6683463264            add       word [esi+32h],64h
  0046e221  668b4632              mov       ax,[esi+32h]
  0046e225  663d0010              cmp       ax,1000h
  0046e229  7c09                  jl        short 0046E234h
  0046e22b  0500f0ffff            add       eax,0FFFFF000h
  0046e230  66894632              mov       [esi+32h],ax

LAB_0046e234:
  0046e234  8b4e5c                mov       ecx,[esi+5Ch]
  0046e237  51                    push      ecx
  0046e238  e8df770d00            call      00545A1Ch
  0046e23d  8d1480                lea       edx,[eax+eax*4]
  0046e240  0fbf4612              movsx     eax,word [esi+12h]
  0046e244  c1e00c                shl       eax,0Ch
  0046e247  03d0                  add       edx,eax
  0046e249  895620                mov       [esi+20h],edx
  0046e24c  8b565c                mov       edx,[esi+5Ch]
  0046e24f  83c264                add       edx,64h
  0046e252  8bc2                  mov       eax,edx
  0046e254  89565c                mov       [esi+5Ch],edx
  0046e257  3d00100000            cmp       eax,1000h
  0046e25c  7c08                  jl        short 0046E266h
  0046e25e  0500f0ffff            add       eax,0FFFFF000h
  0046e263  89465c                mov       [esi+5Ch],eax

LAB_0046e266:
  0046e266  0fbf4e14              movsx     ecx,word [esi+14h]
  0046e26a  8bc1                  mov       eax,ecx
  0046e26c  99                    cdq
  0046e26d  83e207                and       edx,7
  0046e270  03c2                  add       eax,edx
  0046e272  c1f803                sar       eax,3
  0046e275  0fbfc0                movsx     eax,ax
  0046e278  8d14c500000000        lea       edx,[eax*8]
  0046e27f  8a80e0965e00          mov       al,[eax+5E96E0h]
  0046e285  2bca                  sub       ecx,edx
  0046e287  ba01000000            mov       edx,1
  0046e28c  d3e2                  shl       edx,cl
  0046e28e  84d0                  test      al,dl
  0046e290  7513                  jne       short 0046E2A5h
  0046e292  6a00                  push      0
  0046e294  6a00                  push      0
  0046e296  6aff                  push      0FFFFFFFFh
  0046e298  6a00                  push      0
  0046e29a  6a3b                  push      3Bh
  0046e29c  56                    push      esi
  0046e29d  e86e9effff            call      00468110h
  0046e2a2  83c418                add       esp,18h

LAB_0046e2a5:
  0046e2a5  5e                    pop       esi
  0046e2a6  c3                    ret
  0046e2a7  90                    nop
  0046e2a8  90                    nop
  0046e2a9  90                    nop
  0046e2aa  90                    nop
  0046e2ab  90                    nop
  0046e2ac  90                    nop
  0046e2ad  90                    nop
  0046e2ae  90                    nop
  0046e2af  90                    nop

5 cible(s) de saut interne : 0x0046e1e4, 0x0046e211, 0x0046e234, 0x0046e266, 0x0046e2a5
