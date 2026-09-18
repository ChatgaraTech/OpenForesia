
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041fca0  section .text  offset fichier 0x1f0a0  608 octets

  0041fca0  53                    push      ebx
  0041fca1  55                    push      ebp
  0041fca2  56                    push      esi
  0041fca3  8b742410              mov       esi,[esp+10h]
  0041fca7  57                    push      edi
  0041fca8  8b466c                mov       eax,[esi+6Ch]
  0041fcab  8d6e1c                lea       ebp,[esi+1Ch]
  0041fcae  89442414              mov       [esp+14h],eax
  0041fcb2  8b481c                mov       ecx,[eax+1Ch]
  0041fcb5  894d00                mov       [ebp],ecx
  0041fcb8  8b5020                mov       edx,[eax+20h]
  0041fcbb  895620                mov       [esi+20h],edx
  0041fcbe  8b4824                mov       ecx,[eax+24h]
  0041fcc1  894e24                mov       [esi+24h],ecx
  0041fcc4  8b00                  mov       eax,[eax]
  0041fcc6  a900000040            test      eax,40000000h
  0041fccb  0f85e6010000          jne       near 0041FEB7h
  0041fcd1  a900000080            test      eax,80000000h
  0041fcd6  0f84db010000          je        near 0041FEB7h
  0041fcdc  33db                  xor       ebx,ebx
  0041fcde  66399e9a000000        cmp       [esi+9Ah],bx
  0041fce5  0f85ea000000          jne       near 0041FDD5h
  0041fceb  66395e10              cmp       [esi+10h],bx
  0041fcef  7565                  jne       short 0041FD56h
  0041fcf1  bf01000000            mov       edi,1
  0041fcf6  66895e12              mov       [esi+12h],bx
  0041fcfa  66897e10              mov       [esi+10h],di
  0041fcfe  8b15cc975e00          mov       edx,[5E97CCh]
  0041fd04  53                    push      ebx
  0041fd05  53                    push      ebx
  0041fd06  8b82a8000000          mov       eax,[edx+0A8h]
  0041fd0c  68002a0000            push      2A00h
  0041fd11  6800030000            push      300h
  0041fd16  8b08                  mov       ecx,[eax]
  0041fd18  660fb6512c            movzx     dx,byte [ecx+2Ch]
  0041fd1d  52                    push      edx
  0041fd1e  56                    push      esi
  0041fd1f  e85c830400            call      00468080h
  0041fd24  83c418                add       esp,18h
  0041fd27  897e5c                mov       [esi+5Ch],edi
  0041fd2a  e811741200            call      00547140h
  0041fd2f  99                    cdq
  0041fd30  33c2                  xor       eax,edx
  0041fd32  2bc2                  sub       eax,edx
  0041fd34  23c7                  and       eax,edi
  0041fd36  bf0b000000            mov       edi,0Bh
  0041fd3b  33c2                  xor       eax,edx
  0041fd3d  2bc2                  sub       eax,edx
  0041fd3f  894644                mov       [esi+44h],eax

LAB_0041fd42:
  0041fd42  6aff                  push      0FFFFFFFFh
  0041fd44  53                    push      ebx
  0041fd45  68df000000            push      0DFh
  0041fd4a  56                    push      esi
  0041fd4b  e890ad0500            call      0047AAE0h
  0041fd50  83c410                add       esp,10h
  0041fd53  4f                    dec       edi
  0041fd54  75ec                  jne       short 0041FD42h

LAB_0041fd56:
  0041fd56  b901000000            mov       ecx,1
  0041fd5b  66394e10              cmp       [esi+10h],cx
  0041fd5f  7574                  jne       short 0041FDD5h
  0041fd61  668b4612              mov       ax,[esi+12h]
  0041fd65  663bc3                cmp       ax,bx
  0041fd68  740c                  je        short 0041FD76h
  0041fd6a  663d0300              cmp       ax,3
  0041fd6e  7406                  je        short 0041FD76h
  0041fd70  663d0700              cmp       ax,7
  0041fd74  7523                  jne       short 0041FD99h

LAB_0041fd76:
  0041fd76  663bc3                cmp       ax,bx
  0041fd79  895e5c                mov       [esi+5Ch],ebx
  0041fd7c  7506                  jne       short 0041FD84h
  0041fd7e  66895e16              mov       [esi+16h],bx
  0041fd82  eb04                  jmp       short 0041FD88h

LAB_0041fd84:
  0041fd84  66894e16              mov       [esi+16h],cx

LAB_0041fd88:
  0041fd88  6aff                  push      0FFFFFFFFh
  0041fd8a  53                    push      ebx
  0041fd8b  68df000000            push      0DFh
  0041fd90  56                    push      esi
  0041fd91  e84aad0500            call      0047AAE0h
  0041fd96  83c410                add       esp,10h

LAB_0041fd99:
  0041fd99  66ff4612              inc       word [esi+12h]
  0041fd9d  668b4612              mov       ax,[esi+12h]
  0041fda1  b91e000000            mov       ecx,1Eh
  0041fda6  c60527e05d0001        mov       byte [5DE027h],1
  0041fdad  a801                  test      al,1
  0041fdaf  880d24e05d00          mov       [5DE024h],cl
  0041fdb5  7506                  jne       short 0041FDBDh
  0041fdb7  881d24e05d00          mov       [5DE024h],bl

LAB_0041fdbd:
  0041fdbd  881d25e05d00          mov       [5DE025h],bl
  0041fdc3  881d26e05d00          mov       [5DE026h],bl
  0041fdc9  66394e12              cmp       [esi+12h],cx
  0041fdcd  7c06                  jl        short 0041FDD5h
  0041fdcf  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041fdd5:
  0041fdd5  6683be9a00000002      cmp       word [esi+9Ah],2
  0041fddd  0f850b010000          jne       near 0041FEEEh
  0041fde3  66395e10              cmp       [esi+10h],bx
  0041fde7  7528                  jne       short 0041FE11h
  0041fde9  895e44                mov       [esi+44h],ebx
  0041fdec  bf0b000000            mov       edi,0Bh

LAB_0041fdf1:
  0041fdf1  6aff                  push      0FFFFFFFFh
  0041fdf3  53                    push      ebx
  0041fdf4  68df000000            push      0DFh
  0041fdf9  53                    push      ebx
  0041fdfa  55                    push      ebp
  0041fdfb  56                    push      esi
  0041fdfc  e83fae0500            call      0047AC40h
  0041fe01  83c418                add       esp,18h
  0041fe04  4f                    dec       edi
  0041fe05  75ea                  jne       short 0041FDF1h
  0041fe07  66c746100100          mov       word [esi+10h],1
  0041fe0d  66895e12              mov       [esi+12h],bx

LAB_0041fe11:
  0041fe11  b801000000            mov       eax,1
  0041fe16  66394610              cmp       [esi+10h],ax
  0041fe1a  7556                  jne       short 0041FE72h
  0041fe1c  66ff4612              inc       word [esi+12h]
  0041fe20  66837e1232            cmp       word [esi+12h],32h
  0041fe25  7e06                  jle       short 0041FE2Dh
  0041fe27  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041fe2d:
  0041fe2d  394644                cmp       [esi+44h],eax
  0041fe30  7540                  jne       short 0041FE72h
  0041fe32  66c746100200          mov       word [esi+10h],2
  0041fe38  e803731200            call      00547140h
  0041fe3d  99                    cdq
  0041fe3e  b90a000000            mov       ecx,0Ah
  0041fe43  53                    push      ebx
  0041fe44  f7f9                  idiv      ecx
  0041fe46  8b442418              mov       eax,[esp+18h]
  0041fe4a  66895e14              mov       [esi+14h],bx
  0041fe4e  83c20f                add       edx,0Fh
  0041fe51  66895612              mov       [esi+12h],dx
  0041fe55  8b5678                mov       edx,[esi+78h]
  0041fe58  52                    push      edx
  0041fe59  50                    push      eax
  0041fe5a  e8a1000000            call      0041FF00h
  0041fe5f  83c40c                add       esp,0Ch
  0041fe62  6685c0                test      ax,ax
  0041fe65  740b                  je        short 0041FE72h
  0041fe67  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041fe6d  5f                    pop       edi
  0041fe6e  5e                    pop       esi
  0041fe6f  5d                    pop       ebp
  0041fe70  5b                    pop       ebx
  0041fe71  c3                    ret

LAB_0041fe72:
  0041fe72  66837e1002            cmp       word [esi+10h],2
  0041fe77  7575                  jne       short 0041FEEEh
  0041fe79  66ff4614              inc       word [esi+14h]
  0041fe7d  66837e1403            cmp       word [esi+14h],3
  0041fe82  756a                  jne       short 0041FEEEh
  0041fe84  66395e12              cmp       [esi+12h],bx
  0041fe88  66895e14              mov       [esi+14h],bx
  0041fe8c  7e13                  jle       short 0041FEA1h
  0041fe8e  6aff                  push      0FFFFFFFFh
  0041fe90  53                    push      ebx
  0041fe91  68df000000            push      0DFh
  0041fe96  53                    push      ebx
  0041fe97  55                    push      ebp
  0041fe98  56                    push      esi
  0041fe99  e8a2ad0500            call      0047AC40h
  0041fe9e  83c418                add       esp,18h

LAB_0041fea1:
  0041fea1  66ff4e12              dec       word [esi+12h]
  0041fea5  66837e12dd            cmp       word [esi+12h],0FFDDh
  0041feaa  7f42                  jg        short 0041FEEEh
  0041feac  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041feb2  5f                    pop       edi
  0041feb3  5e                    pop       esi
  0041feb4  5d                    pop       ebp
  0041feb5  5b                    pop       ebx
  0041feb6  c3                    ret

LAB_0041feb7:
  0041feb7  8b0e                  mov       ecx,[esi]
  0041feb9  33db                  xor       ebx,ebx
  0041febb  81e1ffffff7f          and       ecx,7FFFFFFFh
  0041fec1  890e                  mov       [esi],ecx
  0041fec3  668bb69a000000        mov       si,[esi+9Ah]
  0041feca  663bf3                cmp       si,bx
  0041fecd  7406                  je        short 0041FED5h
  0041fecf  6683fe01              cmp       si,1
  0041fed3  7519                  jne       short 0041FEEEh

LAB_0041fed5:
  0041fed5  c60527e05d0001        mov       byte [5DE027h],1
  0041fedc  881d24e05d00          mov       [5DE024h],bl
  0041fee2  881d25e05d00          mov       [5DE025h],bl
  0041fee8  881d26e05d00          mov       [5DE026h],bl

LAB_0041feee:
  0041feee  5f                    pop       edi
  0041feef  5e                    pop       esi
  0041fef0  5d                    pop       ebp
  0041fef1  5b                    pop       ebx
  0041fef2  c3                    ret

16 cible(s) de saut interne : 0x0041fd42, 0x0041fd56, 0x0041fd76, 0x0041fd84, 0x0041fd88, 0x0041fd99, 0x0041fdbd, 0x0041fdd5, 0x0041fdf1, 0x0041fe11, 0x0041fe2d, 0x0041fe72, 0x0041fea1, 0x0041feb7, 0x0041fed5, 0x0041feee
