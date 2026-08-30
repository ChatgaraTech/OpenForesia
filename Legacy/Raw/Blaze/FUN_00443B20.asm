; 0x00443b20 — le verbe « joue l'animation de la statue » (95 instructions, absente de Blaze.c). Son etat vit dans le bloc prive : phase en 0x0057150c, compteur en 0x00571510.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00443b20  section .text  offset fichier 0x42f20  320 octets

  00443b20  51                    push      ecx
  00443b21  53                    push      ebx
  00443b22  55                    push      ebp
  00443b23  56                    push      esi
  00443b24  57                    push      edi
  00443b25  8b7c2410              mov       edi,[esp+10h]
  00443b29  33ed                  xor       ebp,ebp
  00443b2b  33c9                  xor       ecx,ecx
  00443b2d  33d2                  xor       edx,edx
  00443b2f  b8a0ca5e00            mov       eax,5ECAA0h
  00443b34  be20000000            mov       esi,20h

LAB_00443b39:
  00443b39  80780c03              cmp       byte [eax+0Ch],3
  00443b3d  752b                  jne       short 00443B6Ah
  00443b3f  f70000000080          test      dword [eax],80000000h
  00443b45  7423                  je        short 00443B6Ah
  00443b47  33d2                  xor       edx,edx
  00443b49  66896818              mov       [eax+18h],bp
  00443b4d  8a5004                mov       dl,[eax+4]
  00443b50  8bc8                  mov       ecx,eax
  00443b52  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00443b59  8b3a                  mov       edi,[edx]
  00443b5b  8b10                  mov       edx,[eax]
  00443b5d  81ca00100008          or        edx,8001000h
  00443b63  8910                  mov       [eax],edx
  00443b65  ba01000000            mov       edx,1

LAB_00443b6a:
  00443b6a  059c000000            add       eax,9Ch
  00443b6f  4e                    dec       esi
  00443b70  75c7                  jne       short 00443B39h
  00443b72  66a10c155700          mov       ax,[57150Ch]
  00443b78  663bc5                cmp       ax,bp
  00443b7b  7517                  jne       short 00443B94h
  00443b7d  663bd5                cmp       dx,bp
  00443b80  7412                  je        short 00443B94h
  00443b82  66b80100              mov       ax,1
  00443b86  c6410a00              mov       byte [ecx+0Ah],0
  00443b8a  6689691a              mov       [ecx+1Ah],bp
  00443b8e  66a30c155700          mov       [57150Ch],ax

LAB_00443b94:
  00443b94  663d0100              cmp       ax,1
  00443b98  751e                  jne       short 00443BB8h
  00443b9a  8405fa965e00          test      [5E96FAh],al
  00443ba0  7416                  je        short 00443BB8h
  00443ba2  66b80200              mov       ax,2
  00443ba6  6633db                xor       bx,bx
  00443ba9  66a30c155700          mov       [57150Ch],ax
  00443baf  66891d10155700        mov       [571510h],bx
  00443bb6  eb07                  jmp       short 00443BBFh

LAB_00443bb8:
  00443bb8  668b1d10155700        mov       bx,[571510h]

LAB_00443bbf:
  00443bbf  663d0200              cmp       ax,2
  00443bc3  7541                  jne       short 00443C06h
  00443bc5  6683fb08              cmp       bx,8
  00443bc9  7e32                  jle       short 00443BFDh
  00443bcb  f6c301                test      bl,1
  00443bce  742d                  je        short 00443BFDh
  00443bd0  66ff411a              inc       word [ecx+1Ah]
  00443bd4  668b5716              mov       dx,[edi+16h]
  00443bd8  668b711a              mov       si,[ecx+1Ah]
  00443bdc  8bfa                  mov       edi,edx
  00443bde  81e7ffff0000          and       edi,0FFFFh
  00443be4  0fbff6                movsx     esi,si
  00443be7  4f                    dec       edi
  00443be8  3bf7                  cmp       esi,edi
  00443bea  7c11                  jl        short 00443BFDh
  00443bec  83c2fe                add       edx,0FFFFFFFEh
  00443bef  66b80300              mov       ax,3
  00443bf3  6689511a              mov       [ecx+1Ah],dx
  00443bf7  66a30c155700          mov       [57150Ch],ax

LAB_00443bfd:
  00443bfd  6643                  inc       bx
  00443bff  66891d10155700        mov       [571510h],bx

LAB_00443c06:
  00443c06  663d0300              cmp       ax,3
  00443c0a  751f                  jne       short 00443C2Bh
  00443c0c  f605fa965e0001        test      byte [5E96FAh],1
  00443c13  7514                  jne       short 00443C29h
  00443c15  66b80400              mov       ax,4
  00443c19  6633db                xor       bx,bx
  00443c1c  66a30c155700          mov       [57150Ch],ax
  00443c22  66891d10155700        mov       [571510h],bx

LAB_00443c29:
  00443c29  33ed                  xor       ebp,ebp

LAB_00443c2b:
  00443c2b  663d0400              cmp       ax,4
  00443c2f  7525                  jne       short 00443C56h
  00443c31  f6c301                test      bl,1
  00443c34  7417                  je        short 00443C4Dh
  00443c36  66ff491a              dec       word [ecx+1Ah]
  00443c3a  6639691a              cmp       [ecx+1Ah],bp
  00443c3e  7f0d                  jg        short 00443C4Dh
  00443c40  6689691a              mov       [ecx+1Ah],bp
  00443c44  66c7050c1557000500    mov       word [57150Ch],5

LAB_00443c4d:
  00443c4d  6643                  inc       bx
  00443c4f  66891d10155700        mov       [571510h],bx

LAB_00443c56:
  00443c56  5f                    pop       edi
  00443c57  5e                    pop       esi
  00443c58  5d                    pop       ebp
  00443c59  33c0                  xor       eax,eax
  00443c5b  5b                    pop       ebx
  00443c5c  59                    pop       ecx
  00443c5d  c3                    ret
  00443c5e  90                    nop
  00443c5f  90                    nop

11 cible(s) de saut interne : 0x00443b39, 0x00443b6a, 0x00443b94, 0x00443bb8, 0x00443bbf, 0x00443bfd, 0x00443c06, 0x00443c29, 0x00443c2b, 0x00443c4d, 0x00443c56
