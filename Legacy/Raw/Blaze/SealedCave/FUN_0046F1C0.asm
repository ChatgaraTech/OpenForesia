Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046f1c0  section .text  offset fichier 0x6e5c0  288 octets

  0046f1c0  56                    push      esi
  0046f1c1  8b742408              mov       esi,[esp+8]
  0046f1c5  57                    push      edi
  0046f1c6  6800000040            push      40000000h
  0046f1cb  6a10                  push      10h
  0046f1cd  56                    push      esi
  0046f1ce  e82d5e0c00            call      00535000h
  0046f1d3  83c40c                add       esp,0Ch
  0046f1d6  8bf8                  mov       edi,eax
  0046f1d8  e8937bffff            call      00466D70h
  0046f1dd  0fbfc0                movsx     eax,ax
  0046f1e0  3bf8                  cmp       edi,eax
  0046f1e2  7522                  jne       short 0046F206h
  0046f1e4  33c0                  xor       eax,eax
  0046f1e6  66394610              cmp       [esi+10h],ax
  0046f1ea  751a                  jne       short 0046F206h
  0046f1ec  8b4e20                mov       ecx,[esi+20h]
  0046f1ef  66c746100100          mov       word [esi+10h],1
  0046f1f5  c1f90c                sar       ecx,0Ch
  0046f1f8  66894612              mov       [esi+12h],ax
  0046f1fc  89465c                mov       [esi+5Ch],eax
  0046f1ff  66898e9a000000        mov       [esi+9Ah],cx

LAB_0046f206:
  0046f206  e8657bffff            call      00466D70h
  0046f20b  85ff                  test      edi,edi
  0046f20d  750d                  jne       short 0046F21Ch
  0046f20f  66837e1063            cmp       word [esi+10h],63h
  0046f214  7506                  jne       short 0046F21Ch
  0046f216  66c746100300          mov       word [esi+10h],3

LAB_0046f21c:
  0046f21c  668b5610              mov       dx,[esi+10h]
  0046f220  6683fa01              cmp       dx,1
  0046f224  753b                  jne       short 0046F261h
  0046f226  668b4612              mov       ax,[esi+12h]
  0046f22a  0fbfc8                movsx     ecx,ax
  0046f22d  8bf9                  mov       edi,ecx
  0046f22f  c1e70c                shl       edi,0Ch
  0046f232  017e20                add       [esi+20h],edi
  0046f235  8b7e5c                mov       edi,[esi+5Ch]
  0046f238  03f9                  add       edi,ecx
  0046f23a  668b4e14              mov       cx,[esi+14h]
  0046f23e  40                    inc       eax
  0046f23f  897e5c                mov       [esi+5Ch],edi
  0046f242  663bc1                cmp       ax,cx
  0046f245  66894612              mov       [esi+12h],ax
  0046f249  7e04                  jle       short 0046F24Fh
  0046f24b  66894e12              mov       [esi+12h],cx

LAB_0046f24f:
  0046f24f  0fbf4616              movsx     eax,word [esi+16h]
  0046f253  39465c                cmp       [esi+5Ch],eax
  0046f256  7e09                  jle       short 0046F261h
  0046f258  66c746100200          mov       word [esi+10h],2
  0046f25e  5f                    pop       edi
  0046f25f  5e                    pop       esi
  0046f260  c3                    ret

LAB_0046f261:
  0046f261  6683fa02              cmp       dx,2
  0046f265  7522                  jne       short 0046F289h
  0046f267  668b4612              mov       ax,[esi+12h]
  0046f26b  8b5620                mov       edx,[esi+20h]
  0046f26e  0fbfc8                movsx     ecx,ax
  0046f271  c1e10c                shl       ecx,0Ch
  0046f274  03d1                  add       edx,ecx
  0046f276  48                    dec       eax
  0046f277  6685c0                test      ax,ax
  0046f27a  895620                mov       [esi+20h],edx
  0046f27d  66894612              mov       [esi+12h],ax
  0046f281  7506                  jne       short 0046F289h
  0046f283  66c746106300          mov       word [esi+10h],63h

LAB_0046f289:
  0046f289  66837e1003            cmp       word [esi+10h],3
  0046f28e  7540                  jne       short 0046F2D0h
  0046f290  668b4612              mov       ax,[esi+12h]
  0046f294  8b7e20                mov       edi,[esi+20h]
  0046f297  0fbfd0                movsx     edx,ax
  0046f29a  c1e20c                shl       edx,0Ch
  0046f29d  03fa                  add       edi,edx
  0046f29f  48                    dec       eax
  0046f2a0  663de2ff              cmp       ax,0FFE2h
  0046f2a4  897e20                mov       [esi+20h],edi
  0046f2a7  66894612              mov       [esi+12h],ax
  0046f2ab  7d06                  jge       short 0046F2B3h
  0046f2ad  66c74612e2ff          mov       word [esi+12h],0FFE2h

LAB_0046f2b3:
  0046f2b3  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0046f2ba  8b4e20                mov       ecx,[esi+20h]
  0046f2bd  c1f90c                sar       ecx,0Ch
  0046f2c0  3bc8                  cmp       ecx,eax
  0046f2c2  7d0c                  jge       short 0046F2D0h
  0046f2c4  c1e00c                shl       eax,0Ch
  0046f2c7  894620                mov       [esi+20h],eax
  0046f2ca  66c746100000          mov       word [esi+10h],0

LAB_0046f2d0:
  0046f2d0  5f                    pop       edi
  0046f2d1  5e                    pop       esi
  0046f2d2  c3                    ret
  0046f2d3  90                    nop
  0046f2d4  90                    nop
  0046f2d5  90                    nop
  0046f2d6  90                    nop
  0046f2d7  90                    nop
  0046f2d8  90                    nop
  0046f2d9  90                    nop
  0046f2da  90                    nop
  0046f2db  90                    nop
  0046f2dc  90                    nop
  0046f2dd  90                    nop
  0046f2de  90                    nop
  0046f2df  90                    nop

7 cible(s) de saut interne : 0x0046f206, 0x0046f21c, 0x0046f24f, 0x0046f261, 0x0046f289, 0x0046f2b3, 0x0046f2d0
