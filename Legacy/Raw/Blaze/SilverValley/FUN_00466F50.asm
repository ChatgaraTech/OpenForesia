Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00466f50  section .text  offset fichier 0x66350  816 octets

  00466f50  53                    push      ebx
  00466f51  55                    push      ebp
  00466f52  56                    push      esi
  00466f53  8b742410              mov       esi,[esp+10h]
  00466f57  33ed                  xor       ebp,ebp
  00466f59  57                    push      edi
  00466f5a  66396e10              cmp       [esi+10h],bp
  00466f5e  bb1e000000            mov       ebx,1Eh
  00466f63  7511                  jne       short 00466F76h
  00466f65  895e5c                mov       [esi+5Ch],ebx
  00466f68  66896e12              mov       [esi+12h],bp
  00466f6c  66896e14              mov       [esi+14h],bp
  00466f70  66c746161400          mov       word [esi+16h],14h

LAB_00466f76:
  00466f76  6800000040            push      40000000h
  00466f7b  6a10                  push      10h
  00466f7d  56                    push      esi
  00466f7e  e87de00c00            call      00535000h
  00466f83  83c40c                add       esp,0Ch
  00466f86  8bf8                  mov       edi,eax
  00466f88  e8e3fdffff            call      00466D70h
  00466f8d  663bf8                cmp       di,ax
  00466f90  750c                  jne       short 00466F9Eh
  00466f92  66396e10              cmp       [esi+10h],bp
  00466f96  7506                  jne       short 00466F9Eh
  00466f98  66c746100100          mov       word [esi+10h],1

LAB_00466f9e:
  00466f9e  b9c0935e00            mov       ecx,5E93C0h
  00466fa3  ba04000000            mov       edx,4

LAB_00466fa8:
  00466fa8  8b01                  mov       eax,[ecx]
  00466faa  a900000080            test      eax,80000000h
  00466faf  7417                  je        short 00466FC8h
  00466fb1  a900000050            test      eax,50000000h
  00466fb6  7510                  jne       short 00466FC8h
  00466fb8  8b4120                mov       eax,[ecx+20h]
  00466fbb  8b7e20                mov       edi,[esi+20h]
  00466fbe  3bc7                  cmp       eax,edi
  00466fc0  7d06                  jge       short 00466FC8h
  00466fc2  66c746100300          mov       word [esi+10h],3

LAB_00466fc8:
  00466fc8  81c19c000000          add       ecx,9Ch
  00466fce  4a                    dec       edx
  00466fcf  75d7                  jne       short 00466FA8h
  00466fd1  668b4610              mov       ax,[esi+10h]
  00466fd5  663d0100              cmp       ax,1
  00466fd9  740a                  je        short 00466FE5h
  00466fdb  663d0300              cmp       ax,3
  00466fdf  0f8505010000          jne       near 004670EAh

LAB_00466fe5:
  00466fe5  8b565c                mov       edx,[esi+5Ch]
  00466fe8  66ff4612              inc       word [esi+12h]
  00466fec  42                    inc       edx
  00466fed  8bc2                  mov       eax,edx
  00466fef  89565c                mov       [esi+5Ch],edx
  00466ff2  3d96000000            cmp       eax,96h
  00466ff7  7e07                  jle       short 00467000h
  00466ff9  c7465c96000000        mov       dword [esi+5Ch],96h

LAB_00467000:
  00467000  0fbf4e32              movsx     ecx,word [esi+32h]
  00467004  51                    push      ecx
  00467005  e812ea0d00            call      00545A1Ch
  0046700a  8b565c                mov       edx,[esi+5Ch]
  0046700d  8b4e24                mov       ecx,[esi+24h]
  00467010  c1fa02                sar       edx,2
  00467013  0fafc2                imul      eax,edx
  00467016  03c8                  add       ecx,eax
  00467018  0fbf4632              movsx     eax,word [esi+32h]
  0046701c  50                    push      eax
  0046701d  894e24                mov       [esi+24h],ecx
  00467020  e8f1e90d00            call      00545A16h
  00467025  8b4e5c                mov       ecx,[esi+5Ch]
  00467028  c1f902                sar       ecx,2
  0046702b  0fafc1                imul      eax,ecx
  0046702e  8b4e1c                mov       ecx,[esi+1Ch]
  00467031  2bc8                  sub       ecx,eax
  00467033  894e1c                mov       [esi+1Ch],ecx
  00467036  668b4e12              mov       cx,[esi+12h]
  0046703a  6683f90a              cmp       cx,0Ah
  0046703e  7e0a                  jle       short 0046704Ah
  00467040  663bcb                cmp       cx,bx
  00467043  7d08                  jge       short 0046704Dh
  00467045  66834632e2            add       word [esi+32h],0FFE2h

LAB_0046704a:
  0046704a  663bcb                cmp       cx,bx

LAB_0046704d:
  0046704d  7e0a                  jle       short 00467059h
  0046704f  6683f932              cmp       cx,32h
  00467053  7d04                  jge       short 00467059h
  00467055  66015e32              add       [esi+32h],bx

LAB_00467059:
  00467059  6683f94b              cmp       cx,4Bh
  0046705d  7e24                  jle       short 00467083h
  0046705f  6683461402            add       word [esi+14h],2
  00467064  668b4614              mov       ax,[esi+14h]
  00467068  66294634              sub       [esi+34h],ax
  0046706c  0fbfc0                movsx     eax,ax
  0046706f  8bd0                  mov       edx,eax
  00467071  c1e202                shl       edx,2
  00467074  2bd0                  sub       edx,eax
  00467076  8b4620                mov       eax,[esi+20h]
  00467079  f7da                  neg       edx
  0046707b  c1e20a                shl       edx,0Ah
  0046707e  03c2                  add       eax,edx
  00467080  894620                mov       [esi+20h],eax

LAB_00467083:
  00467083  6683f961              cmp       cx,61h
  00467087  7e3d                  jle       short 004670C6h
  00467089  6683f962              cmp       cx,62h
  0046708d  66c746100200          mov       word [esi+10h],2
  00467093  66c7869a0000000f00    mov       word [esi+9Ah],0Fh
  0046709c  7528                  jne       short 004670C6h
  0046709e  a1cc975e00            mov       eax,[5E97CCh]
  004670a3  55                    push      ebp
  004670a4  55                    push      ebp
  004670a5  6800200000            push      2000h
  004670aa  8b88a8000000          mov       ecx,[eax+0A8h]
  004670b0  6800040000            push      400h
  004670b5  8b11                  mov       edx,[ecx]
  004670b7  660fb64214            movzx     ax,byte [edx+14h]
  004670bc  50                    push      eax
  004670bd  56                    push      esi
  004670be  e8bd0f0000            call      00468080h
  004670c3  83c418                add       esp,18h

LAB_004670c6:
  004670c6  668b5e12              mov       bx,[esi+12h]
  004670ca  66896e12              mov       [esi+12h],bp
  004670ce  bf04000000            mov       edi,4

LAB_004670d3:
  004670d3  55                    push      ebp
  004670d4  55                    push      ebp
  004670d5  6aff                  push      0FFFFFFFFh
  004670d7  55                    push      ebp
  004670d8  6a31                  push      31h
  004670da  56                    push      esi
  004670db  e830100000            call      00468110h
  004670e0  83c418                add       esp,18h
  004670e3  4f                    dec       edi
  004670e4  75ed                  jne       short 004670D3h
  004670e6  66895e12              mov       [esi+12h],bx

LAB_004670ea:
  004670ea  66837e1002            cmp       word [esi+10h],2
  004670ef  bf38ffffff            mov       edi,0FFFFFF38h
  004670f4  bd78000000            mov       ebp,78h
  004670f9  bb9cffffff            mov       ebx,0FFFFFF9Ch
  004670fe  0f8589000000          jne       near 0046718Dh
  00467104  668b869a000000        mov       ax,[esi+9Ah]
  0046710b  33c9                  xor       ecx,ecx
  0046710d  66f7d8                neg       ax
  00467110  6685c0                test      ax,ax
  00467113  0f9ec1                setle     cl
  00467116  49                    dec       ecx
  00467117  6a00                  push      0
  00467119  83e1fe                and       ecx,0FFFFFFFEh
  0046711c  6a00                  push      0
  0046711e  41                    inc       ecx
  0046711f  68e7030000            push      3E7h
  00467124  03c1                  add       eax,ecx
  00467126  0fbfd0                movsx     edx,ax
  00467129  52                    push      edx
  0046712a  68e7030000            push      3E7h
  0046712f  56                    push      esi
  00467130  6689869a000000        mov       [esi+9Ah],ax
  00467137  e8244dfbff            call      0041BE60h
  0046713c  668b4616              mov       ax,[esi+16h]
  00467140  66017e28              add       [esi+28h],di
  00467144  668b4e28              mov       cx,[esi+28h]
  00467148  66017e2a              add       [esi+2Ah],di
  0046714c  66017e2c              add       [esi+2Ch],di
  00467150  6683463014            add       word [esi+30h],14h
  00467155  66016e32              add       [esi+32h],bp
  00467159  66015e34              add       [esi+34h],bx
  0046715d  0fbfd0                movsx     edx,ax
  00467160  83c002                add       eax,2
  00467163  83c418                add       esp,18h
  00467166  c1e20c                shl       edx,0Ch
  00467169  295620                sub       [esi+20h],edx
  0046716c  66894616              mov       [esi+16h],ax
  00467170  33c0                  xor       eax,eax
  00467172  663bc8                cmp       cx,ax
  00467175  7d16                  jge       short 0046718Dh
  00467177  66894628              mov       [esi+28h],ax
  0046717b  6689462a              mov       [esi+2Ah],ax
  0046717f  6689462c              mov       [esi+2Ch],ax
  00467183  66c746106300          mov       word [esi+10h],63h
  00467189  66894612              mov       [esi+12h],ax

LAB_0046718d:
  0046718d  66837e1003            cmp       word [esi+10h],3
  00467192  757e                  jne       short 00467212h
  00467194  66015e34              add       [esi+34h],bx
  00467198  66017e28              add       [esi+28h],di
  0046719c  8b1e                  mov       ebx,[esi]
  0046719e  668b4e28              mov       cx,[esi+28h]
  004671a2  66017e2a              add       [esi+2Ah],di
  004671a6  66017e2c              add       [esi+2Ch],di
  004671aa  6683463014            add       word [esi+30h],14h
  004671af  66016e32              add       [esi+32h],bp
  004671b3  33d2                  xor       edx,edx
  004671b5  80cf20                or        bh,20h
  004671b8  663bca                cmp       cx,dx
  004671bb  891e                  mov       [esi],ebx
  004671bd  8bc3                  mov       eax,ebx
  004671bf  7d1b                  jge       short 004671DCh
  004671c1  80cc40                or        ah,40h
  004671c4  66895628              mov       [esi+28h],dx
  004671c8  6689562a              mov       [esi+2Ah],dx
  004671cc  6689562c              mov       [esi+2Ch],dx
  004671d0  66c746106300          mov       word [esi+10h],63h
  004671d6  66895612              mov       [esi+12h],dx
  004671da  8906                  mov       [esi],eax

LAB_004671dc:
  004671dc  bfc0935e00            mov       edi,5E93C0h
  004671e1  bb04000000            mov       ebx,4
  004671e6  bd0080f3ff            mov       ebp,0FFF38000h

LAB_004671eb:
  004671eb  f70700000080          test      dword [edi],80000000h
  004671f1  7416                  je        short 00467209h
  004671f3  396f20                cmp       [edi+20h],ebp
  004671f6  7d11                  jge       short 00467209h
  004671f8  a1cc975e00            mov       eax,[5E97CCh]
  004671fd  6a00                  push      0
  004671ff  57                    push      edi
  00467200  50                    push      eax
  00467201  e8cacb0a00            call      00513DD0h
  00467206  83c40c                add       esp,0Ch

LAB_00467209:
  00467209  81c79c000000          add       edi,9Ch
  0046720f  4b                    dec       ebx
  00467210  75d9                  jne       short 004671EBh

LAB_00467212:
  00467212  66837e1063            cmp       word [esi+10h],63h
  00467217  7560                  jne       short 00467279h
  00467219  b8dc935e00            mov       eax,5E93DCh
  0046721e  b904000000            mov       ecx,4

LAB_00467223:
  00467223  f740e400000080        test      dword [eax-1Ch],80000000h
  0046722a  740e                  je        short 0046723Ah
  0046722c  81380080a200          cmp       dword [eax],0A28000h
  00467232  7d06                  jge       short 0046723Ah
  00467234  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0046723a:
  0046723a  059c000000            add       eax,9Ch
  0046723f  49                    dec       ecx
  00467240  75e1                  jne       short 00467223h
  00467242  bec0935e00            mov       esi,5E93C0h
  00467247  bf04000000            mov       edi,4
  0046724c  bb00c0e0ff            mov       ebx,0FFE0C000h

LAB_00467251:
  00467251  f70600000080          test      dword [esi],80000000h
  00467257  7417                  je        short 00467270h
  00467259  395e20                cmp       [esi+20h],ebx
  0046725c  7d12                  jge       short 00467270h
  0046725e  8b0dcc975e00          mov       ecx,[5E97CCh]
  00467264  6a00                  push      0
  00467266  56                    push      esi
  00467267  51                    push      ecx
  00467268  e863cb0a00            call      00513DD0h
  0046726d  83c40c                add       esp,0Ch

LAB_00467270:
  00467270  81c69c000000          add       esi,9Ch
  00467276  4f                    dec       edi
  00467277  75d8                  jne       short 00467251h

LAB_00467279:
  00467279  5f                    pop       edi
  0046727a  5e                    pop       esi
  0046727b  5d                    pop       ebp
  0046727c  5b                    pop       ebx
  0046727d  c3                    ret
  0046727e  90                    nop
  0046727f  90                    nop

23 cible(s) de saut interne : 0x00466f76, 0x00466f9e, 0x00466fa8, 0x00466fc8, 0x00466fe5, 0x00467000, 0x0046704a, 0x0046704d, 0x00467059, 0x00467083, 0x004670c6, 0x004670d3, 0x004670ea, 0x0046718d, 0x004671dc, 0x004671eb, 0x00467209, 0x00467212, 0x00467223, 0x0046723a, 0x00467251, 0x00467270, 0x00467279
