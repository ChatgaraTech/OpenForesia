
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041d1a0  section .text  offset fichier 0x1c5a0  368 octets

  0041d1a0  53                    push      ebx
  0041d1a1  55                    push      ebp
  0041d1a2  56                    push      esi
  0041d1a3  8b742410              mov       esi,[esp+10h]
  0041d1a7  57                    push      edi
  0041d1a8  8b5e6c                mov       ebx,[esi+6Ch]
  0041d1ab  85db                  test      ebx,ebx
  0041d1ad  0f844c010000          je        near 0041D2FFh
  0041d1b3  8b431c                mov       eax,[ebx+1Ch]
  0041d1b6  8d6e1c                lea       ebp,[esi+1Ch]
  0041d1b9  894500                mov       [ebp],eax
  0041d1bc  8b4b20                mov       ecx,[ebx+20h]
  0041d1bf  894e20                mov       [esi+20h],ecx
  0041d1c2  8b5324                mov       edx,[ebx+24h]
  0041d1c5  895624                mov       [esi+24h],edx
  0041d1c8  8b03                  mov       eax,[ebx]
  0041d1ca  a900000040            test      eax,40000000h
  0041d1cf  0f85f2000000          jne       near 0041D2C7h
  0041d1d5  a900000080            test      eax,80000000h
  0041d1da  0f84e7000000          je        near 0041D2C7h
  0041d1e0  66837e1000            cmp       word [esi+10h],0
  0041d1e5  7535                  jne       short 0041D21Ch
  0041d1e7  c7464400000000        mov       dword [esi+44h],0
  0041d1ee  bf0b000000            mov       edi,0Bh

LAB_0041d1f3:
  0041d1f3  6aff                  push      0FFFFFFFFh
  0041d1f5  6a00                  push      0
  0041d1f7  68ed000000            push      0EDh
  0041d1fc  6a00                  push      0
  0041d1fe  55                    push      ebp
  0041d1ff  56                    push      esi
  0041d200  e83bda0500            call      0047AC40h
  0041d205  83c418                add       esp,18h
  0041d208  4f                    dec       edi
  0041d209  75e8                  jne       short 0041D1F3h
  0041d20b  b801000000            mov       eax,1
  0041d210  66c746120000          mov       word [esi+12h],0
  0041d216  66894610              mov       [esi+10h],ax
  0041d21a  eb05                  jmp       short 0041D221h

LAB_0041d21c:
  0041d21c  b801000000            mov       eax,1

LAB_0041d221:
  0041d221  66394610              cmp       [esi+10h],ax
  0041d225  bf02000000            mov       edi,2
  0041d22a  7552                  jne       short 0041D27Eh
  0041d22c  66ff4612              inc       word [esi+12h]
  0041d230  66837e1232            cmp       word [esi+12h],32h
  0041d235  7e06                  jle       short 0041D23Dh
  0041d237  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041d23d:
  0041d23d  394644                cmp       [esi+44h],eax
  0041d240  753c                  jne       short 0041D27Eh
  0041d242  66897e10              mov       [esi+10h],di
  0041d246  e8f59e1200            call      00547140h
  0041d24b  99                    cdq
  0041d24c  b90a000000            mov       ecx,0Ah
  0041d251  6a00                  push      0
  0041d253  f7f9                  idiv      ecx
  0041d255  66c746140000          mov       word [esi+14h],0
  0041d25b  83c20f                add       edx,0Fh
  0041d25e  66895612              mov       [esi+12h],dx
  0041d262  8b5678                mov       edx,[esi+78h]
  0041d265  52                    push      edx
  0041d266  53                    push      ebx
  0041d267  e874f31100            call      0053C5E0h
  0041d26c  83c40c                add       esp,0Ch
  0041d26f  85c0                  test      eax,eax
  0041d271  740b                  je        short 0041D27Eh
  0041d273  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041d279  5f                    pop       edi
  0041d27a  5e                    pop       esi
  0041d27b  5d                    pop       ebp
  0041d27c  5b                    pop       ebx
  0041d27d  c3                    ret

LAB_0041d27e:
  0041d27e  66397e10              cmp       [esi+10h],di
  0041d282  757b                  jne       short 0041D2FFh
  0041d284  66ff4614              inc       word [esi+14h]
  0041d288  66837e1403            cmp       word [esi+14h],3
  0041d28d  7570                  jne       short 0041D2FFh
  0041d28f  66837e1200            cmp       word [esi+12h],0
  0041d294  66c746140000          mov       word [esi+14h],0
  0041d29a  7e15                  jle       short 0041D2B1h
  0041d29c  6aff                  push      0FFFFFFFFh
  0041d29e  6a00                  push      0
  0041d2a0  68ed000000            push      0EDh
  0041d2a5  6a00                  push      0
  0041d2a7  55                    push      ebp
  0041d2a8  56                    push      esi
  0041d2a9  e892d90500            call      0047AC40h
  0041d2ae  83c418                add       esp,18h

LAB_0041d2b1:
  0041d2b1  66ff4e12              dec       word [esi+12h]
  0041d2b5  66837e12dd            cmp       word [esi+12h],0FFDDh
  0041d2ba  7f43                  jg        short 0041D2FFh
  0041d2bc  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041d2c2  5f                    pop       edi
  0041d2c3  5e                    pop       esi
  0041d2c4  5d                    pop       ebp
  0041d2c5  5b                    pop       ebx
  0041d2c6  c3                    ret

LAB_0041d2c7:
  0041d2c7  8b0e                  mov       ecx,[esi]
  0041d2c9  81e1ffffff7f          and       ecx,7FFFFFFFh
  0041d2cf  890e                  mov       [esi],ecx
  0041d2d1  668bb69a000000        mov       si,[esi+9Ah]
  0041d2d8  6685f6                test      si,si
  0041d2db  7406                  je        short 0041D2E3h
  0041d2dd  6683fe01              cmp       si,1
  0041d2e1  751c                  jne       short 0041D2FFh

LAB_0041d2e3:
  0041d2e3  c60527e05d0001        mov       byte [5DE027h],1
  0041d2ea  c60524e05d0000        mov       byte [5DE024h],0
  0041d2f1  c60525e05d0000        mov       byte [5DE025h],0
  0041d2f8  c60526e05d0000        mov       byte [5DE026h],0

LAB_0041d2ff:
  0041d2ff  5f                    pop       edi
  0041d300  5e                    pop       esi
  0041d301  5d                    pop       ebp
  0041d302  5b                    pop       ebx
  0041d303  c3                    ret

9 cible(s) de saut interne : 0x0041d1f3, 0x0041d21c, 0x0041d221, 0x0041d23d, 0x0041d27e, 0x0041d2b1, 0x0041d2c7, 0x0041d2e3, 0x0041d2ff
