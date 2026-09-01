Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044dc60  section .text  offset fichier 0x4d060  614 octets

  0044dc60  53                    push      ebx
  0044dc61  55                    push      ebp
  0044dc62  56                    push      esi
  0044dc63  8b742410              mov       esi,[esp+10h]
  0044dc67  57                    push      edi
  0044dc68  33ff                  xor       edi,edi
  0044dc6a  57                    push      edi
  0044dc6b  57                    push      edi
  0044dc6c  56                    push      esi
  0044dc6d  e8cea3ffff            call      00448040h
  0044dc72  83c40c                add       esp,0Ch
  0044dc75  66397e10              cmp       [esi+10h],di
  0044dc79  752d                  jne       short 0044DCA8h
  0044dc7b  57                    push      edi
  0044dc7c  57                    push      edi
  0044dc7d  56                    push      esi
  0044dc7e  e82da0ffff            call      00447CB0h
  0044dc83  83c40c                add       esp,0Ch
  0044dc86  66397e10              cmp       [esi+10h],di
  0044dc8a  751c                  jne       short 0044DCA8h
  0044dc8c  66c746100100          mov       word [esi+10h],1
  0044dc92  66897e16              mov       [esi+16h],di
  0044dc96  6689be98000000        mov       [esi+98h],di
  0044dc9d  c7465c78000000        mov       dword [esi+5Ch],78h
  0044dca4  66897e12              mov       [esi+12h],di

LAB_0044dca8:
  0044dca8  66ff4616              inc       word [esi+16h]
  0044dcac  66817e163075          cmp       word [esi+16h],7530h
  0044dcb2  7e06                  jle       short 0044DCBAh
  0044dcb4  66c74616b80b          mov       word [esi+16h],0BB8h

LAB_0044dcba:
  0044dcba  6639be98000000        cmp       [esi+98h],di
  0044dcc1  7519                  jne       short 0044DCDCh
  0044dcc3  6800100000            push      1000h
  0044dcc8  6800100000            push      1000h
  0044dccd  6800080000            push      800h
  0044dcd2  6a23                  push      23h
  0044dcd4  e8c7f10e00            call      0053CEA0h
  0044dcd9  83c410                add       esp,10h

LAB_0044dcdc:
  0044dcdc  bd0a000000            mov       ebp,0Ah
  0044dce1  6639ae98000000        cmp       [esi+98h],bp
  0044dce8  7519                  jne       short 0044DD03h
  0044dcea  6800100000            push      1000h
  0044dcef  6800100000            push      1000h
  0044dcf4  6800080000            push      800h
  0044dcf9  6a23                  push      23h
  0044dcfb  e8a0f10e00            call      0053CEA0h
  0044dd00  83c410                add       esp,10h

LAB_0044dd03:
  0044dd03  66ff8698000000        inc       word [esi+98h]
  0044dd0a  6683be980000001e      cmp       word [esi+98h],1Eh
  0044dd12  7507                  jne       short 0044DD1Bh
  0044dd14  6689ae98000000        mov       [esi+98h],bp

LAB_0044dd1b:
  0044dd1b  66837e1001            cmp       word [esi+10h],1
  0044dd20  bb03000000            mov       ebx,3
  0044dd25  bf02000000            mov       edi,2
  0044dd2a  756f                  jne       short 0044DD9Bh
  0044dd2c  6aff                  push      0FFFFFFFFh
  0044dd2e  6a00                  push      0
  0044dd30  689c000000            push      9Ch
  0044dd35  56                    push      esi
  0044dd36  66c7869a0000000000    mov       word [esi+9Ah],0
  0044dd3f  895e5c                mov       [esi+5Ch],ebx
  0044dd42  e899cd0200            call      0047AAE0h
  0044dd47  83c410                add       esp,10h
  0044dd4a  c7465c04000000        mov       dword [esi+5Ch],4
  0044dd51  6aff                  push      0FFFFFFFFh
  0044dd53  6a00                  push      0
  0044dd55  689c000000            push      9Ch
  0044dd5a  56                    push      esi
  0044dd5b  e880cd0200            call      0047AAE0h
  0044dd60  83c410                add       esp,10h
  0044dd63  6689ae9a000000        mov       [esi+9Ah],bp
  0044dd6a  6aff                  push      0FFFFFFFFh
  0044dd6c  6a00                  push      0
  0044dd6e  689c000000            push      9Ch
  0044dd73  56                    push      esi
  0044dd74  e867cd0200            call      0047AAE0h
  0044dd79  83c410                add       esp,10h
  0044dd7c  66c7869a0000001400    mov       word [esi+9Ah],14h
  0044dd85  6aff                  push      0FFFFFFFFh
  0044dd87  6a00                  push      0
  0044dd89  689c000000            push      9Ch
  0044dd8e  56                    push      esi
  0044dd8f  e84ccd0200            call      0047AAE0h
  0044dd94  83c410                add       esp,10h
  0044dd97  66897e10              mov       [esi+10h],di

LAB_0044dd9b:
  0044dd9b  66397e10              cmp       [esi+10h],di
  0044dd9f  0f85ea000000          jne       near 0044DE8Fh
  0044dda5  66837e163c            cmp       word [esi+16h],3Ch
  0044ddaa  7d0e                  jge       short 0044DDBAh
  0044ddac  8b7674                mov       esi,[esi+74h]
  0044ddaf  8b06                  mov       eax,[esi]
  0044ddb1  0c80                  or        al,80h
  0044ddb3  8906                  mov       [esi],eax
  0044ddb5  5f                    pop       edi
  0044ddb6  5e                    pop       esi
  0044ddb7  5d                    pop       ebp
  0044ddb8  5b                    pop       ebx
  0044ddb9  c3                    ret

LAB_0044ddba:
  0044ddba  f70600008000          test      dword [esi],800000h
  0044ddc0  0f84b2000000          je        near 0044DE78h
  0044ddc6  8b4674                mov       eax,[esi+74h]
  0044ddc9  8b08                  mov       ecx,[eax]
  0044ddcb  80c980                or        cl,80h
  0044ddce  8908                  mov       [eax],ecx
  0044ddd0  8b4674                mov       eax,[esi+74h]
  0044ddd3  8b15b4876000          mov       edx,[6087B4h]
  0044ddd9  660fb64008            movzx     ax,byte [eax+8]
  0044ddde  8b2a                  mov       ebp,[edx]
  0044dde0  0fbff8                movsx     edi,ax
  0044dde3  8d04ff                lea       eax,[edi+edi*8]
  0044dde6  8d4f78                lea       ecx,[edi+78h]
  0044dde9  c1e10d                shl       ecx,0Dh
  0044ddec  8d1447                lea       edx,[edi+eax*2]
  0044ddef  33c0                  xor       eax,eax
  0044ddf1  03cd                  add       ecx,ebp
  0044ddf3  8a049588915e00        mov       al,[edx*4+5E9188h]
  0044ddfa  c1e004                shl       eax,4
  0044ddfd  668b90204a6000        mov       dx,[eax+604A20h]
  0044de04  668591c41f0000        test      [ecx+1FC4h],dx
  0044de0b  750a                  jne       short 0044DE17h
  0044de0d  66c746124600          mov       word [esi+12h],46h
  0044de13  66895e10              mov       [esi+10h],bx

LAB_0044de17:
  0044de17  a1f02c5f00            mov       eax,[5F2CF0h]
  0044de1c  33d2                  xor       edx,edx
  0044de1e  bd0a000000            mov       ebp,0Ah
  0044de23  f7f5                  div       ebp
  0044de25  85d2                  test      edx,edx
  0044de27  752c                  jne       short 0044DE55h
  0044de29  668b814e010000        mov       ax,[ecx+14Eh]
  0044de30  663d0200              cmp       ax,2
  0044de34  7c0c                  jl        short 0044DE42h
  0044de36  83c0fe                add       eax,0FFFFFFFEh
  0044de39  6689814e010000        mov       [ecx+14Eh],ax
  0044de40  eb13                  jmp       short 0044DE55h

LAB_0044de42:
  0044de42  66c7814e0100000000    mov       word [ecx+14Eh],0
  0044de4b  66c746122800          mov       word [esi+12h],28h
  0044de51  66895e10              mov       [esi+10h],bx

LAB_0044de55:
  0044de55  f78140010000e8030000  test      dword [ecx+140h],3E8h
  0044de5f  7524                  jne       short 0044DE85h
  0044de61  8d04bf                lea       eax,[edi+edi*4]
  0044de64  c1e003                shl       eax,3
  0044de67  2bc7                  sub       eax,edi
  0044de69  f70485c0935e0000000040  test      dword [eax*4+5E93C0h],40000000h
  0044de74  7419                  je        short 0044DE8Fh
  0044de76  eb0d                  jmp       short 0044DE85h

LAB_0044de78:
  0044de78  8b4e5c                mov       ecx,[esi+5Ch]
  0044de7b  49                    dec       ecx
  0044de7c  8bc1                  mov       eax,ecx
  0044de7e  894e5c                mov       [esi+5Ch],ecx
  0044de81  85c0                  test      eax,eax
  0044de83  7f0a                  jg        short 0044DE8Fh

LAB_0044de85:
  0044de85  66c746124600          mov       word [esi+12h],46h
  0044de8b  66895e10              mov       [esi+10h],bx

LAB_0044de8f:
  0044de8f  66395e10              cmp       [esi+10h],bx
  0044de93  752c                  jne       short 0044DEC1h
  0044de95  8b4674                mov       eax,[esi+74h]
  0044de98  8b10                  mov       edx,[eax]
  0044de9a  80e27f                and       dl,7Fh
  0044de9d  8910                  mov       [eax],edx
  0044de9f  66ff4e12              dec       word [esi+12h]
  0044dea3  66837e1200            cmp       word [esi+12h],0
  0044dea8  7f17                  jg        short 0044DEC1h
  0044deaa  8b4674                mov       eax,[esi+74h]
  0044dead  6a00                  push      0
  0044deaf  6a00                  push      0
  0044deb1  56                    push      esi
  0044deb2  8b18                  mov       ebx,[eax]
  0044deb4  80e37f                and       bl,7Fh
  0044deb7  8918                  mov       [eax],ebx
  0044deb9  e862a1ffff            call      00448020h
  0044debe  83c40c                add       esp,0Ch

LAB_0044dec1:
  0044dec1  5f                    pop       edi
  0044dec2  5e                    pop       esi
  0044dec3  5d                    pop       ebp
  0044dec4  5b                    pop       ebx
  0044dec5  c3                    ret

14 cible(s) de saut interne : 0x0044dca8, 0x0044dcba, 0x0044dcdc, 0x0044dd03, 0x0044dd1b, 0x0044dd9b, 0x0044ddba, 0x0044de17, 0x0044de42, 0x0044de55, 0x0044de78, 0x0044de85, 0x0044de8f, 0x0044dec1
