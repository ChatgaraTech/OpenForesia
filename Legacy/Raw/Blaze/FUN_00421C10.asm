
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00421c10  section .text  offset fichier 0x21010  368 octets

  00421c10  53                    push      ebx
  00421c11  55                    push      ebp
  00421c12  56                    push      esi
  00421c13  8b742410              mov       esi,[esp+10h]
  00421c17  57                    push      edi
  00421c18  8b5e6c                mov       ebx,[esi+6Ch]
  00421c1b  85db                  test      ebx,ebx
  00421c1d  7505                  jne       short 00421C24h
  00421c1f  bb58e55d00            mov       ebx,5DE558h

LAB_00421c24:
  00421c24  8b431c                mov       eax,[ebx+1Ch]
  00421c27  8d6e1c                lea       ebp,[esi+1Ch]
  00421c2a  894500                mov       [ebp],eax
  00421c2d  8b4b20                mov       ecx,[ebx+20h]
  00421c30  894e20                mov       [esi+20h],ecx
  00421c33  8b5324                mov       edx,[ebx+24h]
  00421c36  895624                mov       [esi+24h],edx
  00421c39  8b03                  mov       eax,[ebx]
  00421c3b  a900000040            test      eax,40000000h
  00421c40  0f85fc000000          jne       near 00421D42h
  00421c46  a900000080            test      eax,80000000h
  00421c4b  0f84f1000000          je        near 00421D42h
  00421c51  66837e1000            cmp       word [esi+10h],0
  00421c56  7535                  jne       short 00421C8Dh
  00421c58  c7464400000000        mov       dword [esi+44h],0
  00421c5f  bf0b000000            mov       edi,0Bh

LAB_00421c64:
  00421c64  6aff                  push      0FFFFFFFFh
  00421c66  6a00                  push      0
  00421c68  68ed000000            push      0EDh
  00421c6d  6a00                  push      0
  00421c6f  55                    push      ebp
  00421c70  56                    push      esi
  00421c71  e8ca8f0500            call      0047AC40h
  00421c76  83c418                add       esp,18h
  00421c79  4f                    dec       edi
  00421c7a  75e8                  jne       short 00421C64h
  00421c7c  b801000000            mov       eax,1
  00421c81  66c746120000          mov       word [esi+12h],0
  00421c87  66894610              mov       [esi+10h],ax
  00421c8b  eb05                  jmp       short 00421C92h

LAB_00421c8d:
  00421c8d  b801000000            mov       eax,1

LAB_00421c92:
  00421c92  66394610              cmp       [esi+10h],ax
  00421c96  bf02000000            mov       edi,2
  00421c9b  755c                  jne       short 00421CF9h
  00421c9d  66ff4612              inc       word [esi+12h]
  00421ca1  66837e1232            cmp       word [esi+12h],32h
  00421ca6  7e06                  jle       short 00421CAEh
  00421ca8  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00421cae:
  00421cae  394644                cmp       [esi+44h],eax
  00421cb1  7546                  jne       short 00421CF9h
  00421cb3  66897e10              mov       [esi+10h],di
  00421cb7  e884541200            call      00547140h
  00421cbc  99                    cdq
  00421cbd  b90a000000            mov       ecx,0Ah
  00421cc2  66c746140000          mov       word [esi+14h],0
  00421cc8  f7f9                  idiv      ecx
  00421cca  8b4678                mov       eax,[esi+78h]
  00421ccd  83c20f                add       edx,0Fh
  00421cd0  85c0                  test      eax,eax
  00421cd2  66895612              mov       [esi+12h],dx
  00421cd6  7505                  jne       short 00421CDDh
  00421cd8  b858e55d00            mov       eax,5DE558h

LAB_00421cdd:
  00421cdd  6a00                  push      0
  00421cdf  50                    push      eax
  00421ce0  53                    push      ebx
  00421ce1  e89a000000            call      00421D80h
  00421ce6  83c40c                add       esp,0Ch
  00421ce9  6685c0                test      ax,ax
  00421cec  740b                  je        short 00421CF9h
  00421cee  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00421cf4  5f                    pop       edi
  00421cf5  5e                    pop       esi
  00421cf6  5d                    pop       ebp
  00421cf7  5b                    pop       ebx
  00421cf8  c3                    ret

LAB_00421cf9:
  00421cf9  66397e10              cmp       [esi+10h],di
  00421cfd  757b                  jne       short 00421D7Ah
  00421cff  66ff4614              inc       word [esi+14h]
  00421d03  66837e1403            cmp       word [esi+14h],3
  00421d08  7570                  jne       short 00421D7Ah
  00421d0a  66837e1200            cmp       word [esi+12h],0
  00421d0f  66c746140000          mov       word [esi+14h],0
  00421d15  7e15                  jle       short 00421D2Ch
  00421d17  6aff                  push      0FFFFFFFFh
  00421d19  6a00                  push      0
  00421d1b  68ed000000            push      0EDh
  00421d20  6a00                  push      0
  00421d22  55                    push      ebp
  00421d23  56                    push      esi
  00421d24  e8178f0500            call      0047AC40h
  00421d29  83c418                add       esp,18h

LAB_00421d2c:
  00421d2c  66ff4e12              dec       word [esi+12h]
  00421d30  66837e12dd            cmp       word [esi+12h],0FFDDh
  00421d35  7f43                  jg        short 00421D7Ah
  00421d37  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00421d3d  5f                    pop       edi
  00421d3e  5e                    pop       esi
  00421d3f  5d                    pop       ebp
  00421d40  5b                    pop       ebx
  00421d41  c3                    ret

LAB_00421d42:
  00421d42  8b0e                  mov       ecx,[esi]
  00421d44  81e1ffffff7f          and       ecx,7FFFFFFFh
  00421d4a  890e                  mov       [esi],ecx
  00421d4c  668bb69a000000        mov       si,[esi+9Ah]
  00421d53  6685f6                test      si,si
  00421d56  7406                  je        short 00421D5Eh
  00421d58  6683fe01              cmp       si,1
  00421d5c  751c                  jne       short 00421D7Ah

LAB_00421d5e:
  00421d5e  c60527e05d0001        mov       byte [5DE027h],1
  00421d65  c60524e05d0000        mov       byte [5DE024h],0
  00421d6c  c60525e05d0000        mov       byte [5DE025h],0
  00421d73  c60526e05d0000        mov       byte [5DE026h],0

LAB_00421d7a:
  00421d7a  5f                    pop       edi
  00421d7b  5e                    pop       esi
  00421d7c  5d                    pop       ebp
  00421d7d  5b                    pop       ebx
  00421d7e  c3                    ret

11 cible(s) de saut interne : 0x00421c24, 0x00421c64, 0x00421c8d, 0x00421c92, 0x00421cae, 0x00421cdd, 0x00421cf9, 0x00421d2c, 0x00421d42, 0x00421d5e, 0x00421d7a
