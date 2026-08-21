
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515970  section .text  offset fichier 0x114d70  96 octets

  00515970  8b4c2404              mov       ecx,[esp+4]
  00515974  56                    push      esi
  00515975  8b742410              mov       esi,[esp+10h]
  00515979  33c0                  xor       eax,eax
  0051597b  8b9190000000          mov       edx,[ecx+90h]
  00515981  33c9                  xor       ecx,ecx
  00515983  8a4601                mov       al,[esi+1]
  00515986  668b4cc206            mov       cx,[edx+eax*8+6]
  0051598b  8d04c2                lea       eax,[edx+eax*8]
  0051598e  33d2                  xor       edx,edx
  00515990  51                    push      ecx
  00515991  668b5004              mov       dx,[eax+4]
  00515995  33c9                  xor       ecx,ecx
  00515997  668b4802              mov       cx,[eax+2]
  0051599b  52                    push      edx
  0051599c  8b15cc975e00          mov       edx,[5E97CCh]
  005159a2  51                    push      ecx
  005159a3  8b8aa8000000          mov       ecx,[edx+0A8h]
  005159a9  33d2                  xor       edx,edx
  005159ab  668b10                mov       dx,[eax]
  005159ae  8b01                  mov       eax,[ecx]
  005159b0  33c9                  xor       ecx,ecx
  005159b2  8a0c10                mov       cl,[eax+edx]
  005159b5  51                    push      ecx
  005159b6  e8e5740200            call      0053CEA0h
  005159bb  83c410                add       esp,10h
  005159be  8d4602                lea       eax,[esi+2]
  005159c1  5e                    pop       esi
  005159c2  c3                    ret
  005159c3  90                    nop
  005159c4  90                    nop
  005159c5  90                    nop
  005159c6  90                    nop
  005159c7  90                    nop
  005159c8  90                    nop
  005159c9  90                    nop
  005159ca  90                    nop
  005159cb  90                    nop
  005159cc  90                    nop
  005159cd  90                    nop
  005159ce  90                    nop
  005159cf  90                    nop
