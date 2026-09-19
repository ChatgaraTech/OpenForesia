Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445740  section .text  offset fichier 0x44b40  32 octets

  00445740  a12ce05d00            mov       eax,[5DE02Ch]
  00445745  c605f8455600fe        mov       byte [5645F8h],0FEh
  0044574c  0c02                  or        al,2
  0044574e  c70510e55d000a000000  mov       dword [5DE510h],0Ah
  00445758  a32ce05d00            mov       [5DE02Ch],eax
  0044575d  33c0                  xor       eax,eax
  0044575f  c3                    ret
