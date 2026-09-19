Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043b1d0  section .text  offset fichier 0x3a5d0  320 octets

  0043b1d0  56                    push      esi
  0043b1d1  8b742408              mov       esi,[esp+8]
  0043b1d5  66837e1000            cmp       word [esi+10h],0
  0043b1da  757f                  jne       short 0043B25Bh
  0043b1dc  8a5616                mov       dl,[esi+16h]
  0043b1df  b003                  mov       al,3
  0043b1e1  2ac2                  sub       al,dl
  0043b1e3  66c746100100          mov       word [esi+10h],1
  0043b1e9  c7463c08105500        mov       dword [esi+3Ch],551008h
  0043b1f0  884640                mov       [esi+40h],al
  0043b1f3  c6460a02              mov       byte [esi+0Ah],2
  0043b1f7  c6460b00              mov       byte [esi+0Bh],0
  0043b1fb  66c746120200          mov       word [esi+12h],2
  0043b201  e83abf1000            call      00547140h
  0043b206  99                    cdq
  0043b207  33c2                  xor       eax,edx
  0043b209  8b4e1c                mov       ecx,[esi+1Ch]
  0043b20c  2bc2                  sub       eax,edx
  0043b20e  894e50                mov       [esi+50h],ecx
  0043b211  25ff030000            and       eax,3FFh
  0043b216  66c7869a0000000000    mov       word [esi+9Ah],0
  0043b21f  33c2                  xor       eax,edx
  0043b221  2bc2                  sub       eax,edx
  0043b223  8b5620                mov       edx,[esi+20h]
  0043b226  05000c0000            add       eax,0C00h
  0043b22b  895654                mov       [esi+54h],edx
  0043b22e  66894628              mov       [esi+28h],ax
  0043b232  6689462a              mov       [esi+2Ah],ax
  0043b236  8b4624                mov       eax,[esi+24h]
  0043b239  894658                mov       [esi+58h],eax
  0043b23c  e8ffbe1000            call      00547140h
  0043b241  99                    cdq
  0043b242  b932000000            mov       ecx,32h
  0043b247  c64638fa              mov       byte [esi+38h],0FAh
  0043b24b  f7f9                  idiv      ecx
  0043b24d  b078                  mov       al,78h
  0043b24f  884639                mov       [esi+39h],al
  0043b252  88463a                mov       [esi+3Ah],al
  0043b255  83c250                add       edx,50h
  0043b258  89565c                mov       [esi+5Ch],edx

LAB_0043b25b:
  0043b25b  66837e1001            cmp       word [esi+10h],1
  0043b260  755c                  jne       short 0043B2BEh
  0043b262  66ff4e12              dec       word [esi+12h]
  0043b266  66837e1200            cmp       word [esi+12h],0
  0043b26b  7d51                  jge       short 0043B2BEh
  0043b26d  66837e1603            cmp       word [esi+16h],3
  0043b272  7519                  jne       short 0043B28Dh
  0043b274  6a00                  push      0
  0043b276  6a00                  push      0
  0043b278  6800180000            push      1800h
  0043b27d  6800020000            push      200h
  0043b282  6a1e                  push      1Eh
  0043b284  56                    push      esi
  0043b285  e8f6cd0200            call      00468080h
  0043b28a  83c418                add       esp,18h

LAB_0043b28d:
  0043b28d  66ff4e16              dec       word [esi+16h]
  0043b291  66837e1600            cmp       word [esi+16h],0
  0043b296  66c746100200          mov       word [esi+10h],2
  0043b29c  7c1a                  jl        short 0043B2B8h
  0043b29e  6a00                  push      0
  0043b2a0  6a00                  push      0
  0043b2a2  6aff                  push      0FFFFFFFFh
  0043b2a4  6a00                  push      0
  0043b2a6  6a32                  push      32h
  0043b2a8  56                    push      esi
  0043b2a9  e862ce0200            call      00468110h
  0043b2ae  83c418                add       esp,18h
  0043b2b1  66837e1600            cmp       word [esi+16h],0
  0043b2b6  7d06                  jge       short 0043B2BEh

LAB_0043b2b8:
  0043b2b8  66c746160000          mov       word [esi+16h],0

LAB_0043b2be:
  0043b2be  66837e1002            cmp       word [esi+10h],2
  0043b2c3  7549                  jne       short 0043B30Eh
  0043b2c5  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0043b2cc  52                    push      edx
  0043b2cd  e844a71000            call      00545A16h
  0043b2d2  0faf465c              imul      eax,[esi+5Ch]
  0043b2d6  034650                add       eax,[esi+50h]
  0043b2d9  89461c                mov       [esi+1Ch],eax
  0043b2dc  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0043b2e3  50                    push      eax
  0043b2e4  e833a71000            call      00545A1Ch
  0043b2e9  0faf465c              imul      eax,[esi+5Ch]
  0043b2ed  8b5654                mov       edx,[esi+54h]
  0043b2f0  6683869a00000064      add       word [esi+9Ah],64h
  0043b2f8  03c2                  add       eax,edx
  0043b2fa  6681be9a0000000008    cmp       word [esi+9Ah],800h
  0043b303  894620                mov       [esi+20h],eax
  0043b306  7e06                  jle       short 0043B30Eh
  0043b308  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043b30e:
  0043b30e  5e                    pop       esi
  0043b30f  c3                    ret

5 cible(s) de saut interne : 0x0043b25b, 0x0043b28d, 0x0043b2b8, 0x0043b2be, 0x0043b30e
