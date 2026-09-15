Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442e70  section .text  offset fichier 0x42270  40 octets

  00442e70  8b0db4876000          mov       ecx,[6087B4h]
  00442e76  33c0                  xor       eax,eax
  00442e78  a060905e00            mov       al,[5E9060h]
  00442e7d  8b11                  mov       edx,[ecx]
  00442e7f  c1e00d                shl       eax,0Dh
  00442e82  6681bc1044010f00b400  cmp       word [eax+edx+0F0144h],0B4h
  00442e8c  0f92c0                setb      al
  00442e8f  40                    inc       eax
  00442e90  a260905e00            mov       [5E9060h],al
  00442e95  33c0                  xor       eax,eax
  00442e97  c3                    ret
