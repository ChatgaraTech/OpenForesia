Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00467c10  section .text  offset fichier 0x67010  416 octets

  00467c10  56                    push      esi
  00467c11  8b742408              mov       esi,[esp+8]
  00467c15  57                    push      edi
  00467c16  33ff                  xor       edi,edi
  00467c18  66397e10              cmp       [esi+10h],di
  00467c1c  7512                  jne       short 00467C30h
  00467c1e  66c746100100          mov       word [esi+10h],1
  00467c24  66897e12              mov       [esi+12h],di
  00467c28  66897e14              mov       [esi+14h],di
  00467c2c  66897e16              mov       [esi+16h],di

LAB_00467c30:
  00467c30  66837e1001            cmp       word [esi+10h],1
  00467c35  751e                  jne       short 00467C55h
  00467c37  e804f50d00            call      00547140h
  00467c3c  99                    cdq
  00467c3d  b93c000000            mov       ecx,3Ch
  00467c42  66c746100200          mov       word [esi+10h],2
  00467c48  f7f9                  idiv      ecx
  00467c4a  66897e14              mov       [esi+14h],di
  00467c4e  83c214                add       edx,14h
  00467c51  66895612              mov       [esi+12h],dx

LAB_00467c55:
  00467c55  66837e1002            cmp       word [esi+10h],2
  00467c5a  752f                  jne       short 00467C8Bh
  00467c5c  66ff4614              inc       word [esi+14h]
  00467c60  668b4614              mov       ax,[esi+14h]
  00467c64  66394612              cmp       [esi+12h],ax
  00467c68  7521                  jne       short 00467C8Bh
  00467c6a  66c746100300          mov       word [esi+10h],3
  00467c70  66897e12              mov       [esi+12h],di
  00467c74  e8c7f40d00            call      00547140h
  00467c79  99                    cdq
  00467c7a  33c2                  xor       eax,edx
  00467c7c  2bc2                  sub       eax,edx
  00467c7e  25ff030000            and       eax,3FFh
  00467c83  33c2                  xor       eax,edx
  00467c85  2bc2                  sub       eax,edx
  00467c87  66894614              mov       [esi+14h],ax

LAB_00467c8b:
  00467c8b  66837e1003            cmp       word [esi+10h],3
  00467c90  0f85c0000000          jne       near 00467D56h
  00467c96  0fbf4612              movsx     eax,word [esi+12h]
  00467c9a  83f830                cmp       eax,30h
  00467c9d  0f87a2000000          ja        near 00467D45h
  00467ca3  33d2                  xor       edx,edx
  00467ca5  8a907c7d4600          mov       dl,[eax+467D7Ch]
  00467cab  ff24955c7d4600        jmp       dword [edx*4+467D5Ch]
  00467cb2  0fbf4614              movsx     eax,word [esi+14h]
  00467cb6  0500010000            add       eax,100h
  00467cbb  eb1f                  jmp       short 00467CDCh
  00467cbd  0fbf4614              movsx     eax,word [esi+14h]
  00467cc1  0500020000            add       eax,200h
  00467cc6  eb3a                  jmp       short 00467D02h
  00467cc8  0fbf4614              movsx     eax,word [esi+14h]
  00467ccc  0500040000            add       eax,400h
  00467cd1  eb50                  jmp       short 00467D23h
  00467cd3  0fbf4614              movsx     eax,word [esi+14h]
  00467cd7  0500020000            add       eax,200h

LAB_00467cdc:
  00467cdc  8b0dcc975e00          mov       ecx,[5E97CCh]
  00467ce2  50                    push      eax
  00467ce3  50                    push      eax
  00467ce4  6800040000            push      400h
  00467ce9  8b91a8000000          mov       edx,[ecx+0A8h]
  00467cef  33c9                  xor       ecx,ecx
  00467cf1  8b02                  mov       eax,[edx]
  00467cf3  8a4810                mov       cl,[eax+10h]
  00467cf6  51                    push      ecx
  00467cf7  eb44                  jmp       short 00467D3Dh
  00467cf9  0fbf4614              movsx     eax,word [esi+14h]
  00467cfd  0500010000            add       eax,100h

LAB_00467d02:
  00467d02  8b15cc975e00          mov       edx,[5E97CCh]
  00467d08  50                    push      eax
  00467d09  50                    push      eax
  00467d0a  6800040000            push      400h
  00467d0f  8b82a8000000          mov       eax,[edx+0A8h]
  00467d15  33d2                  xor       edx,edx
  00467d17  8b08                  mov       ecx,[eax]
  00467d19  8a5110                mov       dl,[ecx+10h]
  00467d1c  52                    push      edx
  00467d1d  eb1e                  jmp       short 00467D3Dh
  00467d1f  0fbf4614              movsx     eax,word [esi+14h]

LAB_00467d23:
  00467d23  50                    push      eax
  00467d24  50                    push      eax
  00467d25  a1cc975e00            mov       eax,[5E97CCh]
  00467d2a  6800040000            push      400h
  00467d2f  8b88a8000000          mov       ecx,[eax+0A8h]
  00467d35  33c0                  xor       eax,eax
  00467d37  8b11                  mov       edx,[ecx]
  00467d39  8a4210                mov       al,[edx+10h]
  00467d3c  50                    push      eax

LAB_00467d3d:
  00467d3d  e85e510d00            call      0053CEA0h
  00467d42  83c410                add       esp,10h

LAB_00467d45:
  00467d45  66ff4612              inc       word [esi+12h]
  00467d49  66837e1231            cmp       word [esi+12h],31h
  00467d4e  7506                  jne       short 00467D56h
  00467d50  66c746100100          mov       word [esi+10h],1

LAB_00467d56:
  00467d56  5f                    pop       edi
  00467d57  5e                    pop       esi
  00467d58  c3                    ret
  00467d59  90                    nop
  00467d5a  8bff                  mov       edi,edi
  00467d5c  1f                    pop       ds
  00467d5d  7d46                  jge       short 00467DA5h
  00467d5f  00b27c4600bd          add       [edx-42FFB984h],dh
  00467d65  7c46                  jl        short 00467DADh
  00467d67  00c8                  add       al,cl
  00467d69  7c46                  jl        short 00467DB1h
  00467d6b  00d3                  add       bl,dl
  00467d6d  7c46                  jl        short 00467DB5h
  00467d6f  00f9                  add       cl,bh
  00467d71  7c46                  jl        short 00467DB9h
  00467d73  001f                  add       [edi],bl
  00467d75  7d46                  jge       short 00467DBDh
  00467d77  00457d                add       [ebp+7Dh],al
  00467d7a  46                    inc       esi
  00467d7b  0000                  add       [eax],al
  00467d7d  07                    pop       es
  00467d7e  07                    pop       es
  00467d7f  07                    pop       es
  00467d80  07                    pop       es
  00467d81  07                    pop       es
  00467d82  07                    pop       es
  00467d83  07                    pop       es
  00467d84  0107                  add       [edi],eax
  00467d86  07                    pop       es
  00467d87  07                    pop       es
  00467d88  07                    pop       es
  00467d89  07                    pop       es
  00467d8a  07                    pop       es
  00467d8b  07                    pop       es
  00467d8c  0207                  add       al,[edi]
  00467d8e  07                    pop       es
  00467d8f  07                    pop       es
  00467d90  07                    pop       es
  00467d91  07                    pop       es
  00467d92  07                    pop       es
  00467d93  07                    pop       es
  00467d94  0307                  add       eax,[edi]
  00467d96  07                    pop       es
  00467d97  07                    pop       es
  00467d98  07                    pop       es
  00467d99  07                    pop       es
  00467d9a  07                    pop       es
  00467d9b  07                    pop       es
  00467d9c  0407                  add       al,7
  00467d9e  07                    pop       es
  00467d9f  07                    pop       es
  00467da0  07                    pop       es
  00467da1  07                    pop       es
  00467da2  07                    pop       es
  00467da3  07                    pop       es
  00467da4  0507070707            add       eax,7070707h
  00467da9  07                    pop       es
  00467daa  07                    pop       es
  00467dab  07                    pop       es
  00467dac  06                    push      es

LAB_00467dad:
  00467dad  90                    nop
  00467dae  90                    nop
  00467daf  90                    nop

11 cible(s) de saut interne : 0x00467c30, 0x00467c55, 0x00467c8b, 0x00467cdc, 0x00467d02, 0x00467d23, 0x00467d3d, 0x00467d45, 0x00467d56, 0x00467da5, 0x00467dad
