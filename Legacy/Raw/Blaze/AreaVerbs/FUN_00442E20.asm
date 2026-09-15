Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00442e20  section .text  offset fichier 0x42220  77 octets

  00442e20  8b0db4876000          mov       ecx,[6087B4h]
  00442e26  33c0                  xor       eax,eax
  00442e28  a060905e00            mov       al,[5E9060h]
  00442e2d  53                    push      ebx
  00442e2e  8b11                  mov       edx,[ecx]
  00442e30  c60560905e0000        mov       byte [5E9060h],0
  00442e37  c1e00d                shl       eax,0Dh
  00442e3a  b90a000000            mov       ecx,0Ah
  00442e3f  b301                  mov       bl,1
  00442e41  8d841000030f00        lea       eax,[eax+edx+0F0300h]
  00442e48  bafb2e0000            mov       edx,2EFBh

LAB_00442e4d:
  00442e4d  803800                cmp       byte [eax],0
  00442e50  740f                  je        short 00442E61h
  00442e52  66399086000000        cmp       [eax+86h],dx
  00442e59  7506                  jne       short 00442E61h
  00442e5b  881d60905e00          mov       [5E9060h],bl

LAB_00442e61:
  00442e61  0580020000            add       eax,280h
  00442e66  49                    dec       ecx
  00442e67  75e4                  jne       short 00442E4Dh
  00442e69  33c0                  xor       eax,eax
  00442e6b  5b                    pop       ebx
  00442e6c  c3                    ret

2 cible(s) de saut interne : 0x00442e4d, 0x00442e61
