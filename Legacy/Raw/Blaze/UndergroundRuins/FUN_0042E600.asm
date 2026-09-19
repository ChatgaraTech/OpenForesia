Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042e600  section .text  offset fichier 0x2da00  832 octets

  0042e600  53                    push      ebx
  0042e601  55                    push      ebp
  0042e602  56                    push      esi
  0042e603  8b742410              mov       esi,[esp+10h]
  0042e607  33db                  xor       ebx,ebx
  0042e609  57                    push      edi
  0042e60a  66395e10              cmp       [esi+10h],bx
  0042e60e  bf01000000            mov       edi,1
  0042e613  0f85db000000          jne       near 0042E6F4h
  0042e619  c7463c80095500        mov       dword [esi+3Ch],550980h
  0042e620  885e40                mov       [esi+40h],bl
  0042e623  c6460a0b              mov       byte [esi+0Ah],0Bh
  0042e627  885e0b                mov       [esi+0Bh],bl
  0042e62a  66897e10              mov       [esi+10h],di
  0042e62e  e80d8b1100            call      00547140h
  0042e633  99                    cdq
  0042e634  33c2                  xor       eax,edx
  0042e636  2bc2                  sub       eax,edx
  0042e638  25ff0f0000            and       eax,0FFFh
  0042e63d  33c2                  xor       eax,edx
  0042e63f  2bc2                  sub       eax,edx
  0042e641  66894612              mov       [esi+12h],ax
  0042e645  e8f68a1100            call      00547140h
  0042e64a  99                    cdq
  0042e64b  33c2                  xor       eax,edx
  0042e64d  2bc2                  sub       eax,edx
  0042e64f  25ff030000            and       eax,3FFh
  0042e654  33c2                  xor       eax,edx
  0042e656  2bc2                  sub       eax,edx
  0042e658  66894614              mov       [esi+14h],ax
  0042e65c  e8df8a1100            call      00547140h
  0042e661  99                    cdq
  0042e662  b964000000            mov       ecx,64h
  0042e667  f7f9                  idiv      ecx
  0042e669  81c22c010000          add       edx,12Ch
  0042e66f  6689969a000000        mov       [esi+9Ah],dx
  0042e676  e8c58a1100            call      00547140h
  0042e67b  99                    cdq
  0042e67c  b90f000000            mov       ecx,0Fh
  0042e681  f7f9                  idiv      ecx
  0042e683  8b4620                mov       eax,[esi+20h]
  0042e686  8b4e24                mov       ecx,[esi+24h]
  0042e689  894654                mov       [esi+54h],eax
  0042e68c  894e58                mov       [esi+58h],ecx
  0042e68f  83c20d                add       edx,0Dh
  0042e692  89565c                mov       [esi+5Ch],edx
  0042e695  8b561c                mov       edx,[esi+1Ch]
  0042e698  895650                mov       [esi+50h],edx
  0042e69b  e8a08a1100            call      00547140h
  0042e6a0  99                    cdq
  0042e6a1  33c2                  xor       eax,edx
  0042e6a3  885e38                mov       [esi+38h],bl
  0042e6a6  2bc2                  sub       eax,edx
  0042e6a8  885e39                mov       [esi+39h],bl
  0042e6ab  25ff030000            and       eax,3FFh
  0042e6b0  885e3a                mov       [esi+3Ah],bl
  0042e6b3  33c2                  xor       eax,edx
  0042e6b5  2bc2                  sub       eax,edx
  0042e6b7  0500040000            add       eax,400h
  0042e6bc  66894628              mov       [esi+28h],ax
  0042e6c0  6689462a              mov       [esi+2Ah],ax
  0042e6c4  e8778a1100            call      00547140h
  0042e6c9  99                    cdq
  0042e6ca  b996000000            mov       ecx,96h
  0042e6cf  f7f9                  idiv      ecx
  0042e6d1  895644                mov       [esi+44h],edx
  0042e6d4  e8678a1100            call      00547140h
  0042e6d9  99                    cdq
  0042e6da  b964000000            mov       ecx,64h
  0042e6df  f7f9                  idiv      ecx
  0042e6e1  895648                mov       [esi+48h],edx
  0042e6e4  e8578a1100            call      00547140h
  0042e6e9  99                    cdq
  0042e6ea  b964000000            mov       ecx,64h
  0042e6ef  f7f9                  idiv      ecx
  0042e6f1  89564c                mov       [esi+4Ch],edx

LAB_0042e6f4:
  0042e6f4  66397e10              cmp       [esi+10h],di
  0042e6f8  0f8538010000          jne       near 0042E836h
  0042e6fe  0fbf5614              movsx     edx,word [esi+14h]
  0042e702  52                    push      edx
  0042e703  e80e731100            call      00545A16h
  0042e708  8bf8                  mov       edi,eax
  0042e70a  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042e711  0fbf4e12              movsx     ecx,word [esi+12h]
  0042e715  0faff8                imul      edi,eax
  0042e718  51                    push      ecx
  0042e719  c1ff0c                sar       edi,0Ch
  0042e71c  e8fb721100            call      00545A1Ch
  0042e721  0faff8                imul      edi,eax
  0042e724  0fbf5614              movsx     edx,word [esi+14h]
  0042e728  8b4e50                mov       ecx,[esi+50h]
  0042e72b  52                    push      edx
  0042e72c  03f9                  add       edi,ecx
  0042e72e  897e1c                mov       [esi+1Ch],edi
  0042e731  e8e0721100            call      00545A16h
  0042e736  8bf8                  mov       edi,eax
  0042e738  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042e73f  0fbf4e12              movsx     ecx,word [esi+12h]
  0042e743  0faff8                imul      edi,eax
  0042e746  51                    push      ecx
  0042e747  c1ff0c                sar       edi,0Ch
  0042e74a  e8c7721100            call      00545A16h
  0042e74f  0faff8                imul      edi,eax
  0042e752  8b4e58                mov       ecx,[esi+58h]
  0042e755  0fbf5614              movsx     edx,word [esi+14h]
  0042e759  03f9                  add       edi,ecx
  0042e75b  52                    push      edx
  0042e75c  897e24                mov       [esi+24h],edi
  0042e75f  668bbe9a000000        mov       di,[esi+9Ah]
  0042e766  e8b1721100            call      00545A1Ch
  0042e76b  0fbfcf                movsx     ecx,di
  0042e76e  0fafc1                imul      eax,ecx
  0042e771  8b6e54                mov       ebp,[esi+54h]
  0042e774  668b4e48              mov       cx,[esi+48h]
  0042e778  668b564c              mov       dx,[esi+4Ch]
  0042e77c  66014e12              add       [esi+12h],cx
  0042e780  66015614              add       [esi+14h],dx
  0042e784  03c5                  add       eax,ebp
  0042e786  668b6e14              mov       bp,[esi+14h]
  0042e78a  894620                mov       [esi+20h],eax
  0042e78d  668bc7                mov       ax,di
  0042e790  668b7e12              mov       di,[esi+12h]
  0042e794  662b465c              sub       ax,[esi+5Ch]
  0042e798  6689869a000000        mov       [esi+9Ah],ax
  0042e79f  663dfa00              cmp       ax,0FAh
  0042e7a3  668b4644              mov       ax,[esi+44h]
  0042e7a7  7e0a                  jle       short 0042E7B3h
  0042e7a9  66014628              add       [esi+28h],ax
  0042e7ad  6601462a              add       [esi+2Ah],ax
  0042e7b1  eb17                  jmp       short 0042E7CAh

LAB_0042e7b3:
  0042e7b3  66d1e0                shl       ax,1
  0042e7b6  66014628              add       [esi+28h],ax
  0042e7ba  6601462a              add       [esi+2Ah],ax
  0042e7be  03f9                  add       edi,ecx
  0042e7c0  03ea                  add       ebp,edx
  0042e7c2  66897e12              mov       [esi+12h],di
  0042e7c6  66896e14              mov       [esi+14h],bp

LAB_0042e7ca:
  0042e7ca  66837e1628            cmp       word [esi+16h],28h
  0042e7cf  750a                  jne       short 0042E7DBh
  0042e7d1  8b5644                mov       edx,[esi+44h]
  0042e7d4  f7da                  neg       edx
  0042e7d6  d1fa                  sar       edx,1
  0042e7d8  895644                mov       [esi+44h],edx

LAB_0042e7db:
  0042e7db  8b565c                mov       edx,[esi+5Ch]
  0042e7de  6a0a                  push      0Ah
  0042e7e0  6a0a                  push      0Ah
  0042e7e2  4a                    dec       edx
  0042e7e3  6a0a                  push      0Ah
  0042e7e5  56                    push      esi
  0042e7e6  89565c                mov       [esi+5Ch],edx
  0042e7e9  e8f265fdff            call      00404DE0h
  0042e7ee  8b4e5c                mov       ecx,[esi+5Ch]
  0042e7f1  b802000000            mov       eax,2
  0042e7f6  83c410                add       esp,10h
  0042e7f9  3bc8                  cmp       ecx,eax
  0042e7fb  7f03                  jg        short 0042E800h
  0042e7fd  89465c                mov       [esi+5Ch],eax

LAB_0042e800:
  0042e800  66837e163c            cmp       word [esi+16h],3Ch
  0042e805  7e2f                  jle       short 0042E836h
  0042e807  66c746100300          mov       word [esi+10h],3
  0042e80d  e82e891100            call      00547140h
  0042e812  99                    cdq
  0042e813  b932000000            mov       ecx,32h
  0042e818  f7f9                  idiv      ecx
  0042e81a  83c228                add       edx,28h
  0042e81d  89565c                mov       [esi+5Ch],edx
  0042e820  e81b891100            call      00547140h
  0042e825  99                    cdq
  0042e826  b996000000            mov       ecx,96h
  0042e82b  f7f9                  idiv      ecx
  0042e82d  81c2c8000000          add       edx,0C8h
  0042e833  895644                mov       [esi+44h],edx

LAB_0042e836:
  0042e836  66837e1003            cmp       word [esi+10h],3
  0042e83b  0f85f4000000          jne       near 0042E935h
  0042e841  0fbf5614              movsx     edx,word [esi+14h]
  0042e845  52                    push      edx
  0042e846  e8cb711100            call      00545A16h
  0042e84b  8bf8                  mov       edi,eax
  0042e84d  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042e854  0fbf4e12              movsx     ecx,word [esi+12h]
  0042e858  0faff8                imul      edi,eax
  0042e85b  51                    push      ecx
  0042e85c  c1ff0c                sar       edi,0Ch
  0042e85f  e8b8711100            call      00545A1Ch
  0042e864  0faff8                imul      edi,eax
  0042e867  0fbf5614              movsx     edx,word [esi+14h]
  0042e86b  8b4e50                mov       ecx,[esi+50h]
  0042e86e  52                    push      edx
  0042e86f  03f9                  add       edi,ecx
  0042e871  897e1c                mov       [esi+1Ch],edi
  0042e874  e89d711100            call      00545A16h
  0042e879  8bf8                  mov       edi,eax
  0042e87b  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042e882  0fbf4e12              movsx     ecx,word [esi+12h]
  0042e886  0faff8                imul      edi,eax
  0042e889  51                    push      ecx
  0042e88a  c1ff0c                sar       edi,0Ch
  0042e88d  e884711100            call      00545A16h
  0042e892  0faff8                imul      edi,eax
  0042e895  8b4658                mov       eax,[esi+58h]
  0042e898  0fbf5614              movsx     edx,word [esi+14h]
  0042e89c  03f8                  add       edi,eax
  0042e89e  52                    push      edx
  0042e89f  897e24                mov       [esi+24h],edi
  0042e8a2  668bbe9a000000        mov       di,[esi+9Ah]
  0042e8a9  e86e711100            call      00545A1Ch
  0042e8ae  0fbfcf                movsx     ecx,di
  0042e8b1  0fafc1                imul      eax,ecx
  0042e8b4  8b4e54                mov       ecx,[esi+54h]
  0042e8b7  66037e5c              add       di,[esi+5Ch]
  0042e8bb  6afb                  push      0FFFFFFFBh
  0042e8bd  6af9                  push      0FFFFFFF9h
  0042e8bf  03c1                  add       eax,ecx
  0042e8c1  6af9                  push      0FFFFFFF9h
  0042e8c3  56                    push      esi
  0042e8c4  894620                mov       [esi+20h],eax
  0042e8c7  6689be9a000000        mov       [esi+9Ah],di
  0042e8ce  e80d65fdff            call      00404DE0h
  0042e8d3  668b4644              mov       ax,[esi+44h]
  0042e8d7  ba00020000            mov       edx,200h
  0042e8dc  66d1e0                shl       ax,1
  0042e8df  66294628              sub       [esi+28h],ax
  0042e8e3  668b4e28              mov       cx,[esi+28h]
  0042e8e7  6629462a              sub       [esi+2Ah],ax
  0042e8eb  668b462a              mov       ax,[esi+2Ah]
  0042e8ef  83c410                add       esp,10h
  0042e8f2  663bca                cmp       cx,dx
  0042e8f5  7d04                  jge       short 0042E8FBh
  0042e8f7  66895628              mov       [esi+28h],dx

LAB_0042e8fb:
  0042e8fb  663bc2                cmp       ax,dx
  0042e8fe  7d04                  jge       short 0042E904h
  0042e900  6689562a              mov       [esi+2Ah],dx

LAB_0042e904:
  0042e904  8b5644                mov       edx,[esi+44h]
  0042e907  b80a000000            mov       eax,0Ah
  0042e90c  83c2f6                add       edx,0FFFFFFF6h
  0042e90f  8bca                  mov       ecx,edx
  0042e911  895644                mov       [esi+44h],edx
  0042e914  3bc8                  cmp       ecx,eax
  0042e916  7d03                  jge       short 0042E91Bh
  0042e918  894644                mov       [esi+44h],eax

LAB_0042e91b:
  0042e91b  8b465c                mov       eax,[esi+5Ch]
  0042e91e  8bd0                  mov       edx,eax
  0042e920  c1fa03                sar       edx,3
  0042e923  2bc2                  sub       eax,edx
  0042e925  89465c                mov       [esi+5Ch],eax
  0042e928  8a463a                mov       al,[esi+3Ah]
  0042e92b  3ac3                  cmp       al,bl
  0042e92d  7506                  jne       short 0042E935h
  0042e92f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042e935:
  0042e935  66ff4616              inc       word [esi+16h]
  0042e939  5f                    pop       edi
  0042e93a  5e                    pop       esi
  0042e93b  5d                    pop       ebp
  0042e93c  5b                    pop       ebx
  0042e93d  c3                    ret
  0042e93e  90                    nop
  0042e93f  90                    nop

10 cible(s) de saut interne : 0x0042e6f4, 0x0042e7b3, 0x0042e7ca, 0x0042e7db, 0x0042e800, 0x0042e836, 0x0042e8fb, 0x0042e904, 0x0042e91b, 0x0042e935
