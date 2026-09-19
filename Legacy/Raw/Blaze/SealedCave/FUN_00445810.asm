Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445810  section .text  offset fichier 0x44c10  48 octets

  00445810  56                    push      esi
  00445811  8b742408              mov       esi,[esp+8]
  00445815  6a00                  push      0
  00445817  6a05                  push      5
  00445819  56                    push      esi
  0044581a  e8913f0f00            call      005397B0h
  0044581f  8b06                  mov       eax,[esi]
  00445821  83c40c                add       esp,0Ch
  00445824  0c40                  or        al,40h
  00445826  66c786960000000300    mov       word [esi+96h],3
  0044582f  8906                  mov       [esi],eax
  00445831  33c0                  xor       eax,eax
  00445833  5e                    pop       esi
  00445834  c3                    ret
  00445835  90                    nop
  00445836  90                    nop
  00445837  90                    nop
  00445838  90                    nop
  00445839  90                    nop
  0044583a  90                    nop
  0044583b  90                    nop
  0044583c  90                    nop
  0044583d  90                    nop
  0044583e  90                    nop
  0044583f  90                    nop
