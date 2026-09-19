Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00433cd0  section .text  offset fichier 0x330d0  320 octets

  00433cd0  56                    push      esi
  00433cd1  8b742408              mov       esi,[esp+8]
  00433cd5  66837e1000            cmp       word [esi+10h],0
  00433cda  7573                  jne       short 00433D4Fh
  00433cdc  8a5616                mov       dl,[esi+16h]
  00433cdf  b003                  mov       al,3
  00433ce1  2ac2                  sub       al,dl
  00433ce3  66c746100100          mov       word [esi+10h],1
  00433ce9  c7463cf80c5500        mov       dword [esi+3Ch],550CF8h
  00433cf0  884640                mov       [esi+40h],al
  00433cf3  c6460a02              mov       byte [esi+0Ah],2
  00433cf7  c6460b00              mov       byte [esi+0Bh],0
  00433cfb  66c746120200          mov       word [esi+12h],2
  00433d01  e83a341100            call      00547140h
  00433d06  99                    cdq
  00433d07  33c2                  xor       eax,edx
  00433d09  8b4e1c                mov       ecx,[esi+1Ch]
  00433d0c  2bc2                  sub       eax,edx
  00433d0e  894e50                mov       [esi+50h],ecx
  00433d11  25ff030000            and       eax,3FFh
  00433d16  66c7869a0000000000    mov       word [esi+9Ah],0
  00433d1f  33c2                  xor       eax,edx
  00433d21  2bc2                  sub       eax,edx
  00433d23  8b5620                mov       edx,[esi+20h]
  00433d26  05000c0000            add       eax,0C00h
  00433d2b  895654                mov       [esi+54h],edx
  00433d2e  66894628              mov       [esi+28h],ax
  00433d32  6689462a              mov       [esi+2Ah],ax
  00433d36  8b4624                mov       eax,[esi+24h]
  00433d39  894658                mov       [esi+58h],eax
  00433d3c  e8ff331100            call      00547140h
  00433d41  99                    cdq
  00433d42  b932000000            mov       ecx,32h
  00433d47  f7f9                  idiv      ecx
  00433d49  83c250                add       edx,50h
  00433d4c  89565c                mov       [esi+5Ch],edx

LAB_00433d4f:
  00433d4f  66837e1001            cmp       word [esi+10h],1
  00433d54  755c                  jne       short 00433DB2h
  00433d56  66ff4e12              dec       word [esi+12h]
  00433d5a  66837e1200            cmp       word [esi+12h],0
  00433d5f  7d51                  jge       short 00433DB2h
  00433d61  66837e1603            cmp       word [esi+16h],3
  00433d66  7519                  jne       short 00433D81h
  00433d68  6a00                  push      0
  00433d6a  6a00                  push      0
  00433d6c  6800180000            push      1800h
  00433d71  6800020000            push      200h
  00433d76  6a1e                  push      1Eh
  00433d78  56                    push      esi
  00433d79  e802430300            call      00468080h
  00433d7e  83c418                add       esp,18h

LAB_00433d81:
  00433d81  66ff4e16              dec       word [esi+16h]
  00433d85  66837e1600            cmp       word [esi+16h],0
  00433d8a  66c746100200          mov       word [esi+10h],2
  00433d90  7c1a                  jl        short 00433DACh
  00433d92  6a00                  push      0
  00433d94  6a00                  push      0
  00433d96  6aff                  push      0FFFFFFFFh
  00433d98  6a00                  push      0
  00433d9a  6a32                  push      32h
  00433d9c  56                    push      esi
  00433d9d  e86e430300            call      00468110h
  00433da2  83c418                add       esp,18h
  00433da5  66837e1600            cmp       word [esi+16h],0
  00433daa  7d06                  jge       short 00433DB2h

LAB_00433dac:
  00433dac  66c746160000          mov       word [esi+16h],0

LAB_00433db2:
  00433db2  66837e1002            cmp       word [esi+10h],2
  00433db7  7549                  jne       short 00433E02h
  00433db9  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00433dc0  52                    push      edx
  00433dc1  e8501c1100            call      00545A16h
  00433dc6  0faf465c              imul      eax,[esi+5Ch]
  00433dca  034650                add       eax,[esi+50h]
  00433dcd  89461c                mov       [esi+1Ch],eax
  00433dd0  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00433dd7  50                    push      eax
  00433dd8  e83f1c1100            call      00545A1Ch
  00433ddd  0faf465c              imul      eax,[esi+5Ch]
  00433de1  8b5654                mov       edx,[esi+54h]
  00433de4  6683869a00000064      add       word [esi+9Ah],64h
  00433dec  03c2                  add       eax,edx
  00433dee  6681be9a0000000008    cmp       word [esi+9Ah],800h
  00433df7  894620                mov       [esi+20h],eax
  00433dfa  7e06                  jle       short 00433E02h
  00433dfc  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00433e02:
  00433e02  5e                    pop       esi
  00433e03  c3                    ret
  00433e04  90                    nop
  00433e05  90                    nop
  00433e06  90                    nop
  00433e07  90                    nop
  00433e08  90                    nop
  00433e09  90                    nop
  00433e0a  90                    nop
  00433e0b  90                    nop
  00433e0c  90                    nop
  00433e0d  90                    nop
  00433e0e  90                    nop
  00433e0f  90                    nop

5 cible(s) de saut interne : 0x00433d4f, 0x00433d81, 0x00433dac, 0x00433db2, 0x00433e02
