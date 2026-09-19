Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00432190  section .text  offset fichier 0x31590  176 octets

  00432190  56                    push      esi
  00432191  8b742408              mov       esi,[esp+8]
  00432195  8b466c                mov       eax,[esi+6Ch]
  00432198  85c0                  test      eax,eax
  0043219a  7505                  jne       short 004321A1h
  0043219c  b858e55d00            mov       eax,5DE558h

LAB_004321a1:
  004321a1  8b481c                mov       ecx,[eax+1Ch]
  004321a4  66837e1000            cmp       word [esi+10h],0
  004321a9  894e1c                mov       [esi+1Ch],ecx
  004321ac  8b5020                mov       edx,[eax+20h]
  004321af  895620                mov       [esi+20h],edx
  004321b2  8b4024                mov       eax,[eax+24h]
  004321b5  894624                mov       [esi+24h],eax
  004321b8  b801000000            mov       eax,1
  004321bd  751d                  jne       short 004321DCh
  004321bf  66894610              mov       [esi+10h],ax
  004321c3  c7463c680c5500        mov       dword [esi+3Ch],550C68h
  004321ca  c6464000              mov       byte [esi+40h],0
  004321ce  c6460a09              mov       byte [esi+0Ah],9
  004321d2  c6460b00              mov       byte [esi+0Bh],0
  004321d6  66c746120a00          mov       word [esi+12h],0Ah

LAB_004321dc:
  004321dc  66394610              cmp       [esi+10h],ax
  004321e0  7559                  jne       short 0043223Bh
  004321e2  66ff4e12              dec       word [esi+12h]
  004321e6  e8554f1100            call      00547140h
  004321eb  99                    cdq
  004321ec  33c2                  xor       eax,edx
  004321ee  2bc2                  sub       eax,edx
  004321f0  25ff070000            and       eax,7FFh
  004321f5  33c2                  xor       eax,edx
  004321f7  2bc2                  sub       eax,edx
  004321f9  0500100000            add       eax,1000h
  004321fe  66894628              mov       [esi+28h],ax
  00432202  e8394f1100            call      00547140h
  00432207  99                    cdq
  00432208  33c2                  xor       eax,edx
  0043220a  2bc2                  sub       eax,edx
  0043220c  25ff070000            and       eax,7FFh
  00432211  33c2                  xor       eax,edx
  00432213  2bc2                  sub       eax,edx
  00432215  0500100000            add       eax,1000h
  0043221a  6689462c              mov       [esi+2Ch],ax
  0043221e  e81d4f1100            call      00547140h
  00432223  99                    cdq
  00432224  b903000000            mov       ecx,3
  00432229  f7f9                  idiv      ecx
  0043222b  66837e1200            cmp       word [esi+12h],0
  00432230  885640                mov       [esi+40h],dl
  00432233  7f06                  jg        short 0043223Bh
  00432235  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043223b:
  0043223b  5e                    pop       esi
  0043223c  c3                    ret
  0043223d  90                    nop
  0043223e  90                    nop
  0043223f  90                    nop

3 cible(s) de saut interne : 0x004321a1, 0x004321dc, 0x0043223b
