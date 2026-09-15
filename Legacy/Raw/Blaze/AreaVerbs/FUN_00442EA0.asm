Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442ea0  section .text  offset fichier 0x422a0  114 octets

  00442ea0  8b0db4876000          mov       ecx,[6087B4h]
  00442ea6  33c0                  xor       eax,eax
  00442ea8  a060905e00            mov       al,[5E9060h]
  00442ead  8b11                  mov       edx,[ecx]
  00442eaf  83c078                add       eax,78h
  00442eb2  c1e00d                shl       eax,0Dh
  00442eb5  03c2                  add       eax,edx
  00442eb7  c60560905e0000        mov       byte [5E9060h],0
  00442ebe  6683b84401000014      cmp       word [eax+144h],14h
  00442ec6  7207                  jb        short 00442ECFh
  00442ec8  c60560905e0001        mov       byte [5E9060h],1

LAB_00442ecf:
  00442ecf  6683b84401000028      cmp       word [eax+144h],28h
  00442ed7  7206                  jb        short 00442EDFh
  00442ed9  fe0560905e00          inc       byte [5E9060h]

LAB_00442edf:
  00442edf  6683b8440100003c      cmp       word [eax+144h],3Ch
  00442ee7  7206                  jb        short 00442EEFh
  00442ee9  fe0560905e00          inc       byte [5E9060h]

LAB_00442eef:
  00442eef  6683b84401000050      cmp       word [eax+144h],50h
  00442ef7  7206                  jb        short 00442EFFh
  00442ef9  fe0560905e00          inc       byte [5E9060h]

LAB_00442eff:
  00442eff  6683b84401000064      cmp       word [eax+144h],64h
  00442f07  7206                  jb        short 00442F0Fh
  00442f09  fe0560905e00          inc       byte [5E9060h]

LAB_00442f0f:
  00442f0f  33c0                  xor       eax,eax
  00442f11  c3                    ret

5 cible(s) de saut interne : 0x00442ecf, 0x00442edf, 0x00442eef, 0x00442eff, 0x00442f0f
