Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e100  section .text  offset fichier 0x6d500  192 octets

  0046e100  56                    push      esi
  0046e101  8b742408              mov       esi,[esp+8]
  0046e105  66837e1001            cmp       word [esi+10h],1
  0046e10a  752e                  jne       short 0046E13Ah
  0046e10c  8b4620                mov       eax,[esi+20h]
  0046e10f  6aff                  push      0FFFFFFFFh
  0046e111  6a00                  push      0
  0046e113  0500e00100            add       eax,1E000h
  0046e118  68ff000000            push      0FFh
  0046e11d  56                    push      esi
  0046e11e  894620                mov       [esi+20h],eax
  0046e121  e8bac90000            call      0047AAE0h
  0046e126  8b4620                mov       eax,[esi+20h]
  0046e129  83c410                add       esp,10h
  0046e12c  050020feff            add       eax,0FFFE2000h
  0046e131  66c746100200          mov       word [esi+10h],2
  0046e137  894620                mov       [esi+20h],eax

LAB_0046e13a:
  0046e13a  66837e1000            cmp       word [esi+10h],0
  0046e13f  7526                  jne       short 0046E167h
  0046e141  8b4620                mov       eax,[esi+20h]
  0046e144  66c746100100          mov       word [esi+10h],1
  0046e14a  c1f80c                sar       eax,0Ch
  0046e14d  66894612              mov       [esi+12h],ax
  0046e151  e8ea8f0d00            call      00547140h
  0046e156  99                    cdq
  0046e157  33c2                  xor       eax,edx
  0046e159  2bc2                  sub       eax,edx
  0046e15b  25ff0f0000            and       eax,0FFFh
  0046e160  33c2                  xor       eax,edx
  0046e162  2bc2                  sub       eax,edx
  0046e164  89465c                mov       [esi+5Ch],eax

LAB_0046e167:
  0046e167  66837e1002            cmp       word [esi+10h],2
  0046e16c  754a                  jne       short 0046E1B8h
  0046e16e  6683463264            add       word [esi+32h],64h
  0046e173  668b4632              mov       ax,[esi+32h]
  0046e177  663d0010              cmp       ax,1000h
  0046e17b  7c09                  jl        short 0046E186h
  0046e17d  0500f0ffff            add       eax,0FFFFF000h
  0046e182  66894632              mov       [esi+32h],ax

LAB_0046e186:
  0046e186  8b4e5c                mov       ecx,[esi+5Ch]
  0046e189  51                    push      ecx
  0046e18a  e88d780d00            call      00545A1Ch
  0046e18f  8d1480                lea       edx,[eax+eax*4]
  0046e192  0fbf4612              movsx     eax,word [esi+12h]
  0046e196  c1e00c                shl       eax,0Ch
  0046e199  03d0                  add       edx,eax
  0046e19b  895620                mov       [esi+20h],edx
  0046e19e  8b565c                mov       edx,[esi+5Ch]
  0046e1a1  83c264                add       edx,64h
  0046e1a4  8bc2                  mov       eax,edx
  0046e1a6  89565c                mov       [esi+5Ch],edx
  0046e1a9  3d00100000            cmp       eax,1000h
  0046e1ae  7c08                  jl        short 0046E1B8h
  0046e1b0  0500f0ffff            add       eax,0FFFFF000h
  0046e1b5  89465c                mov       [esi+5Ch],eax

LAB_0046e1b8:
  0046e1b8  5e                    pop       esi
  0046e1b9  c3                    ret
  0046e1ba  90                    nop
  0046e1bb  90                    nop
  0046e1bc  90                    nop
  0046e1bd  90                    nop
  0046e1be  90                    nop
  0046e1bf  90                    nop

4 cible(s) de saut interne : 0x0046e13a, 0x0046e167, 0x0046e186, 0x0046e1b8
