Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00452bd0  section .text  offset fichier 0x51fd0  621 octets

  00452bd0  81ecd4000000          sub       esp,0D4h
  00452bd6  53                    push      ebx
  00452bd7  56                    push      esi
  00452bd8  57                    push      edi
  00452bd9  b80c030000            mov       eax,30Ch
  00452bde  b944fdffff            mov       ecx,0FFFFFD44h
  00452be3  89442428              mov       [esp+28h],eax
  00452be7  8944242c              mov       [esp+2Ch],eax
  00452beb  b8e8030000            mov       eax,3E8h
  00452bf0  894c2450              mov       [esp+50h],ecx
  00452bf4  89442430              mov       [esp+30h],eax
  00452bf8  89442434              mov       [esp+34h],eax
  00452bfc  b8e40c0000            mov       eax,0CE4h
  00452c01  894c2454              mov       [esp+54h],ecx
  00452c05  89442438              mov       [esp+38h],eax
  00452c09  8944243c              mov       [esp+3Ch],eax
  00452c0d  894c2458              mov       [esp+58h],ecx
  00452c11  894c245c              mov       [esp+5Ch],ecx
  00452c15  b85c0d0000            mov       eax,0D5Ch
  00452c1a  b9e0f7ffff            mov       ecx,0FFFFF7E0h
  00452c1f  89442440              mov       [esp+40h],eax
  00452c23  89442444              mov       [esp+44h],eax
  00452c27  894c2460              mov       [esp+60h],ecx
  00452c2b  894c2464              mov       [esp+64h],ecx
  00452c2f  b8ca0d0000            mov       eax,0DCAh
  00452c34  b930f3ffff            mov       ecx,0FFFFF330h
  00452c39  89442448              mov       [esp+48h],eax
  00452c3d  8944244c              mov       [esp+4Ch],eax
  00452c41  894c2468              mov       [esp+68h],ecx
  00452c45  894c246c              mov       [esp+6Ch],ecx
  00452c49  b89cffffff            mov       eax,0FFFFFF9Ch
  00452c4e  b9b8f2ffff            mov       ecx,0FFFFF2B8h
  00452c53  89842480000000        mov       [esp+80h],eax
  00452c5a  89842484000000        mov       [esp+84h],eax
  00452c61  89842488000000        mov       [esp+88h],eax
  00452c68  8984248c000000        mov       [esp+8Ch],eax
  00452c6f  89842490000000        mov       [esp+90h],eax
  00452c76  89842494000000        mov       [esp+94h],eax
  00452c7d  894c2470              mov       [esp+70h],ecx
  00452c81  894c2474              mov       [esp+74h],ecx
  00452c85  8bb424e4000000        mov       esi,[esp+0E4h]
  00452c8c  b838ffffff            mov       eax,0FFFFFF38h
  00452c91  b946f2ffff            mov       ecx,0FFFFF246h
  00452c96  33db                  xor       ebx,ebx
  00452c98  66395e10              cmp       [esi+10h],bx
  00452c9c  89842498000000        mov       [esp+98h],eax
  00452ca3  8984249c000000        mov       [esp+9Ch],eax
  00452caa  898424a0000000        mov       [esp+0A0h],eax
  00452cb1  898424a4000000        mov       [esp+0A4h],eax
  00452cb8  898424a8000000        mov       [esp+0A8h],eax
  00452cbf  898424ac000000        mov       [esp+0ACh],eax
  00452cc6  894c2478              mov       [esp+78h],ecx
  00452cca  894c247c              mov       [esp+7Ch],ecx
  00452cce  898424bc000000        mov       [esp+0BCh],eax
  00452cd5  b900080000            mov       ecx,800h
  00452cda  b8000e0000            mov       eax,0E00h
  00452cdf  895c2420              mov       [esp+20h],ebx
  00452ce3  895c2424              mov       [esp+24h],ebx
  00452ce7  898c24b0000000        mov       [esp+0B0h],ecx
  00452cee  899c24b4000000        mov       [esp+0B4h],ebx
  00452cf5  c78424b8000000c8080000  mov       dword [esp+0B8h],8C8h
  00452d00  898c24c0000000        mov       [esp+0C0h],ecx
  00452d07  899c24c4000000        mov       [esp+0C4h],ebx
  00452d0e  c78424c800000000060000  mov       dword [esp+0C8h],600h
  00452d19  898424cc000000        mov       [esp+0CCh],eax
  00452d20  c78424d000000000060000  mov       dword [esp+0D0h],600h
  00452d2b  898424d4000000        mov       [esp+0D4h],eax
  00452d32  c78424d800000000060000  mov       dword [esp+0D8h],600h
  00452d3d  898424dc000000        mov       [esp+0DCh],eax
  00452d44  750d                  jne       short 00452D53h
  00452d46  66c746100100          mov       word [esi+10h],1
  00452d4c  66899e98000000        mov       [esi+98h],bx

LAB_00452d53:
  00452d53  8b461c                mov       eax,[esi+1Ch]
  00452d56  8b4e20                mov       ecx,[esi+20h]
  00452d59  8b5624                mov       edx,[esi+24h]
  00452d5c  66ff8698000000        inc       word [esi+98h]
  00452d63  8944241c              mov       [esp+1Ch],eax
  00452d67  668b4632              mov       ax,[esi+32h]
  00452d6b  894c2418              mov       [esp+18h],ecx
  00452d6f  89542414              mov       [esp+14h],edx
  00452d73  6689442410            mov       [esp+10h],ax
  00452d78  66895e14              mov       [esi+14h],bx
  00452d7c  33ff                  xor       edi,edi
  00452d7e  c744240c0c000000      mov       dword [esp+0Ch],0Ch

LAB_00452d86:
  00452d86  8b4c3c20              mov       ecx,[esp+edi+20h]
  00452d8a  8b943c80000000        mov       edx,[esp+edi+80h]
  00452d91  8b443c50              mov       eax,[esp+edi+50h]
  00452d95  53                    push      ebx
  00452d96  c1e10c                shl       ecx,0Ch
  00452d99  53                    push      ebx
  00452d9a  6aff                  push      0FFFFFFFFh
  00452d9c  894e1c                mov       [esi+1Ch],ecx
  00452d9f  668b8c3cbc000000      mov       cx,[esp+edi+0BCh]
  00452da7  53                    push      ebx
  00452da8  6a3c                  push      3Ch
  00452daa  c1e20c                shl       edx,0Ch
  00452dad  c1e00c                shl       eax,0Ch
  00452db0  56                    push      esi
  00452db1  895620                mov       [esi+20h],edx
  00452db4  894624                mov       [esi+24h],eax
  00452db7  66894e32              mov       [esi+32h],cx
  00452dbb  e850530100            call      00468110h
  00452dc0  83c418                add       esp,18h
  00452dc3  6683be980000000f      cmp       word [esi+98h],0Fh
  00452dcb  7529                  jne       short 00452DF6h
  00452dcd  8b15cc975e00          mov       edx,[5E97CCh]
  00452dd3  53                    push      ebx
  00452dd4  53                    push      ebx
  00452dd5  6800090000            push      900h
  00452dda  8b82a8000000          mov       eax,[edx+0A8h]
  00452de0  6800060000            push      600h
  00452de5  8b08                  mov       ecx,[eax]
  00452de7  660fb6511d            movzx     dx,byte [ecx+1Dh]
  00452dec  52                    push      edx
  00452ded  56                    push      esi
  00452dee  e88d520100            call      00468080h
  00452df3  83c418                add       esp,18h

LAB_00452df6:
  00452df6  8b44240c              mov       eax,[esp+0Ch]
  00452dfa  83c704                add       edi,4
  00452dfd  48                    dec       eax
  00452dfe  8944240c              mov       [esp+0Ch],eax
  00452e02  7582                  jne       short 00452D86h
  00452e04  6683be980000000f      cmp       word [esi+98h],0Fh
  00452e0c  7507                  jne       short 00452E15h
  00452e0e  66899e98000000        mov       [esi+98h],bx

LAB_00452e15:
  00452e15  8b44241c              mov       eax,[esp+1Ch]
  00452e19  8b4c2418              mov       ecx,[esp+18h]
  00452e1d  8b542414              mov       edx,[esp+14h]
  00452e21  89461c                mov       [esi+1Ch],eax
  00452e24  668b442410            mov       ax,[esp+10h]
  00452e29  894e20                mov       [esi+20h],ecx
  00452e2c  895624                mov       [esi+24h],edx
  00452e2f  66894632              mov       [esi+32h],ax
  00452e33  5f                    pop       edi
  00452e34  5e                    pop       esi
  00452e35  5b                    pop       ebx
  00452e36  81c4d4000000          add       esp,0D4h
  00452e3c  c3                    ret

4 cible(s) de saut interne : 0x00452d53, 0x00452d86, 0x00452df6, 0x00452e15
