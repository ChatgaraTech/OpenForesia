
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515e80  section .text  offset fichier 0x115280  96 octets

  00515e80  8b542408              mov       edx,[esp+8]
  00515e84  56                    push      esi
  00515e85  8b742410              mov       esi,[esp+10h]
  00515e89  33c0                  xor       eax,eax
  00515e8b  33c9                  xor       ecx,ecx
  00515e8d  8a4602                mov       al,[esi+2]
  00515e90  8a4e01                mov       cl,[esi+1]
  00515e93  50                    push      eax
  00515e94  51                    push      ecx
  00515e95  52                    push      edx
  00515e96  e815390200            call      005397B0h
  00515e9b  83c40c                add       esp,0Ch
  00515e9e  8d4603                lea       eax,[esi+3]
  00515ea1  5e                    pop       esi
  00515ea2  c3                    ret
  00515ea3  90                    nop
  00515ea4  90                    nop
  00515ea5  90                    nop
  00515ea6  90                    nop
  00515ea7  90                    nop
  00515ea8  90                    nop
  00515ea9  90                    nop
  00515eaa  90                    nop
  00515eab  90                    nop
  00515eac  90                    nop
  00515ead  90                    nop
  00515eae  90                    nop
  00515eaf  90                    nop
  00515eb0  e8bb5b0000            call      0051BA70h
  00515eb5  8b44240c              mov       eax,[esp+0Ch]
  00515eb9  40                    inc       eax
  00515eba  c3                    ret
  00515ebb  90                    nop
  00515ebc  90                    nop
  00515ebd  90                    nop
  00515ebe  90                    nop
  00515ebf  90                    nop
  00515ec0  e8fb5b0000            call      0051BAC0h
  00515ec5  8b44240c              mov       eax,[esp+0Ch]
  00515ec9  40                    inc       eax
  00515eca  c3                    ret
  00515ecb  90                    nop
  00515ecc  90                    nop
  00515ecd  90                    nop
  00515ece  90                    nop
  00515ecf  90                    nop
  00515ed0  8a0d8c6c5600          mov       cl,[566C8Ch]
  00515ed6  56                    push      esi
  00515ed7  8b742410              mov       esi,[esp+10h]
  00515edb  8a4601                mov       al,[esi+1]
  00515ede  3ac8                  cmp       cl,al
