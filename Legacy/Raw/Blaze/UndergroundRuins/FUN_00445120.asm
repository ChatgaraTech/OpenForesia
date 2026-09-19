Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445120  section .text  offset fichier 0x44520  48 octets

  00445120  b800f45f00            mov       eax,5FF400h
  00445125  b920000000            mov       ecx,20h

LAB_0044512a:
  0044512a  8b10                  mov       edx,[eax]
  0044512c  059c000000            add       eax,9Ch
  00445131  81e2fffffffb          and       edx,0FBFFFFFFh
  00445137  49                    dec       ecx
  00445138  899064ffffff          mov       [eax-9Ch],edx
  0044513e  75ea                  jne       short 0044512Ah
  00445140  33c0                  xor       eax,eax
  00445142  c3                    ret
  00445143  90                    nop
  00445144  90                    nop
  00445145  90                    nop
  00445146  90                    nop
  00445147  90                    nop
  00445148  90                    nop
  00445149  90                    nop
  0044514a  90                    nop
  0044514b  90                    nop
  0044514c  90                    nop
  0044514d  90                    nop
  0044514e  90                    nop
  0044514f  90                    nop

1 cible(s) de saut interne : 0x0044512a
