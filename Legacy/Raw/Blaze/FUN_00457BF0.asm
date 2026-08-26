Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00457bf0  section .text  offset fichier 0x56ff0  503 octets

  00457bf0  a0fb965e00            mov       al,[5E96FBh]
  00457bf5  56                    push      esi
  00457bf6  a801                  test      al,1
  00457bf8  57                    push      edi
  00457bf9  0f84ed010000          je        near 00457DECh
  00457bff  8b74240c              mov       esi,[esp+0Ch]
  00457c03  66837e1000            cmp       word [esi+10h],0
  00457c08  0f859d000000          jne       near 00457CABh
  00457c0e  66c746100100          mov       word [esi+10h],1
  00457c14  e827f50e00            call      00547140h
  00457c19  99                    cdq
  00457c1a  33c2                  xor       eax,edx
  00457c1c  2bc2                  sub       eax,edx
  00457c1e  83e00f                and       eax,0Fh
  00457c21  33c2                  xor       eax,edx
  00457c23  2bc2                  sub       eax,edx
  00457c25  83e808                sub       eax,8
  00457c28  c1e00c                shl       eax,0Ch
  00457c2b  894650                mov       [esi+50h],eax
  00457c2e  e80df50e00            call      00547140h
  00457c33  99                    cdq
  00457c34  33c2                  xor       eax,edx
  00457c36  2bc2                  sub       eax,edx
  00457c38  83e00f                and       eax,0Fh
  00457c3b  33c2                  xor       eax,edx
  00457c3d  2bc2                  sub       eax,edx
  00457c3f  83e808                sub       eax,8
  00457c42  c1e00c                shl       eax,0Ch
  00457c45  894658                mov       [esi+58h],eax
  00457c48  e8f3f40e00            call      00547140h
  00457c4d  99                    cdq
  00457c4e  b90f000000            mov       ecx,0Fh
  00457c53  f7f9                  idiv      ecx
  00457c55  83c205                add       edx,5
  00457c58  c1e20c                shl       edx,0Ch
  00457c5b  895654                mov       [esi+54h],edx
  00457c5e  e8ddf40e00            call      00547140h
  00457c63  99                    cdq
  00457c64  b91e000000            mov       ecx,1Eh
  00457c69  f7f9                  idiv      ecx
  00457c6b  66895612              mov       [esi+12h],dx
  00457c6f  e8ccf40e00            call      00547140h
  00457c74  99                    cdq
  00457c75  b91e000000            mov       ecx,1Eh
  00457c7a  f7f9                  idiv      ecx
  00457c7c  66895614              mov       [esi+14h],dx
  00457c80  e8bbf40e00            call      00547140h
  00457c85  99                    cdq
  00457c86  b90a000000            mov       ecx,0Ah
  00457c8b  c7465c00000000        mov       dword [esi+5Ch],0
  00457c92  f7f9                  idiv      ecx
  00457c94  8b06                  mov       eax,[esi]
  00457c96  66c786980000000000    mov       word [esi+98h],0
  00457c9f  83c214                add       edx,14h
  00457ca2  80cc20                or        ah,20h
  00457ca5  66895616              mov       [esi+16h],dx
  00457ca9  8906                  mov       [esi],eax

LAB_00457cab:
  00457cab  66837e1001            cmp       word [esi+10h],1
  00457cb0  0f8536010000          jne       near 00457DECh
  00457cb6  6683be9800000000      cmp       word [esi+98h],0
  00457cbe  751b                  jne       short 00457CDBh
  00457cc0  bf10000000            mov       edi,10h

LAB_00457cc5:
  00457cc5  6a00                  push      0
  00457cc7  6a00                  push      0
  00457cc9  6aff                  push      0FFFFFFFFh
  00457ccb  6a00                  push      0
  00457ccd  6a31                  push      31h
  00457ccf  56                    push      esi
  00457cd0  e83b040100            call      00468110h
  00457cd5  83c418                add       esp,18h
  00457cd8  4f                    dec       edi
  00457cd9  75ea                  jne       short 00457CC5h

LAB_00457cdb:
  00457cdb  0fbf8698000000        movsx     eax,word [esi+98h]
  00457ce2  83f80c                cmp       eax,0Ch
  00457ce5  0f8797000000          ja        near 00457D82h
  00457ceb  33d2                  xor       edx,edx
  00457ced  8a90087e4500          mov       dl,[eax+457E08h]
  00457cf3  ff2495f07d4500        jmp       dword [edx*4+457DF0h]
  00457cfa  6a00                  push      0
  00457cfc  6a00                  push      0
  00457cfe  6800180000            push      1800h
  00457d03  eb38                  jmp       short 00457D3Dh
  00457d05  6a00                  push      0
  00457d07  6a00                  push      0
  00457d09  6800100000            push      1000h
  00457d0e  eb50                  jmp       short 00457D60h
  00457d10  8b15cc975e00          mov       edx,[5E97CCh]
  00457d16  6a00                  push      0
  00457d18  6a00                  push      0
  00457d1a  68000b0000            push      0B00h
  00457d1f  8b82a8000000          mov       eax,[edx+0A8h]
  00457d25  6800040000            push      400h
  00457d2a  8b08                  mov       ecx,[eax]
  00457d2c  660fb6511c            movzx     dx,byte [ecx+1Ch]
  00457d31  52                    push      edx
  00457d32  eb45                  jmp       short 00457D79h
  00457d34  6a00                  push      0
  00457d36  6a00                  push      0
  00457d38  6800080000            push      800h

LAB_00457d3d:
  00457d3d  a1cc975e00            mov       eax,[5E97CCh]
  00457d42  6800040000            push      400h
  00457d47  8b88a8000000          mov       ecx,[eax+0A8h]
  00457d4d  8b11                  mov       edx,[ecx]
  00457d4f  660fb6421c            movzx     ax,byte [edx+1Ch]
  00457d54  50                    push      eax
  00457d55  eb22                  jmp       short 00457D79h
  00457d57  6a00                  push      0
  00457d59  6a00                  push      0
  00457d5b  6800040000            push      400h

LAB_00457d60:
  00457d60  8b0dcc975e00          mov       ecx,[5E97CCh]
  00457d66  6800040000            push      400h
  00457d6b  8b91a8000000          mov       edx,[ecx+0A8h]
  00457d71  8b02                  mov       eax,[edx]
  00457d73  660fb6481c            movzx     cx,byte [eax+1Ch]
  00457d78  51                    push      ecx

LAB_00457d79:
  00457d79  56                    push      esi
  00457d7a  e801030100            call      00468080h
  00457d7f  83c418                add       esp,18h

LAB_00457d82:
  00457d82  8b5650                mov       edx,[esi+50h]
  00457d85  8b461c                mov       eax,[esi+1Ch]
  00457d88  8b7e20                mov       edi,[esi+20h]
  00457d8b  8b4e58                mov       ecx,[esi+58h]
  00457d8e  03c2                  add       eax,edx
  00457d90  8b5624                mov       edx,[esi+24h]
  00457d93  89461c                mov       [esi+1Ch],eax
  00457d96  8b4654                mov       eax,[esi+54h]
  00457d99  66ff8698000000        inc       word [esi+98h]
  00457da0  03f8                  add       edi,eax
  00457da2  03d1                  add       edx,ecx
  00457da4  0500f0ffff            add       eax,0FFFFF000h
  00457da9  0fbf4e16              movsx     ecx,word [esi+16h]
  00457dad  895624                mov       [esi+24h],edx
  00457db0  668b5612              mov       dx,[esi+12h]
  00457db4  66015632              add       [esi+32h],dx
  00457db8  894654                mov       [esi+54h],eax
  00457dbb  668b4614              mov       ax,[esi+14h]
  00457dbf  897e20                mov       [esi+20h],edi
  00457dc2  8b7e5c                mov       edi,[esi+5Ch]
  00457dc5  66014634              add       [esi+34h],ax
  00457dc9  47                    inc       edi
  00457dca  8bc7                  mov       eax,edi
  00457dcc  897e5c                mov       [esi+5Ch],edi
  00457dcf  3bc1                  cmp       eax,ecx
  00457dd1  7e06                  jle       short 00457DD9h
  00457dd3  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00457dd9:
  00457dd9  a801                  test      al,1
  00457ddb  8b06                  mov       eax,[esi]
  00457ddd  7508                  jne       short 00457DE7h
  00457ddf  80cc40                or        ah,40h
  00457de2  8906                  mov       [esi],eax
  00457de4  5f                    pop       edi
  00457de5  5e                    pop       esi
  00457de6  c3                    ret

8 cible(s) de saut interne : 0x00457cab, 0x00457cc5, 0x00457cdb, 0x00457d3d, 0x00457d60, 0x00457d79, 0x00457d82, 0x00457dd9
