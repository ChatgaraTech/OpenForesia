  0046cc60  51                    push      ecx
  0046cc61  53                    push      ebx
  0046cc62  55                    push      ebp
  0046cc63  56                    push      esi
  0046cc64  8b742414              mov       esi,[esp+14h]
  0046cc68  57                    push      edi
  0046cc69  66837e1000            cmp       word [esi+10h],0
  0046cc6e  751a                  jne       short 0046CC8Ah
  0046cc70  66c746100100          mov       word [esi+10h],1
  0046cc76  e8c5a40d00            call      00547140h
  0046cc7b  99                    cdq
  0046cc7c  b914000000            mov       ecx,14h
  0046cc81  f7f9                  idiv      ecx
  0046cc83  66899698000000        mov       [esi+98h],dx
  0046cc8a  33d2                  xor       edx,edx
  0046cc8c  b8402d5f00            mov       eax,5F2D40h
  0046cc91  89542418              mov       [esp+18h],edx
  0046cc95  b980000000            mov       ecx,80h
  0046cc9a  f70000000080          test      dword [eax],80000000h
  0046cca0  7401                  je        short 0046CCA3h
  0046cca2  42                    inc       edx
  0046cca3  059c000000            add       eax,9Ch
  0046cca8  49                    dec       ecx
  0046cca9  75ef                  jne       short 0046CC9Ah
  0046ccab  89542418              mov       [esp+18h],edx
  0046ccaf  33ed                  xor       ebp,ebp
  0046ccb1  bfc0935e00            mov       edi,5E93C0h
  0046ccb6  bb04000000            mov       ebx,4
  0046ccbb  8b07                  mov       eax,[edi]
  0046ccbd  a900000080            test      eax,80000000h
  0046ccc2  743b                  je        short 0046CCFFh
  0046ccc4  a900000050            test      eax,50000000h
  0046ccc9  7534                  jne       short 0046CCFFh
  0046cccb  8b471c                mov       eax,[edi+1Ch]
  0046ccce  8b561c                mov       edx,[esi+1Ch]
  0046ccd1  8b4f24                mov       ecx,[edi+24h]
  0046ccd4  2bc2                  sub       eax,edx
  0046ccd6  8b5624                mov       edx,[esi+24h]
  0046ccd9  c1f80c                sar       eax,0Ch
  0046ccdc  2bca                  sub       ecx,edx
  0046ccde  8bd0                  mov       edx,eax
  0046cce0  c1f90c                sar       ecx,0Ch
  0046cce3  0fafd0                imul      edx,eax
  0046cce6  8bc1                  mov       eax,ecx
  0046cce8  0fafc1                imul      eax,ecx
  0046cceb  03d0                  add       edx,eax
  0046cced  52                    push      edx
  0046ccee  e81d8d0d00            call      00545A10h
  0046ccf3  3db0040000            cmp       eax,4B0h
  0046ccf8  7d05                  jge       short 0046CCFFh
  0046ccfa  bd01000000            mov       ebp,1
  0046ccff  81c79c000000          add       edi,9Ch
  0046cd05  4b                    dec       ebx
  0046cd06  75b3                  jne       short 0046CCBBh
  0046cd08  6683fd01              cmp       bp,1
  0046cd0c  0f853f010000          jne       near 0046CE51h
  0046cd12  668b4e32              mov       cx,[esi+32h]
  0046cd16  8b7e1c                mov       edi,[esi+1Ch]
  0046cd19  8b5e20                mov       ebx,[esi+20h]
  0046cd1c  8b6e24                mov       ebp,[esi+24h]
  0046cd1f  66894c2410            mov       [esp+10h],cx
  0046cd24  e817a40d00            call      00547140h
  0046cd29  99                    cdq
  0046cd2a  b990010000            mov       ecx,190h
  0046cd2f  f7f9                  idiv      ecx
  0046cd31  81eac8000000          sub       edx,0C8h
  0046cd37  66015632              add       [esi+32h],dx
  0046cd3b  e800a40d00            call      00547140h
  0046cd40  99                    cdq
  0046cd41  b964000000            mov       ecx,64h
  0046cd46  f7f9                  idiv      ecx
  0046cd48  8b4620                mov       eax,[esi+20h]
  0046cd4b  8bca                  mov       ecx,edx
  0046cd4d  ba82000000            mov       edx,82h
  0046cd52  c1e10c                shl       ecx,0Ch
  0046cd55  81e9f41f0300          sub       ecx,31FF4h
  0046cd5b  d3e2                  shl       edx,cl
  0046cd5d  03c2                  add       eax,edx
  0046cd5f  894620                mov       [esi+20h],eax
  0046cd62  e8d9a30d00            call      00547140h
  0046cd67  99                    cdq
  0046cd68  b964000000            mov       ecx,64h
  0046cd6d  f7f9                  idiv      ecx
  0046cd6f  8b4e1c                mov       ecx,[esi+1Ch]
  0046cd72  83ea32                sub       edx,32h
  0046cd75  c1e20c                shl       edx,0Ch
  0046cd78  03ca                  add       ecx,edx
  0046cd7a  894e1c                mov       [esi+1Ch],ecx
  0046cd7d  e8bea30d00            call      00547140h
  0046cd82  99                    cdq
  0046cd83  b964000000            mov       ecx,64h
  0046cd88  f7f9                  idiv      ecx
  0046cd8a  8b4624                mov       eax,[esi+24h]
  0046cd8d  8b4e5c                mov       ecx,[esi+5Ch]
  0046cd90  83ea32                sub       edx,32h
  0046cd93  c1e20c                shl       edx,0Ch
  0046cd96  03c2                  add       eax,edx
  0046cd98  41                    inc       ecx
  0046cd99  894624                mov       [esi+24h],eax
  0046cd9c  8bc1                  mov       eax,ecx
  0046cd9e  3de8030000            cmp       eax,3E8h
  0046cda3  894e5c                mov       [esi+5Ch],ecx
  0046cda6  7c07                  jl        short 0046CDAFh
  0046cda8  c7465c00000000        mov       dword [esi+5Ch],0
  0046cdaf  66837c241814          cmp       word [esp+18h],14h
  0046cdb5  8b4e5c                mov       ecx,[esi+5Ch]
  0046cdb8  7d18                  jge       short 0046CDD2h
  0046cdba  b867666666            mov       eax,66666667h
  0046cdbf  f7e9                  imul      ecx
  0046cdc1  c1fa02                sar       edx,2
  0046cdc4  8bc2                  mov       eax,edx
  0046cdc6  c1e81f                shr       eax,1Fh
  0046cdc9  03d0                  add       edx,eax
  0046cdcb  8d0492                lea       eax,[edx+edx*4]
  0046cdce  d1e0                  shl       eax,1
  0046cdd0  eb17                  jmp       short 0046CDE9h
  0046cdd2  b81f85eb51            mov       eax,51EB851Fh
  0046cdd7  f7e9                  imul      ecx
  0046cdd9  c1fa03                sar       edx,3
  0046cddc  8bc2                  mov       eax,edx
  0046cdde  c1e81f                shr       eax,1Fh
  0046cde1  03d0                  add       edx,eax
  0046cde3  8d0492                lea       eax,[edx+edx*4]
  0046cde6  8d0480                lea       eax,[eax+eax*4]
  0046cde9  0fbfd0                movsx     edx,ax
  0046cdec  3bd1                  cmp       edx,ecx
  0046cdee  751c                  jne       short 0046CE0Ch
  0046cdf0  6a00                  push      0
  0046cdf2  6a00                  push      0
  0046cdf4  6aff                  push      0FFFFFFFFh
  0046cdf6  6a00                  push      0
  0046cdf8  6a35                  push      35h
  0046cdfa  56                    push      esi
  0046cdfb  66c7869a0000000000    mov       word [esi+9Ah],0
  0046ce04  e8f7f1feff            call      0045C000h
  0046ce09  83c418                add       esp,18h
  0046ce0c  668b442410            mov       ax,[esp+10h]
  0046ce11  66ff8698000000        inc       word [esi+98h]
  0046ce18  6683be9800000014      cmp       word [esi+98h],14h
  0046ce20  897e1c                mov       [esi+1Ch],edi
  0046ce23  895e20                mov       [esi+20h],ebx
  0046ce26  896e24                mov       [esi+24h],ebp
  0046ce29  66894632              mov       [esi+32h],ax
  0046ce2d  7e22                  jle       short 0046CE51h
  0046ce2f  6a00                  push      0
  0046ce31  6a00                  push      0
  0046ce33  68000a0000            push      0A00h
  0046ce38  6800040000            push      400h
  0046ce3d  6a20                  push      20h
  0046ce3f  56                    push      esi
  0046ce40  66c786980000000000    mov       word [esi+98h],0
  0046ce49  e832b2ffff            call      00468080h
  0046ce4e  83c418                add       esp,18h
  0046ce51  5f                    pop       edi
  0046ce52  5e                    pop       esi
  0046ce53  5d                    pop       ebp
  0046ce54  5b                    pop       ebx
  0046ce55  59                    pop       ecx
  0046ce56  c3                    ret
