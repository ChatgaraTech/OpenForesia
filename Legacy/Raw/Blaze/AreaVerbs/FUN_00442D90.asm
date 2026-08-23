Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x00442d90  section .text  offset fichier 0x42190  32 octets

  00442d90  0fbf442408            movsx     eax,word [esp+8]
  00442d95  8b542404              mov       edx,[esp+4]
  00442d99  8d0c80                lea       ecx,[eax+eax*4]
  00442d9c  c1e103                shl       ecx,3
  00442d9f  2bc8                  sub       ecx,eax
  00442da1  33c0                  xor       eax,eax
  00442da3  89148d0ccb5e00        mov       [ecx*4+5ECB0Ch],edx
  00442daa  c3                    ret
  00442dab  90                    nop
  00442dac  90                    nop
  00442dad  90                    nop
  00442dae  90                    nop
  00442daf  90                    nop
