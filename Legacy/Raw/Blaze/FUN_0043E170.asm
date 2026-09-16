Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043e170  section .text  offset fichier 0x3d570  1400 octets

  0043e170  53                    push      ebx
  0043e171  55                    push      ebp
  0043e172  56                    push      esi
  0043e173  57                    push      edi
  0043e174  33ed                  xor       ebp,ebp
  0043e176  33ff                  xor       edi,edi
  0043e178  e8d3cdfdff            call      0041AF50h
  0043e17d  e8be04fdff            call      0040E640h
  0043e182  8b742414              mov       esi,[esp+14h]
  0043e186  b8a4ca5e00            mov       eax,5ECAA4h
  0043e18b  ba20000000            mov       edx,20h

LAB_0043e190:
  0043e190  8a4808                mov       cl,[eax+8]
  0043e193  84c9                  test      cl,cl
  0043e195  753a                  jne       short 0043E1D1h
  0043e197  8b58fc                mov       ebx,[eax-4]
  0043e19a  8d48fc                lea       ecx,[eax-4]
  0043e19d  f7c300000080          test      ebx,80000000h
  0043e1a3  742c                  je        short 0043E1D1h
  0043e1a5  8bf9                  mov       edi,ecx
  0043e1a7  8b4818                mov       ecx,[eax+18h]
  0043e1aa  894e1c                mov       [esi+1Ch],ecx
  0043e1ad  8b481c                mov       ecx,[eax+1Ch]
  0043e1b0  894e20                mov       [esi+20h],ecx
  0043e1b3  8b4820                mov       ecx,[eax+20h]
  0043e1b6  894e24                mov       [esi+24h],ecx
  0043e1b9  33c9                  xor       ecx,ecx
  0043e1bb  8a08                  mov       cl,[eax]
  0043e1bd  66c740140200          mov       word [eax+14h],2
  0043e1c3  8b0c8d40965e00        mov       ecx,[ecx*4+5E9640h]
  0043e1ca  8b4908                mov       ecx,[ecx+8]
  0043e1cd  894c2414              mov       [esp+14h],ecx

LAB_0043e1d1:
  0043e1d1  059c000000            add       eax,9Ch
  0043e1d6  4a                    dec       edx
  0043e1d7  75b7                  jne       short 0043E190h
  0043e1d9  3bfd                  cmp       edi,ebp
  0043e1db  7505                  jne       short 0043E1E2h
  0043e1dd  bf58e55d00            mov       edi,5DE558h

LAB_0043e1e2:
  0043e1e2  66396e10              cmp       [esi+10h],bp
  0043e1e6  bb01000000            mov       ebx,1
  0043e1eb  7548                  jne       short 0043E235h
  0043e1ed  66895e10              mov       [esi+10h],bx
  0043e1f1  66896e12              mov       [esi+12h],bp
  0043e1f5  66896e14              mov       [esi+14h],bp
  0043e1f9  66896e16              mov       [esi+16h],bp
  0043e1fd  0fbf15b8925e00        movsx     edx,word [5E92B8h]
  0043e204  895650                mov       [esi+50h],edx
  0043e207  0fbf05ba925e00        movsx     eax,word [5E92BAh]
  0043e20e  894654                mov       [esi+54h],eax
  0043e211  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  0043e218  894e58                mov       [esi+58h],ecx
  0043e21b  66896f1a              mov       [edi+1Ah],bp
  0043e21f  c6470a00              mov       byte [edi+0Ah],0
  0043e223  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0043e22a  66895f36              mov       [edi+36h],bx
  0043e22e  6689ae98000000        mov       [esi+98h],bp

LAB_0043e235:
  0043e235  66395e10              cmp       [esi+10h],bx
  0043e239  0f8520010000          jne       near 0043E35Fh
  0043e23f  6aff                  push      0FFFFFFFFh
  0043e241  66896e14              mov       [esi+14h],bp
  0043e245  6a00                  push      0
  0043e247  8d6e1c                lea       ebp,[esi+1Ch]
  0043e24a  68f8000000            push      0F8h
  0043e24f  6a00                  push      0
  0043e251  55                    push      ebp
  0043e252  56                    push      esi
  0043e253  e8e8c90300            call      0047AC40h
  0043e258  83c418                add       esp,18h
  0043e25b  6aff                  push      0FFFFFFFFh
  0043e25d  6a00                  push      0
  0043e25f  68f8000000            push      0F8h
  0043e264  6a00                  push      0
  0043e266  55                    push      ebp
  0043e267  56                    push      esi
  0043e268  e8d3c90300            call      0047AC40h
  0043e26d  83c418                add       esp,18h
  0043e270  e8cb8e1000            call      00547140h
  0043e275  99                    cdq
  0043e276  b90c000000            mov       ecx,0Ch
  0043e27b  f7f9                  idiv      ecx
  0043e27d  663bd3                cmp       dx,bx
  0043e280  750e                  jne       short 0043E290h
  0043e282  66c786980000000000    mov       word [esi+98h],0
  0043e28b  e9e8030000            jmp       0043E678h

LAB_0043e290:
  0043e290  6683fa02              cmp       dx,2
  0043e294  0f85ae030000          jne       near 0043E648h
  0043e29a  66c786980000000000    mov       word [esi+98h],0

LAB_0043e2a3:
  0043e2a3  6800080000            push      800h
  0043e2a8  6800080000            push      800h
  0043e2ad  6853060000            push      653h
  0043e2b2  6a22                  push      22h

LAB_0043e2b4:
  0043e2b4  e8e7eb0f00            call      0053CEA0h
  0043e2b9  83c410                add       esp,10h
  0043e2bc  6800040000            push      400h
  0043e2c1  6800040000            push      400h
  0043e2c6  6853060000            push      653h
  0043e2cb  6a25                  push      25h
  0043e2cd  e8ceeb0f00            call      0053CEA0h
  0043e2d2  83c410                add       esp,10h

LAB_0043e2d5:
  0043e2d5  66ff8698000000        inc       word [esi+98h]
  0043e2dc  6683be9800000008      cmp       word [esi+98h],8
  0043e2e4  7509                  jne       short 0043E2EFh
  0043e2e6  66c786980000000000    mov       word [esi+98h],0

LAB_0043e2ef:
  0043e2ef  66837e1203            cmp       word [esi+12h],3
  0043e2f4  7f28                  jg        short 0043E31Eh
  0043e2f6  66ff471a              inc       word [edi+1Ah]
  0043e2fa  8b4c2414              mov       ecx,[esp+14h]
  0043e2fe  668b471a              mov       ax,[edi+1Ah]
  0043e302  33d2                  xor       edx,edx
  0043e304  668b5116              mov       dx,[ecx+16h]
  0043e308  0fbfc0                movsx     eax,ax
  0043e30b  83ea0a                sub       edx,0Ah
  0043e30e  3bc2                  cmp       eax,edx
  0043e310  7c4d                  jl        short 0043E35Fh
  0043e312  66c7471a0000          mov       word [edi+1Ah],0
  0043e318  66ff4612              inc       word [esi+12h]
  0043e31c  eb41                  jmp       short 0043E35Fh

LAB_0043e31e:
  0043e31e  845e16                test      [esi+16h],bl
  0043e321  743c                  je        short 0043E35Fh
  0043e323  8b542414              mov       edx,[esp+14h]
  0043e327  66ff471a              inc       word [edi+1Ah]
  0043e32b  668b4f1a              mov       cx,[edi+1Ah]
  0043e32f  668b4216              mov       ax,[edx+16h]
  0043e333  8bd0                  mov       edx,eax
  0043e335  81e2ffff0000          and       edx,0FFFFh
  0043e33b  0fbfc9                movsx     ecx,cx
  0043e33e  83ea04                sub       edx,4
  0043e341  3bca                  cmp       ecx,edx
  0043e343  7c1a                  jl        short 0043E35Fh
  0043e345  83c0fc                add       eax,0FFFFFFFCh
  0043e348  6689471a              mov       [edi+1Ah],ax
  0043e34c  33c0                  xor       eax,eax
  0043e34e  66c746100200          mov       word [esi+10h],2
  0043e354  66894612              mov       [esi+12h],ax
  0043e358  66898698000000        mov       [esi+98h],ax

LAB_0043e35f:
  0043e35f  66837e1002            cmp       word [esi+10h],2
  0043e364  0f856b020000          jne       near 0043E5D5h
  0043e36a  66ff4612              inc       word [esi+12h]
  0043e36e  66837e1203            cmp       word [esi+12h],3
  0043e373  7f11                  jg        short 0043E386h
  0043e375  b82c010000            mov       eax,12Ch
  0043e37a  66014728              add       [edi+28h],ax
  0043e37e  6601472a              add       [edi+2Ah],ax
  0043e382  6601472c              add       [edi+2Ch],ax

LAB_0043e386:
  0043e386  66837e120c            cmp       word [esi+12h],0Ch
  0043e38b  0f8c44020000          jl        near 0043E5D5h
  0043e391  8b07                  mov       eax,[edi]
  0043e393  8d6e1c                lea       ebp,[esi+1Ch]
  0043e396  80cc10                or        ah,10h
  0043e399  bb06000000            mov       ebx,6
  0043e39e  8907                  mov       [edi],eax

LAB_0043e3a0:
  0043e3a0  668b572a              mov       dx,[edi+2Ah]
  0043e3a4  6aff                  push      0FFFFFFFFh
  0043e3a6  6a00                  push      0
  0043e3a8  68f8000000            push      0F8h
  0043e3ad  6a00                  push      0
  0043e3af  55                    push      ebp
  0043e3b0  56                    push      esi
  0043e3b1  66895614              mov       [esi+14h],dx
  0043e3b5  e886c80300            call      0047AC40h
  0043e3ba  83c418                add       esp,18h
  0043e3bd  6aff                  push      0FFFFFFFFh
  0043e3bf  6a00                  push      0
  0043e3c1  68f8000000            push      0F8h
  0043e3c6  6a00                  push      0
  0043e3c8  55                    push      ebp
  0043e3c9  56                    push      esi
  0043e3ca  e871c80300            call      0047AC40h
  0043e3cf  83c418                add       esp,18h
  0043e3d2  4b                    dec       ebx
  0043e3d3  75cb                  jne       short 0043E3A0h
  0043e3d5  6683be9800000000      cmp       word [esi+98h],0
  0043e3dd  7564                  jne       short 0043E443h
  0043e3df  6800080000            push      800h
  0043e3e4  6800080000            push      800h
  0043e3e9  6853060000            push      653h
  0043e3ee  6a1f                  push      1Fh
  0043e3f0  e8abea0f00            call      0053CEA0h
  0043e3f5  83c410                add       esp,10h
  0043e3f8  6800080000            push      800h
  0043e3fd  6800080000            push      800h
  0043e402  68ea010000            push      1EAh
  0043e407  6a03                  push      3
  0043e409  e892ea0f00            call      0053CEA0h
  0043e40e  83c410                add       esp,10h
  0043e411  6800080000            push      800h
  0043e416  6800080000            push      800h
  0043e41b  6800020000            push      200h
  0043e420  6a03                  push      3
  0043e422  e879ea0f00            call      0053CEA0h
  0043e427  83c410                add       esp,10h
  0043e42a  6800040000            push      400h
  0043e42f  6800040000            push      400h
  0043e434  6800020000            push      200h
  0043e439  6a25                  push      25h
  0043e43b  e860ea0f00            call      0053CEA0h
  0043e440  83c410                add       esp,10h

LAB_0043e443:
  0043e443  6683be9800000005      cmp       word [esi+98h],5
  0043e44b  7564                  jne       short 0043E4B1h
  0043e44d  6800080000            push      800h
  0043e452  6800080000            push      800h
  0043e457  6800040000            push      400h
  0043e45c  6a1f                  push      1Fh
  0043e45e  e83dea0f00            call      0053CEA0h
  0043e463  83c410                add       esp,10h
  0043e466  6800080000            push      800h
  0043e46b  6800080000            push      800h
  0043e470  68ea010000            push      1EAh
  0043e475  6a03                  push      3
  0043e477  e824ea0f00            call      0053CEA0h
  0043e47c  83c410                add       esp,10h
  0043e47f  6800080000            push      800h
  0043e484  6800080000            push      800h
  0043e489  6800020000            push      200h
  0043e48e  6a03                  push      3
  0043e490  e80bea0f00            call      0053CEA0h
  0043e495  83c410                add       esp,10h
  0043e498  6800040000            push      400h
  0043e49d  6800040000            push      400h
  0043e4a2  6800020000            push      200h
  0043e4a7  6a25                  push      25h
  0043e4a9  e8f2e90f00            call      0053CEA0h
  0043e4ae  83c410                add       esp,10h

LAB_0043e4b1:
  0043e4b1  6683be980000000a      cmp       word [esi+98h],0Ah
  0043e4b9  7564                  jne       short 0043E51Fh
  0043e4bb  6800080000            push      800h
  0043e4c0  6800080000            push      800h
  0043e4c5  6829030000            push      329h
  0043e4ca  6a1f                  push      1Fh
  0043e4cc  e8cfe90f00            call      0053CEA0h
  0043e4d1  83c410                add       esp,10h
  0043e4d4  6800080000            push      800h
  0043e4d9  6800080000            push      800h
  0043e4de  68ea010000            push      1EAh
  0043e4e3  6a03                  push      3
  0043e4e5  e8b6e90f00            call      0053CEA0h
  0043e4ea  83c410                add       esp,10h
  0043e4ed  6800080000            push      800h
  0043e4f2  6800080000            push      800h
  0043e4f7  6800020000            push      200h
  0043e4fc  6a03                  push      3
  0043e4fe  e89de90f00            call      0053CEA0h
  0043e503  83c410                add       esp,10h
  0043e506  6800040000            push      400h
  0043e50b  6800040000            push      400h
  0043e510  6800020000            push      200h
  0043e515  6a25                  push      25h
  0043e517  e884e90f00            call      0053CEA0h
  0043e51c  83c410                add       esp,10h

LAB_0043e51f:
  0043e51f  6683be980000000f      cmp       word [esi+98h],0Fh
  0043e527  7564                  jne       short 0043E58Dh
  0043e529  6800080000            push      800h
  0043e52e  6800080000            push      800h
  0043e533  6800020000            push      200h
  0043e538  6a1f                  push      1Fh
  0043e53a  e861e90f00            call      0053CEA0h
  0043e53f  83c410                add       esp,10h
  0043e542  6800080000            push      800h
  0043e547  6800080000            push      800h
  0043e54c  68ea010000            push      1EAh
  0043e551  6a03                  push      3
  0043e553  e848e90f00            call      0053CEA0h
  0043e558  83c410                add       esp,10h
  0043e55b  6800080000            push      800h
  0043e560  6800080000            push      800h
  0043e565  6800020000            push      200h
  0043e56a  6a03                  push      3
  0043e56c  e82fe90f00            call      0053CEA0h
  0043e571  83c410                add       esp,10h
  0043e574  6800040000            push      400h
  0043e579  6800040000            push      400h
  0043e57e  6800020000            push      200h
  0043e583  6a25                  push      25h
  0043e585  e816e90f00            call      0053CEA0h
  0043e58a  83c410                add       esp,10h

LAB_0043e58d:
  0043e58d  66ff8698000000        inc       word [esi+98h]
  0043e594  6683be9800000014      cmp       word [esi+98h],14h
  0043e59c  7509                  jne       short 0043E5A7h
  0043e59e  66c786980000000000    mov       word [esi+98h],0

LAB_0043e5a7:
  0043e5a7  b938ffffff            mov       ecx,0FFFFFF38h
  0043e5ac  66014f28              add       [edi+28h],cx
  0043e5b0  668b4728              mov       ax,[edi+28h]
  0043e5b4  66014f2a              add       [edi+2Ah],cx
  0043e5b8  66014f2c              add       [edi+2Ch],cx
  0043e5bc  6685c0                test      ax,ax
  0043e5bf  7d14                  jge       short 0043E5D5h
  0043e5c1  33c0                  xor       eax,eax
  0043e5c3  66894728              mov       [edi+28h],ax
  0043e5c7  6689472a              mov       [edi+2Ah],ax
  0043e5cb  6689472c              mov       [edi+2Ch],ax
  0043e5cf  66c746100300          mov       word [esi+10h],3

LAB_0043e5d5:
  0043e5d5  66817e168c00          cmp       word [esi+16h],8Ch
  0043e5db  7c62                  jl        short 0043E63Fh
  0043e5dd  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043e5e3  e8c8510c00            call      005037B0h
  0043e5e8  33c0                  xor       eax,eax
  0043e5ea  6a00                  push      0
  0043e5ec  8a4709                mov       al,[edi+9]
  0043e5ef  8d0c80                lea       ecx,[eax+eax*4]
  0043e5f2  c1e103                shl       ecx,3
  0043e5f5  2bc8                  sub       ecx,eax
  0043e5f7  a1cc975e00            mov       eax,[5E97CCh]
  0043e5fc  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0043e603  52                    push      edx
  0043e604  50                    push      eax
  0043e605  e8c6570d00            call      00513DD0h
  0043e60a  8b07                  mov       eax,[edi]
  0043e60c  83c40c                add       esp,0Ch
  0043e60f  0d00400002            or        eax,2004000h
  0043e614  8907                  mov       [edi],eax
  0043e616  668b4e50              mov       cx,[esi+50h]
  0043e61a  66890d60915e00        mov       [5E9160h],cx
  0043e621  668b5654              mov       dx,[esi+54h]
  0043e625  66891562915e00        mov       [5E9162h],dx
  0043e62c  668b4658              mov       ax,[esi+58h]
  0043e630  66a364915e00          mov       [5E9164h],ax
  0043e636  66c70566915e000200    mov       word [5E9166h],2

LAB_0043e63f:
  0043e63f  66ff4616              inc       word [esi+16h]
  0043e643  5f                    pop       edi
  0043e644  5e                    pop       esi
  0043e645  5d                    pop       ebp
  0043e646  5b                    pop       ebx
  0043e647  c3                    ret

LAB_0043e648:
  0043e648  6683be9800000000      cmp       word [esi+98h],0
  0043e650  0f857ffcffff          jne       near 0043E2D5h
  0043e656  66c786980000000000    mov       word [esi+98h],0
  0043e65f  e8dc8a1000            call      00547140h
  0043e664  99                    cdq
  0043e665  33c2                  xor       eax,edx
  0043e667  2bc2                  sub       eax,edx
  0043e669  23c3                  and       eax,ebx
  0043e66b  33c2                  xor       eax,edx
  0043e66d  2bc2                  sub       eax,edx
  0043e66f  663bc3                cmp       ax,bx
  0043e672  0f852bfcffff          jne       near 0043E2A3h

LAB_0043e678:
  0043e678  6800080000            push      800h
  0043e67d  6800080000            push      800h
  0043e682  6800020000            push      200h
  0043e687  6a03                  push      3
  0043e689  e926fcffff            jmp       0043E2B4h
  0043e68e  90                    nop
  0043e68f  90                    nop
  0043e690  56                    push      esi
  0043e691  8b742408              mov       esi,[esp+8]
  0043e695  57                    push      edi
  0043e696  66837e1000            cmp       word [esi+10h],0
  0043e69b  0f85f8000000          jne       near 0043E799h
  0043e6a1  66c746100100          mov       word [esi+10h],1
  0043e6a7  c7463c40125500        mov       dword [esi+3Ch],551240h
  0043e6ae  c6464000              mov       byte [esi+40h],0
  0043e6b2  c6460a0c              mov       byte [esi+0Ah],0Ch
  0043e6b6  c6460b00              mov       byte [esi+0Bh],0
  0043e6ba  e8818a1000            call      00547140h
  0043e6bf  99                    cdq
  0043e6c0  33c2                  xor       eax,edx
  0043e6c2  2bc2                  sub       eax,edx
  0043e6c4  25ff070000            and       eax,7FFh
  0043e6c9  33c2                  xor       eax,edx
  0043e6cb  2bc2                  sub       eax,edx
  0043e6cd  0500080000            add       eax,800h
  0043e6d2  66837e1400            cmp       word [esi+14h],0
  0043e6d7  66894628              mov       [esi+28h],ax
  0043e6db  6689462a              mov       [esi+2Ah],ax
  0043e6df  754b                  jne       short 0043E72Ch
  0043e6e1  e85a8a1000            call      00547140h
  0043e6e6  99                    cdq
  0043e6e7  b9                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
