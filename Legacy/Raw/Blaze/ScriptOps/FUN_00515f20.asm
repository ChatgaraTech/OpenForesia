
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515f20  section .text  offset fichier 0x115320  96 octets

  00515f20  8b442408              mov       eax,[esp+8]
  00515f24  8b08                  mov       ecx,[eax]
  00515f26  8bd1                  mov       edx,ecx
  00515f28  81e20000fc00          and       edx,0FC0000h
  00515f2e  81fa00008000          cmp       edx,800000h
  00515f34  8b54240c              mov       edx,[esp+0Ch]
  00515f38  754c                  jne       short 00515F86h
  00515f3a  81e1fffdfff7          and       ecx,0F7FFFDFFh
  00515f40  56                    push      esi
  00515f41  668b7032              mov       si,[eax+32h]
  00515f45  80cd20                or        ch,20h
  00515f48  66ff4012              inc       word [eax+12h]
  00515f4c  6689b098000000        mov       [eax+98h],si
  00515f53  8908                  mov       [eax],ecx
  00515f55  8a0d846c5600          mov       cl,[566C84h]
  00515f5b  880d28e55d00          mov       [5DE528h],cl
  00515f61  8a0d886c5600          mov       cl,[566C88h]
  00515f67  880d2ce55d00          mov       [5DE52Ch],cl
  00515f6d  8a4a01                mov       cl,[edx+1]
  00515f70  880d38e05d00          mov       [5DE038h],cl
  00515f76  8a4008                mov       al,[eax+8]
  00515f79  a23ce55d00            mov       [5DE53Ch],al
  00515f7e  c605                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
