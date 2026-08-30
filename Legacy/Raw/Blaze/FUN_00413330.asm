; 0x00413330 — le stroboscope rouge accroche a son porteur (116 instructions, absente de Blaze.c).

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00413330  section .text  offset fichier 0x12730  400 octets

  00413330  53                    push      ebx
  00413331  56                    push      esi
  00413332  8b74240c              mov       esi,[esp+0Ch]
  00413336  33db                  xor       ebx,ebx
  00413338  57                    push      edi
  00413339  8b466c                mov       eax,[esi+6Ch]
  0041333c  3bc3                  cmp       eax,ebx
  0041333e  0f8476010000          je        near 004134BAh
  00413344  8b481c                mov       ecx,[eax+1Ch]
  00413347  894e1c                mov       [esi+1Ch],ecx
  0041334a  8b5020                mov       edx,[eax+20h]
  0041334d  895620                mov       [esi+20h],edx
  00413350  8b4824                mov       ecx,[eax+24h]
  00413353  894e24                mov       [esi+24h],ecx
  00413356  8b00                  mov       eax,[eax]
  00413358  a900000040            test      eax,40000000h
  0041335d  0f8522010000          jne       near 00413485h
  00413363  a900000080            test      eax,80000000h
  00413368  0f8417010000          je        near 00413485h
  0041336e  66399e9a000000        cmp       [esi+9Ah],bx
  00413375  0f853f010000          jne       near 004134BAh
  0041337b  66395e10              cmp       [esi+10h],bx
  0041337f  7567                  jne       short 004133E8h
  00413381  66c746100100          mov       word [esi+10h],1
  00413387  66895e12              mov       [esi+12h],bx
  0041338b  8b15cc975e00          mov       edx,[5E97CCh]
  00413391  53                    push      ebx
  00413392  53                    push      ebx
  00413393  68002a0000            push      2A00h
  00413398  8b82a8000000          mov       eax,[edx+0A8h]
  0041339e  6800030000            push      300h
  004133a3  8b08                  mov       ecx,[eax]
  004133a5  660fb6512c            movzx     dx,byte [ecx+2Ch]
  004133aa  52                    push      edx
  004133ab  56                    push      esi
  004133ac  e8cf4c0500            call      00468080h
  004133b1  83c418                add       esp,18h
  004133b4  c7465c01000000        mov       dword [esi+5Ch],1
  004133bb  e8803d1300            call      00547140h
  004133c0  99                    cdq
  004133c1  33c2                  xor       eax,edx
  004133c3  bf0b000000            mov       edi,0Bh
  004133c8  2bc2                  sub       eax,edx
  004133ca  83e001                and       eax,1
  004133cd  33c2                  xor       eax,edx
  004133cf  2bc2                  sub       eax,edx
  004133d1  894644                mov       [esi+44h],eax

LAB_004133d4:
  004133d4  6aff                  push      0FFFFFFFFh
  004133d6  53                    push      ebx
  004133d7  68de000000            push      0DEh
  004133dc  56                    push      esi
  004133dd  e8fe760600            call      0047AAE0h
  004133e2  83c410                add       esp,10h
  004133e5  4f                    dec       edi
  004133e6  75ec                  jne       short 004133D4h

LAB_004133e8:
  004133e8  66837e1001            cmp       word [esi+10h],1
  004133ed  0f85c7000000          jne       near 004134BAh
  004133f3  668b4612              mov       ax,[esi+12h]
  004133f7  663bc3                cmp       ax,bx
  004133fa  740c                  je        short 00413408h
  004133fc  663d0300              cmp       ax,3
  00413400  7406                  je        short 00413408h
  00413402  663d0700              cmp       ax,7
  00413406  7525                  jne       short 0041342Dh

LAB_00413408:
  00413408  663bc3                cmp       ax,bx
  0041340b  895e5c                mov       [esi+5Ch],ebx
  0041340e  7506                  jne       short 00413416h
  00413410  66895e16              mov       [esi+16h],bx
  00413414  eb06                  jmp       short 0041341Ch

LAB_00413416:
  00413416  66c746160100          mov       word [esi+16h],1

LAB_0041341c:
  0041341c  6aff                  push      0FFFFFFFFh
  0041341e  53                    push      ebx
  0041341f  68de000000            push      0DEh
  00413424  56                    push      esi
  00413425  e8b6760600            call      0047AAE0h
  0041342a  83c410                add       esp,10h

LAB_0041342d:
  0041342d  66ff4612              inc       word [esi+12h]
  00413431  668b4612              mov       ax,[esi+12h]
  00413435  b91e000000            mov       ecx,1Eh
  0041343a  c60527e05d0001        mov       byte [5DE027h],1
  00413441  a801                  test      al,1
  00413443  880d24e05d00          mov       [5DE024h],cl
  00413449  7506                  jne       short 00413451h
  0041344b  881d24e05d00          mov       [5DE024h],bl

LAB_00413451:
  00413451  881d25e05d00          mov       [5DE025h],bl
  00413457  881d26e05d00          mov       [5DE026h],bl
  0041345d  66394e12              cmp       [esi+12h],cx
  00413461  7c57                  jl        short 004134BAh
  00413463  b8c0935e00            mov       eax,5E93C0h
  00413468  b904000000            mov       ecx,4

LAB_0041346d:
  0041346d  8b10                  mov       edx,[eax]
  0041346f  059c000000            add       eax,9Ch
  00413474  49                    dec       ecx
  00413475  89542410              mov       [esp+10h],edx
  00413479  75f2                  jne       short 0041346Dh
  0041347b  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00413481  5f                    pop       edi
  00413482  5e                    pop       esi
  00413483  5b                    pop       ebx
  00413484  c3                    ret

LAB_00413485:
  00413485  8b0e                  mov       ecx,[esi]
  00413487  81e1ffffff7f          and       ecx,7FFFFFFFh
  0041348d  890e                  mov       [esi],ecx
  0041348f  668bb69a000000        mov       si,[esi+9Ah]
  00413496  663bf3                cmp       si,bx
  00413499  7406                  je        short 004134A1h
  0041349b  6683fe01              cmp       si,1
  0041349f  7519                  jne       short 004134BAh

LAB_004134a1:
  004134a1  c60527e05d0001        mov       byte [5DE027h],1
  004134a8  881d24e05d00          mov       [5DE024h],bl
  004134ae  881d25e05d00          mov       [5DE025h],bl
  004134b4  881d26e05d00          mov       [5DE026h],bl

LAB_004134ba:
  004134ba  5f                    pop       edi
  004134bb  5e                    pop       esi
  004134bc  5b                    pop       ebx
  004134bd  c3                    ret
  004134be  90                    nop
  004134bf  90                    nop

11 cible(s) de saut interne : 0x004133d4, 0x004133e8, 0x00413408, 0x00413416, 0x0041341c, 0x0041342d, 0x00413451, 0x0041346d, 0x00413485, 0x004134a1, 0x004134ba
