Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004508b0  section .text  offset fichier 0x4fcb0  18 octets

  004508b0  8b442404              mov       eax,[esp+4]
  004508b4  8b08                  mov       ecx,[eax]
  004508b6  66c7402c2003          mov       word [eax+2Ch],320h
  004508bc  80cd40                or        ch,40h
  004508bf  8908                  mov       [eax],ecx
  004508c1  c3                    ret
