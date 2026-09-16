Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00409e40  section .text  offset fichier 0x9240  256 octets

  00409e40  53                    push      ebx
  00409e41  56                    push      esi
  00409e42  8b74240c              mov       esi,[esp+0Ch]
  00409e46  33db                  xor       ebx,ebx
  00409e48  57                    push      edi
  00409e49  66395e10              cmp       [esi+10h],bx
  00409e4d  755b                  jne       short 00409EAAh
  00409e4f  66c746100100          mov       word [esi+10h],1
  00409e55  66895e12              mov       [esi+12h],bx
  00409e59  a1cc975e00            mov       eax,[5E97CCh]
  00409e5e  53                    push      ebx
  00409e5f  53                    push      ebx
  00409e60  68002a0000            push      2A00h
  00409e65  8b88a8000000          mov       ecx,[eax+0A8h]
  00409e6b  6800030000            push      300h
  00409e70  8b11                  mov       edx,[ecx]
  00409e72  660fb6422c            movzx     ax,byte [edx+2Ch]
  00409e77  50                    push      eax
  00409e78  56                    push      esi
  00409e79  e802e20500            call      00468080h
  00409e7e  83c418                add       esp,18h
  00409e81  c7465c01000000        mov       dword [esi+5Ch],1
  00409e88  bf0b000000            mov       edi,0Bh

LAB_00409e8d:
  00409e8d  33c9                  xor       ecx,ecx
  00409e8f  33d2                  xor       edx,edx
  00409e91  8a4e0a                mov       cl,[esi+0Ah]
  00409e94  8a560b                mov       dl,[esi+0Bh]
  00409e97  51                    push      ecx
  00409e98  52                    push      edx
  00409e99  68de000000            push      0DEh
  00409e9e  56                    push      esi
  00409e9f  e83c0c0700            call      0047AAE0h
  00409ea4  83c410                add       esp,10h
  00409ea7  4f                    dec       edi
  00409ea8  75e3                  jne       short 00409E8Dh

LAB_00409eaa:
  00409eaa  66837e1001            cmp       word [esi+10h],1
  00409eaf  757f                  jne       short 00409F30h
  00409eb1  668b4612              mov       ax,[esi+12h]
  00409eb5  663bc3                cmp       ax,bx
  00409eb8  740c                  je        short 00409EC6h
  00409eba  663d0300              cmp       ax,3
  00409ebe  7406                  je        short 00409EC6h
  00409ec0  663d0700              cmp       ax,7
  00409ec4  752e                  jne       short 00409EF4h

LAB_00409ec6:
  00409ec6  663bc3                cmp       ax,bx
  00409ec9  895e5c                mov       [esi+5Ch],ebx
  00409ecc  7506                  jne       short 00409ED4h
  00409ece  66895e16              mov       [esi+16h],bx
  00409ed2  eb06                  jmp       short 00409EDAh

LAB_00409ed4:
  00409ed4  66c746160100          mov       word [esi+16h],1

LAB_00409eda:
  00409eda  33c0                  xor       eax,eax
  00409edc  33c9                  xor       ecx,ecx
  00409ede  8a460a                mov       al,[esi+0Ah]
  00409ee1  8a4e0b                mov       cl,[esi+0Bh]
  00409ee4  50                    push      eax
  00409ee5  51                    push      ecx
  00409ee6  68de000000            push      0DEh
  00409eeb  56                    push      esi
  00409eec  e8ef0b0700            call      0047AAE0h
  00409ef1  83c410                add       esp,10h

LAB_00409ef4:
  00409ef4  66ff4612              inc       word [esi+12h]
  00409ef8  668b4612              mov       ax,[esi+12h]
  00409efc  b91e000000            mov       ecx,1Eh
  00409f01  c60527e05d0001        mov       byte [5DE027h],1
  00409f08  a801                  test      al,1
  00409f0a  880d24e05d00          mov       [5DE024h],cl
  00409f10  7506                  jne       short 00409F18h
  00409f12  881d24e05d00          mov       [5DE024h],bl

LAB_00409f18:
  00409f18  881d25e05d00          mov       [5DE025h],bl
  00409f1e  881d26e05d00          mov       [5DE026h],bl
  00409f24  66394e12              cmp       [esi+12h],cx
  00409f28  7c06                  jl        short 00409F30h
  00409f2a  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00409f30:
  00409f30  5f                    pop       edi
  00409f31  5e                    pop       esi
  00409f32  5b                    pop       ebx
  00409f33  c3                    ret
  00409f34  90                    nop
  00409f35  90                    nop
  00409f36  90                    nop
  00409f37  90                    nop
  00409f38  90                    nop
  00409f39  90                    nop
  00409f3a  90                    nop
  00409f3b  90                    nop
  00409f3c  90                    nop
  00409f3d  90                    nop
  00409f3e  90                    nop
  00409f3f  90                    nop

8 cible(s) de saut interne : 0x00409e8d, 0x00409eaa, 0x00409ec6, 0x00409ed4, 0x00409eda, 0x00409ef4, 0x00409f18, 0x00409f30
