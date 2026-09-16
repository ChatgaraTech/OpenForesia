Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00451250  section .text  offset fichier 0x50650  513 octets

  00451250  51                    push      ecx
  00451251  53                    push      ebx
  00451252  55                    push      ebp
  00451253  56                    push      esi
  00451254  8b742414              mov       esi,[esp+14h]
  00451258  33c0                  xor       eax,eax
  0045125a  57                    push      edi
  0045125b  66394610              cmp       [esi+10h],ax
  0045125f  bf01000000            mov       edi,1
  00451264  7508                  jne       short 0045126Eh
  00451266  66897e10              mov       [esi+10h],di
  0045126a  66894612              mov       [esi+12h],ax

LAB_0045126e:
  0045126e  66ff4612              inc       word [esi+12h]
  00451272  66837e120f            cmp       word [esi+12h],0Fh
  00451277  7504                  jne       short 0045127Dh
  00451279  66894612              mov       [esi+12h],ax

LAB_0045127d:
  0045127d  66394612              cmp       [esi+12h],ax
  00451281  752c                  jne       short 004512AFh
  00451283  50                    push      eax
  00451284  a1cc975e00            mov       eax,[5E97CCh]
  00451289  68b80b0000            push      0BB8h
  0045128e  6800180000            push      1800h
  00451293  8b88a8000000          mov       ecx,[eax+0A8h]
  00451299  6800080000            push      800h
  0045129e  8b11                  mov       edx,[ecx]
  004512a0  660fb64213            movzx     ax,byte [edx+13h]
  004512a5  50                    push      eax
  004512a6  56                    push      esi
  004512a7  e8346e0000            call      004580E0h
  004512ac  83c418                add       esp,18h

LAB_004512af:
  004512af  8b4e24                mov       ecx,[esi+24h]
  004512b2  668b5632              mov       dx,[esi+32h]
  004512b6  8b5e1c                mov       ebx,[esi+1Ch]
  004512b9  8b6e20                mov       ebp,[esi+20h]
  004512bc  6a00                  push      0
  004512be  6a00                  push      0
  004512c0  6aff                  push      0FFFFFFFFh
  004512c2  66897e14              mov       [esi+14h],di
  004512c6  6a00                  push      0
  004512c8  bf0020ccff            mov       edi,0FFCC2000h
  004512cd  6a3c                  push      3Ch
  004512cf  56                    push      esi
  004512d0  894c2430              mov       [esp+30h],ecx
  004512d4  6689542428            mov       [esp+28h],dx
  004512d9  c7461c00405100        mov       dword [esi+1Ch],514000h
  004512e0  897e20                mov       [esi+20h],edi
  004512e3  c74624009001ff        mov       dword [esi+24h],0FF019000h
  004512ea  66c74632c808          mov       word [esi+32h],8C8h
  004512f0  e81b6e0100            call      00468110h
  004512f5  83c418                add       esp,18h
  004512f8  66c74632c800          mov       word [esi+32h],0C8h
  004512fe  6a00                  push      0
  00451300  6a00                  push      0
  00451302  6aff                  push      0FFFFFFFFh
  00451304  6a00                  push      0
  00451306  6a3c                  push      3Ch
  00451308  56                    push      esi
  00451309  e8026e0100            call      00468110h
  0045130e  83c418                add       esp,18h
  00451311  66837e1200            cmp       word [esi+12h],0
  00451316  752d                  jne       short 00451345h
  00451318  a1cc975e00            mov       eax,[5E97CCh]
  0045131d  6a00                  push      0
  0045131f  68b80b0000            push      0BB8h
  00451324  6800180000            push      1800h
  00451329  8b88a8000000          mov       ecx,[eax+0A8h]
  0045132f  6800080000            push      800h
  00451334  8b11                  mov       edx,[ecx]
  00451336  660fb64213            movzx     ax,byte [edx+13h]
  0045133b  50                    push      eax
  0045133c  56                    push      esi
  0045133d  e89e6d0000            call      004580E0h
  00451342  83c418                add       esp,18h

LAB_00451345:
  00451345  6a00                  push      0
  00451347  6a00                  push      0
  00451349  6aff                  push      0FFFFFFFFh
  0045134b  6a00                  push      0
  0045134d  6a3c                  push      3Ch
  0045134f  56                    push      esi
  00451350  c7461c0000f2ff        mov       dword [esi+1Ch],0FFF20000h
  00451357  897e20                mov       [esi+20h],edi
  0045135a  c7462400a0cafe        mov       dword [esi+24h],0FECAA000h
  00451361  66c74632c804          mov       word [esi+32h],4C8h
  00451367  e8a46d0100            call      00468110h
  0045136c  83c418                add       esp,18h
  0045136f  66c74632380b          mov       word [esi+32h],0B38h
  00451375  6a00                  push      0
  00451377  6a00                  push      0
  00451379  6aff                  push      0FFFFFFFFh
  0045137b  6a00                  push      0
  0045137d  6a3c                  push      3Ch
  0045137f  56                    push      esi
  00451380  e88b6d0100            call      00468110h
  00451385  83c418                add       esp,18h
  00451388  66837e1200            cmp       word [esi+12h],0
  0045138d  752e                  jne       short 004513BDh
  0045138f  8b0dcc975e00          mov       ecx,[5E97CCh]
  00451395  6a00                  push      0
  00451397  68b80b0000            push      0BB8h
  0045139c  6800180000            push      1800h
  004513a1  8b91a8000000          mov       edx,[ecx+0A8h]
  004513a7  6800080000            push      800h
  004513ac  8b02                  mov       eax,[edx]
  004513ae  660fb64813            movzx     cx,byte [eax+13h]
  004513b3  51                    push      ecx
  004513b4  56                    push      esi
  004513b5  e8266d0000            call      004580E0h
  004513ba  83c418                add       esp,18h

LAB_004513bd:
  004513bd  6a00                  push      0
  004513bf  6a00                  push      0
  004513c1  6aff                  push      0FFFFFFFFh
  004513c3  6a00                  push      0
  004513c5  6a3c                  push      3Ch
  004513c7  56                    push      esi
  004513c8  c7461c00c0bdff        mov       dword [esi+1Ch],0FFBDC000h
  004513cf  897e20                mov       [esi+20h],edi
  004513d2  c7462400206dff        mov       dword [esi+24h],0FF6D2000h
  004513d9  66c74632000a          mov       word [esi+32h],0A00h
  004513df  e82c6d0100            call      00468110h
  004513e4  83c418                add       esp,18h
  004513e7  66c746320002          mov       word [esi+32h],200h
  004513ed  6a00                  push      0
  004513ef  6a00                  push      0
  004513f1  6aff                  push      0FFFFFFFFh
  004513f3  6a00                  push      0
  004513f5  6a3c                  push      3Ch
  004513f7  56                    push      esi
  004513f8  e8136d0100            call      00468110h
  004513fd  83c418                add       esp,18h
  00451400  66837e1200            cmp       word [esi+12h],0
  00451405  752e                  jne       short 00451435h
  00451407  8b15cc975e00          mov       edx,[5E97CCh]
  0045140d  6a00                  push      0
  0045140f  68b80b0000            push      0BB8h
  00451414  6800180000            push      1800h
  00451419  8b82a8000000          mov       eax,[edx+0A8h]
  0045141f  6800080000            push      800h
  00451424  8b08                  mov       ecx,[eax]
  00451426  660fb65113            movzx     dx,byte [ecx+13h]
  0045142b  52                    push      edx
  0045142c  56                    push      esi
  0045142d  e8ae6c0000            call      004580E0h
  00451432  83c418                add       esp,18h

LAB_00451435:
  00451435  8b442418              mov       eax,[esp+18h]
  00451439  668b4c2410            mov       cx,[esp+10h]
  0045143e  895e1c                mov       [esi+1Ch],ebx
  00451441  896e20                mov       [esi+20h],ebp
  00451444  894624                mov       [esi+24h],eax
  00451447  66894e32              mov       [esi+32h],cx
  0045144b  5f                    pop       edi
  0045144c  5e                    pop       esi
  0045144d  5d                    pop       ebp
  0045144e  5b                    pop       ebx
  0045144f  59                    pop       ecx
  00451450  c3                    ret

6 cible(s) de saut interne : 0x0045126e, 0x0045127d, 0x004512af, 0x00451345, 0x004513bd, 0x00451435
