Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004327a0  section .text  offset fichier 0x31ba0  320 octets

  004327a0  56                    push      esi
  004327a1  8b742408              mov       esi,[esp+8]
  004327a5  66837e1000            cmp       word [esi+10h],0
  004327aa  7573                  jne       short 0043281Fh
  004327ac  8a5616                mov       dl,[esi+16h]
  004327af  b003                  mov       al,3
  004327b1  2ac2                  sub       al,dl
  004327b3  66c746100100          mov       word [esi+10h],1
  004327b9  c7463ce80b5500        mov       dword [esi+3Ch],550BE8h
  004327c0  884640                mov       [esi+40h],al
  004327c3  c6460a02              mov       byte [esi+0Ah],2
  004327c7  c6460b00              mov       byte [esi+0Bh],0
  004327cb  66c746120200          mov       word [esi+12h],2
  004327d1  e86a491100            call      00547140h
  004327d6  99                    cdq
  004327d7  33c2                  xor       eax,edx
  004327d9  8b4e1c                mov       ecx,[esi+1Ch]
  004327dc  2bc2                  sub       eax,edx
  004327de  894e50                mov       [esi+50h],ecx
  004327e1  25ff030000            and       eax,3FFh
  004327e6  66c7869a0000000000    mov       word [esi+9Ah],0
  004327ef  33c2                  xor       eax,edx
  004327f1  2bc2                  sub       eax,edx
  004327f3  8b5620                mov       edx,[esi+20h]
  004327f6  05000c0000            add       eax,0C00h
  004327fb  895654                mov       [esi+54h],edx
  004327fe  66894628              mov       [esi+28h],ax
  00432802  6689462a              mov       [esi+2Ah],ax
  00432806  8b4624                mov       eax,[esi+24h]
  00432809  894658                mov       [esi+58h],eax
  0043280c  e82f491100            call      00547140h
  00432811  99                    cdq
  00432812  b932000000            mov       ecx,32h
  00432817  f7f9                  idiv      ecx
  00432819  83c250                add       edx,50h
  0043281c  89565c                mov       [esi+5Ch],edx

LAB_0043281f:
  0043281f  66837e1001            cmp       word [esi+10h],1
  00432824  755c                  jne       short 00432882h
  00432826  66ff4e12              dec       word [esi+12h]
  0043282a  66837e1200            cmp       word [esi+12h],0
  0043282f  7d51                  jge       short 00432882h
  00432831  66837e1603            cmp       word [esi+16h],3
  00432836  7519                  jne       short 00432851h
  00432838  6a00                  push      0
  0043283a  6a00                  push      0
  0043283c  6800180000            push      1800h
  00432841  6800020000            push      200h
  00432846  6a1e                  push      1Eh
  00432848  56                    push      esi
  00432849  e832580300            call      00468080h
  0043284e  83c418                add       esp,18h

LAB_00432851:
  00432851  66ff4e16              dec       word [esi+16h]
  00432855  66837e1600            cmp       word [esi+16h],0
  0043285a  66c746100200          mov       word [esi+10h],2
  00432860  7c1a                  jl        short 0043287Ch
  00432862  6a00                  push      0
  00432864  6a00                  push      0
  00432866  6aff                  push      0FFFFFFFFh
  00432868  6a00                  push      0
  0043286a  6a32                  push      32h
  0043286c  56                    push      esi
  0043286d  e89e580300            call      00468110h
  00432872  83c418                add       esp,18h
  00432875  66837e1600            cmp       word [esi+16h],0
  0043287a  7d06                  jge       short 00432882h

LAB_0043287c:
  0043287c  66c746160000          mov       word [esi+16h],0

LAB_00432882:
  00432882  66837e1002            cmp       word [esi+10h],2
  00432887  7549                  jne       short 004328D2h
  00432889  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00432890  52                    push      edx
  00432891  e880311100            call      00545A16h
  00432896  0faf465c              imul      eax,[esi+5Ch]
  0043289a  034650                add       eax,[esi+50h]
  0043289d  89461c                mov       [esi+1Ch],eax
  004328a0  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004328a7  50                    push      eax
  004328a8  e86f311100            call      00545A1Ch
  004328ad  0faf465c              imul      eax,[esi+5Ch]
  004328b1  8b5654                mov       edx,[esi+54h]
  004328b4  6683869a00000064      add       word [esi+9Ah],64h
  004328bc  03c2                  add       eax,edx
  004328be  6681be9a0000000008    cmp       word [esi+9Ah],800h
  004328c7  894620                mov       [esi+20h],eax
  004328ca  7e06                  jle       short 004328D2h
  004328cc  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004328d2:
  004328d2  5e                    pop       esi
  004328d3  c3                    ret
  004328d4  90                    nop
  004328d5  90                    nop
  004328d6  90                    nop
  004328d7  90                    nop
  004328d8  90                    nop
  004328d9  90                    nop
  004328da  90                    nop
  004328db  90                    nop
  004328dc  90                    nop
  004328dd  90                    nop
  004328de  90                    nop
  004328df  90                    nop

5 cible(s) de saut interne : 0x0043281f, 0x00432851, 0x0043287c, 0x00432882, 0x004328d2
