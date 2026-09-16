
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00407fe0  section .text  offset fichier 0x73e0  96 octets

  00407fe0  8b442404              mov       eax,[esp+4]
  00407fe4  50                    push      eax
  00407fe5  c7405c39000000        mov       dword [eax+5Ch],39h
  00407fec  8b0da4a95f00          mov       ecx,[5FA9A4h]
  00407ff2  89481c                mov       [eax+1Ch],ecx
  00407ff5  8b15a8a95f00          mov       edx,[5FA9A8h]
  00407ffb  895020                mov       [eax+20h],edx
  00407ffe  8b0daca95f00          mov       ecx,[5FA9ACh]
  00408004  894824                mov       [eax+24h],ecx
  00408007  668b15b8a95f00        mov       dx,[5FA9B8h]
  0040800e  66895030              mov       [eax+30h],dx
  00408012  668b0dbaa95f00        mov       cx,[5FA9BAh]
  00408019  66894832              mov       [eax+32h],cx
  0040801d  668b15bca95f00        mov       dx,[5FA9BCh]
  00408024  66895034              mov       [eax+34h],dx
  00408028  e8a3030100            call      004183D0h
  0040802d  83c404                add       esp,4
  00408030  c3                    ret
  00408031  90                    nop
  00408032  90                    nop
  00408033  90                    nop
  00408034  90                    nop
