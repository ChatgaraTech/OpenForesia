  00442870  51                    push      ecx
  00442871  53                    push      ebx
  00442872  55                    push      ebp
  00442873  56                    push      esi
  00442874  8b742414              mov       esi,[esp+14h]
  00442878  57                    push      edi
  00442879  66837e1000            cmp       word [esi+10h],0
  0044287e  0f851b010000          jne       near 0044299Fh
  00442884  66c746100100          mov       word [esi+10h],1
  0044288a  66c746120000          mov       word [esi+12h],0
  00442890  c6460408              mov       byte [esi+4],8
  00442894  c6460a09              mov       byte [esi+0Ah],9
  00442898  c6460b00              mov       byte [esi+0Bh],0
  0044289c  e89f481000            call      00547140h
  004428a1  99                    cdq
  004428a2  b984030000            mov       ecx,384h
  004428a7  f7f9                  idiv      ecx
  004428a9  81c200040000          add       edx,400h
  004428af  66895634              mov       [esi+34h],dx
  004428b3  e888481000            call      00547140h
  004428b8  99                    cdq
  004428b9  33c2                  xor       eax,edx
  004428bb  66c74628b80b          mov       word [esi+28h],0BB8h
  004428c1  2bc2                  sub       eax,edx
  004428c3  25ff0f0000            and       eax,0FFFh
  004428c8  33c2                  xor       eax,edx
  004428ca  8bf8                  mov       edi,eax
  004428cc  2bfa                  sub       edi,edx
  004428ce  66ff4e14              dec       word [esi+14h]
  004428d2  668b4614              mov       ax,[esi+14h]
  004428d6  66897e32              mov       [esi+32h],di
  004428da  6685c0                test      ax,ax
  004428dd  6689869a000000        mov       [esi+9Ah],ax
  004428e4  0f8cae000000          jl        near 00442998h
  004428ea  0fbf4634              movsx     eax,word [esi+34h]
  004428ee  8b5624                mov       edx,[esi+24h]
  004428f1  8b6e1c                mov       ebp,[esi+1Ch]
  004428f4  8b5e20                mov       ebx,[esi+20h]
  004428f7  50                    push      eax
  004428f8  89542414              mov       [esp+14h],edx
  004428fc  e815311000            call      00545A16h
  00442901  8d0440                lea       eax,[eax+eax*2]
  00442904  8d0480                lea       eax,[eax+eax*4]
  00442907  8d0c80                lea       ecx,[eax+eax*4]
  0044290a  d1e1                  shl       ecx,1
  0044290c  0fbfc7                movsx     eax,di
  0044290f  8bd1                  mov       edx,ecx
  00442911  50                    push      eax
  00442912  c1fa0c                sar       edx,0Ch
  00442915  8954241c              mov       [esp+1Ch],edx
  00442919  e8f8301000            call      00545A16h
  0044291e  8b4c2418              mov       ecx,[esp+18h]
  00442922  0fafc8                imul      ecx,eax
  00442925  0fbf5634              movsx     edx,word [esi+34h]
  00442929  8b461c                mov       eax,[esi+1Ch]
  0044292c  52                    push      edx
  0044292d  2bc1                  sub       eax,ecx
  0044292f  89461c                mov       [esi+1Ch],eax
  00442932  e8df301000            call      00545A16h
  00442937  0fbf4e32              movsx     ecx,word [esi+32h]
  0044293b  8d0440                lea       eax,[eax+eax*2]
  0044293e  51                    push      ecx
  0044293f  8d0480                lea       eax,[eax+eax*4]
  00442942  8d0480                lea       eax,[eax+eax*4]
  00442945  d1e0                  shl       eax,1
  00442947  8bf8                  mov       edi,eax
  00442949  c1ff0c                sar       edi,0Ch
  0044294c  e8cb301000            call      00545A1Ch
  00442951  0faff8                imul      edi,eax
  00442954  0fbf5634              movsx     edx,word [esi+34h]
  00442958  8b4624                mov       eax,[esi+24h]
  0044295b  52                    push      edx
  0044295c  03c7                  add       eax,edi
  0044295e  894624                mov       [esi+24h],eax
  00442961  e8b6301000            call      00545A1Ch
  00442966  8d0440                lea       eax,[eax+eax*2]
  00442969  8b4e20                mov       ecx,[esi+20h]
  0044296c  6aff                  push      0FFFFFFFFh
  0044296e  6a00                  push      0
  00442970  8d0480                lea       eax,[eax+eax*4]
  00442973  68c5000000            push      0C5h
  00442978  56                    push      esi
  00442979  8d0480                lea       eax,[eax+eax*4]
  0044297c  d1e0                  shl       eax,1
  0044297e  03c8                  add       ecx,eax
  00442980  894e20                mov       [esi+20h],ecx
  00442983  e858810300            call      0047AAE0h
  00442988  8b4c2420              mov       ecx,[esp+20h]
  0044298c  83c410                add       esp,10h
  0044298f  896e1c                mov       [esi+1Ch],ebp
  00442992  895e20                mov       [esi+20h],ebx
  00442995  894e24                mov       [esi+24h],ecx
  00442998  8b06                  mov       eax,[esi]
  0044299a  80cc40                or        ah,40h
  0044299d  8906                  mov       [esi],eax
  0044299f  bf01000000            mov       edi,1
  004429a4  bb03000000            mov       ebx,3
  004429a9  66397e10              cmp       [esi+10h],di
  004429ad  7515                  jne       short 004429C4h
  004429af  e88c471000            call      00547140h
  004429b4  99                    cdq
  004429b5  33c2                  xor       eax,edx
  004429b7  2bc2                  sub       eax,edx
  004429b9  23c3                  and       eax,ebx
  004429bb  33c2                  xor       eax,edx
  004429bd  2bc2                  sub       eax,edx
  004429bf  0408                  add       al,8
  004429c1  884604                mov       [esi+4],al
  004429c4  66ff8e9a000000        dec       word [esi+9Ah]
  004429cb  668b869a000000        mov       ax,[esi+9Ah]
  004429d2  6685c0                test      ax,ax
  004429d5  7f70                  jg        short 00442A47h
  004429d7  6685c0                test      ax,ax
  004429da  b9fa000000            mov       ecx,0FAh
  004429df  751c                  jne       short 004429FDh
  004429e1  66890d60915e00        mov       [5E9160h],cx
  004429e8  66890d62915e00        mov       [5E9162h],cx
  004429ef  66890d64915e00        mov       [5E9164h],cx
  004429f6  66893d66915e00        mov       [5E9166h],di
  004429fd  8b16                  mov       edx,[esi]
  004429ff  80e6bf                and       dh,0BFh
  00442a02  66395e14              cmp       [esi+14h],bx
  00442a06  8916                  mov       [esi],edx
  00442a08  753d                  jne       short 00442A47h
  00442a0a  6683be9a000000f6      cmp       word [esi+9Ah],0FFF6h
  00442a12  7c33                  jl        short 00442A47h
  00442a14  6aff                  push      0FFFFFFFFh
  00442a16  6a00                  push      0
  00442a18  8d7e1c                lea       edi,[esi+1Ch]
  00442a1b  51                    push      ecx
  00442a1c  6a00                  push      0
  00442a1e  57                    push      edi
  00442a1f  56                    push      esi
  00442a20  66c746140000          mov       word [esi+14h],0
  00442a26  e815820300            call      0047AC40h
  00442a2b  83c418                add       esp,18h
  00442a2e  6aff                  push      0FFFFFFFFh
  00442a30  6a00                  push      0
  00442a32  68fb000000            push      0FBh
  00442a37  6a00                  push      0
  00442a39  57                    push      edi
  00442a3a  56                    push      esi
  00442a3b  e800820300            call      0047AC40h
  00442a40  83c418                add       esp,18h
  00442a43  66895e14              mov       [esi+14h],bx
  00442a47  668b869a000000        mov       ax,[esi+9Ah]
  00442a4e  663dd8ff              cmp       ax,0FFD8h
  00442a52  7f61                  jg        short 00442AB5h
  00442a54  7529                  jne       short 00442A7Fh
  00442a56  668b5650              mov       dx,[esi+50h]
  00442a5a  66891560915e00        mov       [5E9160h],dx
  00442a61  668b4654              mov       ax,[esi+54h]
  00442a65  66a362915e00          mov       [5E9162h],ax
  00442a6b  668b4e58              mov       cx,[esi+58h]
  00442a6f  66890d64915e00        mov       [5E9164h],cx
  00442a76  66c70566915e000200    mov       word [5E9166h],2
  00442a7f  660fb64638            movzx     ax,byte [esi+38h]
  00442a84  660fb64e39            movzx     cx,byte [esi+39h]
  00442a89  660fb6563a            movzx     dx,byte [esi+3Ah]
  00442a8e  83e80f                sub       eax,0Fh
  00442a91  83e90f                sub       ecx,0Fh
  00442a94  83ea0f                sub       edx,0Fh
  00442a97  6685c0                test      ax,ax
  00442a9a  7d10                  jge       short 00442AACh
  00442a9c  8b3e                  mov       edi,[esi]
  00442a9e  33c0                  xor       eax,eax
  00442aa0  33c9                  xor       ecx,ecx
  00442aa2  33d2                  xor       edx,edx
  00442aa4  81e7ffffff7f          and       edi,7FFFFFFFh
  00442aaa  893e                  mov       [esi],edi
  00442aac  884638                mov       [esi+38h],al
  00442aaf  884e39                mov       [esi+39h],cl
  00442ab2  88563a                mov       [esi+3Ah],dl
  00442ab5  5f                    pop       edi
  00442ab6  5e                    pop       esi
  00442ab7  5d                    pop       ebp
  00442ab8  5b                    pop       ebx
  00442ab9  59                    pop       ecx
  00442aba  c3                    ret
