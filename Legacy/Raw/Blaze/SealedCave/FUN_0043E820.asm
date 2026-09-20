  0043e820  53                    push      ebx
  0043e821  56                    push      esi
  0043e822  8b74240c              mov       esi,[esp+0Ch]
  0043e826  57                    push      edi
  0043e827  668b4614              mov       ax,[esi+14h]
  0043e82b  6685c0                test      ax,ax
  0043e82e  7406                  je        short 0043E836h
  0043e830  663d0200              cmp       ax,2
  0043e834  754e                  jne       short 0043E884h
  0043e836  8b4e6c                mov       ecx,[esi+6Ch]
  0043e839  85c9                  test      ecx,ecx
  0043e83b  7505                  jne       short 0043E842h
  0043e83d  b958e55d00            mov       ecx,5DE558h
  0043e842  8b09                  mov       ecx,[ecx]
  0043e844  f7c100000040          test      ecx,40000000h
  0043e84a  7508                  jne       short 0043E854h
  0043e84c  f7c100000080          test      ecx,80000000h
  0043e852  7530                  jne       short 0043E884h
  0043e854  8b16                  mov       edx,[esi]
  0043e856  81e2ffffff7f          and       edx,7FFFFFFFh
  0043e85c  663d0200              cmp       ax,2
  0043e860  8916                  mov       [esi],edx
  0043e862  7520                  jne       short 0043E884h
  0043e864  c60527e05d0001        mov       byte [5DE027h],1
  0043e86b  c60524e05d0000        mov       byte [5DE024h],0
  0043e872  c60525e05d0000        mov       byte [5DE025h],0
  0043e879  c60526e05d0000        mov       byte [5DE026h],0
  0043e880  5f                    pop       edi
  0043e881  5e                    pop       esi
  0043e882  5b                    pop       ebx
  0043e883  c3                    ret
  0043e884  6685c0                test      ax,ax
  0043e887  bb01000000            mov       ebx,1
  0043e88c  0f8567010000          jne       near 0043E9F9h
  0043e892  66394610              cmp       [esi+10h],ax
  0043e896  0f859f000000          jne       near 0043E93Bh
  0043e89c  c7463c18155500        mov       dword [esi+3Ch],551518h
  0043e8a3  e898881000            call      00547140h
  0043e8a8  99                    cdq
  0043e8a9  33c2                  xor       eax,edx
  0043e8ab  c6460a0a              mov       byte [esi+0Ah],0Ah
  0043e8af  2bc2                  sub       eax,edx
  0043e8b1  c6460b00              mov       byte [esi+0Bh],0
  0043e8b5  23c3                  and       eax,ebx
  0043e8b7  66895e10              mov       [esi+10h],bx
  0043e8bb  33c2                  xor       eax,edx
  0043e8bd  2bc2                  sub       eax,edx
  0043e8bf  884640                mov       [esi+40h],al
  0043e8c2  e879881000            call      00547140h
  0043e8c7  99                    cdq
  0043e8c8  33c2                  xor       eax,edx
  0043e8ca  2bc2                  sub       eax,edx
  0043e8cc  25ff0f0000            and       eax,0FFFh
  0043e8d1  33c2                  xor       eax,edx
  0043e8d3  2bc2                  sub       eax,edx
  0043e8d5  66894612              mov       [esi+12h],ax
  0043e8d9  e862881000            call      00547140h
  0043e8de  99                    cdq
  0043e8df  b91e000000            mov       ecx,1Eh
  0043e8e4  f7f9                  idiv      ecx
  0043e8e6  83c214                add       edx,14h
  0043e8e9  89565c                mov       [esi+5Ch],edx
  0043e8ec  e84f881000            call      00547140h
  0043e8f1  99                    cdq
  0043e8f2  b932000000            mov       ecx,32h
  0043e8f7  66c7869a0000000000    mov       word [esi+9Ah],0
  0043e900  f7f9                  idiv      ecx
  0043e902  8b4620                mov       eax,[esi+20h]
  0043e905  b900040000            mov       ecx,400h
  0043e90a  66894e28              mov       [esi+28h],cx
  0043e90e  66894e2a              mov       [esi+2Ah],cx
  0043e912  83c264                add       edx,64h
  0043e915  0500400600            add       eax,64000h
  0043e91a  894620                mov       [esi+20h],eax
  0043e91d  894654                mov       [esi+54h],eax
  0043e920  8b4624                mov       eax,[esi+24h]
  0043e923  66895616              mov       [esi+16h],dx
  0043e927  8b561c                mov       edx,[esi+1Ch]
  0043e92a  894658                mov       [esi+58h],eax
  0043e92d  b080                  mov       al,80h
  0043e92f  895650                mov       [esi+50h],edx
  0043e932  884638                mov       [esi+38h],al
  0043e935  884639                mov       [esi+39h],al
  0043e938  88463a                mov       [esi+3Ah],al
  0043e93b  66395e10              cmp       [esi+10h],bx
  0043e93f  0f85b4000000          jne       near 0043E9F9h
  0043e945  0fbf4e12              movsx     ecx,word [esi+12h]
  0043e949  51                    push      ecx
  0043e94a  e8cd701000            call      00545A1Ch
  0043e94f  0faf465c              imul      eax,[esi+5Ch]
  0043e953  0fbf5612              movsx     edx,word [esi+12h]
  0043e957  8b4e50                mov       ecx,[esi+50h]
  0043e95a  52                    push      edx
  0043e95b  03c1                  add       eax,ecx
  0043e95d  89461c                mov       [esi+1Ch],eax
  0043e960  e8b1701000            call      00545A16h
  0043e965  0faf465c              imul      eax,[esi+5Ch]
  0043e969  8b4e58                mov       ecx,[esi+58h]
  0043e96c  8b7e20                mov       edi,[esi+20h]
  0043e96f  03c1                  add       eax,ecx
  0043e971  81c700800000          add       edi,8000h
  0043e977  894624                mov       [esi+24h],eax
  0043e97a  668b4616              mov       ax,[esi+16h]
  0043e97e  66034612              add       ax,[esi+12h]
  0043e982  897e20                mov       [esi+20h],edi
  0043e985  25ff0f0000            and       eax,0FFFh
  0043e98a  66894612              mov       [esi+12h],ax
  0043e98e  e8ad871000            call      00547140h
  0043e993  99                    cdq
  0043e994  b914000000            mov       ecx,14h
  0043e999  f7f9                  idiv      ecx
  0043e99b  03d1                  add       edx,ecx
  0043e99d  8b4e5c                mov       ecx,[esi+5Ch]
  0043e9a0  03ca                  add       ecx,edx
  0043e9a2  894e5c                mov       [esi+5Ch],ecx
  0043e9a5  b9c8000000            mov       ecx,0C8h
  0043e9aa  66014e28              add       [esi+28h],cx
  0043e9ae  66014e2a              add       [esi+2Ah],cx
  0043e9b2  668b4628              mov       ax,[esi+28h]
  0043e9b6  b988130000            mov       ecx,1388h
  0043e9bb  663bc1                cmp       ax,cx
  0043e9be  7e08                  jle       short 0043E9C8h
  0043e9c0  66894e28              mov       [esi+28h],cx
  0043e9c4  66894e2a              mov       [esi+2Ah],cx
  0043e9c8  66ff869a000000        inc       word [esi+9Ah]
  0043e9cf  e86c871000            call      00547140h
  0043e9d4  99                    cdq
  0043e9d5  b932000000            mov       ecx,32h
  0043e9da  f7f9                  idiv      ecx
  0043e9dc  668b869a000000        mov       ax,[esi+9Ah]
  0043e9e3  663bc1                cmp       ax,cx
  0043e9e6  7f0b                  jg        short 0043E9F3h
  0043e9e8  663d1e00              cmp       ax,1Eh
  0043e9ec  7e0b                  jle       short 0043E9F9h
  0043e9ee  663bd3                cmp       dx,bx
  0043e9f1  7506                  jne       short 0043E9F9h
  0043e9f3  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043e9f9  66837e1402            cmp       word [esi+14h],2
  0043e9fe  0f850f010000          jne       near 0043EB13h
  0043ea04  66837e1000            cmp       word [esi+10h],0
  0043ea09  750a                  jne       short 0043EA15h
  0043ea0b  66895e10              mov       [esi+10h],bx
  0043ea0f  66c746120f00          mov       word [esi+12h],0Fh
  0043ea15  66395e10              cmp       [esi+10h],bx
  0043ea19  0f85f4000000          jne       near 0043EB13h
  0043ea1f  66ff4e12              dec       word [esi+12h]
  0043ea23  66837e120a            cmp       word [esi+12h],0Ah
  0043ea28  751b                  jne       short 0043EA45h
  0043ea2a  881d27e05d00          mov       [5DE027h],bl
  0043ea30  c60524e05d00fa        mov       byte [5DE024h],0FAh
  0043ea37  c60525e05d0064        mov       byte [5DE025h],64h
  0043ea3e  c60526e05d0064        mov       byte [5DE026h],64h
  0043ea45  66837e1209            cmp       word [esi+12h],9
  0043ea4a  b150                  mov       cl,50h
  0043ea4c  7519                  jne       short 0043EA67h
  0043ea4e  881d27e05d00          mov       [5DE027h],bl
  0043ea54  c60524e05d00b4        mov       byte [5DE024h],0B4h
  0043ea5b  880d25e05d00          mov       [5DE025h],cl
  0043ea61  880d26e05d00          mov       [5DE026h],cl
  0043ea67  66837e1208            cmp       word [esi+12h],8
  0043ea6c  7519                  jne       short 0043EA87h
  0043ea6e  b03c                  mov       al,3Ch
  0043ea70  881d27e05d00          mov       [5DE027h],bl
  0043ea76  c60524e05d0078        mov       byte [5DE024h],78h
  0043ea7d  a225e05d00            mov       [5DE025h],al
  0043ea82  a226e05d00            mov       [5DE026h],al
  0043ea87  66837e1207            cmp       word [esi+12h],7
  0043ea8c  751a                  jne       short 0043EAA8h
  0043ea8e  881d27e05d00          mov       [5DE027h],bl
  0043ea94  880d24e05d00          mov       [5DE024h],cl
  0043ea9a  c60525e05d001e        mov       byte [5DE025h],1Eh
  0043eaa1  c60526e05d001e        mov       byte [5DE026h],1Eh
  0043eaa8  b806000000            mov       eax,6
  0043eaad  66394612              cmp       [esi+12h],ax
  0043eab1  7553                  jne       short 0043EB06h
  0043eab3  881d27e05d00          mov       [5DE027h],bl
  0043eab9  c60524e05d0000        mov       byte [5DE024h],0
  0043eac0  c60525e05d0000        mov       byte [5DE025h],0
  0043eac7  c60526e05d0000        mov       byte [5DE026h],0
  0043eace  66394612              cmp       [esi+12h],ax
  0043ead2  7532                  jne       short 0043EB06h
  0043ead4  bfc0935e00            mov       edi,5E93C0h
  0043ead9  bb04000000            mov       ebx,4
  0043eade  8b07                  mov       eax,[edi]
  0043eae0  a900000080            test      eax,80000000h
  0043eae5  7416                  je        short 0043EAFDh
  0043eae7  a900000050            test      eax,50000000h
  0043eaec  750f                  jne       short 0043EAFDh
  0043eaee  8b566c                mov       edx,[esi+6Ch]
  0043eaf1  6a00                  push      0
  0043eaf3  57                    push      edi
  0043eaf4  52                    push      edx
  0043eaf5  e826000000            call      0043EB20h
  0043eafa  83c40c                add       esp,0Ch
  0043eafd  81c79c000000          add       edi,9Ch
  0043eb03  4b                    dec       ebx
  0043eb04  75d8                  jne       short 0043EADEh
  0043eb06  66837e1200            cmp       word [esi+12h],0
  0043eb0b  7506                  jne       short 0043EB13h
  0043eb0d  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0043eb13  5f                    pop       edi
  0043eb14  5e                    pop       esi
  0043eb15  5b                    pop       ebx
  0043eb16  c3                    ret
