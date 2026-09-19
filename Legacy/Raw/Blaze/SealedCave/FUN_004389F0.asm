Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004389f0  section .text  offset fichier 0x37df0  240 octets

  004389f0  53                    push      ebx
  004389f1  56                    push      esi
  004389f2  8b74240c              mov       esi,[esp+0Ch]
  004389f6  33db                  xor       ebx,ebx
  004389f8  66395e10              cmp       [esi+10h],bx
  004389fc  0f85a8000000          jne       near 00438AAAh
  00438a02  66c746100100          mov       word [esi+10h],1
  00438a08  c7463c400f5500        mov       dword [esi+3Ch],550F40h
  00438a0f  c6460a09              mov       byte [esi+0Ah],9
  00438a13  885e0b                mov       [esi+0Bh],bl
  00438a16  e825e71000            call      00547140h
  00438a1b  99                    cdq
  00438a1c  b928000000            mov       ecx,28h
  00438a21  f7f9                  idiv      ecx
  00438a23  8b461c                mov       eax,[esi+1Ch]
  00438a26  83ea14                sub       edx,14h
  00438a29  c1e20c                shl       edx,0Ch
  00438a2c  03c2                  add       eax,edx
  00438a2e  89461c                mov       [esi+1Ch],eax
  00438a31  e80ae71000            call      00547140h
  00438a36  99                    cdq
  00438a37  b928000000            mov       ecx,28h
  00438a3c  f7f9                  idiv      ecx
  00438a3e  8b4e24                mov       ecx,[esi+24h]
  00438a41  83ea14                sub       edx,14h
  00438a44  c1e20c                shl       edx,0Ch
  00438a47  03ca                  add       ecx,edx
  00438a49  894e24                mov       [esi+24h],ecx
  00438a4c  e8efe61000            call      00547140h
  00438a51  99                    cdq
  00438a52  b928000000            mov       ecx,28h
  00438a57  f7f9                  idiv      ecx
  00438a59  8b4620                mov       eax,[esi+20h]
  00438a5c  83ea14                sub       edx,14h
  00438a5f  c1e20c                shl       edx,0Ch
  00438a62  03c2                  add       eax,edx
  00438a64  894620                mov       [esi+20h],eax
  00438a67  e8d4e61000            call      00547140h
  00438a6c  99                    cdq
  00438a6d  33c2                  xor       eax,edx
  00438a6f  2bc2                  sub       eax,edx
  00438a71  25ff070000            and       eax,7FFh
  00438a76  33c2                  xor       eax,edx
  00438a78  2bc2                  sub       eax,edx
  00438a7a  ba00140000            mov       edx,1400h
  00438a7f  2bd0                  sub       edx,eax
  00438a81  66895628              mov       [esi+28h],dx
  00438a85  e8b6e61000            call      00547140h
  00438a8a  99                    cdq
  00438a8b  33c2                  xor       eax,edx
  00438a8d  b900140000            mov       ecx,1400h
  00438a92  2bc2                  sub       eax,edx
  00438a94  66895e12              mov       [esi+12h],bx
  00438a98  25ff070000            and       eax,7FFh
  00438a9d  885e40                mov       [esi+40h],bl
  00438aa0  33c2                  xor       eax,edx
  00438aa2  2bc2                  sub       eax,edx
  00438aa4  2bc8                  sub       ecx,eax
  00438aa6  66894e2a              mov       [esi+2Ah],cx

LAB_00438aaa:
  00438aaa  66837e1001            cmp       word [esi+10h],1
  00438aaf  7523                  jne       short 00438AD4h
  00438ab1  66ff4612              inc       word [esi+12h]
  00438ab5  66837e1202            cmp       word [esi+12h],2
  00438aba  7518                  jne       short 00438AD4h
  00438abc  8a5640                mov       dl,[esi+40h]
  00438abf  66895e12              mov       [esi+12h],bx
  00438ac3  fec2                  inc       dl
  00438ac5  8ac2                  mov       al,dl
  00438ac7  885640                mov       [esi+40h],dl
  00438aca  3c04                  cmp       al,4
  00438acc  7506                  jne       short 00438AD4h
  00438ace  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00438ad4:
  00438ad4  5e                    pop       esi
  00438ad5  5b                    pop       ebx
  00438ad6  c3                    ret
  00438ad7  90                    nop
  00438ad8  90                    nop
  00438ad9  90                    nop
  00438ada  90                    nop
  00438adb  90                    nop
  00438adc  90                    nop
  00438add  90                    nop
  00438ade  90                    nop
  00438adf  90                    nop

2 cible(s) de saut interne : 0x00438aaa, 0x00438ad4
