Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046e360  section .text  offset fichier 0x6d760  176 octets

  0046e360  56                    push      esi
  0046e361  8b742408              mov       esi,[esp+8]
  0046e365  66837e1000            cmp       word [esi+10h],0
  0046e36a  754e                  jne       short 0046E3BAh
  0046e36c  8b4620                mov       eax,[esi+20h]
  0046e36f  66c746100100          mov       word [esi+10h],1
  0046e375  c1f80c                sar       eax,0Ch
  0046e378  66894612              mov       [esi+12h],ax
  0046e37c  e8bf8d0d00            call      00547140h
  0046e381  99                    cdq
  0046e382  33c2                  xor       eax,edx
  0046e384  6aff                  push      0FFFFFFFFh
  0046e386  2bc2                  sub       eax,edx
  0046e388  6a00                  push      0
  0046e38a  25ff0f0000            and       eax,0FFFh
  0046e38f  68ff000000            push      0FFh
  0046e394  33c2                  xor       eax,edx
  0046e396  56                    push      esi
  0046e397  2bc2                  sub       eax,edx
  0046e399  89465c                mov       [esi+5Ch],eax
  0046e39c  8b4620                mov       eax,[esi+20h]
  0046e39f  0500e00100            add       eax,1E000h
  0046e3a4  894620                mov       [esi+20h],eax
  0046e3a7  e834c70000            call      0047AAE0h
  0046e3ac  8b4620                mov       eax,[esi+20h]
  0046e3af  83c410                add       esp,10h
  0046e3b2  050020feff            add       eax,0FFFE2000h
  0046e3b7  894620                mov       [esi+20h],eax

LAB_0046e3ba:
  0046e3ba  66837e1001            cmp       word [esi+10h],1
  0046e3bf  754a                  jne       short 0046E40Bh
  0046e3c1  6683463264            add       word [esi+32h],64h
  0046e3c6  668b4632              mov       ax,[esi+32h]
  0046e3ca  663d0010              cmp       ax,1000h
  0046e3ce  7c09                  jl        short 0046E3D9h
  0046e3d0  0500f0ffff            add       eax,0FFFFF000h
  0046e3d5  66894632              mov       [esi+32h],ax

LAB_0046e3d9:
  0046e3d9  8b4e5c                mov       ecx,[esi+5Ch]
  0046e3dc  51                    push      ecx
  0046e3dd  e83a760d00            call      00545A1Ch
  0046e3e2  8d1480                lea       edx,[eax+eax*4]
  0046e3e5  0fbf4612              movsx     eax,word [esi+12h]
  0046e3e9  c1e00c                shl       eax,0Ch
  0046e3ec  03d0                  add       edx,eax
  0046e3ee  895620                mov       [esi+20h],edx
  0046e3f1  8b565c                mov       edx,[esi+5Ch]
  0046e3f4  83c264                add       edx,64h
  0046e3f7  8bc2                  mov       eax,edx
  0046e3f9  89565c                mov       [esi+5Ch],edx
  0046e3fc  3d00100000            cmp       eax,1000h
  0046e401  7c08                  jl        short 0046E40Bh
  0046e403  0500f0ffff            add       eax,0FFFFF000h
  0046e408  89465c                mov       [esi+5Ch],eax

LAB_0046e40b:
  0046e40b  5e                    pop       esi
  0046e40c  c3                    ret
  0046e40d  90                    nop
  0046e40e  90                    nop
  0046e40f  90                    nop

3 cible(s) de saut interne : 0x0046e3ba, 0x0046e3d9, 0x0046e40b
