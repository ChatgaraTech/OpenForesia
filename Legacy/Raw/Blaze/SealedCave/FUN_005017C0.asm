Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x005017c0  section .text  offset fichier 0x100bc0  128 octets

  005017c0  8b442404              mov       eax,[esp+4]
  005017c4  33d2                  xor       edx,edx
  005017c6  56                    push      esi
  005017c7  57                    push      edi
  005017c8  8b08                  mov       ecx,[eax]
  005017ca  8a5004                mov       dl,[eax+4]
  005017cd  0fbf7018              movsx     esi,word [eax+18h]
  005017d1  81e1fffffffd          and       ecx,0FDFFFFFFh
  005017d7  80cd20                or        ch,20h
  005017da  8908                  mov       [eax],ecx
  005017dc  8b149540965e00        mov       edx,[edx*4+5E9640h]
  005017e3  f7c100000001          test      ecx,1000000h
  005017e9  8b34b2                mov       esi,[edx+esi*4]
  005017ec  741e                  je        short 0050180Ch
  005017ee  66ff401a              inc       word [eax+1Ah]
  005017f2  668b481a              mov       cx,[eax+1Ah]
  005017f6  33d2                  xor       edx,edx
  005017f8  668b5616              mov       dx,[esi+16h]
  005017fc  0fbfc9                movsx     ecx,cx
  005017ff  3bca                  cmp       ecx,edx
  00501801  7c2b                  jl        short 0050182Eh
  00501803  66c7401a0000          mov       word [eax+1Ah],0
  00501809  5f                    pop       edi
  0050180a  5e                    pop       esi
  0050180b  c3                    ret

LAB_0050180c:
  0050180c  66ff401a              inc       word [eax+1Ah]
  00501810  668b501a              mov       dx,[eax+1Ah]
  00501814  33ff                  xor       edi,edi
  00501816  668b7e16              mov       di,[esi+16h]
  0050181a  0fbff2                movsx     esi,dx
  0050181d  3bf7                  cmp       esi,edi
  0050181f  7c0d                  jl        short 0050182Eh
  00501821  4a                    dec       edx
  00501822  81c900000002          or        ecx,2000000h
  00501828  6689501a              mov       [eax+1Ah],dx
  0050182c  8908                  mov       [eax],ecx

LAB_0050182e:
  0050182e  5f                    pop       edi
  0050182f  5e                    pop       esi
  00501830  c3                    ret
  00501831  90                    nop
  00501832  90                    nop
  00501833  90                    nop
  00501834  90                    nop
  00501835  90                    nop
  00501836  90                    nop
  00501837  90                    nop
  00501838  90                    nop
  00501839  90                    nop
  0050183a  90                    nop
  0050183b  90                    nop
  0050183c  90                    nop
  0050183d  90                    nop
  0050183e  90                    nop
  0050183f  90                    nop

2 cible(s) de saut interne : 0x0050180c, 0x0050182e
