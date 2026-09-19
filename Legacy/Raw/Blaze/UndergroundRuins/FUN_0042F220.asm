Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f220  section .text  offset fichier 0x2e620  300 octets

  0042f220  8b442404              mov       eax,[esp+4]
  0042f224  53                    push      ebx
  0042f225  85c0                  test      eax,eax
  0042f227  7505                  jne       short 0042F22Eh
  0042f229  b858e55d00            mov       eax,5DE558h

LAB_0042f22e:
  0042f22e  8b485c                mov       ecx,[eax+5Ch]
  0042f231  8a5810                mov       bl,[eax+10h]
  0042f234  8bd1                  mov       edx,ecx
  0042f236  81e3ff000000          and       ebx,0FFh
  0042f23c  81e2ffffff3f          and       edx,3FFFFFFFh
  0042f242  33d3                  xor       edx,ebx
  0042f244  741b                  je        short 0042F261h
  0042f246  f7c100000080          test      ecx,80000000h
  0042f24c  7413                  je        short 0042F261h
  0042f24e  8a4810                mov       cl,[eax+10h]
  0042f251  0fbf5014              movsx     edx,word [eax+14h]
  0042f255  81e1ff000000          and       ecx,0FFh
  0042f25b  895048                mov       [eax+48h],edx
  0042f25e  89485c                mov       [eax+5Ch],ecx

LAB_0042f261:
  0042f261  8b505c                mov       edx,[eax+5Ch]
  0042f264  b900000040            mov       ecx,40000000h
  0042f269  85d1                  test      ecx,edx
  0042f26b  7519                  jne       short 0042F286h
  0042f26d  0fbf5010              movsx     edx,word [eax+10h]
  0042f271  c1fa08                sar       edx,8
  0042f274  895044                mov       [eax+44h],edx
  0042f277  0fbf5012              movsx     edx,word [eax+12h]
  0042f27b  89504c                mov       [eax+4Ch],edx
  0042f27e  8b505c                mov       edx,[eax+5Ch]
  0042f281  0bd1                  or        edx,ecx
  0042f283  89505c                mov       [eax+5Ch],edx

LAB_0042f286:
  0042f286  8b485c                mov       ecx,[eax+5Ch]
  0042f289  5b                    pop       ebx
  0042f28a  81e1ffffff7f          and       ecx,7FFFFFFFh
  0042f290  89485c                mov       [eax+5Ch],ecx
  0042f293  c3                    ret
  0042f294  90                    nop
  0042f295  90                    nop
  0042f296  90                    nop
  0042f297  90                    nop
  0042f298  90                    nop
  0042f299  90                    nop
  0042f29a  90                    nop
  0042f29b  90                    nop
  0042f29c  90                    nop
  0042f29d  90                    nop
  0042f29e  90                    nop
  0042f29f  90                    nop
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
  0042f34b  89                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
