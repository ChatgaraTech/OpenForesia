Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004333b0  section .text  offset fichier 0x327b0  144 octets

  004333b0  56                    push      esi
  004333b1  8b742408              mov       esi,[esp+8]
  004333b5  66837e1000            cmp       word [esi+10h],0
  004333ba  7520                  jne       short 004333DCh
  004333bc  e87f3d1100            call      00547140h
  004333c1  99                    cdq
  004333c2  b90a000000            mov       ecx,0Ah
  004333c7  66c746140000          mov       word [esi+14h],0
  004333cd  f7f9                  idiv      ecx
  004333cf  66c746100100          mov       word [esi+10h],1
  004333d5  83c205                add       edx,5
  004333d8  66895612              mov       [esi+12h],dx

LAB_004333dc:
  004333dc  66ff4e12              dec       word [esi+12h]
  004333e0  66837e1200            cmp       word [esi+12h],0
  004333e5  7d57                  jge       short 0043343Eh
  004333e7  e8543d1100            call      00547140h
  004333ec  99                    cdq
  004333ed  b90a000000            mov       ecx,0Ah
  004333f2  f7f9                  idiv      ecx
  004333f4  83c205                add       edx,5
  004333f7  66ff4614              inc       word [esi+14h]
  004333fb  66837e1402            cmp       word [esi+14h],2
  00433400  66895612              mov       [esi+12h],dx
  00433404  7c06                  jl        short 0043340Ch
  00433406  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043340c:
  0043340c  6683be9a00000000      cmp       word [esi+9Ah],0
  00433414  6aff                  push      0FFFFFFFFh
  00433416  6a00                  push      0
  00433418  7516                  jne       short 00433430h
  0043341a  68fd000000            push      0FDh
  0043341f  8d561c                lea       edx,[esi+1Ch]
  00433422  6a00                  push      0
  00433424  52                    push      edx
  00433425  56                    push      esi
  00433426  e815780400            call      0047AC40h
  0043342b  83c418                add       esp,18h
  0043342e  5e                    pop       esi
  0043342f  c3                    ret

LAB_00433430:
  00433430  68ff000000            push      0FFh
  00433435  56                    push      esi
  00433436  e8a5760400            call      0047AAE0h
  0043343b  83c410                add       esp,10h

LAB_0043343e:
  0043343e  5e                    pop       esi
  0043343f  c3                    ret

4 cible(s) de saut interne : 0x004333dc, 0x0043340c, 0x00433430, 0x0043343e
