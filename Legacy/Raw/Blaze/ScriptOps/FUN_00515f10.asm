
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515f10  section .text  offset fichier 0x115310  96 octets

  00515f10  8b44240c              mov       eax,[esp+0Ch]
  00515f14  83c002                add       eax,2
  00515f17  c3                    ret
  00515f18  90                    nop
  00515f19  90                    nop
  00515f1a  90                    nop
  00515f1b  90                    nop
  00515f1c  90                    nop
  00515f1d  90                    nop
  00515f1e  90                    nop
  00515f1f  90                    nop
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
