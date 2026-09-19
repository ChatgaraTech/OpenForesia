Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004700c0  section .text  offset fichier 0x6f4c0  112 octets

  004700c0  56                    push      esi
  004700c1  8b742408              mov       esi,[esp+8]
  004700c5  66837e1000            cmp       word [esi+10h],0
  004700ca  7514                  jne       short 004700E0h
  004700cc  668b4614              mov       ax,[esi+14h]
  004700d0  66c746100100          mov       word [esi+10h],1
  004700d6  66894612              mov       [esi+12h],ax
  004700da  66c746140000          mov       word [esi+14h],0

LAB_004700e0:
  004700e0  6683463244            add       word [esi+32h],44h
  004700e5  668b4632              mov       ax,[esi+32h]
  004700e9  663d0010              cmp       ax,1000h
  004700ed  7e09                  jle       short 004700F8h
  004700ef  0500f0ffff            add       eax,0FFFFF000h
  004700f4  66894632              mov       [esi+32h],ax

LAB_004700f8:
  004700f8  0fbf4e32              movsx     ecx,word [esi+32h]
  004700fc  51                    push      ecx
  004700fd  e81a590d00            call      00545A1Ch
  00470102  8a4e12                mov       cl,[esi+12h]
  00470105  6a00                  push      0
  00470107  d3e0                  shl       eax,cl
  00470109  8b4e20                mov       ecx,[esi+20h]
  0047010c  6a00                  push      0
  0047010e  6aff                  push      0FFFFFFFFh
  00470110  6a00                  push      0
  00470112  6a3c                  push      3Ch
  00470114  56                    push      esi
  00470115  03c8                  add       ecx,eax
  00470117  894e20                mov       [esi+20h],ecx
  0047011a  e8f17fffff            call      00468110h
  0047011f  83c418                add       esp,18h
  00470122  5e                    pop       esi
  00470123  c3                    ret
  00470124  90                    nop
  00470125  90                    nop
  00470126  90                    nop
  00470127  90                    nop
  00470128  90                    nop
  00470129  90                    nop
  0047012a  90                    nop
  0047012b  90                    nop
  0047012c  90                    nop
  0047012d  90                    nop
  0047012e  90                    nop
  0047012f  90                    nop

2 cible(s) de saut interne : 0x004700e0, 0x004700f8
