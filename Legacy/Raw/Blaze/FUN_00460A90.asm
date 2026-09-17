Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460a90  section .text  offset fichier 0x5fe90  41 octets

  00460a90  803d2ce55d0001        cmp       byte [5DE52Ch],1
  00460a97  7509                  jne       short 00460AA2h
  00460a99  f605fd965e0004        test      byte [5E96FDh],4
  00460aa0  7416                  je        short 00460AB8h

LAB_00460aa2:
  00460aa2  8b4c2404              mov       ecx,[esp+4]
  00460aa6  8b01                  mov       eax,[ecx]
  00460aa8  a900000040            test      eax,40000000h
  00460aad  7404                  je        short 00460AB3h
  00460aaf  a820                  test      al,20h
  00460ab1  7405                  je        short 00460AB8h

LAB_00460ab3:
  00460ab3  80cc20                or        ah,20h
  00460ab6  8901                  mov       [ecx],eax

LAB_00460ab8:
  00460ab8  c3                    ret

3 cible(s) de saut interne : 0x00460aa2, 0x00460ab3, 0x00460ab8
