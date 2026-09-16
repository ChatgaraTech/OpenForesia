Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040b8d0  section .text  offset fichier 0xacd0  48 octets

  0040b8d0  8b442404              mov       eax,[esp+4]
  0040b8d4  6aff                  push      0FFFFFFFFh
  0040b8d6  66c780980000000100    mov       word [eax+98h],1
  0040b8df  660fb60de8135700      movzx     cx,byte [5713E8h]
  0040b8e7  51                    push      ecx
  0040b8e8  6a01                  push      1
  0040b8ea  50                    push      eax
  0040b8eb  e800f2ffff            call      0040AAF0h
  0040b8f0  83c410                add       esp,10h
  0040b8f3  c3                    ret
