Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046c260  section .text  offset fichier 0x6b660  144 octets

  0046c260  56                    push      esi
  0046c261  8b742408              mov       esi,[esp+8]
  0046c265  66837e1001            cmp       word [esi+10h],1
  0046c26a  7511                  jne       short 0046C27Dh
  0046c26c  0fbf4632              movsx     eax,word [esi+32h]
  0046c270  50                    push      eax
  0046c271  e8a6970d00            call      00545A1Ch
  0046c276  81462000c0f9ff        add       dword [esi+20h],0FFF9C000h

LAB_0046c27d:
  0046c27d  66837e1000            cmp       word [esi+10h],0
  0046c282  7514                  jne       short 0046C298h
  0046c284  668b4e14              mov       cx,[esi+14h]
  0046c288  66c746100100          mov       word [esi+10h],1
  0046c28e  66894e12              mov       [esi+12h],cx
  0046c292  66c746140000          mov       word [esi+14h],0

LAB_0046c298:
  0046c298  803d2ce55d0007        cmp       byte [5DE52Ch],7
  0046c29f  7407                  je        short 0046C2A8h
  0046c2a1  6842a80000            push      0A842h
  0046c2a6  eb05                  jmp       short 0046C2ADh

LAB_0046c2a8:
  0046c2a8  6842880000            push      8842h

LAB_0046c2ad:
  0046c2ad  56                    push      esi
  0046c2ae  e87d98feff            call      00455B30h
  0046c2b3  6683463244            add       word [esi+32h],44h
  0046c2b8  668b4632              mov       ax,[esi+32h]
  0046c2bc  83c408                add       esp,8
  0046c2bf  663d0010              cmp       ax,1000h
  0046c2c3  7c09                  jl        short 0046C2CEh
  0046c2c5  0500f0ffff            add       eax,0FFFFF000h
  0046c2ca  66894632              mov       [esi+32h],ax

LAB_0046c2ce:
  0046c2ce  0fbf5632              movsx     edx,word [esi+32h]
  0046c2d2  52                    push      edx
  0046c2d3  e844970d00            call      00545A1Ch
  0046c2d8  8b5620                mov       edx,[esi+20h]
  0046c2db  56                    push      esi
  0046c2dc  81c200400600          add       edx,64000h
  0046c2e2  895620                mov       [esi+20h],edx
  0046c2e5  e8e6890c00            call      00534CD0h
  0046c2ea  83c404                add       esp,4
  0046c2ed  5e                    pop       esi
  0046c2ee  c3                    ret
  0046c2ef  90                    nop

5 cible(s) de saut interne : 0x0046c27d, 0x0046c298, 0x0046c2a8, 0x0046c2ad, 0x0046c2ce
