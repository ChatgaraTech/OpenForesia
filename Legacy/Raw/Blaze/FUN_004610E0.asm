Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004610e0  section .text  offset fichier 0x604e0  18 octets

  004610e0  8b442404              mov       eax,[esp+4]
  004610e4  8b08                  mov       ecx,[eax]
  004610e6  66c74028cc10          mov       word [eax+28h],10CCh
  004610ec  80cd40                or        ch,40h
  004610ef  8908                  mov       [eax],ecx
  004610f1  c3                    ret
