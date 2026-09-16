Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00408ab0  section .text  offset fichier 0x7eb0  432 octets

  00408ab0  53                    push      ebx
  00408ab1  56                    push      esi
  00408ab2  8b74240c              mov       esi,[esp+0Ch]
  00408ab6  33db                  xor       ebx,ebx
  00408ab8  66395e10              cmp       [esi+10h],bx
  00408abc  0f8533010000          jne       near 00408BF5h
  00408ac2  c7463ca8f35400        mov       dword [esi+3Ch],54F3A8h
  00408ac9  e872e61300            call      00547140h
  00408ace  99                    cdq
  00408acf  33c2                  xor       eax,edx
  00408ad1  2bc2                  sub       eax,edx
  00408ad3  25ff030000            and       eax,3FFh
  00408ad8  33c2                  xor       eax,edx
  00408ada  2bc2                  sub       eax,edx
  00408adc  05000c0000            add       eax,0C00h
  00408ae1  66894628              mov       [esi+28h],ax
  00408ae5  e856e61300            call      00547140h
  00408aea  99                    cdq
  00408aeb  33c2                  xor       eax,edx
  00408aed  2bc2                  sub       eax,edx
  00408aef  25ff030000            and       eax,3FFh
  00408af4  33c2                  xor       eax,edx
  00408af6  2bc2                  sub       eax,edx
  00408af8  05000c0000            add       eax,0C00h
  00408afd  66837e1401            cmp       word [esi+14h],1
  00408b02  6689462a              mov       [esi+2Ah],ax
  00408b06  7506                  jne       short 00408B0Eh
  00408b08  c6460a08              mov       byte [esi+0Ah],8
  00408b0c  eb04                  jmp       short 00408B12h

LAB_00408b0e:
  00408b0e  c6460a06              mov       byte [esi+0Ah],6

LAB_00408b12:
  00408b12  885e0b                mov       [esi+0Bh],bl
  00408b15  e826e61300            call      00547140h
  00408b1a  99                    cdq
  00408b1b  b964000000            mov       ecx,64h
  00408b20  f7f9                  idiv      ecx
  00408b22  8b461c                mov       eax,[esi+1Ch]
  00408b25  83ea32                sub       edx,32h
  00408b28  c1e20c                shl       edx,0Ch
  00408b2b  03c2                  add       eax,edx
  00408b2d  89461c                mov       [esi+1Ch],eax
  00408b30  e80be61300            call      00547140h
  00408b35  99                    cdq
  00408b36  b932000000            mov       ecx,32h
  00408b3b  f7f9                  idiv      ecx
  00408b3d  8b4e20                mov       ecx,[esi+20h]
  00408b40  83ea19                sub       edx,19h
  00408b43  c1e20c                shl       edx,0Ch
  00408b46  03ca                  add       ecx,edx
  00408b48  894e20                mov       [esi+20h],ecx
  00408b4b  e8f0e51300            call      00547140h
  00408b50  99                    cdq
  00408b51  b964000000            mov       ecx,64h
  00408b56  f7f9                  idiv      ecx
  00408b58  8b4624                mov       eax,[esi+24h]
  00408b5b  83ea32                sub       edx,32h
  00408b5e  c1e20c                shl       edx,0Ch
  00408b61  03c2                  add       eax,edx
  00408b63  894624                mov       [esi+24h],eax
  00408b66  e8d5e51300            call      00547140h
  00408b6b  99                    cdq
  00408b6c  33c2                  xor       eax,edx
  00408b6e  2bc2                  sub       eax,edx
  00408b70  83e00f                and       eax,0Fh
  00408b73  33c2                  xor       eax,edx
  00408b75  2bc2                  sub       eax,edx
  00408b77  83e808                sub       eax,8
  00408b7a  c1e00c                shl       eax,0Ch
  00408b7d  894650                mov       [esi+50h],eax
  00408b80  e8bbe51300            call      00547140h
  00408b85  99                    cdq
  00408b86  b90a000000            mov       ecx,0Ah
  00408b8b  f7f9                  idiv      ecx
  00408b8d  83c206                add       edx,6
  00408b90  c1e20c                shl       edx,0Ch
  00408b93  895654                mov       [esi+54h],edx
  00408b96  e8a5e51300            call      00547140h
  00408b9b  99                    cdq
  00408b9c  33c2                  xor       eax,edx
  00408b9e  2bc2                  sub       eax,edx
  00408ba0  83e00f                and       eax,0Fh
  00408ba3  33c2                  xor       eax,edx
  00408ba5  2bc2                  sub       eax,edx
  00408ba7  83e808                sub       eax,8
  00408baa  c1e00c                shl       eax,0Ch
  00408bad  894658                mov       [esi+58h],eax
  00408bb0  e88be51300            call      00547140h
  00408bb5  99                    cdq
  00408bb6  33c2                  xor       eax,edx
  00408bb8  2bc2                  sub       eax,edx
  00408bba  25ff070000            and       eax,7FFh
  00408bbf  33c2                  xor       eax,edx
  00408bc1  2bc2                  sub       eax,edx
  00408bc3  0500080000            add       eax,800h
  00408bc8  66894628              mov       [esi+28h],ax
  00408bcc  e86fe51300            call      00547140h
  00408bd1  99                    cdq
  00408bd2  33c2                  xor       eax,edx
  00408bd4  66c746100100          mov       word [esi+10h],1
  00408bda  2bc2                  sub       eax,edx
  00408bdc  66895e12              mov       [esi+12h],bx
  00408be0  25ff070000            and       eax,7FFh
  00408be5  885e40                mov       [esi+40h],bl
  00408be8  33c2                  xor       eax,edx
  00408bea  2bc2                  sub       eax,edx
  00408bec  0500080000            add       eax,800h
  00408bf1  6689462a              mov       [esi+2Ah],ax

LAB_00408bf5:
  00408bf5  66837e1001            cmp       word [esi+10h],1
  00408bfa  7553                  jne       short 00408C4Fh
  00408bfc  8b5650                mov       edx,[esi+50h]
  00408bff  8b4e1c                mov       ecx,[esi+1Ch]
  00408c02  8b4620                mov       eax,[esi+20h]
  00408c05  03ca                  add       ecx,edx
  00408c07  8b5e24                mov       ebx,[esi+24h]
  00408c0a  894e1c                mov       [esi+1Ch],ecx
  00408c0d  8b4e54                mov       ecx,[esi+54h]
  00408c10  03c1                  add       eax,ecx
  00408c12  81c100f0ffff          add       ecx,0FFFFF000h
  00408c18  894620                mov       [esi+20h],eax
  00408c1b  8b4658                mov       eax,[esi+58h]
  00408c1e  03d8                  add       ebx,eax
  00408c20  66ff4612              inc       word [esi+12h]
  00408c24  668b4612              mov       ax,[esi+12h]
  00408c28  895e24                mov       [esi+24h],ebx
  00408c2b  663d1800              cmp       ax,18h
  00408c2f  894e54                mov       [esi+54h],ecx
  00408c32  7506                  jne       short 00408C3Ah
  00408c34  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00408c3a:
  00408c3a  663d0600              cmp       ax,6
  00408c3e  740c                  je        short 00408C4Ch
  00408c40  663d0c00              cmp       ax,0Ch
  00408c44  7406                  je        short 00408C4Ch
  00408c46  663d1200              cmp       ax,12h
  00408c4a  7503                  jne       short 00408C4Fh

LAB_00408c4c:
  00408c4c  fe4640                inc       byte [esi+40h]

LAB_00408c4f:
  00408c4f  5e                    pop       esi
  00408c50  5b                    pop       ebx
  00408c51  c3                    ret
  00408c52  90                    nop
  00408c53  90                    nop
  00408c54  90                    nop
  00408c55  90                    nop
  00408c56  90                    nop
  00408c57  90                    nop
  00408c58  90                    nop
  00408c59  90                    nop
  00408c5a  90                    nop
  00408c5b  90                    nop
  00408c5c  90                    nop
  00408c5d  90                    nop
  00408c5e  90                    nop
  00408c5f  90                    nop

6 cible(s) de saut interne : 0x00408b0e, 0x00408b12, 0x00408bf5, 0x00408c3a, 0x00408c4c, 0x00408c4f
