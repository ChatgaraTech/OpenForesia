Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442f20  section .text  offset fichier 0x42320  57 octets

  00442f20  8b0db4876000          mov       ecx,[6087B4h]
  00442f26  33c0                  xor       eax,eax
  00442f28  a062905e00            mov       al,[5E9062h]
  00442f2d  8b11                  mov       edx,[ecx]
  00442f2f  83c078                add       eax,78h
  00442f32  c1e00d                shl       eax,0Dh
  00442f35  03c2                  add       eax,edx
  00442f37  33c9                  xor       ecx,ecx
  00442f39  0578010000            add       eax,178h
  00442f3e  ba07000000            mov       edx,7

LAB_00442f43:
  00442f43  803805                cmp       byte [eax],5
  00442f46  7304                  jae       short 00442F4Ch
  00442f48  c60005                mov       byte [eax],5
  00442f4b  41                    inc       ecx

LAB_00442f4c:
  00442f4c  40                    inc       eax
  00442f4d  4a                    dec       edx
  00442f4e  75f3                  jne       short 00442F43h
  00442f50  33c0                  xor       eax,eax
  00442f52  6685c9                test      cx,cx
  00442f55  0f94c0                sete      al
  00442f58  c3                    ret

2 cible(s) de saut interne : 0x00442f43, 0x00442f4c
