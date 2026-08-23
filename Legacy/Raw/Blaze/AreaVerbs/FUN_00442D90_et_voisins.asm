Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00442d90  section .text  offset fichier 0x42190  464 octets

  00442d90  0fbf442408            movsx     eax,word [esp+8]
  00442d95  8b542404              mov       edx,[esp+4]
  00442d99  8d0c80                lea       ecx,[eax+eax*4]
  00442d9c  c1e103                shl       ecx,3
  00442d9f  2bc8                  sub       ecx,eax
  00442da1  33c0                  xor       eax,eax
  00442da3  89148d0ccb5e00        mov       [ecx*4+5ECB0Ch],edx
  00442daa  c3                    ret
  00442dab  90                    nop
  00442dac  90                    nop
  00442dad  90                    nop
  00442dae  90                    nop
  00442daf  90                    nop
  00442db0  53                    push      ebx
  00442db1  56                    push      esi
  00442db2  57                    push      edi
  00442db3  bec0935e00            mov       esi,5E93C0h
  00442db8  bf04000000            mov       edi,4
  00442dbd  33db                  xor       ebx,ebx

LAB_00442dbf:
  00442dbf  f70600000080          test      dword [esi],80000000h
  00442dc5  742b                  je        short 00442DF2h
  00442dc7  53                    push      ebx
  00442dc8  6a64                  push      64h
  00442dca  53                    push      ebx
  00442dcb  881d9f905e00          mov       [5E909Fh],bl
  00442dd1  895e6c                mov       [esi+6Ch],ebx
  00442dd4  e8477b0f00            call      0053A920h
  00442dd9  83c408                add       esp,8
  00442ddc  50                    push      eax
  00442ddd  56                    push      esi
  00442dde  e83de10e00            call      00530F20h
  00442de3  83c40c                add       esp,0Ch
  00442de6  895e6c                mov       [esi+6Ch],ebx
  00442de9  56                    push      esi
  00442dea  e8e1e40e00            call      005312D0h
  00442def  83c404                add       esp,4

LAB_00442df2:
  00442df2  81c69c000000          add       esi,9Ch
  00442df8  4f                    dec       edi
  00442df9  75c4                  jne       short 00442DBFh
  00442dfb  5f                    pop       edi
  00442dfc  5e                    pop       esi
  00442dfd  33c0                  xor       eax,eax
  00442dff  5b                    pop       ebx
  00442e00  c3                    ret
  00442e01  90                    nop
  00442e02  90                    nop
  00442e03  90                    nop
  00442e04  90                    nop
  00442e05  90                    nop
  00442e06  90                    nop
  00442e07  90                    nop
  00442e08  90                    nop
  00442e09  90                    nop
  00442e0a  90                    nop
  00442e0b  90                    nop
  00442e0c  90                    nop
  00442e0d  90                    nop
  00442e0e  90                    nop
  00442e0f  90                    nop
  00442e10  33c0                  xor       eax,eax
  00442e12  a0773c5600            mov       al,[563C77h]
  00442e17  83e001                and       eax,1
  00442e1a  c3                    ret
  00442e1b  90                    nop
  00442e1c  90                    nop
  00442e1d  90                    nop
  00442e1e  90                    nop
  00442e1f  90                    nop
  00442e20  8b0db4876000          mov       ecx,[6087B4h]
  00442e26  33c0                  xor       eax,eax
  00442e28  a060905e00            mov       al,[5E9060h]
  00442e2d  53                    push      ebx
  00442e2e  8b11                  mov       edx,[ecx]
  00442e30  c60560905e0000        mov       byte [5E9060h],0
  00442e37  c1e00d                shl       eax,0Dh
  00442e3a  b90a000000            mov       ecx,0Ah
  00442e3f  b301                  mov       bl,1
  00442e41  8d841000030f00        lea       eax,[eax+edx+0F0300h]
  00442e48  bafb2e0000            mov       edx,2EFBh

LAB_00442e4d:
  00442e4d  803800                cmp       byte [eax],0
  00442e50  740f                  je        short 00442E61h
  00442e52  66399086000000        cmp       [eax+86h],dx
  00442e59  7506                  jne       short 00442E61h
  00442e5b  881d60905e00          mov       [5E9060h],bl

LAB_00442e61:
  00442e61  0580020000            add       eax,280h
  00442e66  49                    dec       ecx
  00442e67  75e4                  jne       short 00442E4Dh
  00442e69  33c0                  xor       eax,eax
  00442e6b  5b                    pop       ebx
  00442e6c  c3                    ret
  00442e6d  90                    nop
  00442e6e  90                    nop
  00442e6f  90                    nop
  00442e70  8b0db4876000          mov       ecx,[6087B4h]
  00442e76  33c0                  xor       eax,eax
  00442e78  a060905e00            mov       al,[5E9060h]
  00442e7d  8b11                  mov       edx,[ecx]
  00442e7f  c1e00d                shl       eax,0Dh
  00442e82  6681bc1044010f00b400  cmp       word [eax+edx+0F0144h],0B4h
  00442e8c  0f92c0                setb      al
  00442e8f  40                    inc       eax
  00442e90  a260905e00            mov       [5E9060h],al
  00442e95  33c0                  xor       eax,eax
  00442e97  c3                    ret
  00442e98  90                    nop
  00442e99  90                    nop
  00442e9a  90                    nop
  00442e9b  90                    nop
  00442e9c  90                    nop
  00442e9d  90                    nop
  00442e9e  90                    nop
  00442e9f  90                    nop
  00442ea0  8b0db4876000          mov       ecx,[6087B4h]
  00442ea6  33c0                  xor       eax,eax
  00442ea8  a060905e00            mov       al,[5E9060h]
  00442ead  8b11                  mov       edx,[ecx]
  00442eaf  83c078                add       eax,78h
  00442eb2  c1e00d                shl       eax,0Dh
  00442eb5  03c2                  add       eax,edx
  00442eb7  c60560905e0000        mov       byte [5E9060h],0
  00442ebe  6683b84401000014      cmp       word [eax+144h],14h
  00442ec6  7207                  jb        short 00442ECFh
  00442ec8  c60560905e0001        mov       byte [5E9060h],1

LAB_00442ecf:
  00442ecf  6683b84401000028      cmp       word [eax+144h],28h
  00442ed7  7206                  jb        short 00442EDFh
  00442ed9  fe0560905e00          inc       byte [5E9060h]

LAB_00442edf:
  00442edf  6683b8440100003c      cmp       word [eax+144h],3Ch
  00442ee7  7206                  jb        short 00442EEFh
  00442ee9  fe0560905e00          inc       byte [5E9060h]

LAB_00442eef:
  00442eef  6683b84401000050      cmp       word [eax+144h],50h
  00442ef7  7206                  jb        short 00442EFFh
  00442ef9  fe0560905e00          inc       byte [5E9060h]

LAB_00442eff:
  00442eff  6683b84401000064      cmp       word [eax+144h],64h
  00442f07  7206                  jb        short 00442F0Fh
  00442f09  fe0560905e00          inc       byte [5E9060h]

LAB_00442f0f:
  00442f0f  33c0                  xor       eax,eax
  00442f11  c3                    ret
  00442f12  90                    nop
  00442f13  90                    nop
  00442f14  90                    nop
  00442f15  90                    nop
  00442f16  90                    nop
  00442f17  90                    nop
  00442f18  90                    nop
  00442f19  90                    nop
  00442f1a  90                    nop
  00442f1b  90                    nop
  00442f1c  90                    nop
  00442f1d  90                    nop
  00442f1e  90                    nop
  00442f1f  90                    nop
  00442f20  8b0db4876000          mov       ecx,[6087B4h]
  00442f26  33c0                  xor       eax,eax
  00442f28  a062905e00            mov       al,[5E9062h]
  00442f2d  8b11                  mov       edx,[ecx]
  00442f2f  83c078                add       eax,78h
  00442f32  c1e00d                shl       eax,0Dh
  00442f35  03c2                  add       eax,edx
  00442f37  33c9                  xor       ecx,ecx
  00442f39  0578010000            add       eax,178h
  00442f3e  ba07000000            mov       edx,7

LAB_00442f43:
  00442f43  803805                cmp       byte [eax],5
  00442f46  7304                  jae       short 00442F4Ch
  00442f48  c60005                mov       byte [eax],5
  00442f4b  41                    inc       ecx

LAB_00442f4c:
  00442f4c  40                    inc       eax
  00442f4d  4a                    dec       edx
  00442f4e  75f3                  jne       short 00442F43h
  00442f50  33c0                  xor       eax,eax
  00442f52  6685c9                test      cx,cx
  00442f55  0f94c0                sete      al
  00442f58  c3                    ret
  00442f59  90                    nop
  00442f5a  90                    nop
  00442f5b  90                    nop
  00442f5c  90                    nop
  00442f5d  90                    nop
  00442f5e  90                    nop
  00442f5f  90                    nop

11 cible(s) de saut interne : 0x00442dbf, 0x00442df2, 0x00442e4d, 0x00442e61, 0x00442ecf, 0x00442edf, 0x00442eef, 0x00442eff, 0x00442f0f, 0x00442f43, 0x00442f4c
