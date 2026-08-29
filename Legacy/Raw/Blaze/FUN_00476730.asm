
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00476730  section .text  offset fichier 0x75b30  45 octets

  00476730  8b542404              mov       edx,[esp+4]
  00476734  66837a1000            cmp       word [edx+10h],0
  00476739  7422                  je        short 0047675Dh
  0047673b  8b0dace05d00          mov       ecx,[5DE0ACh]
  00476741  8bc1                  mov       eax,ecx
  00476743  c1f802                sar       eax,2
  00476746  2bc1                  sub       eax,ecx
  00476748  a3ace05d00            mov       [5DE0ACh],eax
  0047674d  7902                  jns       short 00476751h
  0047674f  f7d8                  neg       eax

LAB_00476751:
  00476751  83f801                cmp       eax,1
  00476754  7f17                  jg        short 0047676Dh
  00476756  c70200000000          mov       dword [edx],0
  0047675c  c3                    ret

1 cible(s) de saut interne : 0x00476751


