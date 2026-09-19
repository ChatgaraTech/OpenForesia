Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043af70  section .text  offset fichier 0x3a370  240 octets

  0043af70  56                    push      esi
  0043af71  8b742408              mov       esi,[esp+8]
  0043af75  66837e1000            cmp       word [esi+10h],0
  0043af7a  0f8593000000          jne       near 0043B013h
  0043af80  c7463ca8105500        mov       dword [esi+3Ch],5510A8h
  0043af87  c6464000              mov       byte [esi+40h],0
  0043af8b  c6460a02              mov       byte [esi+0Ah],2
  0043af8f  c6460b01              mov       byte [esi+0Bh],1
  0043af93  e8a8c11000            call      00547140h
  0043af98  99                    cdq
  0043af99  33c2                  xor       eax,edx
  0043af9b  2bc2                  sub       eax,edx
  0043af9d  25ff030000            and       eax,3FFh
  0043afa2  33c2                  xor       eax,edx
  0043afa4  2bc2                  sub       eax,edx
  0043afa6  0500040000            add       eax,400h
  0043afab  66894628              mov       [esi+28h],ax
  0043afaf  6689462a              mov       [esi+2Ah],ax
  0043afb3  e888c11000            call      00547140h
  0043afb8  99                    cdq
  0043afb9  b932000000            mov       ecx,32h
  0043afbe  f7f9                  idiv      ecx
  0043afc0  8b4e1c                mov       ecx,[esi+1Ch]
  0043afc3  83ea19                sub       edx,19h
  0043afc6  c1e20c                shl       edx,0Ch
  0043afc9  03ca                  add       ecx,edx
  0043afcb  894e1c                mov       [esi+1Ch],ecx
  0043afce  e86dc11000            call      00547140h
  0043afd3  99                    cdq
  0043afd4  b932000000            mov       ecx,32h
  0043afd9  f7f9                  idiv      ecx
  0043afdb  8b4624                mov       eax,[esi+24h]
  0043afde  83ea19                sub       edx,19h
  0043afe1  c1e20c                shl       edx,0Ch
  0043afe4  03c2                  add       eax,edx
  0043afe6  894624                mov       [esi+24h],eax
  0043afe9  e852c11000            call      00547140h
  0043afee  99                    cdq
  0043afef  b91e000000            mov       ecx,1Eh
  0043aff4  66c746100100          mov       word [esi+10h],1
  0043affa  f7f9                  idiv      ecx
  0043affc  8b4620                mov       eax,[esi+20h]
  0043afff  66c746120200          mov       word [esi+12h],2
  0043b005  66c746140200          mov       word [esi+14h],2
  0043b00b  c1e20c                shl       edx,0Ch
  0043b00e  03c2                  add       eax,edx
  0043b010  894620                mov       [esi+20h],eax

LAB_0043b013:
  0043b013  66837e1001            cmp       word [esi+10h],1
  0043b018  7535                  jne       short 0043B04Fh
  0043b01a  66ff4e14              dec       word [esi+14h]
  0043b01e  8b5620                mov       edx,[esi+20h]
  0043b021  668b4614              mov       ax,[esi+14h]
  0043b025  81c200a00000          add       edx,0A000h
  0043b02b  6685c0                test      ax,ax
  0043b02e  895620                mov       [esi+20h],edx
  0043b031  7f1c                  jg        short 0043B04Fh
  0043b033  8a4e40                mov       cl,[esi+40h]
  0043b036  668b5612              mov       dx,[esi+12h]
  0043b03a  fec1                  inc       cl
  0043b03c  66895614              mov       [esi+14h],dx
  0043b040  8ac1                  mov       al,cl
  0043b042  884e40                mov       [esi+40h],cl
  0043b045  3c04                  cmp       al,4
  0043b047  7506                  jne       short 0043B04Fh
  0043b049  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043b04f:
  0043b04f  5e                    pop       esi
  0043b050  c3                    ret
  0043b051  90                    nop
  0043b052  90                    nop
  0043b053  90                    nop
  0043b054  90                    nop
  0043b055  90                    nop
  0043b056  90                    nop
  0043b057  90                    nop
  0043b058  90                    nop
  0043b059  90                    nop
  0043b05a  90                    nop
  0043b05b  90                    nop
  0043b05c  90                    nop
  0043b05d  90                    nop
  0043b05e  90                    nop
  0043b05f  90                    nop

2 cible(s) de saut interne : 0x0043b013, 0x0043b04f
