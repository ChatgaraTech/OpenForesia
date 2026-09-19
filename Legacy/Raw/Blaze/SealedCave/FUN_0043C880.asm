Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043c880  section .text  offset fichier 0x3bc80  128 octets

  0043c880  56                    push      esi
  0043c881  8b742408              mov       esi,[esp+8]
  0043c885  66837e1000            cmp       word [esi+10h],0
  0043c88a  7538                  jne       short 0043C8C4h
  0043c88c  c7463c30135500        mov       dword [esi+3Ch],551330h
  0043c893  e8a8a81000            call      00547140h
  0043c898  99                    cdq
  0043c899  b903000000            mov       ecx,3
  0043c89e  c6460a02              mov       byte [esi+0Ah],2
  0043c8a2  f7f9                  idiv      ecx
  0043c8a4  b8a00f0000            mov       eax,0FA0h
  0043c8a9  c6460b00              mov       byte [esi+0Bh],0
  0043c8ad  66894628              mov       [esi+28h],ax
  0043c8b1  6689462a              mov       [esi+2Ah],ax
  0043c8b5  66c746100100          mov       word [esi+10h],1
  0043c8bb  66c746120b00          mov       word [esi+12h],0Bh
  0043c8c1  885640                mov       [esi+40h],dl

LAB_0043c8c4:
  0043c8c4  66837e1001            cmp       word [esi+10h],1
  0043c8c9  7526                  jne       short 0043C8F1h
  0043c8cb  8a5640                mov       dl,[esi+40h]
  0043c8ce  66ff4e12              dec       word [esi+12h]
  0043c8d2  668b4612              mov       ax,[esi+12h]
  0043c8d6  fec2                  inc       dl
  0043c8d8  8aca                  mov       cl,dl
  0043c8da  885640                mov       [esi+40h],dl
  0043c8dd  80f903                cmp       cl,3
  0043c8e0  7204                  jb        short 0043C8E6h
  0043c8e2  c6464000              mov       byte [esi+40h],0

LAB_0043c8e6:
  0043c8e6  6685c0                test      ax,ax
  0043c8e9  7f06                  jg        short 0043C8F1h
  0043c8eb  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043c8f1:
  0043c8f1  5e                    pop       esi
  0043c8f2  c3                    ret
  0043c8f3  90                    nop
  0043c8f4  90                    nop
  0043c8f5  90                    nop
  0043c8f6  90                    nop
  0043c8f7  90                    nop
  0043c8f8  90                    nop
  0043c8f9  90                    nop
  0043c8fa  90                    nop
  0043c8fb  90                    nop
  0043c8fc  90                    nop
  0043c8fd  90                    nop
  0043c8fe  90                    nop
  0043c8ff  90                    nop

3 cible(s) de saut interne : 0x0043c8c4, 0x0043c8e6, 0x0043c8f1
