Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043da60  section .text  offset fichier 0x3ce60  208 octets

  0043da60  56                    push      esi
  0043da61  8b742408              mov       esi,[esp+8]
  0043da65  66837e1000            cmp       word [esi+10h],0
  0043da6a  0f858e000000          jne       near 0043DAFEh
  0043da70  b800080000            mov       eax,800h
  0043da75  66c746100100          mov       word [esi+10h],1
  0043da7b  c7463cf0115500        mov       dword [esi+3Ch],5511F0h
  0043da82  c6464000              mov       byte [esi+40h],0
  0043da86  c6460a06              mov       byte [esi+0Ah],6
  0043da8a  c6460b00              mov       byte [esi+0Bh],0
  0043da8e  66894628              mov       [esi+28h],ax
  0043da92  6689462a              mov       [esi+2Ah],ax
  0043da96  e8a5961000            call      00547140h
  0043da9b  99                    cdq
  0043da9c  b996000000            mov       ecx,96h
  0043daa1  f7f9                  idiv      ecx
  0043daa3  8b4e20                mov       ecx,[esi+20h]
  0043daa6  c1e20c                shl       edx,0Ch
  0043daa9  2bca                  sub       ecx,edx
  0043daab  894e20                mov       [esi+20h],ecx
  0043daae  e88d961000            call      00547140h
  0043dab3  99                    cdq
  0043dab4  b914000000            mov       ecx,14h
  0043dab9  f7f9                  idiv      ecx
  0043dabb  8b461c                mov       eax,[esi+1Ch]
  0043dabe  83ea0a                sub       edx,0Ah
  0043dac1  c1e20c                shl       edx,0Ch
  0043dac4  03c2                  add       eax,edx
  0043dac6  89461c                mov       [esi+1Ch],eax
  0043dac9  e872961000            call      00547140h
  0043dace  99                    cdq
  0043dacf  b914000000            mov       ecx,14h
  0043dad4  66c746120000          mov       word [esi+12h],0
  0043dada  f7f9                  idiv      ecx
  0043dadc  8b4e24                mov       ecx,[esi+24h]
  0043dadf  83ea0a                sub       edx,0Ah
  0043dae2  c1e20c                shl       edx,0Ch
  0043dae5  03ca                  add       ecx,edx
  0043dae7  894e24                mov       [esi+24h],ecx
  0043daea  e851961000            call      00547140h
  0043daef  99                    cdq
  0043daf0  b90f000000            mov       ecx,0Fh
  0043daf5  f7f9                  idiv      ecx
  0043daf7  83c214                add       edx,14h
  0043dafa  66895614              mov       [esi+14h],dx

LAB_0043dafe:
  0043dafe  66837e1001            cmp       word [esi+10h],1
  0043db03  7522                  jne       short 0043DB27h
  0043db05  668b4614              mov       ax,[esi+14h]
  0043db09  8b4e20                mov       ecx,[esi+20h]
  0043db0c  0fbfd0                movsx     edx,ax
  0043db0f  c1e20c                shl       edx,0Ch
  0043db12  2bca                  sub       ecx,edx
  0043db14  48                    dec       eax
  0043db15  6685c0                test      ax,ax
  0043db18  894e20                mov       [esi+20h],ecx
  0043db1b  66894614              mov       [esi+14h],ax
  0043db1f  7f06                  jg        short 0043DB27h
  0043db21  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043db27:
  0043db27  5e                    pop       esi
  0043db28  c3                    ret
  0043db29  90                    nop
  0043db2a  90                    nop
  0043db2b  90                    nop
  0043db2c  90                    nop
  0043db2d  90                    nop
  0043db2e  90                    nop
  0043db2f  90                    nop

2 cible(s) de saut interne : 0x0043dafe, 0x0043db27
