Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046dfc0  section .text  offset fichier 0x6d3c0  176 octets

  0046dfc0  56                    push      esi
  0046dfc1  8b742408              mov       esi,[esp+8]
  0046dfc5  66837e1001            cmp       word [esi+10h],1
  0046dfca  751e                  jne       short 0046DFEAh
  0046dfcc  0fbf4632              movsx     eax,word [esi+32h]
  0046dfd0  50                    push      eax
  0046dfd1  e8467a0d00            call      00545A1Ch
  0046dfd6  8a4e12                mov       cl,[esi+12h]
  0046dfd9  d3e0                  shl       eax,cl
  0046dfdb  b900c0f9ff            mov       ecx,0FFF9C000h
  0046dfe0  2bc8                  sub       ecx,eax
  0046dfe2  8b4620                mov       eax,[esi+20h]
  0046dfe5  03c1                  add       eax,ecx
  0046dfe7  894620                mov       [esi+20h],eax

LAB_0046dfea:
  0046dfea  66837e1000            cmp       word [esi+10h],0
  0046dfef  7514                  jne       short 0046E005h
  0046dff1  668b5614              mov       dx,[esi+14h]
  0046dff5  66c746100100          mov       word [esi+10h],1
  0046dffb  66895612              mov       [esi+12h],dx
  0046dfff  66c746140000          mov       word [esi+14h],0

LAB_0046e005:
  0046e005  803d2ce55d0007        cmp       byte [5DE52Ch],7
  0046e00c  7407                  je        short 0046E015h
  0046e00e  6840a80000            push      0A840h
  0046e013  eb05                  jmp       short 0046E01Ah

LAB_0046e015:
  0046e015  6840880000            push      8840h

LAB_0046e01a:
  0046e01a  56                    push      esi
  0046e01b  e8107bfeff            call      00455B30h
  0046e020  6683463244            add       word [esi+32h],44h
  0046e025  668b4632              mov       ax,[esi+32h]
  0046e029  83c408                add       esp,8
  0046e02c  663d0010              cmp       ax,1000h
  0046e030  7c09                  jl        short 0046E03Bh
  0046e032  0500f0ffff            add       eax,0FFFFF000h
  0046e037  66894632              mov       [esi+32h],ax

LAB_0046e03b:
  0046e03b  0fbf4632              movsx     eax,word [esi+32h]
  0046e03f  50                    push      eax
  0046e040  e8d7790d00            call      00545A1Ch
  0046e045  8a4e12                mov       cl,[esi+12h]
  0046e048  8b5620                mov       edx,[esi+20h]
  0046e04b  d3e0                  shl       eax,cl
  0046e04d  56                    push      esi
  0046e04e  0500400600            add       eax,64000h
  0046e053  03d0                  add       edx,eax
  0046e055  895620                mov       [esi+20h],edx
  0046e058  e8736c0c00            call      00534CD0h
  0046e05d  83c404                add       esp,4
  0046e060  5e                    pop       esi
  0046e061  c3                    ret
  0046e062  90                    nop
  0046e063  90                    nop
  0046e064  90                    nop
  0046e065  90                    nop
  0046e066  90                    nop
  0046e067  90                    nop
  0046e068  90                    nop
  0046e069  90                    nop
  0046e06a  90                    nop
  0046e06b  90                    nop
  0046e06c  90                    nop
  0046e06d  90                    nop
  0046e06e  90                    nop
  0046e06f  90                    nop

5 cible(s) de saut interne : 0x0046dfea, 0x0046e005, 0x0046e015, 0x0046e01a, 0x0046e03b
