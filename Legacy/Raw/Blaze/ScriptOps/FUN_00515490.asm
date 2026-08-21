
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515490  section .text  offset fichier 0x114890  96 octets

  00515490  8b542408              mov       edx,[esp+8]
  00515494  56                    push      esi
  00515495  8b742410              mov       esi,[esp+10h]
  00515499  33c9                  xor       ecx,ecx
  0051549b  33c0                  xor       eax,eax
  0051549d  8a4e02                mov       cl,[esi+2]
  005154a0  8a4601                mov       al,[esi+1]
  005154a3  8a6e03                mov       ch,[esi+3]
  005154a6  51                    push      ecx
  005154a7  52                    push      edx
  005154a8  ff1485c8ae5600        call      dword [eax*4+56AEC8h]
  005154af  8b4c241c              mov       ecx,[esp+1Ch]
  005154b3  83c408                add       esp,8
  005154b6  8901                  mov       [ecx],eax
  005154b8  8d4604                lea       eax,[esi+4]
  005154bb  5e                    pop       esi
  005154bc  c3                    ret
  005154bd  90                    nop
  005154be  90                    nop
  005154bf  90                    nop
  005154c0  8b44240c              mov       eax,[esp+0Ch]
  005154c4  8b542404              mov       edx,[esp+4]
  005154c8  33c9                  xor       ecx,ecx
  005154ca  8a4801                mov       cl,[eax+1]
  005154cd  8a6802                mov       ch,[eax+2]
  005154d0  8b828c000000          mov       eax,[edx+8Ch]
  005154d6  8b0488                mov       eax,[eax+ecx*4]
  005154d9  c3                    ret
  005154da  90                    nop
  005154db  90                    nop
  005154dc  90                    nop
  005154dd  90                    nop
  005154de  90                    nop
  005154df  90                    nop
  005154e0  8b4c2410              mov       ecx,[esp+10h]
  005154e4  833900                cmp       dword [ecx],0
  005154e7  7420                  je        short 00515509h
  005154e9  8b44240c              mov       eax,[esp+0Ch]
  005154ed  33d2                  xor       edx,edx
  005154ef  8a                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
