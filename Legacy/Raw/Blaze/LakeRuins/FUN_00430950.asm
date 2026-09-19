Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00430950  section .text  offset fichier 0x2fd50  752 octets

  00430950  51                    push      ecx
  00430951  53                    push      ebx
  00430952  55                    push      ebp
  00430953  56                    push      esi
  00430954  57                    push      edi
  00430955  33db                  xor       ebx,ebx
  00430957  33ff                  xor       edi,edi
  00430959  e8f2a5feff            call      0041AF50h
  0043095e  e8dddcfdff            call      0040E640h
  00430963  8b742418              mov       esi,[esp+18h]
  00430967  8b6c2418              mov       ebp,[esp+18h]
  0043096b  b8dbca5e00            mov       eax,5ECADBh
  00430970  c744241020000000      mov       dword [esp+10h],20h

LAB_00430978:
  00430978  8078d104              cmp       byte [eax-2Fh],4
  0043097c  7542                  jne       short 004309C0h
  0043097e  8b50c5                mov       edx,[eax-3Bh]
  00430981  8d48c5                lea       ecx,[eax-3Bh]
  00430984  f7c200000080          test      edx,80000000h
  0043098a  7434                  je        short 004309C0h
  0043098c  8b50e1                mov       edx,[eax-1Fh]
  0043098f  8bf9                  mov       edi,ecx
  00430991  89561c                mov       [esi+1Ch],edx
  00430994  8b50e5                mov       edx,[eax-1Bh]
  00430997  895620                mov       [esi+20h],edx
  0043099a  8b50e9                mov       edx,[eax-17h]
  0043099d  895624                mov       [esi+24h],edx
  004309a0  33d2                  xor       edx,edx
  004309a2  8a50c9                mov       dl,[eax-37h]
  004309a5  668958dd              mov       [eax-23h],bx
  004309a9  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004309b0  8b2a                  mov       ebp,[edx]
  004309b2  8a10                  mov       dl,[eax]
  004309b4  80ca02                or        dl,2
  004309b7  8810                  mov       [eax],dl
  004309b9  8b11                  mov       edx,[ecx]
  004309bb  80ce10                or        dh,10h
  004309be  8911                  mov       [ecx],edx

LAB_004309c0:
  004309c0  8b4c2410              mov       ecx,[esp+10h]
  004309c4  059c000000            add       eax,9Ch
  004309c9  49                    dec       ecx
  004309ca  894c2410              mov       [esp+10h],ecx
  004309ce  75a8                  jne       short 00430978h
  004309d0  3bfb                  cmp       edi,ebx
  004309d2  7505                  jne       short 004309D9h
  004309d4  bf58e55d00            mov       edi,5DE558h

LAB_004309d9:
  004309d9  33c9                  xor       ecx,ecx
  004309db  bb01000000            mov       ebx,1
  004309e0  66394e10              cmp       [esi+10h],cx
  004309e4  7547                  jne       short 00430A2Dh
  004309e6  66895e10              mov       [esi+10h],bx
  004309ea  66894e12              mov       [esi+12h],cx
  004309ee  66894e14              mov       [esi+14h],cx
  004309f2  66894e16              mov       [esi+16h],cx
  004309f6  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  004309fd  894650                mov       [esi+50h],eax
  00430a00  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  00430a07  895654                mov       [esi+54h],edx
  00430a0a  0fbf05bc925e00        movsx     eax,word [5E92BCh]
  00430a11  894658                mov       [esi+58h],eax
  00430a14  66894f1a              mov       [edi+1Ah],cx
  00430a18  884f0a                mov       [edi+0Ah],cl
  00430a1b  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  00430a22  66895f36              mov       [edi+36h],bx
  00430a26  66898e98000000        mov       [esi+98h],cx

LAB_00430a2d:
  00430a2d  66395e10              cmp       [esi+10h],bx
  00430a31  751d                  jne       short 00430A50h
  00430a33  8b5720                mov       edx,[edi+20h]
  00430a36  6afe                  push      0FFFFFFFEh
  00430a38  6afe                  push      0FFFFFFFEh
  00430a3a  81c200c0ffff          add       edx,0FFFFC000h
  00430a40  6afe                  push      0FFFFFFFEh
  00430a42  57                    push      edi
  00430a43  895720                mov       [edi+20h],edx
  00430a46  e89543fdff            call      00404DE0h
  00430a4b  83c410                add       esp,10h
  00430a4e  33c9                  xor       ecx,ecx

LAB_00430a50:
  00430a50  845e16                test      [esi+16h],bl
  00430a53  7519                  jne       short 00430A6Eh
  00430a55  66ff471a              inc       word [edi+1Ah]
  00430a59  668b471a              mov       ax,[edi+1Ah]
  00430a5d  33d2                  xor       edx,edx
  00430a5f  668b5516              mov       dx,[ebp+16h]
  00430a63  0fbfc0                movsx     eax,ax
  00430a66  3bc2                  cmp       eax,edx
  00430a68  7c04                  jl        short 00430A6Eh
  00430a6a  66894f1a              mov       [edi+1Ah],cx

LAB_00430a6e:
  00430a6e  6aff                  push      0FFFFFFFFh
  00430a70  51                    push      ecx
  00430a71  8d6e1c                lea       ebp,[esi+1Ch]
  00430a74  68f3000000            push      0F3h
  00430a79  51                    push      ecx
  00430a7a  55                    push      ebp
  00430a7b  56                    push      esi
  00430a7c  e8bfa10400            call      0047AC40h
  00430a81  83c418                add       esp,18h
  00430a84  6aff                  push      0FFFFFFFFh
  00430a86  6a00                  push      0
  00430a88  68f3000000            push      0F3h
  00430a8d  6a00                  push      0
  00430a8f  55                    push      ebp
  00430a90  56                    push      esi
  00430a91  e8aaa10400            call      0047AC40h
  00430a96  83c418                add       esp,18h
  00430a99  6aff                  push      0FFFFFFFFh
  00430a9b  6a00                  push      0
  00430a9d  68f3000000            push      0F3h
  00430aa2  6a00                  push      0
  00430aa4  55                    push      ebp
  00430aa5  56                    push      esi
  00430aa6  e895a10400            call      0047AC40h
  00430aab  83c418                add       esp,18h
  00430aae  66ff4616              inc       word [esi+16h]
  00430ab2  66817e16e600          cmp       word [esi+16h],0E6h
  00430ab8  7e64                  jle       short 00430B1Eh
  00430aba  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00430ac0  e8eb2c0d00            call      005037B0h
  00430ac5  33c0                  xor       eax,eax
  00430ac7  6a00                  push      0
  00430ac9  a038e55d00            mov       al,[5DE538h]
  00430ace  8d0c80                lea       ecx,[eax+eax*4]
  00430ad1  c1e103                shl       ecx,3
  00430ad4  2bc8                  sub       ecx,eax
  00430ad6  a1cc975e00            mov       eax,[5E97CCh]
  00430adb  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00430ae2  52                    push      edx
  00430ae3  50                    push      eax
  00430ae4  e8e7320e00            call      00513DD0h
  00430ae9  8b07                  mov       eax,[edi]
  00430aeb  83c40c                add       esp,0Ch
  00430aee  0d00400002            or        eax,2004000h
  00430af3  8907                  mov       [edi],eax
  00430af5  668b4e50              mov       cx,[esi+50h]
  00430af9  66890d60915e00        mov       [5E9160h],cx
  00430b00  668b5654              mov       dx,[esi+54h]
  00430b04  66891562915e00        mov       [5E9162h],dx
  00430b0b  668b4658              mov       ax,[esi+58h]
  00430b0f  66a364915e00          mov       [5E9164h],ax
  00430b15  66c70566915e000200    mov       word [5E9166h],2

LAB_00430b1e:
  00430b1e  66ff4616              inc       word [esi+16h]
  00430b22  6683be9800000000      cmp       word [esi+98h],0
  00430b2a  7519                  jne       short 00430B45h
  00430b2c  6800080000            push      800h
  00430b31  6800080000            push      800h
  00430b36  6854050000            push      554h
  00430b3b  6a18                  push      18h
  00430b3d  e85ec31000            call      0053CEA0h
  00430b42  83c410                add       esp,10h

LAB_00430b45:
  00430b45  6683be9800000007      cmp       word [esi+98h],7
  00430b4d  7519                  jne       short 00430B68h
  00430b4f  6800080000            push      800h
  00430b54  6800080000            push      800h
  00430b59  68ff040000            push      4FFh
  00430b5e  6a18                  push      18h
  00430b60  e83bc31000            call      0053CEA0h
  00430b65  83c410                add       esp,10h

LAB_00430b68:
  00430b68  6683be980000000e      cmp       word [esi+98h],0Eh
  00430b70  7519                  jne       short 00430B8Bh
  00430b72  6800080000            push      800h
  00430b77  6800080000            push      800h
  00430b7c  68aa040000            push      4AAh
  00430b81  6a18                  push      18h
  00430b83  e818c31000            call      0053CEA0h
  00430b88  83c410                add       esp,10h

LAB_00430b8b:
  00430b8b  6683be9800000015      cmp       word [esi+98h],15h
  00430b93  7519                  jne       short 00430BAEh
  00430b95  6800080000            push      800h
  00430b9a  6800080000            push      800h
  00430b9f  6800040000            push      400h
  00430ba4  6a18                  push      18h
  00430ba6  e8f5c21000            call      0053CEA0h
  00430bab  83c410                add       esp,10h

LAB_00430bae:
  00430bae  6683be980000001c      cmp       word [esi+98h],1Ch
  00430bb6  7519                  jne       short 00430BD1h
  00430bb8  6800080000            push      800h
  00430bbd  6800080000            push      800h
  00430bc2  687e030000            push      37Eh
  00430bc7  6a18                  push      18h
  00430bc9  e8d2c21000            call      0053CEA0h
  00430bce  83c410                add       esp,10h

LAB_00430bd1:
  00430bd1  6683be9800000023      cmp       word [esi+98h],23h
  00430bd9  7519                  jne       short 00430BF4h
  00430bdb  6800080000            push      800h
  00430be0  6800080000            push      800h
  00430be5  68d4020000            push      2D4h
  00430bea  6a18                  push      18h
  00430bec  e8afc21000            call      0053CEA0h
  00430bf1  83c410                add       esp,10h

LAB_00430bf4:
  00430bf4  6683be980000002a      cmp       word [esi+98h],2Ah
  00430bfc  7519                  jne       short 00430C17h
  00430bfe  6800080000            push      800h
  00430c03  6800080000            push      800h
  00430c08  6800020000            push      200h
  00430c0d  6a18                  push      18h
  00430c0f  e88cc21000            call      0053CEA0h
  00430c14  83c410                add       esp,10h

LAB_00430c17:
  00430c17  66ff8698000000        inc       word [esi+98h]
  00430c1e  6683be9800000031      cmp       word [esi+98h],31h
  00430c26  7c09                  jl        short 00430C31h
  00430c28  66c786980000000000    mov       word [esi+98h],0

LAB_00430c31:
  00430c31  5f                    pop       edi
  00430c32  5e                    pop       esi
  00430c33  5d                    pop       ebp
  00430c34  5b                    pop       ebx
  00430c35  59                    pop       ecx
  00430c36  c3                    ret
  00430c37  90                    nop
  00430c38  90                    nop
  00430c39  90                    nop
  00430c3a  90                    nop
  00430c3b  90                    nop
  00430c3c  90                    nop
  00430c3d  90                    nop
  00430c3e  90                    nop
  00430c3f  90                    nop

15 cible(s) de saut interne : 0x00430978, 0x004309c0, 0x004309d9, 0x00430a2d, 0x00430a50, 0x00430a6e, 0x00430b1e, 0x00430b45, 0x00430b68, 0x00430b8b, 0x00430bae, 0x00430bd1, 0x00430bf4, 0x00430c17, 0x00430c31
