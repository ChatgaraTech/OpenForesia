
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515850  section .text  offset fichier 0x114c50  96 octets

  00515850  a060a55f00            mov       al,[5FA560h]
  00515855  33c9                  xor       ecx,ecx
  00515857  fec0                  inc       al
  00515859  a260a55f00            mov       [5FA560h],al
  0051585e  8ac8                  mov       cl,al
  00515860  8b44240c              mov       eax,[esp+0Ch]
  00515864  8a5001                mov       dl,[eax+1]
  00515867  83c002                add       eax,2
  0051586a  889160a55f00          mov       [ecx+5FA560h],dl
  00515870  c3                    ret
  00515871  90                    nop
  00515872  90                    nop
  00515873  90                    nop
  00515874  90                    nop
  00515875  90                    nop
  00515876  90                    nop
  00515877  90                    nop
  00515878  90                    nop
  00515879  90                    nop
  0051587a  90                    nop
  0051587b  90                    nop
  0051587c  90                    nop
  0051587d  90                    nop
  0051587e  90                    nop
  0051587f  90                    nop
  00515880  56                    push      esi
  00515881  8b742410              mov       esi,[esp+10h]
  00515885  33c0                  xor       eax,eax
  00515887  8a4601                mov       al,[esi+1]
  0051588a  50                    push      eax
  0051588b  e820f80100            call      005350B0h
  00515890  83c404                add       esp,4
  00515893  8d4602                lea       eax,[esi+2]
  00515896  5e                    pop       esi
  00515897  c3                    ret
  00515898  90                    nop
  00515899  90                    nop
  0051589a  90                    nop
  0051589b  90                    nop
  0051589c  90                    nop
  0051589d  90                    nop
  0051589e  90                    nop
  0051589f  90                    nop
  005158a0  56                    push      esi
  005158a1  8b742410              mov       esi,[esp+10h]
  005158a5  33c0                  xor       eax,eax
  005158a7  33c9                  xor       ecx,ecx
  005158a9  8a4604                mov       al,[esi+4]
  005158ac  8a4e01                mov       cl,[esi+1]
  005158af  50                    push      eax
