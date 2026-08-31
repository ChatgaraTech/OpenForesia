
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047e590  section .text  offset fichier 0x7d990  342 octets

  0047e590  53                    push      ebx
  0047e591  55                    push      ebp
  0047e592  56                    push      esi
  0047e593  8b742410              mov       esi,[esp+10h]
  0047e597  33ed                  xor       ebp,ebp
  0047e599  57                    push      edi
  0047e59a  66396e10              cmp       [esi+10h],bp
  0047e59e  7421                  je        short 0047E5C1h
  0047e5a0  66396e16              cmp       [esi+16h],bp
  0047e5a4  0f8587000000          jne       near 0047E631h
  0047e5aa  892e                  mov       [esi],ebp
  0047e5ac  8b7674                mov       esi,[esi+74h]
  0047e5af  8b06                  mov       eax,[esi]
  0047e5b1  247f                  and       al,7Fh
  0047e5b3  8906                  mov       [esi],eax
  0047e5b5  66892d66915e00        mov       [5E9166h],bp
  0047e5bc  5f                    pop       edi
  0047e5bd  5e                    pop       esi
  0047e5be  5d                    pop       ebp
  0047e5bf  5b                    pop       ebx
  0047e5c0  c3                    ret

LAB_0047e5c1:
  0047e5c1  8b4674                mov       eax,[esi+74h]
  0047e5c4  8b10                  mov       edx,[eax]
  0047e5c6  80ca80                or        dl,80h
  0047e5c9  8910                  mov       [eax],edx
  0047e5cb  8b4674                mov       eax,[esi+74h]
  0047e5ce  50                    push      eax
  0047e5cf  896e78                mov       [esi+78h],ebp
  0047e5d2  e8e9a50b00            call      00538BC0h
  0047e5d7  8b4e20                mov       ecx,[esi+20h]
  0047e5da  24fe                  and       al,0FEh
  0047e5dc  c1e00b                shl       eax,0Bh
  0047e5df  83c404                add       esp,4
  0047e5e2  03c8                  add       ecx,eax
  0047e5e4  894e20                mov       [esi+20h],ecx
  0047e5e7  66896e16              mov       [esi+16h],bp
  0047e5eb  bf04000000            mov       edi,4

LAB_0047e5f0:
  0047e5f0  6aff                  push      0FFFFFFFFh
  0047e5f2  55                    push      ebp
  0047e5f3  6a64                  push      64h
  0047e5f5  56                    push      esi
  0047e5f6  e8d5c3ffff            call      0047A9D0h
  0047e5fb  66014616              add       [esi+16h],ax
  0047e5ff  83c410                add       esp,10h
  0047e602  4f                    dec       edi
  0047e603  75eb                  jne       short 0047E5F0h
  0047e605  8d4e1c                lea       ecx,[esi+1Ch]
  0047e608  c70560915e00c400c400  mov       dword [5E9160h],0C400C4h
  0047e612  51                    push      ecx
  0047e613  6800120000            push      1200h
  0047e618  6829030000            push      329h
  0047e61d  6a2c                  push      2Ch
  0047e61f  c70564915e00c4000200  mov       dword [5E9164h],200C4h
  0047e629  e8f2f40b00            call      0053DB20h
  0047e62e  83c410                add       esp,10h

LAB_0047e631:
  0047e631  8b06                  mov       eax,[esi]
  0047e633  a900000002            test      eax,2000000h
  0047e638  0f858c000000          jne       near 0047E6CAh
  0047e63e  a900008000            test      eax,800000h
  0047e643  7477                  je        short 0047E6BCh
  0047e645  8b5674                mov       edx,[esi+74h]
  0047e648  8b3db4876000          mov       edi,[6087B4h]
  0047e64e  33c9                  xor       ecx,ecx
  0047e650  8a4a08                mov       cl,[edx+8]
  0047e653  8b1f                  mov       ebx,[edi]
  0047e655  8bd1                  mov       edx,ecx
  0047e657  8d4a78                lea       ecx,[edx+78h]
  0047e65a  8d3cd2                lea       edi,[edx+edx*8]
  0047e65d  c1e10d                shl       ecx,0Dh
  0047e660  03cb                  add       ecx,ebx
  0047e662  8d147a                lea       edx,[edx+edi*2]
  0047e665  33db                  xor       ebx,ebx
  0047e667  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047e66e  c1e304                shl       ebx,4
  0047e671  668b93204a6000        mov       dx,[ebx+604A20h]
  0047e678  668591c41f0000        test      [ecx+1FC4h],dx
  0047e67f  7507                  jne       short 0047E688h
  0047e681  0d00000002            or        eax,2000000h
  0047e686  8906                  mov       [esi],eax

LAB_0047e688:
  0047e688  a1f02c5f00            mov       eax,[5F2CF0h]
  0047e68d  33d2                  xor       edx,edx
  0047e68f  bf0a000000            mov       edi,0Ah
  0047e694  f7f7                  div       edi
  0047e696  85d2                  test      edx,edx
  0047e698  7530                  jne       short 0047E6CAh
  0047e69a  668b814e010000        mov       ax,[ecx+14Eh]
  0047e6a1  663d0100              cmp       ax,1
  0047e6a5  7c0a                  jl        short 0047E6B1h
  0047e6a7  48                    dec       eax
  0047e6a8  6689814e010000        mov       [ecx+14Eh],ax
  0047e6af  eb19                  jmp       short 0047E6CAh

LAB_0047e6b1:
  0047e6b1  6689a94e010000        mov       [ecx+14Eh],bp
  0047e6b8  8b06                  mov       eax,[esi]
  0047e6ba  eb07                  jmp       short 0047E6C3h

LAB_0047e6bc:
  0047e6bc  66837e1078            cmp       word [esi+10h],78h
  0047e6c1  7c07                  jl        short 0047E6CAh

LAB_0047e6c3:
  0047e6c3  0d00000002            or        eax,2000000h
  0047e6c8  8906                  mov       [esi],eax

LAB_0047e6ca:
  0047e6ca  56                    push      esi
  0047e6cb  e8406affff            call      00475110h
  0047e6d0  83c404                add       esp,4
  0047e6d3  85c0                  test      eax,eax
  0047e6d5  7406                  je        short 0047E6DDh
  0047e6d7  810e00000002          or        dword [esi],2000000h

LAB_0047e6dd:
  0047e6dd  66ff4610              inc       word [esi+10h]
  0047e6e1  5f                    pop       edi
  0047e6e2  5e                    pop       esi
  0047e6e3  5d                    pop       ebp
  0047e6e4  5b                    pop       ebx
  0047e6e5  c3                    ret

9 cible(s) de saut interne : 0x0047e5c1, 0x0047e5f0, 0x0047e631, 0x0047e688, 0x0047e6b1, 0x0047e6bc, 0x0047e6c3, 0x0047e6ca, 0x0047e6dd
