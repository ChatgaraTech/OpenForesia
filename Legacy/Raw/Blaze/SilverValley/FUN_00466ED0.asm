Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466ed0  section .text  offset fichier 0x662d0  128 octets

  00466ed0  56                    push      esi
  00466ed1  8b742408              mov       esi,[esp+8]
  00466ed5  66837e1000            cmp       word [esi+10h],0
  00466eda  7506                  jne       short 00466EE2h
  00466edc  66c746100100          mov       word [esi+10h],1

LAB_00466ee2:
  00466ee2  66837e1001            cmp       word [esi+10h],1
  00466ee7  755b                  jne       short 00466F44h
  00466ee9  0fbf4614              movsx     eax,word [esi+14h]
  00466eed  50                    push      eax
  00466eee  e829eb0d00            call      00545A1Ch
  00466ef3  6683461450            add       word [esi+14h],50h
  00466ef8  c1f806                sar       eax,6
  00466efb  66894634              mov       [esi+34h],ax
  00466eff  668b4614              mov       ax,[esi+14h]
  00466f03  663d0010              cmp       ax,1000h
  00466f07  7c3b                  jl        short 00466F44h
  00466f09  0500f0ffff            add       eax,0FFFFF000h
  00466f0e  66837e1601            cmp       word [esi+16h],1
  00466f13  66894614              mov       [esi+14h],ax
  00466f17  752b                  jne       short 00466F44h
  00466f19  8b0dcc975e00          mov       ecx,[5E97CCh]
  00466f1f  6a00                  push      0
  00466f21  6a00                  push      0
  00466f23  6800100000            push      1000h
  00466f28  8b91a8000000          mov       edx,[ecx+0A8h]
  00466f2e  6800020000            push      200h
  00466f33  8b02                  mov       eax,[edx]
  00466f35  660fb64812            movzx     cx,byte [eax+12h]
  00466f3a  51                    push      ecx
  00466f3b  56                    push      esi
  00466f3c  e83f110000            call      00468080h
  00466f41  83c418                add       esp,18h

LAB_00466f44:
  00466f44  5e                    pop       esi
  00466f45  c3                    ret
  00466f46  90                    nop
  00466f47  90                    nop
  00466f48  90                    nop
  00466f49  90                    nop
  00466f4a  90                    nop
  00466f4b  90                    nop
  00466f4c  90                    nop
  00466f4d  90                    nop
  00466f4e  90                    nop
  00466f4f  90                    nop

2 cible(s) de saut interne : 0x00466ee2, 0x00466f44
