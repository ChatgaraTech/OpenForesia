
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00515a10  section .text  offset fichier 0x114e10  96 octets

  00515a10  8b442408              mov       eax,[esp+8]
  00515a14  8b08                  mov       ecx,[eax]
  00515a16  8bd1                  mov       edx,ecx
  00515a18  81e20000fc00          and       edx,0FC0000h
  00515a1e  81fa00008000          cmp       edx,800000h
  00515a24  8b54240c              mov       edx,[esp+0Ch]
  00515a28  7550                  jne       short 00515A7Ah
  00515a2a  81e1fffdfff7          and       ecx,0F7FFFDFFh
  00515a30  56                    push      esi
  00515a31  668b7032              mov       si,[eax+32h]
  00515a35  80cd20                or        ch,20h
  00515a38  66ff4012              inc       word [eax+12h]
  00515a3c  6689b098000000        mov       [eax+98h],si
  00515a43  8908                  mov       [eax],ecx
  00515a45  8a4a01                mov       cl,[edx+1]
  00515a48  80f9ff                cmp       cl,0FFh
  00515a4b  7406                  je        short 00515A53h
  00515a4d  880d28e55d00          mov       [5DE528h],cl

LAB_00515a53:
  00515a53  8a4a02                mov       cl,[edx+2]
  00515a56  80f9ff                cmp       cl,0FFh
  00515a59  7406                  je        short 00515A61h
  00515a5b  880d2ce55d00          mov       [5DE52Ch],cl

LAB_00515a61:
  00515a61  8a4a03                mov       cl,[edx+3]
  00515a64  5e                    pop       esi
  00515a65  880d30e55d00          mov       [5DE530h],cl
  00515a6b  8a4008                mov       al,[eax+8]
  00515a6e  a23c                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
