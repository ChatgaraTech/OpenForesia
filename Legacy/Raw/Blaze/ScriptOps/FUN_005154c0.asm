
Legacy/Binaries/Blaze.exe  base 0x00400000
0x005154c0  section .text  offset fichier 0x1148c0  96 octets

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
  005154ef  8a5001                mov       dl,[eax+1]
  005154f2  8a7002                mov       dh,[eax+2]
  005154f5  8b442404              mov       eax,[esp+4]
  005154f9  8b808c000000          mov       eax,[eax+8Ch]
  005154ff  8b0490                mov       eax,[eax+edx*4]
  00515502  c70100000000          mov       dword [ecx],0
  00515508  c3                    ret

LAB_00515509:
  00515509  8b44240c              mov       eax,[esp+0Ch]
  0051550d  83c003                add       eax,3
  00515510  c3                    ret
  00515511  90                    nop
  00515512  90                    nop
  00515513  90                    nop
  00515514  90                    nop
  00515515  90                    nop
  00515516  90                    nop
  00515517  90                    nop
  00515518  90                    nop
  00515519  90                    nop
  0051551a  90                    nop
  0051551b  90                    nop
  0051551c  90                    nop
  0051551d  90                    nop
  0051551e  90                    nop
  0051551f  90                    nop

1 cible(s) de saut interne : 0x00515509
