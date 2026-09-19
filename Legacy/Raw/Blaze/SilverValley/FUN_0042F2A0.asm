Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f2a0  section .text  offset fichier 0x2e6a0  272 octets

  0042f2a0  56                    push      esi
  0042f2a1  8b742408              mov       esi,[esp+8]
  0042f2a5  8b466c                mov       eax,[esi+6Ch]
  0042f2a8  66837e1000            cmp       word [esi+10h],0
  0042f2ad  894674                mov       [esi+74h],eax
  0042f2b0  7547                  jne       short 0042F2F9h
  0042f2b2  8b4e1c                mov       ecx,[esi+1Ch]
  0042f2b5  8b5620                mov       edx,[esi+20h]
  0042f2b8  8b4624                mov       eax,[esi+24h]
  0042f2bb  6a00                  push      0
  0042f2bd  894e50                mov       [esi+50h],ecx
  0042f2c0  6a00                  push      0
  0042f2c2  0fbf4e32              movsx     ecx,word [esi+32h]
  0042f2c6  6800150000            push      1500h
  0042f2cb  68a80a0000            push      0AA8h
  0042f2d0  6a23                  push      23h
  0042f2d2  56                    push      esi
  0042f2d3  66c746100100          mov       word [esi+10h],1
  0042f2d9  66c746121400          mov       word [esi+12h],14h
  0042f2df  895654                mov       [esi+54h],edx
  0042f2e2  894658                mov       [esi+58h],eax
  0042f2e5  894e48                mov       [esi+48h],ecx
  0042f2e8  e8938d0300            call      00468080h
  0042f2ed  83c418                add       esp,18h
  0042f2f0  66c786980000000000    mov       word [esi+98h],0

LAB_0042f2f9:
  0042f2f9  66837e1001            cmp       word [esi+10h],1
  0042f2fe  0f85a8000000          jne       near 0042F3ACh
  0042f304  66ff8698000000        inc       word [esi+98h]
  0042f30b  6683be9800000002      cmp       word [esi+98h],2
  0042f313  7c22                  jl        short 0042F337h
  0042f315  6a00                  push      0
  0042f317  6a00                  push      0
  0042f319  6800180000            push      1800h
  0042f31e  6850150000            push      1550h
  0042f323  6a23                  push      23h
  0042f325  56                    push      esi
  0042f326  66c786980000000000    mov       word [esi+98h],0
  0042f32f  e84c8d0300            call      00468080h
  0042f334  83c418                add       esp,18h

LAB_0042f337:
  0042f337  8b5650                mov       edx,[esi+50h]
  0042f33a  8b4654                mov       eax,[esi+54h]
  0042f33d  8b4e58                mov       ecx,[esi+58h]
  0042f340  66ff4e12              dec       word [esi+12h]
  0042f344  89561c                mov       [esi+1Ch],edx
  0042f347  668b5648              mov       dx,[esi+48h]
  0042f34b  894620                mov       [esi+20h],eax
  0042f34e  894e24                mov       [esi+24h],ecx
  0042f351  66895632              mov       [esi+32h],dx
  0042f355  e8e67d1100            call      00547140h
  0042f35a  99                    cdq
  0042f35b  b9bc020000            mov       ecx,2BCh
  0042f360  f7f9                  idiv      ecx
  0042f362  81ea5e010000          sub       edx,15Eh
  0042f368  66015632              add       [esi+32h],dx
  0042f36c  e8cf7d1100            call      00547140h
  0042f371  99                    cdq
  0042f372  b996000000            mov       ecx,96h
  0042f377  f7f9                  idiv      ecx
  0042f379  8a4612                mov       al,[esi+12h]
  0042f37c  03d1                  add       edx,ecx
  0042f37e  8b4e20                mov       ecx,[esi+20h]
  0042f381  c1e20c                shl       edx,0Ch
  0042f384  03ca                  add       ecx,edx
  0042f386  a801                  test      al,1
  0042f388  894e20                mov       [esi+20h],ecx
  0042f38b  7412                  je        short 0042F39Fh
  0042f38d  6aff                  push      0FFFFFFFFh
  0042f38f  6a00                  push      0
  0042f391  68c4000000            push      0C4h
  0042f396  56                    push      esi
  0042f397  e844b70400            call      0047AAE0h
  0042f39c  83c410                add       esp,10h

LAB_0042f39f:
  0042f39f  66837e1200            cmp       word [esi+12h],0
  0042f3a4  7d06                  jge       short 0042F3ACh
  0042f3a6  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042f3ac:
  0042f3ac  5e                    pop       esi
  0042f3ad  c3                    ret
  0042f3ae  90                    nop
  0042f3af  90                    nop

4 cible(s) de saut interne : 0x0042f2f9, 0x0042f337, 0x0042f39f, 0x0042f3ac
