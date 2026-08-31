Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448040  section .text  offset fichier 0x47440  21 octets

  00448040  8b4c2404              mov       ecx,[esp+4]
  00448044  8b01                  mov       eax,[ecx]
  00448046  a900000040            test      eax,40000000h
  0044804b  7407                  je        short 00448054h
  0044804d  25ffffff7f            and       eax,7FFFFFFFh
  00448052  8901                  mov       [ecx],eax

LAB_00448054:
  00448054  c3                    ret

1 cible(s) de saut interne : 0x00448054
