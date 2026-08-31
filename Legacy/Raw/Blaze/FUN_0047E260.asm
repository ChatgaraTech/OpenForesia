
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047e260  section .text  offset fichier 0x7d660  257 octets

  0047e260  56                    push      esi
  0047e261  8b742408              mov       esi,[esp+8]
  0047e265  f70600000002          test      dword [esi],2000000h
  0047e26b  7412                  je        short 0047E27Fh
  0047e26d  8b4674                mov       eax,[esi+74h]
  0047e270  8b08                  mov       ecx,[eax]
  0047e272  80e17f                and       cl,7Fh
  0047e275  8908                  mov       [eax],ecx
  0047e277  c70600000000          mov       dword [esi],0
  0047e27d  5e                    pop       esi
  0047e27e  c3                    ret

LAB_0047e27f:
  0047e27f  66837e1000            cmp       word [esi+10h],0
  0047e284  55                    push      ebp
  0047e285  53                    push      ebx
  0047e286  752e                  jne       short 0047E2B6h
  0047e288  8b4674                mov       eax,[esi+74h]
  0047e28b  6aff                  push      0FFFFFFFFh
  0047e28d  8b08                  mov       ecx,[eax]
  0047e28f  80c980                or        cl,80h
  0047e292  8908                  mov       [eax],ecx
  0047e294  33c0                  xor       eax,eax
  0047e296  8a460b                mov       al,[esi+0Bh]
  0047e299  c7462800000000        mov       dword [esi+28h],0
  0047e2a0  50                    push      eax
  0047e2a1  6a6e                  push      6Eh
  0047e2a3  56                    push      esi
  0047e2a4  66c7462c0000          mov       word [esi+2Ch],0
  0047e2aa  c6460402              mov       byte [esi+4],2
  0047e2ae  e82dc8ffff            call      0047AAE0h
  0047e2b3  83c410                add       esp,10h

LAB_0047e2b6:
  0047e2b6  8b06                  mov       eax,[esi]
  0047e2b8  a900008000            test      eax,800000h
  0047e2bd  7479                  je        short 0047E338h
  0047e2bf  8b4e74                mov       ecx,[esi+74h]
  0047e2c2  8b1db4876000          mov       ebx,[6087B4h]
  0047e2c8  33d2                  xor       edx,edx
  0047e2ca  8a5108                mov       dl,[ecx+8]
  0047e2cd  8b2b                  mov       ebp,[ebx]
  0047e2cf  8d1cd2                lea       ebx,[edx+edx*8]
  0047e2d2  8d4a78                lea       ecx,[edx+78h]
  0047e2d5  c1e10d                shl       ecx,0Dh
  0047e2d8  8d145a                lea       edx,[edx+ebx*2]
  0047e2db  33db                  xor       ebx,ebx
  0047e2dd  03cd                  add       ecx,ebp
  0047e2df  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047e2e6  c1e304                shl       ebx,4
  0047e2e9  668b93204a6000        mov       dx,[ebx+604A20h]
  0047e2f0  668591c41f0000        test      [ecx+1FC4h],dx
  0047e2f7  7507                  jne       short 0047E300h
  0047e2f9  0d00000002            or        eax,2000000h
  0047e2fe  8906                  mov       [esi],eax

LAB_0047e300:
  0047e300  a1f02c5f00            mov       eax,[5F2CF0h]
  0047e305  33d2                  xor       edx,edx
  0047e307  bb14000000            mov       ebx,14h
  0047e30c  f7f3                  div       ebx
  0047e30e  85d2                  test      edx,edx
  0047e310  7534                  jne       short 0047E346h
  0047e312  668b814e010000        mov       ax,[ecx+14Eh]
  0047e319  663d0600              cmp       ax,6
  0047e31d  7c0c                  jl        short 0047E32Bh
  0047e31f  83c0fa                add       eax,0FFFFFFFAh
  0047e322  6689814e010000        mov       [ecx+14Eh],ax
  0047e329  eb1b                  jmp       short 0047E346h

LAB_0047e32b:
  0047e32b  66c7814e0100000000    mov       word [ecx+14Eh],0
  0047e334  8b06                  mov       eax,[esi]
  0047e336  eb07                  jmp       short 0047E33Fh

LAB_0047e338:
  0047e338  66837e1078            cmp       word [esi+10h],78h
  0047e33d  7c07                  jl        short 0047E346h

LAB_0047e33f:
  0047e33f  0d00000002            or        eax,2000000h
  0047e344  8906                  mov       [esi],eax

LAB_0047e346:
  0047e346  56                    push      esi
  0047e347  e8c46dffff            call      00475110h
  0047e34c  83c404                add       esp,4
  0047e34f  85c0                  test      eax,eax
  0047e351  7406                  je        short 0047E359h
  0047e353  810e00000002          or        dword [esi],2000000h

LAB_0047e359:
  0047e359  66ff4610              inc       word [esi+10h]
  0047e35d  5b                    pop       ebx
  0047e35e  5d                    pop       ebp
  0047e35f  5e                    pop       esi
  0047e360  c3                    ret

8 cible(s) de saut interne : 0x0047e27f, 0x0047e2b6, 0x0047e300, 0x0047e32b, 0x0047e338, 0x0047e33f, 0x0047e346, 0x0047e359
