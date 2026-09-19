Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466be0  section .text  offset fichier 0x65fe0  80 octets

  00466be0  56                    push      esi
  00466be1  8b742408              mov       esi,[esp+8]
  00466be5  66837e1000            cmp       word [esi+10h],0
  00466bea  7529                  jne       short 00466C15h
  00466bec  0fbf4614              movsx     eax,word [esi+14h]
  00466bf0  c1e008                shl       eax,8
  00466bf3  50                    push      eax
  00466bf4  50                    push      eax
  00466bf5  a1cc975e00            mov       eax,[5E97CCh]
  00466bfa  6800040000            push      400h
  00466bff  8b88a8000000          mov       ecx,[eax+0A8h]
  00466c05  33c0                  xor       eax,eax
  00466c07  8b11                  mov       edx,[ecx]
  00466c09  8a4210                mov       al,[edx+10h]
  00466c0c  50                    push      eax
  00466c0d  e88e620d00            call      0053CEA0h
  00466c12  83c410                add       esp,10h

LAB_00466c15:
  00466c15  66ff4610              inc       word [esi+10h]
  00466c19  66837e1014            cmp       word [esi+10h],14h
  00466c1e  7506                  jne       short 00466C26h
  00466c20  66c746100000          mov       word [esi+10h],0

LAB_00466c26:
  00466c26  5e                    pop       esi
  00466c27  c3                    ret
  00466c28  90                    nop
  00466c29  90                    nop
  00466c2a  90                    nop
  00466c2b  90                    nop
  00466c2c  90                    nop
  00466c2d  90                    nop
  00466c2e  90                    nop
  00466c2f  90                    nop

2 cible(s) de saut interne : 0x00466c15, 0x00466c26
