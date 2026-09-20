  004412d0  53                    push      ebx
  004412d1  56                    push      esi
  004412d2  8b74240c              mov       esi,[esp+0Ch]
  004412d6  33db                  xor       ebx,ebx
  004412d8  66395e10              cmp       [esi+10h],bx
  004412dc  0f85b1000000          jne       near 00441393h
  004412e2  a0c4de5e00            mov       al,[5EDEC4h]
  004412e7  c6460a03              mov       byte [esi+0Ah],3
  004412eb  0402                  add       al,2
  004412ed  885e0b                mov       [esi+0Bh],bl
  004412f0  884604                mov       [esi+4],al
  004412f3  66c746100100          mov       word [esi+10h],1
  004412f9  e8425e1000            call      00547140h
  004412fe  99                    cdq
  004412ff  b9f4010000            mov       ecx,1F4h
  00441304  f7f9                  idiv      ecx
  00441306  8b4e1c                mov       ecx,[esi+1Ch]
  00441309  81eafa000000          sub       edx,0FAh
  0044130f  c1e20c                shl       edx,0Ch
  00441312  03ca                  add       ecx,edx
  00441314  894e1c                mov       [esi+1Ch],ecx
  00441317  e8245e1000            call      00547140h
  0044131c  99                    cdq
  0044131d  b9f4010000            mov       ecx,1F4h
  00441322  f7f9                  idiv      ecx
  00441324  8b4624                mov       eax,[esi+24h]
  00441327  81eafa000000          sub       edx,0FAh
  0044132d  c1e20c                shl       edx,0Ch
  00441330  03c2                  add       eax,edx
  00441332  894624                mov       [esi+24h],eax
  00441335  e8065e1000            call      00547140h
  0044133a  99                    cdq
  0044133b  b970170000            mov       ecx,1770h
  00441340  f7f9                  idiv      ecx
  00441342  81c290010000          add       edx,190h
  00441348  66895628              mov       [esi+28h],dx
  0044134c  e8ef5d1000            call      00547140h
  00441351  99                    cdq
  00441352  b9d0070000            mov       ecx,7D0h
  00441357  f7f9                  idiv      ecx
  00441359  81c290010000          add       edx,190h
  0044135f  6689562c              mov       [esi+2Ch],dx
  00441363  e8d85d1000            call      00547140h
  00441368  99                    cdq
  00441369  33c2                  xor       eax,edx
  0044136b  66895e2a              mov       [esi+2Ah],bx
  0044136f  2bc2                  sub       eax,edx
  00441371  25ff0f0000            and       eax,0FFFh
  00441376  33c2                  xor       eax,edx
  00441378  2bc2                  sub       eax,edx
  0044137a  66894632              mov       [esi+32h],ax
  0044137e  e8bd5d1000            call      00547140h
  00441383  99                    cdq
  00441384  b914000000            mov       ecx,14h
  00441389  66895e16              mov       [esi+16h],bx
  0044138d  f7f9                  idiv      ecx
  0044138f  66895612              mov       [esi+12h],dx
  00441393  66837e1001            cmp       word [esi+10h],1
  00441398  7542                  jne       short 004413DCh
  0044139a  66ff4e12              dec       word [esi+12h]
  0044139e  66395e12              cmp       [esi+12h],bx
  004413a2  7538                  jne       short 004413DCh
  004413a4  6800100000            push      1000h
  004413a9  6800100000            push      1000h
  004413ae  6800020000            push      200h
  004413b3  6a0c                  push      0Ch
  004413b5  66c746100200          mov       word [esi+10h],2
  004413bb  e8e0ba0f00            call      0053CEA0h
  004413c0  83c410                add       esp,10h
  004413c3  6800080000            push      800h
  004413c8  6800080000            push      800h
  004413cd  6800080000            push      800h
  004413d2  6a0d                  push      0Dh
  004413d4  e8c7ba0f00            call      0053CEA0h
  004413d9  83c410                add       esp,10h
  004413dc  66837e1002            cmp       word [esi+10h],2
  004413e1  7516                  jne       short 004413F9h
  004413e3  6681462ab80b          add       word [esi+2Ah],0BB8h
  004413e9  668b462a              mov       ax,[esi+2Ah]
  004413ed  663d606d              cmp       ax,6D60h
  004413f1  7c06                  jl        short 004413F9h
  004413f3  66c7462a606d          mov       word [esi+2Ah],6D60h
  004413f9  66837e160a            cmp       word [esi+16h],0Ah
  004413fe  7516                  jne       short 00441416h
  00441400  e83b5d1000            call      00547140h
  00441405  99                    cdq
  00441406  b914000000            mov       ecx,14h
  0044140b  f7f9                  idiv      ecx
  0044140d  66837e160a            cmp       word [esi+16h],0Ah
  00441412  66895612              mov       [esi+12h],dx
  00441416  7c2a                  jl        short 00441442h
  00441418  66ff4e12              dec       word [esi+12h]
  0044141c  66395e12              cmp       [esi+12h],bx
  00441420  7d20                  jge       short 00441442h
  00441422  6aec                  push      0FFFFFFECh
  00441424  6aec                  push      0FFFFFFECh
  00441426  6aec                  push      0FFFFFFECh
  00441428  56                    push      esi
  00441429  66895e12              mov       [esi+12h],bx
  0044142d  e8ae39fcff            call      00404DE0h
  00441432  8a4638                mov       al,[esi+38h]
  00441435  83c410                add       esp,10h
  00441438  3ac3                  cmp       al,bl
  0044143a  7506                  jne       short 00441442h
  0044143c  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00441442  66ff4616              inc       word [esi+16h]
  00441446  5e                    pop       esi
  00441447  5b                    pop       ebx
  00441448  c3                    ret
