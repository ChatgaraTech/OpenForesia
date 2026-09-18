
Legacy/Binaries/Blaze.exe  base 0x00400000
0x004244d0  section .text  offset fichier 0x238d0  816 octets

  004244d0  53                    push      ebx
  004244d1  55                    push      ebp
  004244d2  56                    push      esi
  004244d3  57                    push      edi
  004244d4  33ed                  xor       ebp,ebp
  004244d6  33ff                  xor       edi,edi
  004244d8  e8736affff            call      0041AF50h
  004244dd  e85ea1feff            call      0040E640h
  004244e2  8b742414              mov       esi,[esp+14h]
  004244e6  b8a0ca5e00            mov       eax,5ECAA0h
  004244eb  b920000000            mov       ecx,20h

LAB_004244f0:
  004244f0  80780c02              cmp       byte [eax+0Ch],2
  004244f4  753c                  jne       short 00424532h
  004244f6  f70000000080          test      dword [eax],80000000h
  004244fc  7434                  je        short 00424532h
  004244fe  8b501c                mov       edx,[eax+1Ch]
  00424501  8bf8                  mov       edi,eax
  00424503  89561c                mov       [esi+1Ch],edx
  00424506  8b5020                mov       edx,[eax+20h]
  00424509  895620                mov       [esi+20h],edx
  0042450c  8b5024                mov       edx,[eax+24h]
  0042450f  895624                mov       [esi+24h],edx
  00424512  33d2                  xor       edx,edx
  00424514  8a5004                mov       dl,[eax+4]
  00424517  66c740180200          mov       word [eax+18h],2
  0042451d  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00424524  8b5208                mov       edx,[edx+8]
  00424527  89542414              mov       [esp+14h],edx
  0042452b  8b10                  mov       edx,[eax]
  0042452d  80ce10                or        dh,10h
  00424530  8910                  mov       [eax],edx

LAB_00424532:
  00424532  059c000000            add       eax,9Ch
  00424537  49                    dec       ecx
  00424538  75b6                  jne       short 004244F0h
  0042453a  3bfd                  cmp       edi,ebp
  0042453c  7505                  jne       short 00424543h
  0042453e  bf58e55d00            mov       edi,5DE558h

LAB_00424543:
  00424543  66396e10              cmp       [esi+10h],bp
  00424547  bb01000000            mov       ebx,1
  0042454c  0f8589000000          jne       near 004245DBh
  00424552  66895e10              mov       [esi+10h],bx
  00424556  66896e12              mov       [esi+12h],bp
  0042455a  66896e14              mov       [esi+14h],bp
  0042455e  66896e16              mov       [esi+16h],bp
  00424562  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  00424569  894650                mov       [esi+50h],eax
  0042456c  0fbf0dba925e00        movsx     ecx,word [5E92BAh]
  00424573  894e54                mov       [esi+54h],ecx
  00424576  0fbf15bc925e00        movsx     edx,word [5E92BCh]
  0042457d  895658                mov       [esi+58h],edx
  00424580  8a4709                mov       al,[edi+9]
  00424583  66896f1a              mov       [edi+1Ah],bp
  00424587  c6470a00              mov       byte [edi+0Ah],0
  0042458b  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  00424592  66895f36              mov       [edi+36h],bx
  00424596  a2d8145700            mov       [5714D8h],al
  0042459b  8b4620                mov       eax,[esi+20h]
  0042459e  0500400100            add       eax,14000h
  004245a3  894620                mov       [esi+20h],eax
  004245a6  8b4f1c                mov       ecx,[edi+1Ch]
  004245a9  894e44                mov       [esi+44h],ecx
  004245ac  8b5720                mov       edx,[edi+20h]
  004245af  895648                mov       [esi+48h],edx
  004245b2  8b4724                mov       eax,[edi+24h]
  004245b5  89464c                mov       [esi+4Ch],eax
  004245b8  b8aa000000            mov       eax,0AAh
  004245bd  66c70560915e00fa00    mov       word [5E9160h],0FAh
  004245c6  66a362915e00          mov       [5E9162h],ax
  004245cc  66a364915e00          mov       [5E9164h],ax
  004245d2  66c70566915e000200    mov       word [5E9166h],2

LAB_004245db:
  004245db  66395e10              cmp       [esi+10h],bx
  004245df  755a                  jne       short 0042463Bh
  004245e1  6aff                  push      0FFFFFFFFh
  004245e3  55                    push      ebp
  004245e4  68c5000000            push      0C5h
  004245e9  56                    push      esi
  004245ea  66c7869a000000ac0d    mov       word [esi+9Ah],0DACh
  004245f3  c7465c4c1d0000        mov       dword [esi+5Ch],1D4Ch
  004245fa  66c786980000009cff    mov       word [esi+98h],0FF9Ch
  00424603  e8d8640500            call      0047AAE0h
  00424608  83c410                add       esp,10h
  0042460b  66c7869a000000b80b    mov       word [esi+9Ah],0BB8h
  00424614  c7465c401f0000        mov       dword [esi+5Ch],1F40h
  0042461b  66c786980000005000    mov       word [esi+98h],50h
  00424624  6aff                  push      0FFFFFFFFh
  00424626  55                    push      ebp
  00424627  68c5000000            push      0C5h
  0042462c  56                    push      esi
  0042462d  e8ae640500            call      0047AAE0h
  00424632  83c410                add       esp,10h
  00424635  66c746100200          mov       word [esi+10h],2

LAB_0042463b:
  0042463b  66837e1002            cmp       word [esi+10h],2
  00424640  757f                  jne       short 004246C1h
  00424642  6aff                  push      0FFFFFFFFh
  00424644  6a00                  push      0
  00424646  8d6e1c                lea       ebp,[esi+1Ch]
  00424649  68c6000000            push      0C6h
  0042464e  6a00                  push      0
  00424650  55                    push      ebp
  00424651  56                    push      esi
  00424652  e8e9650500            call      0047AC40h
  00424657  83c418                add       esp,18h
  0042465a  6aff                  push      0FFFFFFFFh
  0042465c  6a00                  push      0
  0042465e  68c6000000            push      0C6h
  00424663  6a00                  push      0
  00424665  55                    push      ebp
  00424666  56                    push      esi
  00424667  e8d4650500            call      0047AC40h
  0042466c  8b5720                mov       edx,[edi+20h]
  0042466f  83c418                add       esp,18h
  00424672  81c200100000          add       edx,1000h
  00424678  6af6                  push      0FFFFFFF6h
  0042467a  6af6                  push      0FFFFFFF6h
  0042467c  6af6                  push      0FFFFFFF6h
  0042467e  57                    push      edi
  0042467f  895720                mov       [edi+20h],edx
  00424682  e85907feff            call      00404DE0h
  00424687  8a4616                mov       al,[esi+16h]
  0042468a  83c410                add       esp,10h
  0042468d  84c3                  test      bl,al
  0042468f  7530                  jne       short 004246C1h
  00424691  8b542414              mov       edx,[esp+14h]
  00424695  66ff471a              inc       word [edi+1Ah]
  00424699  668b4f1a              mov       cx,[edi+1Ah]
  0042469d  668b4216              mov       ax,[edx+16h]
  004246a1  8bd0                  mov       edx,eax
  004246a3  0fbfc9                movsx     ecx,cx
  004246a6  81e2ffff0000          and       edx,0FFFFh
  004246ac  3bca                  cmp       ecx,edx
  004246ae  7c11                  jl        short 004246C1h
  004246b0  48                    dec       eax
  004246b1  6689471a              mov       [edi+1Ah],ax
  004246b5  66c746100300          mov       word [esi+10h],3
  004246bb  66c746120000          mov       word [esi+12h],0

LAB_004246c1:
  004246c1  66837e1003            cmp       word [esi+10h],3
  004246c6  751d                  jne       short 004246E5h
  004246c8  8b5720                mov       edx,[edi+20h]
  004246cb  81c200100000          add       edx,1000h
  004246d1  895720                mov       [edi+20h],edx
  004246d4  66ff4612              inc       word [esi+12h]
  004246d8  66837e121e            cmp       word [esi+12h],1Eh
  004246dd  7c06                  jl        short 004246E5h
  004246df  66c746100400          mov       word [esi+10h],4

LAB_004246e5:
  004246e5  66837e1664            cmp       word [esi+16h],64h
  004246ea  7529                  jne       short 00424715h
  004246ec  668b5650              mov       dx,[esi+50h]
  004246f0  66891560915e00        mov       [5E9160h],dx
  004246f7  668b4654              mov       ax,[esi+54h]
  004246fb  66a362915e00          mov       [5E9162h],ax
  00424701  668b4e58              mov       cx,[esi+58h]
  00424705  66890d64915e00        mov       [5E9164h],cx
  0042470c  66c70566915e000200    mov       word [5E9166h],2

LAB_00424715:
  00424715  668b4616              mov       ax,[esi+16h]
  00424719  663d6e00              cmp       ax,6Eh
  0042471d  7c1e                  jl        short 0042473Dh
  0042471f  663daa00              cmp       ax,0AAh
  00424723  7f18                  jg        short 0042473Dh
  00424725  6aff                  push      0FFFFFFFFh
  00424727  6a00                  push      0
  00424729  68f9000000            push      0F9h
  0042472e  8d561c                lea       edx,[esi+1Ch]
  00424731  6a00                  push      0
  00424733  52                    push      edx
  00424734  56                    push      esi
  00424735  e806650500            call      0047AC40h
  0042473a  83c418                add       esp,18h

LAB_0042473d:
  0042473d  668b4616              mov       ax,[esi+16h]
  00424741  663daa00              cmp       ax,0AAh
  00424745  7c3a                  jl        short 00424781h
  00424747  663db400              cmp       ax,0B4h
  0042474b  7f34                  jg        short 00424781h
  0042474d  6aff                  push      0FFFFFFFFh
  0042474f  6a00                  push      0
  00424751  8d6e1c                lea       ebp,[esi+1Ch]
  00424754  68c5000000            push      0C5h
  00424759  6a00                  push      0
  0042475b  55                    push      ebp
  0042475c  56                    push      esi
  0042475d  e8de640500            call      0047AC40h
  00424762  83c418                add       esp,18h
  00424765  6aff                  push      0FFFFFFFFh
  00424767  6a00                  push      0
  00424769  68c5000000            push      0C5h
  0042476e  6a00                  push      0
  00424770  55                    push      ebp
  00424771  56                    push      esi
  00424772  e8c9640500            call      0047AC40h
  00424777  8b07                  mov       eax,[edi]
  00424779  83c418                add       esp,18h
  0042477c  80cc40                or        ah,40h
  0042477f  8907                  mov       [edi],eax

LAB_00424781:
  00424781  66817e16c800          cmp       word [esi+16h],0C8h
  00424787  7c67                  jl        short 004247F0h
  00424789  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0042478f  e81cf00d00            call      005037B0h
  00424794  a1d8145700            mov       eax,[5714D8h]
  00424799  6a00                  push      0
  0042479b  25ff000000            and       eax,0FFh
  004247a0  8d0c80                lea       ecx,[eax+eax*4]
  004247a3  c1e103                shl       ecx,3
  004247a6  2bc8                  sub       ecx,eax
  004247a8  a1cc975e00            mov       eax,[5E97CCh]
  004247ad  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  004247b4  52                    push      edx
  004247b5  50                    push      eax
  004247b6  e815f60e00            call      00513DD0h
  004247bb  8b07                  mov       eax,[edi]
  004247bd  83c40c                add       esp,0Ch
  004247c0  0d00400002            or        eax,2004000h
  004247c5  8907                  mov       [edi],eax
  004247c7  668b4e50              mov       cx,[esi+50h]
  004247cb  66890d60915e00        mov       [5E9160h],cx
  004247d2  668b5654              mov       dx,[esi+54h]
  004247d6  66891562915e00        mov       [5E9162h],dx
  004247dd  668b4658              mov       ax,[esi+58h]
  004247e1  66a364915e00          mov       [5E9164h],ax
  004247e7  66c70566915e000200    mov       word [5E9166h],2

LAB_004247f0:
  004247f0  66ff4616              inc       word [esi+16h]
  004247f4  5f                    pop       edi
  004247f5  5e                    pop       esi
  004247f6  5d                    pop       ebp
  004247f7  5b                    pop       ebx
  004247f8  c3                    ret

11 cible(s) de saut interne : 0x004244f0, 0x00424532, 0x00424543, 0x004245db, 0x0042463b, 0x004246c1, 0x004246e5, 0x00424715, 0x0042473d, 0x00424781, 0x004247f0
