  0046c690  55                    push      ebp
  0046c691  56                    push      esi
  0046c692  8b74240c              mov       esi,[esp+0Ch]
  0046c696  33c0                  xor       eax,eax
  0046c698  57                    push      edi
  0046c699  668b6e10              mov       bp,[esi+10h]
  0046c69d  663be8                cmp       bp,ax
  0046c6a0  751b                  jne       short 0046C6BDh
  0046c6a2  668b4e32              mov       cx,[esi+32h]
  0046c6a6  668b5630              mov       dx,[esi+30h]
  0046c6aa  66894e16              mov       [esi+16h],cx
  0046c6ae  66895614              mov       [esi+14h],dx
  0046c6b2  66894630              mov       [esi+30h],ax
  0046c6b6  66898698000000        mov       [esi+98h],ax
  0046c6bd  668b7e14              mov       di,[esi+14h]
  0046c6c1  0fbfcf                movsx     ecx,di
  0046c6c4  8bc1                  mov       eax,ecx
  0046c6c6  99                    cdq
  0046c6c7  83e207                and       edx,7
  0046c6ca  03c2                  add       eax,edx
  0046c6cc  c1f803                sar       eax,3
  0046c6cf  0fbfc0                movsx     eax,ax
  0046c6d2  8d14c500000000        lea       edx,[eax*8]
  0046c6d9  2bca                  sub       ecx,edx
  0046c6db  ba01000000            mov       edx,1
  0046c6e0  d3e2                  shl       edx,cl
  0046c6e2  8a88e0965e00          mov       cl,[eax+5E96E0h]
  0046c6e8  84d1                  test      cl,dl
  0046c6ea  668b4e16              mov       cx,[esi+16h]
  0046c6ee  7405                  je        short 0046C6F5h
  0046c6f0  6681c10008            add       cx,800h
  0046c6f5  6685ed                test      bp,bp
  0046c6f8  66894e12              mov       [esi+12h],cx
  0046c6fc  750a                  jne       short 0046C708h
  0046c6fe  66894e32              mov       [esi+32h],cx
  0046c702  66c746100100          mov       word [esi+10h],1
  0046c708  66ff8698000000        inc       word [esi+98h]
  0046c70f  668b8e98000000        mov       cx,[esi+98h]
  0046c716  6683f90a              cmp       cx,0Ah
  0046c71a  0f8588000000          jne       near 0046C7A8h
  0046c720  8b0d68e05d00          mov       ecx,[5DE068h]
  0046c726  6681ff1201            cmp       di,112h
  0046c72b  8b7e1c                mov       edi,[esi+1Ch]
  0046c72e  750c                  jne       short 0046C73Ch
  0046c730  8a80e0965e00          mov       al,[eax+5E96E0h]
  0046c736  3bcf                  cmp       ecx,edi
  0046c738  7f1e                  jg        short 0046C758h
  0046c73a  eb0a                  jmp       short 0046C746h
  0046c73c  8a80e0965e00          mov       al,[eax+5E96E0h]
  0046c742  3bcf                  cmp       ecx,edi
  0046c744  7e12                  jle       short 0046C758h
  0046c746  22c2                  and       al,dl
  0046c748  f6d8                  neg       al
  0046c74a  1bc0                  sbb       eax,eax
  0046c74c  2500180000            and       eax,1800h
  0046c751  0500080000            add       eax,800h
  0046c756  eb10                  jmp       short 0046C768h
  0046c758  22c2                  and       al,dl
  0046c75a  f6d8                  neg       al
  0046c75c  1bc0                  sbb       eax,eax
  0046c75e  2500e8ffff            and       eax,0FFFFE800h
  0046c763  0500200000            add       eax,2000h
  0046c768  813d6ce05d0000e00b00  cmp       dword [5DE06Ch],0BE000h
  0046c772  7c05                  jl        short 0046C779h
  0046c774  b800140000            mov       eax,1400h
  0046c779  8b15cc975e00          mov       edx,[5E97CCh]
  0046c77f  0fbfc0                movsx     eax,ax
  0046c782  50                    push      eax
  0046c783  50                    push      eax
  0046c784  8b82a8000000          mov       eax,[edx+0A8h]
  0046c78a  33d2                  xor       edx,edx
  0046c78c  6800040000            push      400h
  0046c791  8b08                  mov       ecx,[eax]
  0046c793  8a513b                mov       dl,[ecx+3Bh]
  0046c796  52                    push      edx
  0046c797  e804070d00            call      0053CEA0h
  0046c79c  83c410                add       esp,10h
  0046c79f  66c786980000000000    mov       word [esi+98h],0
  0046c7a8  66837e1001            cmp       word [esi+10h],1
  0046c7ad  7542                  jne       short 0046C7F1h
  0046c7af  668b4632              mov       ax,[esi+32h]
  0046c7b3  6aff                  push      0FFFFFFFFh
  0046c7b5  0fbf4e12              movsx     ecx,word [esi+12h]
  0046c7b9  0fbfd0                movsx     edx,ax
  0046c7bc  2bca                  sub       ecx,edx
  0046c7be  6a00                  push      0
  0046c7c0  c1f904                sar       ecx,4
  0046c7c3  03c8                  add       ecx,eax
  0046c7c5  66817e141201          cmp       word [esi+14h],112h
  0046c7cb  66894e32              mov       [esi+32h],cx
  0046c7cf  7512                  jne       short 0046C7E3h
  0046c7d1  68fc000000            push      0FCh
  0046c7d6  56                    push      esi
  0046c7d7  e804e30000            call      0047AAE0h
  0046c7dc  83c410                add       esp,10h
  0046c7df  5f                    pop       edi
  0046c7e0  5e                    pop       esi
  0046c7e1  5d                    pop       ebp
  0046c7e2  c3                    ret
  0046c7e3  68fb000000            push      0FBh
  0046c7e8  56                    push      esi
  0046c7e9  e8f2e20000            call      0047AAE0h
  0046c7ee  83c410                add       esp,10h
  0046c7f1  5f                    pop       edi
  0046c7f2  5e                    pop       esi
  0046c7f3  5d                    pop       ebp
  0046c7f4  c3                    ret
