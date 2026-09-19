Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00431000  section .text  offset fichier 0x30400  368 octets

  00431000  53                    push      ebx
  00431001  56                    push      esi
  00431002  8b74240c              mov       esi,[esp+0Ch]
  00431006  33db                  xor       ebx,ebx
  00431008  66395e10              cmp       [esi+10h],bx
  0043100c  0f85d9000000          jne       near 004310EBh
  00431012  66395e32              cmp       [esi+32h],bx
  00431016  66c746100100          mov       word [esi+10h],1
  0043101c  c7463c200b5500        mov       dword [esi+3Ch],550B20h
  00431023  c6460a02              mov       byte [esi+0Ah],2
  00431027  885e0b                mov       [esi+0Bh],bl
  0043102a  751d                  jne       short 00431049h
  0043102c  e80f611100            call      00547140h
  00431031  99                    cdq
  00431032  b92c010000            mov       ecx,12Ch
  00431037  f7f9                  idiv      ecx
  00431039  8b461c                mov       eax,[esi+1Ch]
  0043103c  83c264                add       edx,64h
  0043103f  c1e20c                shl       edx,0Ch
  00431042  03c2                  add       eax,edx
  00431044  89461c                mov       [esi+1Ch],eax
  00431047  eb1f                  jmp       short 00431068h

LAB_00431049:
  00431049  e8f2601100            call      00547140h
  0043104e  99                    cdq
  0043104f  b92c010000            mov       ecx,12Ch
  00431054  f7f9                  idiv      ecx
  00431056  8b4e1c                mov       ecx,[esi+1Ch]
  00431059  b800c0f9ff            mov       eax,0FFF9C000h
  0043105e  c1e20c                shl       edx,0Ch
  00431061  2bc2                  sub       eax,edx
  00431063  03c8                  add       ecx,eax
  00431065  894e1c                mov       [esi+1Ch],ecx

LAB_00431068:
  00431068  e8d3601100            call      00547140h
  0043106d  99                    cdq
  0043106e  b9a0000000            mov       ecx,0A0h
  00431073  f7f9                  idiv      ecx
  00431075  8b4624                mov       eax,[esi+24h]
  00431078  83ea50                sub       edx,50h
  0043107b  c1e20c                shl       edx,0Ch
  0043107e  03c2                  add       eax,edx
  00431080  894624                mov       [esi+24h],eax
  00431083  e8b8601100            call      00547140h
  00431088  99                    cdq
  00431089  b90a000000            mov       ecx,0Ah
  0043108e  f7f9                  idiv      ecx
  00431090  8b4e20                mov       ecx,[esi+20h]
  00431093  83ea05                sub       edx,5
  00431096  c1e20c                shl       edx,0Ch
  00431099  03ca                  add       ecx,edx
  0043109b  894e20                mov       [esi+20h],ecx
  0043109e  e89d601100            call      00547140h
  004310a3  99                    cdq
  004310a4  b9d0070000            mov       ecx,7D0h
  004310a9  f7f9                  idiv      ecx
  004310ab  81c2dc050000          add       edx,5DCh
  004310b1  66895628              mov       [esi+28h],dx
  004310b5  e886601100            call      00547140h
  004310ba  99                    cdq
  004310bb  b9d0070000            mov       ecx,7D0h
  004310c0  66895e14              mov       [esi+14h],bx
  004310c4  f7f9                  idiv      ecx
  004310c6  81c2dc050000          add       edx,5DCh
  004310cc  6689562a              mov       [esi+2Ah],dx
  004310d0  e86b601100            call      00547140h
  004310d5  99                    cdq
  004310d6  b905000000            mov       ecx,5
  004310db  885e40                mov       [esi+40h],bl
  004310de  f7f9                  idiv      ecx
  004310e0  8b06                  mov       eax,[esi]
  004310e2  80cc40                or        ah,40h
  004310e5  8906                  mov       [esi],eax
  004310e7  66895612              mov       [esi+12h],dx

LAB_004310eb:
  004310eb  66837e1001            cmp       word [esi+10h],1
  004310f0  751b                  jne       short 0043110Dh
  004310f2  66ff4e12              dec       word [esi+12h]
  004310f6  66395e12              cmp       [esi+12h],bx
  004310fa  7d11                  jge       short 0043110Dh
  004310fc  8b06                  mov       eax,[esi]
  004310fe  66c746100200          mov       word [esi+10h],2
  00431104  80e4bf                and       ah,0BFh
  00431107  66895e12              mov       [esi+12h],bx
  0043110b  8906                  mov       [esi],eax

LAB_0043110d:
  0043110d  66837e1002            cmp       word [esi+10h],2
  00431112  7559                  jne       short 0043116Dh
  00431114  e827601100            call      00547140h
  00431119  99                    cdq
  0043111a  b90a000000            mov       ecx,0Ah
  0043111f  f7f9                  idiv      ecx
  00431121  8b4e1c                mov       ecx,[esi+1Ch]
  00431124  83ea05                sub       edx,5
  00431127  c1e20c                shl       edx,0Ch
  0043112a  03ca                  add       ecx,edx
  0043112c  894e1c                mov       [esi+1Ch],ecx
  0043112f  e80c601100            call      00547140h
  00431134  99                    cdq
  00431135  b90a000000            mov       ecx,0Ah
  0043113a  f7f9                  idiv      ecx
  0043113c  8b4624                mov       eax,[esi+24h]
  0043113f  83ea05                sub       edx,5
  00431142  c1e20c                shl       edx,0Ch
  00431145  03c2                  add       eax,edx
  00431147  66ff4612              inc       word [esi+12h]
  0043114b  66837e1202            cmp       word [esi+12h],2
  00431150  894624                mov       [esi+24h],eax
  00431153  7c18                  jl        short 0043116Dh
  00431155  8a4e40                mov       cl,[esi+40h]
  00431158  66895e12              mov       [esi+12h],bx
  0043115c  fec1                  inc       cl
  0043115e  8ac1                  mov       al,cl
  00431160  884e40                mov       [esi+40h],cl
  00431163  3c04                  cmp       al,4
  00431165  7506                  jne       short 0043116Dh
  00431167  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043116d:
  0043116d  5e                    pop       esi
  0043116e  5b                    pop       ebx
  0043116f  c3                    ret

5 cible(s) de saut interne : 0x00431049, 0x00431068, 0x004310eb, 0x0043110d, 0x0043116d
