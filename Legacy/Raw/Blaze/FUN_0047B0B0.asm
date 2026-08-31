
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b0b0  section .text  offset fichier 0x7a4b0  159 octets

  0047b0b0  53                    push      ebx
  0047b0b1  56                    push      esi
  0047b0b2  8b74240c              mov       esi,[esp+0Ch]
  0047b0b6  57                    push      edi
  0047b0b7  668b4636              mov       ax,[esi+36h]
  0047b0bb  8d7e1c                lea       edi,[esi+1Ch]
  0047b0be  0fbf5e32              movsx     ebx,word [esi+32h]
  0047b0c2  24fb                  and       al,0FBh
  0047b0c4  53                    push      ebx
  0047b0c5  0c02                  or        al,2
  0047b0c7  66894636              mov       [esi+36h],ax
  0047b0cb  e84ca90c00            call      00545A1Ch
  0047b0d0  8b17                  mov       edx,[edi]
  0047b0d2  8d0c80                lea       ecx,[eax+eax*4]
  0047b0d5  c1e103                shl       ecx,3
  0047b0d8  03d1                  add       edx,ecx
  0047b0da  53                    push      ebx
  0047b0db  8917                  mov       [edi],edx
  0047b0dd  e834a90c00            call      00545A16h
  0047b0e2  8b5e24                mov       ebx,[esi+24h]
  0047b0e5  8d1480                lea       edx,[eax+eax*4]
  0047b0e8  8b4674                mov       eax,[esi+74h]
  0047b0eb  c1e203                shl       edx,3
  0047b0ee  03da                  add       ebx,edx
  0047b0f0  50                    push      eax
  0047b0f1  895e24                mov       [esi+24h],ebx
  0047b0f4  e8c7da0b00            call      00538BC0h
  0047b0f9  8b4e20                mov       ecx,[esi+20h]
  0047b0fc  24fe                  and       al,0FEh
  0047b0fe  c1e00b                shl       eax,0Bh
  0047b101  03c8                  add       ecx,eax
  0047b103  83c404                add       esp,4
  0047b106  894e20                mov       [esi+20h],ecx
  0047b109  33c9                  xor       ecx,ecx
  0047b10b  8a4e0b                mov       cl,[esi+0Bh]
  0047b10e  6aff                  push      0FFFFFFFFh
  0047b110  51                    push      ecx
  0047b111  6a47                  push      47h
  0047b113  68a81b5500            push      551BA8h
  0047b118  57                    push      edi
  0047b119  56                    push      esi
  0047b11a  c7462800140014        mov       dword [esi+28h],14001400h
  0047b121  c74638ffffff00        mov       dword [esi+38h],0FFFFFFh
  0047b128  e813fbffff            call      0047AC40h
  0047b12d  83c418                add       esp,18h
  0047b130  57                    push      edi
  0047b131  6800120000            push      1200h
  0047b136  6853060000            push      653h
  0047b13b  6a14                  push      14h
  0047b13d  e8de290c00            call      0053DB20h
  0047b142  83c410                add       esp,10h
  0047b145  c70600000000          mov       dword [esi],0
  0047b14b  5f                    pop       edi
  0047b14c  5e                    pop       esi
  0047b14d  5b                    pop       ebx
  0047b14e  c3                    ret
