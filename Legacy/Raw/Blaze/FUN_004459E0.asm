
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004459e0  section .text  offset fichier 0x44de0  773 octets

  004459e0  53                    push      ebx
  004459e1  56                    push      esi
  004459e2  8b74240c              mov       esi,[esp+0Ch]
  004459e6  33db                  xor       ebx,ebx
  004459e8  57                    push      edi
  004459e9  bf01000000            mov       edi,1
  004459ee  395e44                cmp       [esi+44h],ebx
  004459f1  0f8560010000          jne       near 00445B57h
  004459f7  66395e10              cmp       [esi+10h],bx
  004459fb  0f85b9000000          jne       near 00445ABAh
  00445a01  0fbf4614              movsx     eax,word [esi+14h]
  00445a05  8b5620                mov       edx,[esi+20h]
  00445a08  8b4e1c                mov       ecx,[esi+1Ch]
  00445a0b  24fe                  and       al,0FEh
  00445a0d  c7463cb8155500        mov       dword [esi+3Ch],5515B8h
  00445a14  c1e00b                shl       eax,0Bh
  00445a17  03d0                  add       edx,eax
  00445a19  885e40                mov       [esi+40h],bl
  00445a1c  895620                mov       [esi+20h],edx
  00445a1f  8bc2                  mov       eax,edx
  00445a21  8b5624                mov       edx,[esi+24h]
  00445a24  c6460a09              mov       byte [esi+0Ah],9
  00445a28  885e0b                mov       [esi+0Bh],bl
  00445a2b  c64638b4              mov       byte [esi+38h],0B4h
  00445a2f  885e39                mov       [esi+39h],bl
  00445a32  c6463ab4              mov       byte [esi+3Ah],0B4h
  00445a36  894e50                mov       [esi+50h],ecx
  00445a39  894654                mov       [esi+54h],eax
  00445a3c  895658                mov       [esi+58h],edx
  00445a3f  e8fc161000            call      00547140h
  00445a44  99                    cdq
  00445a45  33c2                  xor       eax,edx
  00445a47  2bc2                  sub       eax,edx
  00445a49  25ff0f0000            and       eax,0FFFh
  00445a4e  33c2                  xor       eax,edx
  00445a50  2bc2                  sub       eax,edx
  00445a52  6689869a000000        mov       [esi+9Ah],ax
  00445a59  e8e2161000            call      00547140h
  00445a5e  0fbf4e14              movsx     ecx,word [esi+14h]
  00445a62  d1f9                  sar       ecx,1
  00445a64  99                    cdq
  00445a65  41                    inc       ecx
  00445a66  66897e10              mov       [esi+10h],di
  00445a6a  f7f9                  idiv      ecx
  00445a6c  89565c                mov       [esi+5Ch],edx
  00445a6f  e8cc161000            call      00547140h
  00445a74  99                    cdq
  00445a75  33c2                  xor       eax,edx
  00445a77  2bc2                  sub       eax,edx
  00445a79  25ff030000            and       eax,3FFh
  00445a7e  33c2                  xor       eax,edx
  00445a80  2bc2                  sub       eax,edx
  00445a82  0500040000            add       eax,400h
  00445a87  66894628              mov       [esi+28h],ax
  00445a8b  6689462a              mov       [esi+2Ah],ax
  00445a8f  e8ac161000            call      00547140h
  00445a94  99                    cdq
  00445a95  b903000000            mov       ecx,3
  00445a9a  f7f9                  idiv      ecx
  00445a9c  03d1                  add       edx,ecx
  00445a9e  66895612              mov       [esi+12h],dx
  00445aa2  e899161000            call      00547140h
  00445aa7  99                    cdq
  00445aa8  33c2                  xor       eax,edx
  00445aaa  2bc2                  sub       eax,edx
  00445aac  83e00f                and       eax,0Fh
  00445aaf  33c2                  xor       eax,edx
  00445ab1  2bc2                  sub       eax,edx
  00445ab3  83c008                add       eax,8
  00445ab6  66894616              mov       [esi+16h],ax

LAB_00445aba:
  00445aba  66397e10              cmp       [esi+10h],di
  00445abe  0f8593000000          jne       near 00445B57h
  00445ac4  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00445acb  52                    push      edx
  00445acc  e84bff0f00            call      00545A1Ch
  00445ad1  0faf465c              imul      eax,[esi+5Ch]
  00445ad5  034650                add       eax,[esi+50h]
  00445ad8  89461c                mov       [esi+1Ch],eax
  00445adb  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00445ae2  50                    push      eax
  00445ae3  e82eff0f00            call      00545A16h
  00445ae8  0faf465c              imul      eax,[esi+5Ch]
  00445aec  034658                add       eax,[esi+58h]
  00445aef  894624                mov       [esi+24h],eax
  00445af2  e849161000            call      00547140h
  00445af7  99                    cdq
  00445af8  b932000000            mov       ecx,32h
  00445afd  f7f9                  idiv      ecx
  00445aff  668b4612              mov       ax,[esi+12h]
  00445b03  8b4e20                mov       ecx,[esi+20h]
  00445b06  6603969a000000        add       dx,[esi+9Ah]
  00445b0d  83c264                add       edx,64h
  00445b10  81e2ff0f0000          and       edx,0FFFh
  00445b16  6689969a000000        mov       [esi+9Ah],dx
  00445b1d  0fbfd0                movsx     edx,ax
  00445b20  c1e20c                shl       edx,0Ch
  00445b23  03ca                  add       ecx,edx
  00445b25  40                    inc       eax
  00445b26  66894612              mov       [esi+12h],ax
  00445b2a  668b4616              mov       ax,[esi+16h]
  00445b2e  66f7d8                neg       ax
  00445b31  50                    push      eax
  00445b32  50                    push      eax
  00445b33  50                    push      eax
  00445b34  56                    push      esi
  00445b35  894e20                mov       [esi+20h],ecx
  00445b38  e8a3f2fbff            call      00404DE0h
  00445b3d  8a4638                mov       al,[esi+38h]
  00445b40  83c410                add       esp,10h
  00445b43  3ac3                  cmp       al,bl
  00445b45  7510                  jne       short 00445B57h
  00445b47  385e39                cmp       [esi+39h],bl
  00445b4a  750b                  jne       short 00445B57h
  00445b4c  385e3a                cmp       [esi+3Ah],bl
  00445b4f  7506                  jne       short 00445B57h
  00445b51  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00445b57:
  00445b57  397e44                cmp       [esi+44h],edi
  00445b5a  0f8581010000          jne       near 00445CE1h
  00445b60  66395e10              cmp       [esi+10h],bx
  00445b64  0f85b7000000          jne       near 00445C21h
  00445b6a  0fbf4614              movsx     eax,word [esi+14h]
  00445b6e  8b5620                mov       edx,[esi+20h]
  00445b71  8b4e1c                mov       ecx,[esi+1Ch]
  00445b74  24fe                  and       al,0FEh
  00445b76  c7463cc0155500        mov       dword [esi+3Ch],5515C0h
  00445b7d  c1e00b                shl       eax,0Bh
  00445b80  03d0                  add       edx,eax
  00445b82  885e40                mov       [esi+40h],bl
  00445b85  895620                mov       [esi+20h],edx
  00445b88  8bc2                  mov       eax,edx
  00445b8a  8b5624                mov       edx,[esi+24h]
  00445b8d  c6460a09              mov       byte [esi+0Ah],9
  00445b91  885e0b                mov       [esi+0Bh],bl
  00445b94  885e38                mov       [esi+38h],bl
  00445b97  c64639b4              mov       byte [esi+39h],0B4h
  00445b9b  c6463ac8              mov       byte [esi+3Ah],0C8h
  00445b9f  894e50                mov       [esi+50h],ecx
  00445ba2  894654                mov       [esi+54h],eax
  00445ba5  895658                mov       [esi+58h],edx
  00445ba8  e893151000            call      00547140h
  00445bad  99                    cdq
  00445bae  33c2                  xor       eax,edx
  00445bb0  2bc2                  sub       eax,edx
  00445bb2  25ff0f0000            and       eax,0FFFh
  00445bb7  33c2                  xor       eax,edx
  00445bb9  2bc2                  sub       eax,edx
  00445bbb  6689869a000000        mov       [esi+9Ah],ax
  00445bc2  e879151000            call      00547140h
  00445bc7  0fbf4e14              movsx     ecx,word [esi+14h]
  00445bcb  d1f9                  sar       ecx,1
  00445bcd  99                    cdq
  00445bce  41                    inc       ecx
  00445bcf  66897e10              mov       [esi+10h],di
  00445bd3  f7f9                  idiv      ecx
  00445bd5  89565c                mov       [esi+5Ch],edx
  00445bd8  e863151000            call      00547140h
  00445bdd  99                    cdq
  00445bde  33c2                  xor       eax,edx
  00445be0  2bc2                  sub       eax,edx
  00445be2  25ff030000            and       eax,3FFh
  00445be7  33c2                  xor       eax,edx
  00445be9  2bc2                  sub       eax,edx
  00445beb  0500080000            add       eax,800h
  00445bf0  66894628              mov       [esi+28h],ax
  00445bf4  6689462a              mov       [esi+2Ah],ax
  00445bf8  e843151000            call      00547140h
  00445bfd  99                    cdq
  00445bfe  b903000000            mov       ecx,3
  00445c03  f7f9                  idiv      ecx
  00445c05  03d1                  add       edx,ecx
  00445c07  66895612              mov       [esi+12h],dx
  00445c0b  e830151000            call      00547140h
  00445c10  99                    cdq
  00445c11  b906000000            mov       ecx,6
  00445c16  895e48                mov       [esi+48h],ebx
  00445c19  f7f9                  idiv      ecx
  00445c1b  03d1                  add       edx,ecx
  00445c1d  66895616              mov       [esi+16h],dx

LAB_00445c21:
  00445c21  66397e10              cmp       [esi+10h],di
  00445c25  0f85b6000000          jne       near 00445CE1h
  00445c2b  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00445c32  52                    push      edx
  00445c33  e8e4fd0f00            call      00545A1Ch
  00445c38  0faf465c              imul      eax,[esi+5Ch]
  00445c3c  034650                add       eax,[esi+50h]
  00445c3f  89461c                mov       [esi+1Ch],eax
  00445c42  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00445c49  50                    push      eax
  00445c4a  e8c7fd0f00            call      00545A16h
  00445c4f  0faf465c              imul      eax,[esi+5Ch]
  00445c53  034658                add       eax,[esi+58h]
  00445c56  894624                mov       [esi+24h],eax
  00445c59  e8e2141000            call      00547140h
  00445c5e  99                    cdq
  00445c5f  b932000000            mov       ecx,32h
  00445c64  8b7e20                mov       edi,[esi+20h]
  00445c67  f7f9                  idiv      ecx
  00445c69  668b4612              mov       ax,[esi+12h]
  00445c6d  8b4e48                mov       ecx,[esi+48h]
  00445c70  6603969a000000        add       dx,[esi+9Ah]
  00445c77  83c264                add       edx,64h
  00445c7a  81e2ff0f0000          and       edx,0FFFh
  00445c80  6689969a000000        mov       [esi+9Ah],dx
  00445c87  0fbfd0                movsx     edx,ax
  00445c8a  c1e20c                shl       edx,0Ch
  00445c8d  03fa                  add       edi,edx
  00445c8f  40                    inc       eax
  00445c90  41                    inc       ecx
  00445c91  66894612              mov       [esi+12h],ax
  00445c95  8bc1                  mov       eax,ecx
  00445c97  897e20                mov       [esi+20h],edi
  00445c9a  83f806                cmp       eax,6
  00445c9d  894e48                mov       [esi+48h],ecx
  00445ca0  7c15                  jl        short 00445CB7h
  00445ca2  8a4e40                mov       cl,[esi+40h]
  00445ca5  895e48                mov       [esi+48h],ebx
  00445ca8  fec1                  inc       cl
  00445caa  8ac1                  mov       al,cl
  00445cac  884e40                mov       [esi+40h],cl
  00445caf  3c04                  cmp       al,4
  00445cb1  7504                  jne       short 00445CB7h
  00445cb3  c6464003              mov       byte [esi+40h],3

LAB_00445cb7:
  00445cb7  668b4616              mov       ax,[esi+16h]
  00445cbb  66f7d8                neg       ax
  00445cbe  50                    push      eax
  00445cbf  50                    push      eax
  00445cc0  50                    push      eax
  00445cc1  56                    push      esi
  00445cc2  e819f1fbff            call      00404DE0h
  00445cc7  8a4638                mov       al,[esi+38h]
  00445cca  83c410                add       esp,10h
  00445ccd  3ac3                  cmp       al,bl
  00445ccf  7510                  jne       short 00445CE1h
  00445cd1  385e39                cmp       [esi+39h],bl
  00445cd4  750b                  jne       short 00445CE1h
  00445cd6  385e3a                cmp       [esi+3Ah],bl
  00445cd9  7506                  jne       short 00445CE1h
  00445cdb  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00445ce1:
  00445ce1  5f                    pop       edi
  00445ce2  5e                    pop       esi
  00445ce3  5b                    pop       ebx
  00445ce4  c3                    ret

5 cible(s) de saut interne : 0x00445aba, 0x00445b57, 0x00445c21, 0x00445cb7, 0x00445ce1


