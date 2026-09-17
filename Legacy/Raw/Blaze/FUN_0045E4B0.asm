Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045e4b0  section .text  offset fichier 0x5d8b0  32 octets

  0045e4b0  8b442404              mov       eax,[esp+4]
  0045e4b4  0fbf4814              movsx     ecx,word [eax+14h]
  0045e4b8  8d1489                lea       edx,[ecx+ecx*4]
  0045e4bb  c1e203                shl       edx,3
  0045e4be  2bd1                  sub       edx,ecx
  0045e4c0  8b0c90                mov       ecx,[eax+edx*4]
  0045e4c3  f6c520                test      ch,20h
  0045e4c6  8b08                  mov       ecx,[eax]
  0045e4c8  7406                  je        short 0045E4D0h
  0045e4ca  80cd20                or        ch,20h
  0045e4cd  8908                  mov       [eax],ecx
  0045e4cf  c3                    ret
