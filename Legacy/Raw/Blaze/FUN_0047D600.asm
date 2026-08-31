
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d600  section .text  offset fichier 0x7ca00  105 octets

  0047d600  56                    push      esi
  0047d601  8b742408              mov       esi,[esp+8]
  0047d605  8b4674                mov       eax,[esi+74h]
  0047d608  50                    push      eax
  0047d609  e8b2b50b00            call      00538BC0h
  0047d60e  8b4e20                mov       ecx,[esi+20h]
  0047d611  24fe                  and       al,0FEh
  0047d613  c1e00b                shl       eax,0Bh
  0047d616  03c8                  add       ecx,eax
  0047d618  83c404                add       esp,4
  0047d61b  894e20                mov       [esi+20h],ecx
  0047d61e  33c9                  xor       ecx,ecx
  0047d620  8a4e0b                mov       cl,[esi+0Bh]
  0047d623  6aff                  push      0FFFFFFFFh
  0047d625  51                    push      ecx
  0047d626  6a71                  push      71h
  0047d628  56                    push      esi
  0047d629  c7463800000000        mov       dword [esi+38h],0
  0047d630  c7462800000000        mov       dword [esi+28h],0
  0047d637  66c7462c0000          mov       word [esi+2Ch],0
  0047d63d  c646041b              mov       byte [esi+4],1Bh
  0047d641  e89ad4ffff            call      0047AAE0h
  0047d646  83c410                add       esp,10h
  0047d649  8d561c                lea       edx,[esi+1Ch]
  0047d64c  52                    push      edx
  0047d64d  6800120000            push      1200h
  0047d652  6800040000            push      400h
  0047d657  6a1c                  push      1Ch
  0047d659  e8c2040c00            call      0053DB20h
  0047d65e  83c410                add       esp,10h
  0047d661  c70600000000          mov       dword [esi],0
  0047d667  5e                    pop       esi
  0047d668  c3                    ret
