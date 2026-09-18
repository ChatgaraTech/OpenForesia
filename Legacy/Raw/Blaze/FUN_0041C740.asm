
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041c740  section .text  offset fichier 0x1bb40  448 octets

  0041c740  53                    push      ebx
  0041c741  56                    push      esi
  0041c742  8b74240c              mov       esi,[esp+0Ch]
  0041c746  33db                  xor       ebx,ebx
  0041c748  8b466c                mov       eax,[esi+6Ch]
  0041c74b  3bc3                  cmp       eax,ebx
  0041c74d  742c                  je        short 0041C77Bh
  0041c74f  8b481c                mov       ecx,[eax+1Ch]
  0041c752  894e1c                mov       [esi+1Ch],ecx
  0041c755  8b5020                mov       edx,[eax+20h]
  0041c758  895620                mov       [esi+20h],edx
  0041c75b  8b4824                mov       ecx,[eax+24h]
  0041c75e  894e24                mov       [esi+24h],ecx
  0041c761  8b08                  mov       ecx,[eax]
  0041c763  f7c100000040          test      ecx,40000000h
  0041c769  0f857a010000          jne       near 0041C8E9h
  0041c76f  f7c100000080          test      ecx,80000000h
  0041c775  0f846e010000          je        near 0041C8E9h

LAB_0041c77b:
  0041c77b  66395e10              cmp       [esi+10h],bx
  0041c77f  7526                  jne       short 0041C7A7h
  0041c781  53                    push      ebx
  0041c782  53                    push      ebx
  0041c783  6800130000            push      1300h
  0041c788  68fa0d0000            push      0DFAh
  0041c78d  6a10                  push      10h
  0041c78f  56                    push      esi
  0041c790  66c746100100          mov       word [esi+10h],1
  0041c796  66c746161400          mov       word [esi+16h],14h
  0041c79c  894674                mov       [esi+74h],eax
  0041c79f  e8dcb80400            call      00468080h
  0041c7a4  83c418                add       esp,18h

LAB_0041c7a7:
  0041c7a7  66837e1001            cmp       word [esi+10h],1
  0041c7ac  0f8590000000          jne       near 0041C842h
  0041c7b2  66ff4e16              dec       word [esi+16h]
  0041c7b6  66395e16              cmp       [esi+16h],bx
  0041c7ba  7f0d                  jg        short 0041C7C9h
  0041c7bc  66c746100200          mov       word [esi+10h],2
  0041c7c2  66899e98000000        mov       [esi+98h],bx

LAB_0041c7c9:
  0041c7c9  8b566c                mov       edx,[esi+6Ch]
  0041c7cc  c646043f              mov       byte [esi+4],3Fh
  0041c7d0  c6460a02              mov       byte [esi+0Ah],2
  0041c7d4  885e0b                mov       [esi+0Bh],bl
  0041c7d7  895670                mov       [esi+70h],edx
  0041c7da  895e44                mov       [esi+44h],ebx
  0041c7dd  c7464800000500        mov       dword [esi+48h],50000h
  0041c7e4  895e4c                mov       [esi+4Ch],ebx
  0041c7e7  e854a91200            call      00547140h
  0041c7ec  99                    cdq
  0041c7ed  33c2                  xor       eax,edx
  0041c7ef  2bc2                  sub       eax,edx
  0041c7f1  25ff030000            and       eax,3FFh
  0041c7f6  33c2                  xor       eax,edx
  0041c7f8  2bc2                  sub       eax,edx
  0041c7fa  0500040000            add       eax,400h
  0041c7ff  66894632              mov       [esi+32h],ax
  0041c803  e838a91200            call      00547140h
  0041c808  99                    cdq
  0041c809  33c2                  xor       eax,edx
  0041c80b  6aff                  push      0FFFFFFFFh
  0041c80d  2bc2                  sub       eax,edx
  0041c80f  53                    push      ebx
  0041c810  25ff0f0000            and       eax,0FFFh
  0041c815  6a70                  push      70h
  0041c817  33c2                  xor       eax,edx
  0041c819  56                    push      esi
  0041c81a  2bc2                  sub       eax,edx
  0041c81c  66895e14              mov       [esi+14h],bx
  0041c820  66894634              mov       [esi+34h],ax
  0041c824  66c74612c800          mov       word [esi+12h],0C8h
  0041c82a  66c74628ff1f          mov       word [esi+28h],1FFFh
  0041c830  66895e2a              mov       [esi+2Ah],bx
  0041c834  66c7462c0001          mov       word [esi+2Ch],100h
  0041c83a  e8a1e20500            call      0047AAE0h
  0041c83f  83c410                add       esp,10h

LAB_0041c842:
  0041c842  66837e1002            cmp       word [esi+10h],2
  0041c847  0f858b000000          jne       near 0041C8D8h
  0041c84d  6aff                  push      0FFFFFFFFh
  0041c84f  53                    push      ebx
  0041c850  68de000000            push      0DEh
  0041c855  56                    push      esi
  0041c856  66c746100300          mov       word [esi+10h],3
  0041c85c  66c746161e00          mov       word [esi+16h],1Eh
  0041c862  c7465c00040000        mov       dword [esi+5Ch],400h
  0041c869  e872e20500            call      0047AAE0h
  0041c86e  83c410                add       esp,10h
  0041c871  c7465c00fcffff        mov       dword [esi+5Ch],0FFFFFC00h
  0041c878  6aff                  push      0FFFFFFFFh
  0041c87a  53                    push      ebx
  0041c87b  68de000000            push      0DEh
  0041c880  56                    push      esi
  0041c881  e85ae20500            call      0047AAE0h
  0041c886  83c410                add       esp,10h
  0041c889  6683be9800000001      cmp       word [esi+98h],1
  0041c891  751e                  jne       short 0041C8B1h
  0041c893  53                    push      ebx
  0041c894  53                    push      ebx
  0041c895  6800130000            push      1300h
  0041c89a  6800100000            push      1000h
  0041c89f  6a1c                  push      1Ch
  0041c8a1  56                    push      esi
  0041c8a2  e8d9b70400            call      00468080h
  0041c8a7  83c418                add       esp,18h
  0041c8aa  66ff8698000000        inc       word [esi+98h]

LAB_0041c8b1:
  0041c8b1  66399e98000000        cmp       [esi+98h],bx
  0041c8b8  751e                  jne       short 0041C8D8h
  0041c8ba  53                    push      ebx
  0041c8bb  53                    push      ebx
  0041c8bc  6800130000            push      1300h
  0041c8c1  6800080000            push      800h
  0041c8c6  6a15                  push      15h
  0041c8c8  56                    push      esi
  0041c8c9  e8b2b70400            call      00468080h
  0041c8ce  83c418                add       esp,18h
  0041c8d1  66ff8698000000        inc       word [esi+98h]

LAB_0041c8d8:
  0041c8d8  66837e1003            cmp       word [esi+10h],3
  0041c8dd  7510                  jne       short 0041C8EFh
  0041c8df  66ff4e16              dec       word [esi+16h]
  0041c8e3  66395e16              cmp       [esi+16h],bx
  0041c8e7  7d06                  jge       short 0041C8EFh

LAB_0041c8e9:
  0041c8e9  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041c8ef:
  0041c8ef  5e                    pop       esi
  0041c8f0  5b                    pop       ebx
  0041c8f1  c3                    ret

8 cible(s) de saut interne : 0x0041c77b, 0x0041c7a7, 0x0041c7c9, 0x0041c842, 0x0041c8b1, 0x0041c8d8, 0x0041c8e9, 0x0041c8ef
