Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00432240  section .text  offset fichier 0x31640  336 octets

  00432240  56                    push      esi
  00432241  8b742408              mov       esi,[esp+8]
  00432245  57                    push      edi
  00432246  bf01000000            mov       edi,1
  0043224b  66837e1000            cmp       word [esi+10h],0
  00432250  0f85c2000000          jne       near 00432318h
  00432256  66897e10              mov       [esi+10h],di
  0043225a  c7463cb80c5500        mov       dword [esi+3Ch],550CB8h
  00432261  c6464000              mov       byte [esi+40h],0
  00432265  c6460a02              mov       byte [esi+0Ah],2
  00432269  c6460b00              mov       byte [esi+0Bh],0
  0043226d  e8ce4e1100            call      00547140h
  00432272  99                    cdq
  00432273  b9e8030000            mov       ecx,3E8h
  00432278  f7f9                  idiv      ecx
  0043227a  8d8220030000          lea       eax,[edx+320h]
  00432280  66894628              mov       [esi+28h],ax
  00432284  6689462a              mov       [esi+2Ah],ax
  00432288  e8b34e1100            call      00547140h
  0043228d  99                    cdq
  0043228e  b920030000            mov       ecx,320h
  00432293  f7f9                  idiv      ecx
  00432295  81ea90010000          sub       edx,190h
  0043229b  66015632              add       [esi+32h],dx
  0043229f  e89c4e1100            call      00547140h
  004322a4  99                    cdq
  004322a5  33c2                  xor       eax,edx
  004322a7  2bc2                  sub       eax,edx
  004322a9  83e007                and       eax,7
  004322ac  33c2                  xor       eax,edx
  004322ae  2bc2                  sub       eax,edx
  004322b0  83c008                add       eax,8
  004322b3  66894614              mov       [esi+14h],ax
  004322b7  e8844e1100            call      00547140h
  004322bc  99                    cdq
  004322bd  b906000000            mov       ecx,6
  004322c2  f7f9                  idiv      ecx
  004322c4  03d1                  add       edx,ecx
  004322c6  66895616              mov       [esi+16h],dx
  004322ca  e8714e1100            call      00547140h
  004322cf  99                    cdq
  004322d0  b93c000000            mov       ecx,3Ch
  004322d5  f7f9                  idiv      ecx
  004322d7  8b4e1c                mov       ecx,[esi+1Ch]
  004322da  83ea1e                sub       edx,1Eh
  004322dd  c1e20c                shl       edx,0Ch
  004322e0  03ca                  add       ecx,edx
  004322e2  894e1c                mov       [esi+1Ch],ecx
  004322e5  e8564e1100            call      00547140h
  004322ea  99                    cdq
  004322eb  b93c000000            mov       ecx,3Ch
  004322f0  f7f9                  idiv      ecx
  004322f2  8b4624                mov       eax,[esi+24h]
  004322f5  83ea1e                sub       edx,1Eh
  004322f8  c1e20c                shl       edx,0Ch
  004322fb  03c2                  add       eax,edx
  004322fd  894624                mov       [esi+24h],eax
  00432300  e83b4e1100            call      00547140h
  00432305  99                    cdq
  00432306  b914000000            mov       ecx,14h
  0043230b  f7f9                  idiv      ecx
  0043230d  8b4620                mov       eax,[esi+20h]
  00432310  c1e20c                shl       edx,0Ch
  00432313  03c2                  add       eax,edx
  00432315  894620                mov       [esi+20h],eax

LAB_00432318:
  00432318  66397e10              cmp       [esi+10h],di
  0043231c  7567                  jne       short 00432385h
  0043231e  0fbf5632              movsx     edx,word [esi+32h]
  00432322  52                    push      edx
  00432323  e8f4361100            call      00545A1Ch
  00432328  0fbf4e14              movsx     ecx,word [esi+14h]
  0043232c  0fafc1                imul      eax,ecx
  0043232f  0fbf5632              movsx     edx,word [esi+32h]
  00432333  8b4e1c                mov       ecx,[esi+1Ch]
  00432336  668b7e14              mov       di,[esi+14h]
  0043233a  03c8                  add       ecx,eax
  0043233c  52                    push      edx
  0043233d  894e1c                mov       [esi+1Ch],ecx
  00432340  e8d1361100            call      00545A16h
  00432345  0fbfcf                movsx     ecx,di
  00432348  0fafc1                imul      eax,ecx
  0043234b  8b5624                mov       edx,[esi+24h]
  0043234e  6ae2                  push      0FFFFFFE2h
  00432350  03d0                  add       edx,eax
  00432352  8b4620                mov       eax,[esi+20h]
  00432355  895624                mov       [esi+24h],edx
  00432358  6ae2                  push      0FFFFFFE2h
  0043235a  0fbf5616              movsx     edx,word [esi+16h]
  0043235e  c1e20c                shl       edx,0Ch
  00432361  03c2                  add       eax,edx
  00432363  83c7fe                add       edi,0FFFFFFFEh
  00432366  6ae2                  push      0FFFFFFE2h
  00432368  56                    push      esi
  00432369  894620                mov       [esi+20h],eax
  0043236c  66897e14              mov       [esi+14h],di
  00432370  e86b2afdff            call      00404DE0h
  00432375  83c410                add       esp,10h
  00432378  66837e1400            cmp       word [esi+14h],0
  0043237d  7f06                  jg        short 00432385h
  0043237f  c706ffffff7f          mov       dword [esi],7FFFFFFFh

LAB_00432385:
  00432385  5f                    pop       edi
  00432386  5e                    pop       esi
  00432387  c3                    ret
  00432388  90                    nop
  00432389  90                    nop
  0043238a  90                    nop
  0043238b  90                    nop
  0043238c  90                    nop
  0043238d  90                    nop
  0043238e  90                    nop
  0043238f  90                    nop

2 cible(s) de saut interne : 0x00432318, 0x00432385
