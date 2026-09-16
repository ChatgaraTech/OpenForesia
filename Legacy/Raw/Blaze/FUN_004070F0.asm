
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004070f0  section .text  offset fichier 0x64f0  240 octets

  004070f0  53                    push      ebx
  004070f1  56                    push      esi
  004070f2  8b74240c              mov       esi,[esp+0Ch]
  004070f6  33db                  xor       ebx,ebx
  004070f8  66395e10              cmp       [esi+10h],bx
  004070fc  0f85a8000000          jne       near 004071AAh
  00407102  66c746100100          mov       word [esi+10h],1
  00407108  c7463ce8f15400        mov       dword [esi+3Ch],54F1E8h
  0040710f  c6460a09              mov       byte [esi+0Ah],9
  00407113  885e0b                mov       [esi+0Bh],bl
  00407116  e825001400            call      00547140h
  0040711b  99                    cdq
  0040711c  b928000000            mov       ecx,28h
  00407121  f7f9                  idiv      ecx
  00407123  8b461c                mov       eax,[esi+1Ch]
  00407126  83ea14                sub       edx,14h
  00407129  c1e20c                shl       edx,0Ch
  0040712c  03c2                  add       eax,edx
  0040712e  89461c                mov       [esi+1Ch],eax
  00407131  e80a001400            call      00547140h
  00407136  99                    cdq
  00407137  b928000000            mov       ecx,28h
  0040713c  f7f9                  idiv      ecx
  0040713e  8b4e24                mov       ecx,[esi+24h]
  00407141  83ea14                sub       edx,14h
  00407144  c1e20c                shl       edx,0Ch
  00407147  03ca                  add       ecx,edx
  00407149  894e24                mov       [esi+24h],ecx
  0040714c  e8efff1300            call      00547140h
  00407151  99                    cdq
  00407152  b928000000            mov       ecx,28h
  00407157  f7f9                  idiv      ecx
  00407159  8b4620                mov       eax,[esi+20h]
  0040715c  83ea14                sub       edx,14h
  0040715f  c1e20c                shl       edx,0Ch
  00407162  03c2                  add       eax,edx
  00407164  894620                mov       [esi+20h],eax
  00407167  e8d4ff1300            call      00547140h
  0040716c  99                    cdq
  0040716d  33c2                  xor       eax,edx
  0040716f  2bc2                  sub       eax,edx
  00407171  25ff070000            and       eax,7FFh
  00407176  33c2                  xor       eax,edx
  00407178  2bc2                  sub       eax,edx
  0040717a  ba00140000            mov       edx,1400h
  0040717f  2bd0                  sub       edx,eax
  00407181  66895628              mov       [esi+28h],dx
  00407185  e8b6ff1300            call      00547140h
  0040718a  99                    cdq
  0040718b  33c2                  xor       eax,edx
  0040718d  b900140000            mov       ecx,1400h
  00407192  2bc2                  sub       eax,edx
  00407194  66895e12              mov       [esi+12h],bx
  00407198  25ff070000            and       eax,7FFh
  0040719d  885e40                mov       [esi+40h],bl
  004071a0  33c2                  xor       eax,edx
  004071a2  2bc2                  sub       eax,edx
  004071a4  2bc8                  sub       ecx,eax
  004071a6  66894e2a              mov       [esi+2Ah],cx

LAB_004071aa:
  004071aa  66837e1001            cmp       word [esi+10h],1
  004071af  7523                  jne       short 004071D4h
  004071b1  66ff4612              inc       word [esi+12h]
  004071b5  66837e1202            cmp       word [esi+12h],2
  004071ba  7518                  jne       short 004071D4h
  004071bc  8a5640                mov       dl,[esi+40h]
  004071bf  66895e12              mov       [esi+12h],bx
  004071c3  fec2                  inc       dl
  004071c5  8ac2                  mov       al,dl
  004071c7  885640                mov       [esi+40h],dl
  004071ca  3c04                  cmp       al,4
  004071cc  7506                  jne       short 004071D4h
  004071ce  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004071d4:
  004071d4  5e                    pop       esi
  004071d5  5b                    pop       ebx
  004071d6  c3                    ret
  004071d7  90                    nop
  004071d8  90                    nop
  004071d9  90                    nop
  004071da  90                    nop
  004071db  90                    nop
  004071dc  90                    nop
  004071dd  90                    nop
  004071de  90                    nop
  004071df  90                    nop

2 cible(s) de saut interne : 0x004071aa, 0x004071d4


