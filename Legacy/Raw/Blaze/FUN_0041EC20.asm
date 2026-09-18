
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041ec20  section .text  offset fichier 0x1e020  432 octets

  0041ec20  53                    push      ebx
  0041ec21  56                    push      esi
  0041ec22  8b74240c              mov       esi,[esp+0Ch]
  0041ec26  8b466c                mov       eax,[esi+6Ch]
  0041ec29  8b481c                mov       ecx,[eax+1Ch]
  0041ec2c  894e1c                mov       [esi+1Ch],ecx
  0041ec2f  8b5020                mov       edx,[eax+20h]
  0041ec32  895620                mov       [esi+20h],edx
  0041ec35  8b4824                mov       ecx,[eax+24h]
  0041ec38  894e24                mov       [esi+24h],ecx
  0041ec3b  8b08                  mov       ecx,[eax]
  0041ec3d  f7c100000040          test      ecx,40000000h
  0041ec43  0f857c010000          jne       near 0041EDC5h
  0041ec49  f7c100000080          test      ecx,80000000h
  0041ec4f  0f8470010000          je        near 0041EDC5h
  0041ec55  33db                  xor       ebx,ebx
  0041ec57  66395e10              cmp       [esi+10h],bx
  0041ec5b  7526                  jne       short 0041EC83h
  0041ec5d  53                    push      ebx
  0041ec5e  53                    push      ebx
  0041ec5f  6800130000            push      1300h
  0041ec64  68fa0d0000            push      0DFAh
  0041ec69  6a10                  push      10h
  0041ec6b  56                    push      esi
  0041ec6c  66c746100100          mov       word [esi+10h],1
  0041ec72  66c746161400          mov       word [esi+16h],14h
  0041ec78  894674                mov       [esi+74h],eax
  0041ec7b  e800940400            call      00468080h
  0041ec80  83c418                add       esp,18h

LAB_0041ec83:
  0041ec83  66837e1001            cmp       word [esi+10h],1
  0041ec88  0f8590000000          jne       near 0041ED1Eh
  0041ec8e  66ff4e16              dec       word [esi+16h]
  0041ec92  66395e16              cmp       [esi+16h],bx
  0041ec96  7f0d                  jg        short 0041ECA5h
  0041ec98  66c746100200          mov       word [esi+10h],2
  0041ec9e  66899e98000000        mov       [esi+98h],bx

LAB_0041eca5:
  0041eca5  8b566c                mov       edx,[esi+6Ch]
  0041eca8  c646043f              mov       byte [esi+4],3Fh
  0041ecac  c6460a02              mov       byte [esi+0Ah],2
  0041ecb0  885e0b                mov       [esi+0Bh],bl
  0041ecb3  895670                mov       [esi+70h],edx
  0041ecb6  895e44                mov       [esi+44h],ebx
  0041ecb9  c7464800000500        mov       dword [esi+48h],50000h
  0041ecc0  895e4c                mov       [esi+4Ch],ebx
  0041ecc3  e878841200            call      00547140h
  0041ecc8  99                    cdq
  0041ecc9  33c2                  xor       eax,edx
  0041eccb  2bc2                  sub       eax,edx
  0041eccd  25ff030000            and       eax,3FFh
  0041ecd2  33c2                  xor       eax,edx
  0041ecd4  2bc2                  sub       eax,edx
  0041ecd6  0500040000            add       eax,400h
  0041ecdb  66894632              mov       [esi+32h],ax
  0041ecdf  e85c841200            call      00547140h
  0041ece4  99                    cdq
  0041ece5  33c2                  xor       eax,edx
  0041ece7  6aff                  push      0FFFFFFFFh
  0041ece9  2bc2                  sub       eax,edx
  0041eceb  53                    push      ebx
  0041ecec  25ff0f0000            and       eax,0FFFh
  0041ecf1  6a70                  push      70h
  0041ecf3  33c2                  xor       eax,edx
  0041ecf5  56                    push      esi
  0041ecf6  2bc2                  sub       eax,edx
  0041ecf8  66895e14              mov       [esi+14h],bx
  0041ecfc  66894634              mov       [esi+34h],ax
  0041ed00  66c74612c800          mov       word [esi+12h],0C8h
  0041ed06  66c74628ff1f          mov       word [esi+28h],1FFFh
  0041ed0c  66895e2a              mov       [esi+2Ah],bx
  0041ed10  66c7462c0001          mov       word [esi+2Ch],100h
  0041ed16  e8c5bd0500            call      0047AAE0h
  0041ed1b  83c410                add       esp,10h

LAB_0041ed1e:
  0041ed1e  66837e1002            cmp       word [esi+10h],2
  0041ed23  0f858b000000          jne       near 0041EDB4h
  0041ed29  6aff                  push      0FFFFFFFFh
  0041ed2b  53                    push      ebx
  0041ed2c  68de000000            push      0DEh
  0041ed31  56                    push      esi
  0041ed32  66c746100300          mov       word [esi+10h],3
  0041ed38  66c746161e00          mov       word [esi+16h],1Eh
  0041ed3e  c7465c00040000        mov       dword [esi+5Ch],400h
  0041ed45  e896bd0500            call      0047AAE0h
  0041ed4a  83c410                add       esp,10h
  0041ed4d  c7465c00fcffff        mov       dword [esi+5Ch],0FFFFFC00h
  0041ed54  6aff                  push      0FFFFFFFFh
  0041ed56  53                    push      ebx
  0041ed57  68de000000            push      0DEh
  0041ed5c  56                    push      esi
  0041ed5d  e87ebd0500            call      0047AAE0h
  0041ed62  83c410                add       esp,10h
  0041ed65  6683be9800000001      cmp       word [esi+98h],1
  0041ed6d  751e                  jne       short 0041ED8Dh
  0041ed6f  53                    push      ebx
  0041ed70  53                    push      ebx
  0041ed71  6800130000            push      1300h
  0041ed76  6800100000            push      1000h
  0041ed7b  6a1c                  push      1Ch
  0041ed7d  56                    push      esi
  0041ed7e  e8fd920400            call      00468080h
  0041ed83  83c418                add       esp,18h
  0041ed86  66ff8698000000        inc       word [esi+98h]

LAB_0041ed8d:
  0041ed8d  66399e98000000        cmp       [esi+98h],bx
  0041ed94  751e                  jne       short 0041EDB4h
  0041ed96  53                    push      ebx
  0041ed97  53                    push      ebx
  0041ed98  6800130000            push      1300h
  0041ed9d  6800080000            push      800h
  0041eda2  6a15                  push      15h
  0041eda4  56                    push      esi
  0041eda5  e8d6920400            call      00468080h
  0041edaa  83c418                add       esp,18h
  0041edad  66ff8698000000        inc       word [esi+98h]

LAB_0041edb4:
  0041edb4  66837e1003            cmp       word [esi+10h],3
  0041edb9  7510                  jne       short 0041EDCBh
  0041edbb  66ff4e16              dec       word [esi+16h]
  0041edbf  66395e16              cmp       [esi+16h],bx
  0041edc3  7d06                  jge       short 0041EDCBh

LAB_0041edc5:
  0041edc5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041edcb:
  0041edcb  5e                    pop       esi
  0041edcc  5b                    pop       ebx
  0041edcd  c3                    ret

7 cible(s) de saut interne : 0x0041ec83, 0x0041eca5, 0x0041ed1e, 0x0041ed8d, 0x0041edb4, 0x0041edc5, 0x0041edcb
