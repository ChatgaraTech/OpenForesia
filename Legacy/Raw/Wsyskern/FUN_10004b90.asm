
Legacy/Binaries/wsyskern.dll  base 0x10000000
0x10004b90  section .text  offset fichier 0x3f90  80 octets

  10004b90  8b442404              mov       eax,[esp+4]
  10004b94  56                    push      esi
  10004b95  8b74240c              mov       esi,[esp+0Ch]
  10004b99  c60600                mov       byte [esi],0
  10004b9c  8b510c                mov       edx,[ecx+0Ch]
  10004b9f  3bc2                  cmp       eax,edx
  10004ba1  7206                  jb        short 10004BA9h
  10004ba3  33c0                  xor       eax,eax
  10004ba5  5e                    pop       esi
  10004ba6  c20c00                ret       0Ch

LAB_10004ba9:
  10004ba9  8b4910                mov       ecx,[ecx+10h]
  10004bac  8b0481                mov       eax,[ecx+eax*4]
  10004baf  85c0                  test      eax,eax
  10004bb1  7419                  je        short 10004BCCh
  10004bb3  57                    push      edi
  10004bb4  8b7c2414              mov       edi,[esp+14h]
  10004bb8  8d57ff                lea       edx,[edi-1]
  10004bbb  52                    push      edx
  10004bbc  50                    push      eax
  10004bbd  56                    push      esi
  10004bbe  e8fdde0000            call      10012AC0h
  10004bc3  83c40c                add       esp,0Ch
  10004bc6  c6443eff00            mov       byte [esi+edi-1],0
  10004bcb  5f                    pop       edi

LAB_10004bcc:
  10004bcc  b801000000            mov       eax,1
  10004bd1  5e                    pop       esi
  10004bd2  c20c00                ret       0Ch
  10004bd5  90                    nop
  10004bd6  90                    nop
  10004bd7  90                    nop
  10004bd8  90                    nop
  10004bd9  90                    nop
  10004bda  90                    nop
  10004bdb  90                    nop
  10004bdc  90                    nop
  10004bdd  90                    nop
  10004bde  90                    nop
  10004bdf  90                    nop

2 cible(s) de saut interne : 0x10004ba9, 0x10004bcc
