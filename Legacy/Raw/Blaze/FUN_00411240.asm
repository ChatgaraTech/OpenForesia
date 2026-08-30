; 0x00411240 — l'amorce de la scene cinema parlante (10 instructions, absente de Blaze.c).
; Elle pose +0x98=0x39 puis appelle FUN_00411270(entite, 1, memoire, -1), qui est decompilee.
; Copie : 0x004146b0 -> FUN_00414d10, bloc prive 0x00571450.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00411240  section .text  offset fichier 0x10640  40 octets

  00411240  8b442404              mov       eax,[esp+4]
  00411244  6aff                  push      0FFFFFFFFh
  00411246  66c780980000003900    mov       word [eax+98h],39h
  0041124f  660fb60d48145700      movzx     cx,byte [571448h]
  00411257  51                    push      ecx
  00411258  6a01                  push      1
  0041125a  50                    push      eax
  0041125b  e810000000            call      00411270h
  00411260  83c410                add       esp,10h
  00411263  c3                    ret
  00411264  90                    nop
  00411265  90                    nop
  00411266  90                    nop
  00411267  90                    nop
