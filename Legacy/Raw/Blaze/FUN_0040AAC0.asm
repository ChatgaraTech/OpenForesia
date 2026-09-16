Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040aac0  section .text  offset fichier 0x9ec0  48 octets

  0040aac0  8b442404              mov       eax,[esp+4]
  0040aac4  6a08                  push      8
  0040aac6  66c780980000000000    mov       word [eax+98h],0
  0040aacf  660fb60de8135700      movzx     cx,byte [5713E8h]
  0040aad7  51                    push      ecx
  0040aad8  6aff                  push      0FFFFFFFFh
  0040aada  50                    push      eax
  0040aadb  e810000000            call      0040AAF0h
  0040aae0  83c410                add       esp,10h
  0040aae3  c3                    ret
