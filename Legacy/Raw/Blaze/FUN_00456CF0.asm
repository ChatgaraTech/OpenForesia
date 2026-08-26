Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456cf0  section .text  offset fichier 0x560f0  395 octets

  00456cf0  53                    push      ebx
  00456cf1  55                    push      ebp
  00456cf2  56                    push      esi
  00456cf3  8b742410              mov       esi,[esp+10h]
  00456cf7  33db                  xor       ebx,ebx
  00456cf9  57                    push      edi
  00456cfa  66395e10              cmp       [esi+10h],bx
  00456cfe  bd01000000            mov       ebp,1
  00456d03  7537                  jne       short 00456D3Ch
  00456d05  b894110000            mov       eax,1194h
  00456d0a  66896e10              mov       [esi+10h],bp
  00456d0e  66894628              mov       [esi+28h],ax
  00456d12  6689462c              mov       [esi+2Ch],ax
  00456d16  66895e12              mov       [esi+12h],bx
  00456d1a  66895e16              mov       [esi+16h],bx
  00456d1e  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  00456d25  894644                mov       [esi+44h],eax
  00456d28  0fbf0dba925e00        movsx     ecx,word [5E92BAh]
  00456d2f  894e48                mov       [esi+48h],ecx
  00456d32  0fbf15bc925e00        movsx     edx,word [5E92BCh]
  00456d39  89564c                mov       [esi+4Ch],edx

LAB_00456d3c:
  00456d3c  66396e10              cmp       [esi+10h],bp
  00456d40  754b                  jne       short 00456D8Dh
  00456d42  8b0e                  mov       ecx,[esi]
  00456d44  6800000040            push      40000000h
  00456d49  80e5df                and       ch,0DFh
  00456d4c  6a10                  push      10h
  00456d4e  56                    push      esi
  00456d4f  890e                  mov       [esi],ecx
  00456d51  895e54                mov       [esi+54h],ebx
  00456d54  895e50                mov       [esi+50h],ebx
  00456d57  895e58                mov       [esi+58h],ebx
  00456d5a  e8a1e20d00            call      00535000h
  00456d5f  8bf8                  mov       edi,eax
  00456d61  83c40c                add       esp,0Ch
  00456d64  663bfb                cmp       di,bx
  00456d67  7420                  je        short 00456D89h
  00456d69  66395e12              cmp       [esi+12h],bx
  00456d6d  7515                  jne       short 00456D84h
  00456d6f  66896e12              mov       [esi+12h],bp
  00456d73  e8c8030f00            call      00547140h
  00456d78  99                    cdq
  00456d79  b906000000            mov       ecx,6
  00456d7e  f7f9                  idiv      ecx
  00456d80  66895614              mov       [esi+14h],dx

LAB_00456d84:
  00456d84  663bfb                cmp       di,bx
  00456d87  7504                  jne       short 00456D8Dh

LAB_00456d89:
  00456d89  66895e12              mov       [esi+12h],bx

LAB_00456d8d:
  00456d8d  bf02000000            mov       edi,2
  00456d92  66397e10              cmp       [esi+10h],di
  00456d96  755c                  jne       short 00456DF4h
  00456d98  b88a915e00            mov       eax,5E918Ah
  00456d9d  b904000000            mov       ecx,4

LAB_00456da2:
  00456da2  8818                  mov       [eax],bl
  00456da4  83c04c                add       eax,4Ch
  00456da7  49                    dec       ecx
  00456da8  75f8                  jne       short 00456DA2h
  00456daa  8b06                  mov       eax,[esi]
  00456dac  8a5607                mov       dl,[esi+7]
  00456daf  80cc20                or        ah,20h
  00456db2  80ca10                or        dl,10h
  00456db5  8906                  mov       [esi],eax
  00456db7  885607                mov       [esi+7],dl
  00456dba  c7465400400600        mov       dword [esi+54h],64000h
  00456dc1  895e50                mov       [esi+50h],ebx
  00456dc4  895e58                mov       [esi+58h],ebx
  00456dc7  8b15cc975e00          mov       edx,[5E97CCh]
  00456dcd  53                    push      ebx
  00456dce  53                    push      ebx
  00456dcf  8b82a8000000          mov       eax,[edx+0A8h]
  00456dd5  6800180000            push      1800h
  00456dda  6800040000            push      400h
  00456ddf  8b08                  mov       ecx,[eax]
  00456de1  660fb6512a            movzx     dx,byte [ecx+2Ah]
  00456de6  52                    push      edx
  00456de7  56                    push      esi
  00456de8  e893120100            call      00468080h
  00456ded  83c418                add       esp,18h
  00456df0  66896e10              mov       [esi+10h],bp

LAB_00456df4:
  00456df4  66397e16              cmp       [esi+16h],di
  00456df8  7527                  jne       short 00456E21h
  00456dfa  668b4644              mov       ax,[esi+44h]
  00456dfe  66a360915e00          mov       [5E9160h],ax
  00456e04  668b4e48              mov       cx,[esi+48h]
  00456e08  66890d62915e00        mov       [5E9162h],cx
  00456e0f  668b564c              mov       dx,[esi+4Ch]
  00456e13  66891564915e00        mov       [5E9164h],dx
  00456e1a  66893d66915e00        mov       [5E9166h],di

LAB_00456e21:
  00456e21  66396e16              cmp       [esi+16h],bp
  00456e25  754c                  jne       short 00456E73h
  00456e27  57                    push      edi
  00456e28  53                    push      ebx
  00456e29  55                    push      ebp
  00456e2a  6a04                  push      4
  00456e2c  56                    push      esi
  00456e2d  e80e6c0000            call      0045DA40h
  00456e32  83c414                add       esp,14h
  00456e35  6800080000            push      800h
  00456e3a  6800080000            push      800h
  00456e3f  6800040000            push      400h
  00456e44  6a03                  push      3
  00456e46  e855600e00            call      0053CEA0h
  00456e4b  b83c000000            mov       eax,3Ch
  00456e50  66897e16              mov       [esi+16h],di
  00456e54  83c410                add       esp,10h
  00456e57  66c70560915e00fa00    mov       word [5E9160h],0FAh
  00456e60  66a362915e00          mov       [5E9162h],ax
  00456e66  66a364915e00          mov       [5E9164h],ax
  00456e6c  66892d66915e00        mov       [5E9166h],bp

LAB_00456e73:
  00456e73  885e09                mov       [esi+9],bl
  00456e76  5f                    pop       edi
  00456e77  5e                    pop       esi
  00456e78  5d                    pop       ebp
  00456e79  5b                    pop       ebx
  00456e7a  c3                    ret

8 cible(s) de saut interne : 0x00456d3c, 0x00456d84, 0x00456d89, 0x00456d8d, 0x00456da2, 0x00456df4, 0x00456e21, 0x00456e73
