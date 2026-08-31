Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044f290  section .text  offset fichier 0x4e690  874 octets

  0044f290  53                    push      ebx
  0044f291  55                    push      ebp
  0044f292  56                    push      esi
  0044f293  8b742410              mov       esi,[esp+10h]
  0044f297  57                    push      edi
  0044f298  33db                  xor       ebx,ebx
  0044f29a  8b7e78                mov       edi,[esi+78h]
  0044f29d  3bfb                  cmp       edi,ebx
  0044f29f  7505                  jne       short 0044F2A6h
  0044f2a1  bf58e55d00            mov       edi,5DE558h

LAB_0044f2a6:
  0044f2a6  f70700000040          test      dword [edi],40000000h
  0044f2ac  752b                  jne       short 0044F2D9h
  0044f2ae  f70600000002          test      dword [esi],2000000h
  0044f2b4  7523                  jne       short 0044F2D9h
  0044f2b6  8b471c                mov       eax,[edi+1Ch]
  0044f2b9  89461c                mov       [esi+1Ch],eax
  0044f2bc  8b4760                mov       eax,[edi+60h]
  0044f2bf  0500500000            add       eax,5000h
  0044f2c4  894620                mov       [esi+20h],eax
  0044f2c7  894660                mov       [esi+60h],eax
  0044f2ca  8b4f24                mov       ecx,[edi+24h]
  0044f2cd  894e24                mov       [esi+24h],ecx
  0044f2d0  8b07                  mov       eax,[edi]
  0044f2d2  80cc10                or        ah,10h
  0044f2d5  8907                  mov       [edi],eax
  0044f2d7  eb06                  jmp       short 0044F2DFh

LAB_0044f2d9:
  0044f2d9  810e00000002          or        dword [esi],2000000h

LAB_0044f2df:
  0044f2df  57                    push      edi
  0044f2e0  e8db980e00            call      00538BC0h
  0044f2e5  8d0440                lea       eax,[eax+eax*2]
  0044f2e8  83c404                add       esp,4
  0044f2eb  8d0480                lea       eax,[eax+eax*4]
  0044f2ee  d1e0                  shl       eax,1
  0044f2f0  3d00100000            cmp       eax,1000h
  0044f2f5  7d05                  jge       short 0044F2FCh
  0044f2f7  b800100000            mov       eax,1000h

LAB_0044f2fc:
  0044f2fc  3d30750000            cmp       eax,7530h
  0044f301  7e05                  jle       short 0044F308h
  0044f303  b830750000            mov       eax,7530h

LAB_0044f308:
  0044f308  66395e10              cmp       [esi+10h],bx
  0044f30c  754b                  jne       short 0044F359h
  0044f30e  c646042b              mov       byte [esi+4],2Bh
  0044f312  c6460a06              mov       byte [esi+0Ah],6
  0044f316  c6460b00              mov       byte [esi+0Bh],0
  0044f31a  66c746100100          mov       word [esi+10h],1
  0044f320  66895e16              mov       [esi+16h],bx
  0044f324  66895e28              mov       [esi+28h],bx
  0044f328  66c7462a0010          mov       word [esi+2Ah],1000h
  0044f32e  66895e2c              mov       [esi+2Ch],bx
  0044f332  66895e32              mov       [esi+32h],bx
  0044f336  8b0f                  mov       ecx,[edi]
  0044f338  f7c100000080          test      ecx,80000000h
  0044f33e  7408                  je        short 0044F348h
  0044f340  f7c100000040          test      ecx,40000000h
  0044f346  7411                  je        short 0044F359h

LAB_0044f348:
  0044f348  8b16                  mov       edx,[esi]
  0044f34a  81e2ffffff7f          and       edx,7FFFFFFFh
  0044f350  8916                  mov       [esi],edx
  0044f352  8b0f                  mov       ecx,[edi]
  0044f354  80e5ef                and       ch,0EFh
  0044f357  890f                  mov       [edi],ecx

LAB_0044f359:
  0044f359  66837e1001            cmp       word [esi+10h],1
  0044f35e  ba02000000            mov       edx,2
  0044f363  7552                  jne       short 0044F3B7h
  0044f365  668b4e32              mov       cx,[esi+32h]
  0044f369  bd00080000            mov       ebp,800h
  0044f36e  0fbfd9                movsx     ebx,cx
  0044f371  2beb                  sub       ebp,ebx
  0044f373  c1fd02                sar       ebp,2
  0044f376  03e9                  add       ebp,ecx
  0044f378  668b4e28              mov       cx,[esi+28h]
  0044f37c  0fbfd9                movsx     ebx,cx
  0044f37f  66896e32              mov       [esi+32h],bp
  0044f383  8be8                  mov       ebp,eax
  0044f385  2beb                  sub       ebp,ebx
  0044f387  c1fd02                sar       ebp,2
  0044f38a  03e9                  add       ebp,ecx
  0044f38c  668b4e2c              mov       cx,[esi+2Ch]
  0044f390  0fbfd9                movsx     ebx,cx
  0044f393  2bc3                  sub       eax,ebx
  0044f395  66896e28              mov       [esi+28h],bp
  0044f399  c1f802                sar       eax,2
  0044f39c  03c1                  add       eax,ecx
  0044f39e  66837e160e            cmp       word [esi+16h],0Eh
  0044f3a3  6689462c              mov       [esi+2Ch],ax
  0044f3a7  7c0c                  jl        short 0044F3B5h
  0044f3a9  33db                  xor       ebx,ebx
  0044f3ab  66895610              mov       [esi+10h],dx
  0044f3af  66895e16              mov       [esi+16h],bx
  0044f3b3  eb02                  jmp       short 0044F3B7h

LAB_0044f3b5:
  0044f3b5  33db                  xor       ebx,ebx

LAB_0044f3b7:
  0044f3b7  66395610              cmp       [esi+10h],dx
  0044f3bb  0f8518020000          jne       near 0044F5D9h
  0044f3c1  66395e16              cmp       [esi+16h],bx
  0044f3c5  7519                  jne       short 0044F3E0h
  0044f3c7  6800100000            push      1000h
  0044f3cc  6800100000            push      1000h
  0044f3d1  6800010000            push      100h
  0044f3d6  6a0f                  push      0Fh
  0044f3d8  e8c3da0e00            call      0053CEA0h
  0044f3dd  83c410                add       esp,10h

LAB_0044f3e0:
  0044f3e0  66837e1601            cmp       word [esi+16h],1
  0044f3e5  7519                  jne       short 0044F400h
  0044f3e7  6800100000            push      1000h
  0044f3ec  6800100000            push      1000h
  0044f3f1  6855010000            push      155h
  0044f3f6  6a0f                  push      0Fh
  0044f3f8  e8a3da0e00            call      0053CEA0h
  0044f3fd  83c410                add       esp,10h

LAB_0044f400:
  0044f400  66837e1614            cmp       word [esi+16h],14h
  0044f405  7519                  jne       short 0044F420h
  0044f407  6800100000            push      1000h
  0044f40c  6800100000            push      1000h
  0044f411  6800080000            push      800h
  0044f416  6a0b                  push      0Bh
  0044f418  e883da0e00            call      0053CEA0h
  0044f41d  83c410                add       esp,10h

LAB_0044f420:
  0044f420  66837e1615            cmp       word [esi+16h],15h
  0044f425  7519                  jne       short 0044F440h
  0044f427  6800100000            push      1000h
  0044f42c  6800100000            push      1000h
  0044f431  6853060000            push      653h
  0044f436  6a0c                  push      0Ch
  0044f438  e863da0e00            call      0053CEA0h
  0044f43d  83c410                add       esp,10h

LAB_0044f440:
  0044f440  66837e1616            cmp       word [esi+16h],16h
  0044f445  7519                  jne       short 0044F460h
  0044f447  6800100000            push      1000h
  0044f44c  6800100000            push      1000h
  0044f451  6853060000            push      653h
  0044f456  6a1c                  push      1Ch
  0044f458  e843da0e00            call      0053CEA0h
  0044f45d  83c410                add       esp,10h

LAB_0044f460:
  0044f460  66837e1600            cmp       word [esi+16h],0
  0044f465  bbdc050000            mov       ebx,5DCh
  0044f46a  bd28000000            mov       ebp,28h
  0044f46f  752e                  jne       short 0044F49Fh
  0044f471  6aff                  push      0FFFFFFFFh
  0044f473  6a00                  push      0
  0044f475  689f000000            push      9Fh
  0044f47a  56                    push      esi
  0044f47b  66c7869a0000000600    mov       word [esi+9Ah],6
  0044f484  66c746140010          mov       word [esi+14h],1000h
  0044f48a  895e5c                mov       [esi+5Ch],ebx
  0044f48d  c7464430750000        mov       dword [esi+44h],7530h
  0044f494  896e48                mov       [esi+48h],ebp
  0044f497  e844b60200            call      0047AAE0h
  0044f49c  83c410                add       esp,10h

LAB_0044f49f:
  0044f49f  66837e1604            cmp       word [esi+16h],4
  0044f4a4  752e                  jne       short 0044F4D4h
  0044f4a6  6aff                  push      0FFFFFFFFh
  0044f4a8  6a00                  push      0
  0044f4aa  689f000000            push      9Fh
  0044f4af  56                    push      esi
  0044f4b0  66c7869a0000000600    mov       word [esi+9Ah],6
  0044f4b9  66c746143c0f          mov       word [esi+14h],0F3Ch
  0044f4bf  895e5c                mov       [esi+5Ch],ebx
  0044f4c2  c7464430750000        mov       dword [esi+44h],7530h
  0044f4c9  896e48                mov       [esi+48h],ebp
  0044f4cc  e80fb60200            call      0047AAE0h
  0044f4d1  83c410                add       esp,10h

LAB_0044f4d4:
  0044f4d4  668b4616              mov       ax,[esi+16h]
  0044f4d8  663d0800              cmp       ax,8
  0044f4dc  7c1e                  jl        short 0044F4FCh
  0044f4de  663d1e00              cmp       ax,1Eh
  0044f4e2  7f18                  jg        short 0044F4FCh
  0044f4e4  6aff                  push      0FFFFFFFFh
  0044f4e6  6a00                  push      0
  0044f4e8  6896000000            push      96h
  0044f4ed  8d561c                lea       edx,[esi+1Ch]
  0044f4f0  6a00                  push      0
  0044f4f2  52                    push      edx
  0044f4f3  56                    push      esi
  0044f4f4  e847b70200            call      0047AC40h
  0044f4f9  83c418                add       esp,18h

LAB_0044f4fc:
  0044f4fc  66837e1614            cmp       word [esi+16h],14h
  0044f501  7540                  jne       short 0044F543h
  0044f503  6aff                  push      0FFFFFFFFh
  0044f505  6a00                  push      0
  0044f507  6893000000            push      93h
  0044f50c  56                    push      esi
  0044f50d  c60527e05d0001        mov       byte [5DE027h],1
  0044f514  c60524e05d0096        mov       byte [5DE024h],96h
  0044f51b  c60525e05d00c8        mov       byte [5DE025h],0C8h
  0044f522  c60526e05d00b4        mov       byte [5DE026h],0B4h
  0044f529  e8b2b50200            call      0047AAE0h
  0044f52e  83c410                add       esp,10h
  0044f531  6aff                  push      0FFFFFFFFh
  0044f533  6a00                  push      0
  0044f535  6893000000            push      93h
  0044f53a  56                    push      esi
  0044f53b  e8a0b50200            call      0047AAE0h
  0044f540  83c410                add       esp,10h

LAB_0044f543:
  0044f543  66837e1616            cmp       word [esi+16h],16h
  0044f548  754d                  jne       short 0044F597h
  0044f54a  8b5678                mov       edx,[esi+78h]
  0044f54d  33c0                  xor       eax,eax
  0044f54f  8a4606                mov       al,[esi+6]
  0044f552  33c9                  xor       ecx,ecx
  0044f554  8a4e07                mov       cl,[esi+7]
  0044f557  6a00                  push      0
  0044f559  50                    push      eax
  0044f55a  8b4674                mov       eax,[esi+74h]
  0044f55d  51                    push      ecx
  0044f55e  52                    push      edx
  0044f55f  50                    push      eax
  0044f560  e84bad0e00            call      0053A2B0h
  0044f565  8b4678                mov       eax,[esi+78h]
  0044f568  83c414                add       esp,14h
  0044f56b  8b08                  mov       ecx,[eax]
  0044f56d  66c780960000001000    mov       word [eax+96h],10h
  0044f576  83c940                or        ecx,40h
  0044f579  8908                  mov       [eax],ecx
  0044f57b  c60527e05d0001        mov       byte [5DE027h],1
  0044f582  c60524e05d0000        mov       byte [5DE024h],0
  0044f589  c60525e05d0000        mov       byte [5DE025h],0
  0044f590  c60526e05d0000        mov       byte [5DE026h],0

LAB_0044f597:
  0044f597  66837e163c            cmp       word [esi+16h],3Ch
  0044f59c  7e2e                  jle       short 0044F5CCh
  0044f59e  668b4628              mov       ax,[esi+28h]
  0044f5a2  0fbfc8                movsx     ecx,ax
  0044f5a5  f7d9                  neg       ecx
  0044f5a7  c1f903                sar       ecx,3
  0044f5aa  03c8                  add       ecx,eax
  0044f5ac  668b462c              mov       ax,[esi+2Ch]
  0044f5b0  0fbfd0                movsx     edx,ax
  0044f5b3  f7da                  neg       edx
  0044f5b5  c1fa03                sar       edx,3
  0044f5b8  03d0                  add       edx,eax
  0044f5ba  8b4678                mov       eax,[esi+78h]
  0044f5bd  66894e28              mov       [esi+28h],cx
  0044f5c1  6689562c              mov       [esi+2Ch],dx
  0044f5c5  c7407400000000        mov       dword [eax+74h],0

LAB_0044f5cc:
  0044f5cc  66837e1650            cmp       word [esi+16h],50h
  0044f5d1  7506                  jne       short 0044F5D9h
  0044f5d3  66c746100300          mov       word [esi+10h],3

LAB_0044f5d9:
  0044f5d9  66837e1003            cmp       word [esi+10h],3
  0044f5de  7511                  jne       short 0044F5F1h
  0044f5e0  8b0e                  mov       ecx,[esi]
  0044f5e2  81e1ffffff7f          and       ecx,7FFFFFFFh
  0044f5e8  890e                  mov       [esi],ecx
  0044f5ea  8b07                  mov       eax,[edi]
  0044f5ec  80e4ef                and       ah,0EFh
  0044f5ef  8907                  mov       [edi],eax

LAB_0044f5f1:
  0044f5f1  66ff4616              inc       word [esi+16h]
  0044f5f5  5f                    pop       edi
  0044f5f6  5e                    pop       esi
  0044f5f7  5d                    pop       ebp
  0044f5f8  5b                    pop       ebx
  0044f5f9  c3                    ret

22 cible(s) de saut interne : 0x0044f2a6, 0x0044f2d9, 0x0044f2df, 0x0044f2fc, 0x0044f308, 0x0044f348, 0x0044f359, 0x0044f3b5, 0x0044f3b7, 0x0044f3e0, 0x0044f400, 0x0044f420, 0x0044f440, 0x0044f460, 0x0044f49f, 0x0044f4d4, 0x0044f4fc, 0x0044f543, 0x0044f597, 0x0044f5cc, 0x0044f5d9, 0x0044f5f1
