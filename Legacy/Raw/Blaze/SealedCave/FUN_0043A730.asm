Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043a730  section .text  offset fichier 0x39b30  304 octets

  0043a730  56                    push      esi
  0043a731  8b742408              mov       esi,[esp+8]
  0043a735  57                    push      edi
  0043a736  66837e1000            cmp       word [esi+10h],0
  0043a73b  0f85bc000000          jne       near 0043A7FDh
  0043a741  66c746100100          mov       word [esi+10h],1
  0043a747  66c746160000          mov       word [esi+16h],0
  0043a74d  8d7e1c                lea       edi,[esi+1Ch]
  0043a750  e8ebc91000            call      00547140h
  0043a755  99                    cdq
  0043a756  b9c8000000            mov       ecx,0C8h
  0043a75b  f7f9                  idiv      ecx
  0043a75d  8b0f                  mov       ecx,[edi]
  0043a75f  83ea64                sub       edx,64h
  0043a762  c1e20c                shl       edx,0Ch
  0043a765  03ca                  add       ecx,edx
  0043a767  890f                  mov       [edi],ecx
  0043a769  e8d2c91000            call      00547140h
  0043a76e  99                    cdq
  0043a76f  b9c8000000            mov       ecx,0C8h
  0043a774  f7f9                  idiv      ecx
  0043a776  8b4624                mov       eax,[esi+24h]
  0043a779  83ea64                sub       edx,64h
  0043a77c  c1e20c                shl       edx,0Ch
  0043a77f  03c2                  add       eax,edx
  0043a781  894624                mov       [esi+24h],eax
  0043a784  e8b7c91000            call      00547140h
  0043a789  99                    cdq
  0043a78a  33c2                  xor       eax,edx
  0043a78c  2bc2                  sub       eax,edx
  0043a78e  25ff030000            and       eax,3FFh
  0043a793  33c2                  xor       eax,edx
  0043a795  2bc2                  sub       eax,edx
  0043a797  8d90dc050000          lea       edx,[eax+5DCh]
  0043a79d  05c4090000            add       eax,9C4h
  0043a7a2  66895628              mov       [esi+28h],dx
  0043a7a6  6689462a              mov       [esi+2Ah],ax
  0043a7aa  6689462c              mov       [esi+2Ch],ax
  0043a7ae  e88dc91000            call      00547140h
  0043a7b3  99                    cdq
  0043a7b4  b928000000            mov       ecx,28h
  0043a7b9  f7f9                  idiv      ecx
  0043a7bb  83ea14                sub       edx,14h
  0043a7be  895650                mov       [esi+50h],edx
  0043a7c1  e87ac91000            call      00547140h
  0043a7c6  99                    cdq
  0043a7c7  b928000000            mov       ecx,28h
  0043a7cc  f7f9                  idiv      ecx
  0043a7ce  83ea14                sub       edx,14h
  0043a7d1  895658                mov       [esi+58h],edx
  0043a7d4  e867c91000            call      00547140h
  0043a7d9  99                    cdq
  0043a7da  b932000000            mov       ecx,32h
  0043a7df  6aff                  push      0FFFFFFFFh
  0043a7e1  f7f9                  idiv      ecx
  0043a7e3  6a00                  push      0
  0043a7e5  68f9000000            push      0F9h
  0043a7ea  6a00                  push      0
  0043a7ec  57                    push      edi
  0043a7ed  56                    push      esi
  0043a7ee  8d541250              lea       edx,[edx+edx+50h]
  0043a7f2  89565c                mov       [esi+5Ch],edx
  0043a7f5  e846040400            call      0047AC40h
  0043a7fa  83c418                add       esp,18h

LAB_0043a7fd:
  0043a7fd  66837e1001            cmp       word [esi+10h],1
  0043a802  754c                  jne       short 0043A850h
  0043a804  66837e1600            cmp       word [esi+16h],0
  0043a809  7545                  jne       short 0043A850h
  0043a80b  6aff                  push      0FFFFFFFFh
  0043a80d  6a00                  push      0
  0043a80f  68c5000000            push      0C5h
  0043a814  56                    push      esi
  0043a815  66c746141c00          mov       word [esi+14h],1Ch
  0043a81b  e8c0020400            call      0047AAE0h
  0043a820  83c410                add       esp,10h
  0043a823  66c746143900          mov       word [esi+14h],39h
  0043a829  6aff                  push      0FFFFFFFFh
  0043a82b  6a00                  push      0
  0043a82d  68c5000000            push      0C5h
  0043a832  56                    push      esi
  0043a833  e8a8020400            call      0047AAE0h
  0043a838  8b06                  mov       eax,[esi]
  0043a83a  83c410                add       esp,10h
  0043a83d  25ffffff7f            and       eax,7FFFFFFFh
  0043a842  66c746100200          mov       word [esi+10h],2
  0043a848  66c746120000          mov       word [esi+12h],0
  0043a84e  8906                  mov       [esi],eax

LAB_0043a850:
  0043a850  5f                    pop       edi
  0043a851  5e                    pop       esi
  0043a852  c3                    ret
  0043a853  90                    nop
  0043a854  90                    nop
  0043a855  90                    nop
  0043a856  90                    nop
  0043a857  90                    nop
  0043a858  90                    nop
  0043a859  90                    nop
  0043a85a  90                    nop
  0043a85b  90                    nop
  0043a85c  90                    nop
  0043a85d  90                    nop
  0043a85e  90                    nop
  0043a85f  90                    nop

2 cible(s) de saut interne : 0x0043a7fd, 0x0043a850
