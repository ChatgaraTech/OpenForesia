  0043cee0  51                    push      ecx
  0043cee1  53                    push      ebx
  0043cee2  55                    push      ebp
  0043cee3  56                    push      esi
  0043cee4  57                    push      edi
  0043cee5  33ff                  xor       edi,edi
  0043cee7  e864e0fdff            call      0041AF50h
  0043ceec  e84f17fdff            call      0040E640h
  0043cef1  8b742418              mov       esi,[esp+18h]
  0043cef5  b8dbca5e00            mov       eax,5ECADBh
  0043cefa  bd20000000            mov       ebp,20h
  0043ceff  8a48d1                mov       cl,[eax-2Fh]
  0043cf02  84c9                  test      cl,cl
  0043cf04  7549                  jne       short 0043CF4Fh
  0043cf06  8b50c5                mov       edx,[eax-3Bh]
  0043cf09  8d48c5                lea       ecx,[eax-3Bh]
  0043cf0c  f7c200000080          test      edx,80000000h
  0043cf12  743b                  je        short 0043CF4Fh
  0043cf14  8b50e1                mov       edx,[eax-1Fh]
  0043cf17  8bf9                  mov       edi,ecx
  0043cf19  89561c                mov       [esi+1Ch],edx
  0043cf1c  8b50e5                mov       edx,[eax-1Bh]
  0043cf1f  895620                mov       [esi+20h],edx
  0043cf22  8b50e9                mov       edx,[eax-17h]
  0043cf25  895624                mov       [esi+24h],edx
  0043cf28  33d2                  xor       edx,edx
  0043cf2a  8a50c9                mov       dl,[eax-37h]
  0043cf2d  8a18                  mov       bl,[eax]
  0043cf2f  66c740dd0200          mov       word [eax-23h],2
  0043cf35  80cb02                or        bl,2
  0043cf38  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0043cf3f  8b5208                mov       edx,[edx+8]
  0043cf42  8818                  mov       [eax],bl
  0043cf44  89542418              mov       [esp+18h],edx
  0043cf48  8b11                  mov       edx,[ecx]
  0043cf4a  80ce10                or        dh,10h
  0043cf4d  8911                  mov       [ecx],edx
  0043cf4f  059c000000            add       eax,9Ch
  0043cf54  4d                    dec       ebp
  0043cf55  75a8                  jne       short 0043CEFFh
  0043cf57  85ff                  test      edi,edi
  0043cf59  7505                  jne       short 0043CF60h
  0043cf5b  bf58e55d00            mov       edi,5DE558h
  0043cf60  66837e1000            cmp       word [esi+10h],0
  0043cf65  bb01000000            mov       ebx,1
  0043cf6a  754c                  jne       short 0043CFB8h
  0043cf6c  33ed                  xor       ebp,ebp
  0043cf6e  66895e10              mov       [esi+10h],bx
  0043cf72  66896e12              mov       [esi+12h],bp
  0043cf76  66896e14              mov       [esi+14h],bp
  0043cf7a  66896e16              mov       [esi+16h],bp
  0043cf7e  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  0043cf85  894650                mov       [esi+50h],eax
  0043cf88  0fbf0dba925e00        movsx     ecx,word [5E92BAh]
  0043cf8f  894e54                mov       [esi+54h],ecx
  0043cf92  0fbf15bc925e00        movsx     edx,word [5E92BCh]
  0043cf99  895658                mov       [esi+58h],edx
  0043cf9c  66896f1a              mov       [edi+1Ah],bp
  0043cfa0  c6470a00              mov       byte [edi+0Ah],0
  0043cfa4  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0043cfab  66895f36              mov       [edi+36h],bx
  0043cfaf  6689ae98000000        mov       [esi+98h],bp
  0043cfb6  eb02                  jmp       short 0043CFBAh
  0043cfb8  33ed                  xor       ebp,ebp
  0043cfba  66395e10              cmp       [esi+10h],bx
  0043cfbe  0f85b9010000          jne       near 0043D17Dh
  0043cfc4  66ff8698000000        inc       word [esi+98h]
  0043cfcb  6683be9800000005      cmp       word [esi+98h],5
  0043cfd3  7e4b                  jle       short 0043D020h
  0043cfd5  e866a11000            call      00547140h
  0043cfda  99                    cdq
  0043cfdb  b90a000000            mov       ecx,0Ah
  0043cfe0  f7f9                  idiv      ecx
  0043cfe2  663bd3                cmp       dx,bx
  0043cfe5  740a                  je        short 0043CFF1h
  0043cfe7  6683be9800000014      cmp       word [esi+98h],14h
  0043cfef  7e2f                  jle       short 0043D020h
  0043cff1  6800100000            push      1000h
  0043cff6  6800100000            push      1000h
  0043cffb  6689ae98000000        mov       [esi+98h],bp
  0043d002  e839a11000            call      00547140h
  0043d007  99                    cdq
  0043d008  b92c010000            mov       ecx,12Ch
  0043d00d  f7f9                  idiv      ecx
  0043d00f  81c200020000          add       edx,200h
  0043d015  52                    push      edx
  0043d016  6a20                  push      20h
  0043d018  e883fe0f00            call      0053CEA0h
  0043d01d  83c410                add       esp,10h
  0043d020  b896000000            mov       eax,96h
  0043d025  66a360915e00          mov       [5E9160h],ax
  0043d02b  66a362915e00          mov       [5E9162h],ax
  0043d031  e80aa11000            call      00547140h
  0043d036  99                    cdq
  0043d037  b93c000000            mov       ecx,3Ch
  0043d03c  66891d66915e00        mov       [5E9166h],bx
  0043d043  f7f9                  idiv      ecx
  0043d045  81c2b4000000          add       edx,0B4h
  0043d04b  66891564915e00        mov       [5E9164h],dx
  0043d052  66ff4612              inc       word [esi+12h]
  0043d056  668b4612              mov       ax,[esi+12h]
  0043d05a  66ff4616              inc       word [esi+16h]
  0043d05e  668b4e16              mov       cx,[esi+16h]
  0043d062  663bc3                cmp       ax,bx
  0043d065  7c6c                  jl        short 0043D0D3h
  0043d067  6683f950              cmp       cx,50h
  0043d06b  7d66                  jge       short 0043D0D3h
  0043d06d  66ff4614              inc       word [esi+14h]
  0043d071  668b5e14              mov       bx,[esi+14h]
  0043d075  66896e12              mov       [esi+12h],bp
  0043d079  e8c2a01000            call      00547140h
  0043d07e  99                    cdq
  0043d07f  33c2                  xor       eax,edx
  0043d081  6aff                  push      0FFFFFFFFh
  0043d083  2bc2                  sub       eax,edx
  0043d085  66896e14              mov       [esi+14h],bp
  0043d089  83e003                and       eax,3
  0043d08c  6a00                  push      0
  0043d08e  33c2                  xor       eax,edx
  0043d090  8d6e1c                lea       ebp,[esi+1Ch]
  0043d093  68fc000000            push      0FCh
  0043d098  6a00                  push      0
  0043d09a  2bc2                  sub       eax,edx
  0043d09c  55                    push      ebp
  0043d09d  56                    push      esi
  0043d09e  89442428              mov       [esp+28h],eax
  0043d0a2  e899db0300            call      0047AC40h
  0043d0a7  668b4616              mov       ax,[esi+16h]
  0043d0ab  83c418                add       esp,18h
  0043d0ae  a801                  test      al,1
  0043d0b0  66895e14              mov       [esi+14h],bx
  0043d0b4  751b                  jne       short 0043D0D1h
  0043d0b6  663d4600              cmp       ax,46h
  0043d0ba  7d15                  jge       short 0043D0D1h
  0043d0bc  6aff                  push      0FFFFFFFFh
  0043d0be  6a00                  push      0
  0043d0c0  68fd000000            push      0FDh
  0043d0c5  6a00                  push      0
  0043d0c7  55                    push      ebp
  0043d0c8  56                    push      esi
  0043d0c9  e872db0300            call      0047AC40h
  0043d0ce  83c418                add       esp,18h
  0043d0d1  33ed                  xor       ebp,ebp
  0043d0d3  66837e1664            cmp       word [esi+16h],64h
  0043d0d8  7e29                  jle       short 0043D103h
  0043d0da  668b5650              mov       dx,[esi+50h]
  0043d0de  66891560915e00        mov       [5E9160h],dx
  0043d0e5  668b4654              mov       ax,[esi+54h]
  0043d0e9  66a362915e00          mov       [5E9162h],ax
  0043d0ef  668b4e58              mov       cx,[esi+58h]
  0043d0f3  66890d64915e00        mov       [5E9164h],cx
  0043d0fa  66c70566915e000200    mov       word [5E9166h],2
  0043d103  66817e168c00          cmp       word [esi+16h],8Ch
  0043d109  7c39                  jl        short 0043D144h
  0043d10b  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043d111  e89a660c00            call      005037B0h
  0043d116  33c0                  xor       eax,eax
  0043d118  8b0dcc975e00          mov       ecx,[5E97CCh]
  0043d11e  8a4709                mov       al,[edi+9]
  0043d121  55                    push      ebp
  0043d122  8d1480                lea       edx,[eax+eax*4]
  0043d125  c1e203                shl       edx,3
  0043d128  2bd0                  sub       edx,eax
  0043d12a  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  0043d131  50                    push      eax
  0043d132  51                    push      ecx
  0043d133  e8986c0d00            call      00513DD0h
  0043d138  8b07                  mov       eax,[edi]
  0043d13a  83c40c                add       esp,0Ch
  0043d13d  0d00400002            or        eax,2004000h
  0043d142  8907                  mov       [edi],eax
  0043d144  f6461601              test      byte [esi+16h],1
  0043d148  7524                  jne       short 0043D16Eh
  0043d14a  8b542418              mov       edx,[esp+18h]
  0043d14e  66ff471a              inc       word [edi+1Ah]
  0043d152  668b4f1a              mov       cx,[edi+1Ah]
  0043d156  668b4216              mov       ax,[edx+16h]
  0043d15a  8bd0                  mov       edx,eax
  0043d15c  0fbfc9                movsx     ecx,cx
  0043d15f  81e2ffff0000          and       edx,0FFFFh
  0043d165  3bca                  cmp       ecx,edx
  0043d167  7c05                  jl        short 0043D16Eh
  0043d169  48                    dec       eax
  0043d16a  6689471a              mov       [edi+1Ah],ax
  0043d16e  6afc                  push      0FFFFFFFCh
  0043d170  6afc                  push      0FFFFFFFCh
  0043d172  6afe                  push      0FFFFFFFEh
  0043d174  57                    push      edi
  0043d175  e8667cfcff            call      00404DE0h
  0043d17a  83c410                add       esp,10h
  0043d17d  5f                    pop       edi
  0043d17e  5e                    pop       esi
  0043d17f  5d                    pop       ebp
  0043d180  5b                    pop       ebx
  0043d181  59                    pop       ecx
  0043d182  c3                    ret
