Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460240  section .text  offset fichier 0x5f640  28 octets

  00460240  8b442404              mov       eax,[esp+4]
  00460244  8b08                  mov       ecx,[eax]
  00460246  80cd40                or        ch,40h
  00460249  8908                  mov       [eax],ecx
  0046024b  8a15fd965e00          mov       dl,[5E96FDh]
  00460251  f6c210                test      dl,10h
  00460254  7405                  je        short 0046025Bh
  00460256  80cd20                or        ch,20h
  00460259  8908                  mov       [eax],ecx

LAB_0046025b:
  0046025b  c3                    ret

1 cible(s) de saut interne : 0x0046025b
