Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00445150  section .text  offset fichier 0x44550  96 octets

  00445150  53                    push      ebx
  00445151  8a1d30e05d00          mov       bl,[5DE030h]
  00445157  56                    push      esi
  00445158  57                    push      edi
  00445159  b8c0935e00            mov       eax,5E93C0h
  0044515e  bf04000000            mov       edi,4
  00445163  33f6                  xor       esi,esi
  00445165  ba00100000            mov       edx,1000h

LAB_0044516a:
  0044516a  8b08                  mov       ecx,[eax]
  0044516c  f7c100000010          test      ecx,10000000h
  00445172  7428                  je        short 0044519Ch
  00445174  81e1ff9fffef          and       ecx,0EFFF9FFFh
  0044517a  fecb                  dec       bl
  0044517c  8908                  mov       [eax],ecx
  0044517e  8a0d44e55d00          mov       cl,[5DE544h]
  00445184  66897012              mov       [eax+12h],si
  00445188  66895028              mov       [eax+28h],dx
  0044518c  fec9                  dec       cl
  0044518e  6689502a              mov       [eax+2Ah],dx
  00445192  6689502c              mov       [eax+2Ch],dx
  00445196  880d44e55d00          mov       [5DE544h],cl

LAB_0044519c:
  0044519c  059c000000            add       eax,9Ch
  004451a1  4f                    dec       edi
  004451a2  75c6                  jne       short 0044516Ah
  004451a4  5f                    pop       edi
  004451a5  881d30e05d00          mov       [5DE030h],bl
  004451ab  5e                    pop       esi
  004451ac  33c0                  xor       eax,eax
  004451ae  5b                    pop       ebx
  004451af  c3                    ret

2 cible(s) de saut interne : 0x0044516a, 0x0044519c
