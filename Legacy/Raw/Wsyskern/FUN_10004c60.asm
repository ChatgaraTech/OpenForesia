
Legacy/Binaries/wsyskern.dll  base 0x10000000
0x10004c60  section .text  offset fichier 0x4060  64 octets

  10004c60  8b442404              mov       eax,[esp+4]
  10004c64  56                    push      esi
  10004c65  8bf1                  mov       esi,ecx
  10004c67  3b460c                cmp       eax,[esi+0Ch]
  10004c6a  7206                  jb        short 10004C72h
  10004c6c  33c0                  xor       eax,eax
  10004c6e  5e                    pop       esi
  10004c6f  c20800                ret       8

LAB_10004c72:
  10004c72  57                    push      edi
  10004c73  8d3c8500000000        lea       edi,[eax*4]
  10004c7a  8b4610                mov       eax,[esi+10h]
  10004c7d  8b0c38                mov       ecx,[eax+edi]
  10004c80  51                    push      ecx
  10004c81  e8aa6f0000            call      1000BC30h
  10004c86  8b542410              mov       edx,[esp+10h]
  10004c8a  52                    push      edx
  10004c8b  e800700000            call      1000BC90h
  10004c90  8b4e10                mov       ecx,[esi+10h]
  10004c93  890439                mov       [ecx+edi],eax
  10004c96  5f                    pop       edi
  10004c97  b801000000            mov       eax,1
  10004c9c  5e                    pop       esi
  10004c9d  c20800                ret       8

1 cible(s) de saut interne : 0x10004c72
