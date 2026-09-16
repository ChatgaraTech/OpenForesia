Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00454bb0  section .text  offset fichier 0x53fb0  475 octets

  00454bb0  55                    push      ebp
  00454bb1  56                    push      esi
  00454bb2  8b74240c              mov       esi,[esp+0Ch]
  00454bb6  33ed                  xor       ebp,ebp
  00454bb8  57                    push      edi
  00454bb9  66396e10              cmp       [esi+10h],bp
  00454bbd  750d                  jne       short 00454BCCh
  00454bbf  66c746100100          mov       word [esi+10h],1
  00454bc5  66896e12              mov       [esi+12h],bp
  00454bc9  896e5c                mov       [esi+5Ch],ebp

LAB_00454bcc:
  00454bcc  837e5c0a              cmp       dword [esi+5Ch],0Ah
  00454bd0  752b                  jne       short 00454BFDh
  00454bd2  896e5c                mov       [esi+5Ch],ebp
  00454bd5  a1cc975e00            mov       eax,[5E97CCh]
  00454bda  55                    push      ebp
  00454bdb  55                    push      ebp
  00454bdc  8b88a8000000          mov       ecx,[eax+0A8h]
  00454be2  6800100000            push      1000h
  00454be7  6800040000            push      400h
  00454bec  8b11                  mov       edx,[ecx]
  00454bee  660fb6421b            movzx     ax,byte [edx+1Bh]
  00454bf3  50                    push      eax
  00454bf4  56                    push      esi
  00454bf5  e886340100            call      00468080h
  00454bfa  83c418                add       esp,18h

LAB_00454bfd:
  00454bfd  8b4e5c                mov       ecx,[esi+5Ch]
  00454c00  41                    inc       ecx
  00454c01  66837e1001            cmp       word [esi+10h],1
  00454c06  894e5c                mov       [esi+5Ch],ecx
  00454c09  0f8578010000          jne       near 00454D87h
  00454c0f  668b4e14              mov       cx,[esi+14h]
  00454c13  663bcd                cmp       cx,bp
  00454c16  7528                  jne       short 00454C40h
  00454c18  66ff4616              inc       word [esi+16h]
  00454c1c  8b561c                mov       edx,[esi+1Ch]
  00454c1f  668b4616              mov       ax,[esi+16h]
  00454c23  81c200400100          add       edx,14000h
  00454c29  663d0800              cmp       ax,8
  00454c2d  89561c                mov       [esi+1Ch],edx
  00454c30  7c0e                  jl        short 00454C40h
  00454c32  66c746140100          mov       word [esi+14h],1
  00454c38  66896e16              mov       [esi+16h],bp
  00454c3c  5f                    pop       edi
  00454c3d  5e                    pop       esi
  00454c3e  5d                    pop       ebp
  00454c3f  c3                    ret

LAB_00454c40:
  00454c40  6683f901              cmp       cx,1
  00454c44  7523                  jne       short 00454C69h
  00454c46  66ff4616              inc       word [esi+16h]
  00454c4a  6683463240            add       word [esi+32h],40h
  00454c4f  66817e320010          cmp       word [esi+32h],1000h
  00454c55  7c12                  jl        short 00454C69h
  00454c57  66c746140200          mov       word [esi+14h],2
  00454c5d  66896e16              mov       [esi+16h],bp
  00454c61  66896e32              mov       [esi+32h],bp
  00454c65  5f                    pop       edi
  00454c66  5e                    pop       esi
  00454c67  5d                    pop       ebp
  00454c68  c3                    ret

LAB_00454c69:
  00454c69  6683f902              cmp       cx,2
  00454c6d  ba00c0feff            mov       edx,0FFFEC000h
  00454c72  7524                  jne       short 00454C98h
  00454c74  66ff4616              inc       word [esi+16h]
  00454c78  8b7e1c                mov       edi,[esi+1Ch]
  00454c7b  668b4616              mov       ax,[esi+16h]
  00454c7f  03fa                  add       edi,edx
  00454c81  663d0800              cmp       ax,8
  00454c85  897e1c                mov       [esi+1Ch],edi
  00454c88  7c0e                  jl        short 00454C98h
  00454c8a  66c746140300          mov       word [esi+14h],3
  00454c90  66896e16              mov       [esi+16h],bp
  00454c94  5f                    pop       edi
  00454c95  5e                    pop       esi
  00454c96  5d                    pop       ebp
  00454c97  c3                    ret

LAB_00454c98:
  00454c98  6683f903              cmp       cx,3
  00454c9c  7530                  jne       short 00454CCEh
  00454c9e  8b7e1c                mov       edi,[esi+1Ch]
  00454ca1  66ff4616              inc       word [esi+16h]
  00454ca5  668b4616              mov       ax,[esi+16h]
  00454ca9  03fa                  add       edi,edx
  00454cab  897e1c                mov       [esi+1Ch],edi
  00454cae  8b7e20                mov       edi,[esi+20h]
  00454cb1  81c7002cffff          add       edi,0FFFF2C00h
  00454cb7  663d1e01              cmp       ax,11Eh
  00454cbb  897e20                mov       [esi+20h],edi
  00454cbe  7c0e                  jl        short 00454CCEh
  00454cc0  66c746140400          mov       word [esi+14h],4
  00454cc6  66896e16              mov       [esi+16h],bp
  00454cca  5f                    pop       edi
  00454ccb  5e                    pop       esi
  00454ccc  5d                    pop       ebp
  00454ccd  c3                    ret

LAB_00454cce:
  00454cce  6683f904              cmp       cx,4
  00454cd2  7524                  jne       short 00454CF8h
  00454cd4  66ff4616              inc       word [esi+16h]
  00454cd8  8b7e1c                mov       edi,[esi+1Ch]
  00454cdb  668b4616              mov       ax,[esi+16h]
  00454cdf  03fa                  add       edi,edx
  00454ce1  663d0800              cmp       ax,8
  00454ce5  897e1c                mov       [esi+1Ch],edi
  00454ce8  7c0e                  jl        short 00454CF8h
  00454cea  66c746140500          mov       word [esi+14h],5
  00454cf0  66896e16              mov       [esi+16h],bp
  00454cf4  5f                    pop       edi
  00454cf5  5e                    pop       esi
  00454cf6  5d                    pop       ebp
  00454cf7  c3                    ret

LAB_00454cf8:
  00454cf8  6683f905              cmp       cx,5
  00454cfc  7527                  jne       short 00454D25h
  00454cfe  6683463240            add       word [esi+32h],40h
  00454d03  668b4632              mov       ax,[esi+32h]
  00454d07  66ff4616              inc       word [esi+16h]
  00454d0b  663d0008              cmp       ax,800h
  00454d0f  7c14                  jl        short 00454D25h
  00454d11  66c746140600          mov       word [esi+14h],6
  00454d17  66896e16              mov       [esi+16h],bp
  00454d1b  66c746320008          mov       word [esi+32h],800h
  00454d21  5f                    pop       edi
  00454d22  5e                    pop       esi
  00454d23  5d                    pop       ebp
  00454d24  c3                    ret

LAB_00454d25:
  00454d25  6683f906              cmp       cx,6
  00454d29  7528                  jne       short 00454D53h
  00454d2b  66ff4616              inc       word [esi+16h]
  00454d2f  8b7e1c                mov       edi,[esi+1Ch]
  00454d32  668b4616              mov       ax,[esi+16h]
  00454d36  81c700400100          add       edi,14000h
  00454d3c  663d0800              cmp       ax,8
  00454d40  897e1c                mov       [esi+1Ch],edi
  00454d43  7c0e                  jl        short 00454D53h
  00454d45  66c746140700          mov       word [esi+14h],7
  00454d4b  66896e16              mov       [esi+16h],bp
  00454d4f  5f                    pop       edi
  00454d50  5e                    pop       esi
  00454d51  5d                    pop       ebp
  00454d52  c3                    ret

LAB_00454d53:
  00454d53  6683f907              cmp       cx,7
  00454d57  752e                  jne       short 00454D87h
  00454d59  66ff4616              inc       word [esi+16h]
  00454d5d  8b7e1c                mov       edi,[esi+1Ch]
  00454d60  8b5620                mov       edx,[esi+20h]
  00454d63  668b4616              mov       ax,[esi+16h]
  00454d67  81c700400100          add       edi,14000h
  00454d6d  81c200d40000          add       edx,0D400h
  00454d73  663d1e01              cmp       ax,11Eh
  00454d77  897e1c                mov       [esi+1Ch],edi
  00454d7a  895620                mov       [esi+20h],edx
  00454d7d  7c08                  jl        short 00454D87h
  00454d7f  66896e14              mov       [esi+14h],bp
  00454d83  66896e16              mov       [esi+16h],bp

LAB_00454d87:
  00454d87  5f                    pop       edi
  00454d88  5e                    pop       esi
  00454d89  5d                    pop       ebp
  00454d8a  c3                    ret

10 cible(s) de saut interne : 0x00454bcc, 0x00454bfd, 0x00454c40, 0x00454c69, 0x00454c98, 0x00454cce, 0x00454cf8, 0x00454d25, 0x00454d53, 0x00454d87
