Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470130  section .text  offset fichier 0x6f530  96 octets

  00470130  56                    push      esi
  00470131  8b742408              mov       esi,[esp+8]
  00470135  66837e1000            cmp       word [esi+10h],0
  0047013a  7514                  jne       short 00470150h
  0047013c  668b4614              mov       ax,[esi+14h]
  00470140  66c746100100          mov       word [esi+10h],1
  00470146  66894612              mov       [esi+12h],ax
  0047014a  66c746140000          mov       word [esi+14h],0

LAB_00470150:
  00470150  6683463244            add       word [esi+32h],44h
  00470155  668b4632              mov       ax,[esi+32h]
  00470159  663d0010              cmp       ax,1000h
  0047015d  7e09                  jle       short 00470168h
  0047015f  0500f0ffff            add       eax,0FFFFF000h
  00470164  66894632              mov       [esi+32h],ax

LAB_00470168:
  00470168  0fbf4e32              movsx     ecx,word [esi+32h]
  0047016c  51                    push      ecx
  0047016d  e8aa580d00            call      00545A1Ch
  00470172  8a4e12                mov       cl,[esi+12h]
  00470175  d3e0                  shl       eax,cl
  00470177  8b4e20                mov       ecx,[esi+20h]
  0047017a  03c8                  add       ecx,eax
  0047017c  894e20                mov       [esi+20h],ecx
  0047017f  5e                    pop       esi
  00470180  c3                    ret
  00470181  90                    nop
  00470182  90                    nop
  00470183  90                    nop
  00470184  90                    nop
  00470185  90                    nop
  00470186  90                    nop
  00470187  90                    nop
  00470188  90                    nop
  00470189  90                    nop
  0047018a  90                    nop
  0047018b  90                    nop
  0047018c  90                    nop
  0047018d  90                    nop
  0047018e  90                    nop
  0047018f  90                    nop

2 cible(s) de saut interne : 0x00470150, 0x00470168
